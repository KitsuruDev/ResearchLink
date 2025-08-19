using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ResearchLink
{
    public partial class FormInfoDetailsEdit : Form
    {
        private NpgsqlCommand command = new NpgsqlCommand();
        private DataTable dt = new DataTable();
        private int past_research_id;
        private string past_type, past_name_source, past_date, past_link_amount, past_details;
        private Dictionary<byte, string> dict_patterns = new Dictionary<byte, string> {
            { 1, @"^[A-ZА-ЯЁ][a-zа-яё\-\:]+(?: [0-9A-Za-zА-ЯЁа-яё\-""\:]+)*$" }, // тип и название
            { 2, @"^20\d{2}-\d{2}-\d{2}$" }, // дата
            { 3, @"^[1-9][0-9]+(?:\.([0-9])+)?$" }, // сумма
            { 4, @"^https?://(?:[a-zA-Z0-9_-]+\.)+[a-zA-Z0-9]+(?:/[a-zA-Z0-9_-]+)*$" }, // ссылка
            { 5, @"^\d{4,}(?:\.\d+)?$" } // сумма
        };
        private System.Windows.Forms.TextBox[] list_textBox;
        private string data_type;
        private int data_id;
        private bool data_new;

        private FormInfoDetailsEdit(NpgsqlConnection connetion)
        {
            InitializeComponent();

            list_textBox = new System.Windows.Forms.TextBox[] {
                textBoxType, textBoxNameSource, textBoxDate, textBoxAmountURL
            };

            command.Connection = connetion;
            command.CommandType = CommandType.Text;
        }

        public FormInfoDetailsEdit(NpgsqlConnection connetion, string Text, string data_type) : this (connetion)
        {
            this.Text = Text;
            this.data_type = data_type;

            bool is_publication = data_type == "публикация";

            labelMain.Text = is_publication ? "Публикация" : "Финансирование";
            labelNameSource.Text = is_publication ? "Название" : "Источник";
            labelAmountURL.Text = is_publication ? "Ссылка" : "Сумма (руб.)";
            buttonOK.Text = "Добавить";

            if (is_publication)
            {
                checkBoxURL.Visible = true;
                labelDetails.Visible = false;
                textBoxDetails.Visible = false;
                tableLayoutPanel2.RowCount = 6;
            }

            data_new = true;
        }

        public FormInfoDetailsEdit(NpgsqlConnection connetion, string Text, string data_type,
            int id, string type, string name, string date, string link
            ) : this(connetion)
        {
            this.Text = Text;
            this.data_type = data_type;

            labelMain.Text = "Публикация";
            labelNameSource.Text = "Название";
            labelAmountURL.Text = "Ссылка";

            checkBoxURL.Visible = true;
            labelDetails.Visible = false;
            textBoxDetails.Visible = false;
            tableLayoutPanel2.RowCount = 6;

            textBoxType.Text = type;
            textBoxNameSource.Text = name;
            textBoxDate.Text = date;
            if (string.IsNullOrEmpty(link))
            {
                textBoxAmountURL.Enabled = false;
                checkBoxURL.Checked = true;
            } 
            else
                textBoxAmountURL.Text = link;

            past_type = type;
            past_name_source = name;
            past_date = date;
            past_link_amount = link;

            data_id = id;
            data_new = false;
        }

        public FormInfoDetailsEdit(NpgsqlConnection connetion, string Text, string data_type,
            int id, string type, string name, string date, double amount, string description
            ) : this(connetion)
        {
            this.Text = Text;
            this.data_type = data_type;

            labelMain.Text = "Финансирование";
            labelNameSource.Text = "Источник";
            labelAmountURL.Text = "Сумма (руб.)";

            textBoxType.Text = type;
            textBoxNameSource.Text = name;
            textBoxDate.Text = date;
            textBoxAmountURL.Text = amount.ToString().Replace(',', '.');
            textBoxDetails.Text = description;

            past_type = type;
            past_name_source = name;
            past_date = date;
            past_link_amount = amount.ToString();
            past_details = description;

            data_id = id;
            data_new = false;
        }

        private void FormInfoDetailsEdit_Load(object sender, EventArgs e)
        {
            command.CommandText = "SELECT type, title FROM researches WHERE complete = TRUE";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);

                dt.DefaultView.Sort = "type ASC, title ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow row in dt.Rows)
                    comboBoxResearch.Items.Add($"{row["type"]}: {row["title"]}");

                dt.Rows.Clear();
                dt.Columns.Clear();
            }

            if (!data_new)
            {
                command.CommandText = "SELECT r.id, r.title FROM researches r ";
                command.CommandText += data_type == "публикация" ?
                    "JOIN publications p ON r.id = p.research_id WHERE p.id = @id AND r.complete = true"
                    :
                    "JOIN funding f ON r.id = f.research_id WHERE f.id = @id";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, data_id);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    past_research_id = reader.GetInt32(0);
                    string title = reader.GetString(1);

                    for (int i = 0; i < comboBoxResearch.Items.Count; i++)
                    {
                        if (comboBoxResearch.Items[i].ToString().Contains(title))
                        {
                            comboBoxResearch.SelectedItem = comboBoxResearch.Items[i];
                            comboBoxResearch.Text = comboBoxResearch.SelectedItem.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void textBoxFullName_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            if (Regex.IsMatch(textBox.Text, dict_patterns[1]))
                errorProvider.SetError(textBox, "");
            else
                errorProvider.SetError(textBox, "Неверный формат. Пример: Грант, Журнал, Инновации 2024: новинки, Компания \"Company\"");
        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {
            string date = textBoxDate.Text;

            if (Regex.IsMatch(date, dict_patterns[2]))
            {
                try
                {
                    DateTime inputDate = DateTime.Parse(date);

                    if (inputDate > DateTime.Today)
                        errorProvider.SetError(textBoxDate, "Неверное значение. Дата не может превышать текущую");
                    else
                        errorProvider.SetError(textBoxDate, "");
                }
                catch (FormatException)
                {
                    errorProvider.SetError(textBoxDate, "Неверный формат даты. Некорректное сочетание цифр.");
                }
            }
            else
                errorProvider.SetError(textBoxDate, "Неверный формат даты. Введите дату от 2000-01-01. Пример: 2024-10-25");
        }

        private void textBoxAmountURL_TextChanged(object sender, EventArgs e)
        {
            if (data_type == "публикация")
            {
                if (Regex.IsMatch(textBoxAmountURL.Text, dict_patterns[4]))
                    errorProvider.SetError(textBoxAmountURL, "");
                else
                    errorProvider.SetError(textBoxAmountURL, "Неверный формат. Пример: http://very-simple_site.ru/article/123, https://site.com/book");
            }
            else
            {
                if (Regex.IsMatch(textBoxAmountURL.Text, dict_patterns[5]))
                    errorProvider.SetError(textBoxAmountURL, "");
                else
                    errorProvider.SetError(textBoxAmountURL, "Неверный формат и/или значение. Сумма должна быть от 1000 руб. Пример: 1000, 15843.61");
            }
        }

        private void checkBoxURL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxURL.Checked == true)
            {
                textBoxAmountURL.Enabled = false;
                textBoxAmountURL.Clear();
                errorProvider.SetError(textBoxAmountURL, "");
            }
            else
                textBoxAmountURL.Enabled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool errors = false, empty = false;

            foreach (System.Windows.Forms.TextBox textBox in list_textBox)
            {
                if (checkBoxURL.Visible)
                    if (textBox == textBoxAmountURL && checkBoxURL.Checked)
                        continue;

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    empty = true;
                    break;
                }

                if (!string.IsNullOrEmpty(errorProvider.GetError(textBox)))
                {
                    errors = true;
                    break;
                }
            }


            if (!errors && !empty)
            {
                if (comboBoxResearch.SelectedItem != null)
                {
                    try
                    {
                        string title = comboBoxResearch.SelectedItem.ToString();

                        command.CommandText = "SELECT id FROM researches WHERE title = @title";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title.Substring(title.IndexOf(':') + 1).Trim());
                        int reseach_id = Convert.ToInt32(command.ExecuteScalar());

                        string type, name_source, date_string, link_amount;
                        type = textBoxType.Text;
                        name_source = textBoxNameSource.Text;
                        date_string = textBoxDate.Text;
                        DateTime date = DateTime.ParseExact(date_string, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        link_amount = textBoxAmountURL.Text;

                        command.Parameters.Clear();

                        if (data_new)
                        {
                            command.CommandText = data_type == "публикация" ?
                                "CALL add_publication(@research_id, @type, @name_source, @date, @link)"
                                :
                                "CALL add_funding(@research_id, @amount, @type, @name_source, @date, @details)";

                            command.Parameters.AddWithValue("@research_id", NpgsqlDbType.Integer, reseach_id);
                            command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, type);
                            command.Parameters.AddWithValue("@name_source", NpgsqlDbType.Text, name_source);
                            command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);

                            if (data_type == "публикация")
                                command.Parameters.AddWithValue("@link", NpgsqlDbType.Text, link_amount);
                            else
                            {
                                command.Parameters.AddWithValue("@amount", NpgsqlDbType.Numeric, Convert.ToDouble(link_amount.Replace('.', ',')));
                                if (string.IsNullOrEmpty(textBoxDetails.Text))
                                    command.Parameters.AddWithValue("@details", DBNull.Value);
                                else
                                    command.Parameters.AddWithValue("@details", NpgsqlDbType.Text, textBoxDetails.Text);
                            }    
                        }
                        else
                        {
                            command.CommandText = data_type == "публикация" ?
                            "CALL update_publication(@id, @research_id, @type, @name_source, @date, @link)"
                            :
                            "CALL update_funding(@id, @research_id, @amount, @type, @name_source, @date, @details)";

                            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, data_id);

                            if (past_research_id != reseach_id)
                                command.Parameters.AddWithValue("@research_id", NpgsqlDbType.Integer, reseach_id);
                            else
                                command.Parameters.AddWithValue("@research_id", DBNull.Value);

                            if (past_type != type)
                                command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, type);
                            else
                                command.Parameters.AddWithValue("@type", DBNull.Value);

                            if (past_name_source != name_source)
                                command.Parameters.AddWithValue("@name_source", NpgsqlDbType.Text, name_source);
                            else
                                command.Parameters.AddWithValue("@name_source", DBNull.Value);

                            if (past_date != date_string)
                                command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);
                            else
                                command.Parameters.AddWithValue("@date", DBNull.Value);

                            if (data_type == "публикация")
                            {
                                if (past_link_amount != link_amount)
                                    command.Parameters.AddWithValue("@link", NpgsqlDbType.Text, link_amount);
                                else
                                    command.Parameters.AddWithValue("@link", DBNull.Value);
                            }
                            else
                            {
                                if (past_link_amount != link_amount)
                                    command.Parameters.AddWithValue("@amount", NpgsqlDbType.Numeric, Convert.ToDouble(link_amount.Replace('.', ',')));
                                else
                                    command.Parameters.AddWithValue("@amount", DBNull.Value);

                                string details = textBoxDetails.Text;
                                if (past_details != details && !string.IsNullOrEmpty(details))
                                    command.Parameters.AddWithValue("@details", NpgsqlDbType.Text, details);
                                else
                                    command.Parameters.AddWithValue("@details", DBNull.Value);
                            }
                        }
                        
                        command.ExecuteNonQuery();

                        if (!data_new && past_research_id != reseach_id)
                            DialogResult = DialogResult.Ignore;
                        else
                            DialogResult = DialogResult.Yes;

                        Close();
                    }
                    catch (NpgsqlException ex)
                    {
                        MessageBox.Show($"Ошибка при передаче данных: {ex.Message}",
                            "Ошибка передачи данных",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Выберите научную работу из выпадающего списка!",
                        "Ошибка данных поля \"связанная работа\"",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Заполните все поля однострочные данными и исправьте их формат,\n" +
                                "если у них отображается значок ошибки!",
                    "Ошибка в заполнении данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInfoDetailsEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Yes)
                DialogResult = DialogResult.No;
        }
    }
}
