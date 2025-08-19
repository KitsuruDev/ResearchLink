using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ResearchLink
{
    public partial class FormInfoPeopleEdit : Form
    {
        private NpgsqlCommand command = new NpgsqlCommand();
        private string past_name, past_surname, past_patronymic, past_gender, past_group_degree, past_email;
        private List<string> list_group;
        private DataTable dt;
        private Dictionary<byte, string> dict_patterns = new Dictionary<byte, string> {
            { 1, @"^[А-ЯЁ][а-яё]+$" }, // фио
            { 2, @"^[А-ЯЁ]{4}-\d{2}-\d{2}$" }, // группа
            { 3, @"^[А-ЯЁ][а-яё]+(?: [а-яё\-]+)*$" }, // научная степень
            { 4, @"^[a-z]+\.[a-z]\.?[a-z]?[0-9]?$" }, // почта студента
            { 5, @"^[a-z]+$" } // почта научного руководителя
        };
        private System.Windows.Forms.TextBox[] list_textBox;
        private string data_type;
        private int data_id;
        private bool data_new;
        private string data_new_group;

        private FormInfoPeopleEdit(NpgsqlConnection connetion, string data_type)
        {
            InitializeComponent();

            list_textBox = new System.Windows.Forms.TextBox[] {
                textBoxName, textBoxSurname, textBoxPatronymic, textBoxGroupDegree, textBoxEmail
            };

            command.Connection = connetion;
            command.CommandType = CommandType.Text;

            this.data_type = data_type;

            if (data_type == "студент")
            {
                command.CommandText = "SELECT group_name FROM students";
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt = new DataTable();

                        dt.Load(reader);
                        list_group = dt.AsEnumerable().Select(row => row.Field<string>("group_name")).Distinct().ToList();

                        dt.Rows.Clear();
                        dt.Columns.Clear();
                    }
                }
            }
        }

        public FormInfoPeopleEdit(NpgsqlConnection connetion, string Text, string data_type) : this(connetion, data_type)
        {
            this.Text = Text;

            bool is_student = data_type == "студент";

            labelGroupDegree.Text = is_student ? "Группа" : "Научная степень";
            textBoxGroupDegree.MaxLength = is_student ? 50 : 100;
            labelEmail.Text = is_student ? "Почта в домене @edu.mirea.ru:" : "Почта в домене @mirea.ru:";
            buttonOK.Text = "Добавить";

            data_new = true;
        }

        public FormInfoPeopleEdit(NpgsqlConnection connetion, string Text, string data_type,
            int id, string surname, string name, string patronymic, string gender, string group_degree, string email
        ) : this(connetion, data_type)
        {
            this.Text = Text;

            bool is_student = data_type == "студент";

            labelGroupDegree.Text = is_student ? "Группа" : "Научная степень";
            textBoxGroupDegree.MaxLength = is_student ? 50 : 100;
            labelEmail.Text = is_student ? "Почта в домене @edu.mirea.ru:" : "Почта в домене @mirea.ru:";

            textBoxSurname.Text = surname;
            textBoxName.Text = name;
            if (patronymic != "-")
                textBoxPatronymic.Text = patronymic;
            else
            {
                textBoxPatronymic.Enabled = false;
                checkBoxPatronymic.Checked = true;
            }
                
            radioButtonM.Checked = gender == "М";
            radioButtonF.Checked = gender == "Ж";
            textBoxGroupDegree.Text = group_degree;
            textBoxEmail.Text = email;

            past_surname = surname;
            past_name = name;
            past_patronymic = patronymic;
            past_gender = gender;
            past_group_degree = name;
            past_email = email;

            data_id = id;
            data_new = false;
        }

        public string GetGroupNewData() => data_new_group;

        private void checkBoxPatronymic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPatronymic.Checked == true)
            {
                textBoxPatronymic.Enabled = false;
                textBoxPatronymic.Clear();
                errorProvider.SetError(textBoxPatronymic, "");
            }
            else
                textBoxPatronymic.Enabled = true;
        }

        private void textBoxFullName_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            if (Regex.IsMatch(textBox.Text, dict_patterns[1]))
                errorProvider.SetError(textBox, "");
            else
                errorProvider.SetError(textBox, "Неверный формат. Пример: Иванов, Иван, Иванович");
        }

        private void textBoxGroupDegree_TextChanged(object sender, EventArgs e)
        {
            if (data_type == "студент")
            {
                if (Regex.IsMatch(textBoxGroupDegree.Text, dict_patterns[2]))
                    errorProvider.SetError(textBoxGroupDegree, "");
                else
                    errorProvider.SetError(textBoxGroupDegree, "Неверный формат. Пример: AAAA-00-00");
            }
            else
            {
                if (Regex.IsMatch(textBoxGroupDegree.Text, dict_patterns[3]))
                    errorProvider.SetError(textBoxGroupDegree, "");
                else
                    errorProvider.SetError(textBoxGroupDegree, "Неверный формат. Пример: Доцент компьютерных наук");
            }
            
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (data_type == "студент")
            {
                if (Regex.IsMatch(textBoxEmail.Text, dict_patterns[4]))
                    errorProvider.SetError(textBoxEmail, "");
                else
                    errorProvider.SetError(textBoxEmail, "Неверный формат. Пример: ivanov.i.i, ivanov.i, ivanov.i.i2");
            }
            else
            {
                if (Regex.IsMatch(textBoxEmail.Text, dict_patterns[5]))
                    errorProvider.SetError(textBoxEmail, "");
                else
                    errorProvider.SetError(textBoxEmail, "Неверный формат. Пример: ivanov");
            }
                
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool errors = false, empty = false;

            foreach (System.Windows.Forms.TextBox textBox in list_textBox)
            {
                if (textBox == textBoxPatronymic && checkBoxPatronymic.Checked)
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
                if (radioButtonM.Checked || radioButtonF.Checked)
                {
                    try
                    {
                        string surname, name, patronymic, gender, group_degree, email;
                        surname = textBoxSurname.Text;
                        name = textBoxName.Text;
                        patronymic = textBoxPatronymic.Text;
                        gender = radioButtonM.Checked == true ? "М" : "Ж";
                        group_degree = textBoxGroupDegree.Text;
                        email = textBoxEmail.Text;
                        if (data_type == "студент")
                            email += "@edu.mirea.ru";
                        else
                            email += "@mirea.ru";

                        if (data_new)
                        {
                            command.CommandText = data_type == "студент" ?
                                "CALL add_student(@name, @surname, @patronymic, @gender, @group_degree, @email)"
                                :
                                "CALL add_supervisor(@name, @surname, @patronymic, @gender, @group_degree, @email)";

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, name);
                            command.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, surname);
                            if (textBoxPatronymic.Enabled)
                                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, patronymic);
                            else
                                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, "");
                            command.Parameters.AddWithValue("@gender", NpgsqlDbType.Text, gender);
                            command.Parameters.AddWithValue("@group_degree", NpgsqlDbType.Text, group_degree);
                            command.Parameters.AddWithValue("@email", NpgsqlDbType.Text, email);
                        }
                        else
                        {
                            command.CommandText = data_type == "студент" ?
                                "CALL update_student(@id, @name, @surname, @patronymic, @gender, @group_degree, @email)"
                                :
                                "CALL update_supervisor(@id, @name, @surname, @patronymic, @gender, @group_degree, @email)";

                            command.Parameters.Clear();

                            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, data_id);

                            if (past_name != name)
                                command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, name);
                            else
                                command.Parameters.AddWithValue("@name", DBNull.Value);

                            if (past_name != surname)
                                command.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, surname);
                            else
                                command.Parameters.AddWithValue("@surname", DBNull.Value);

                            if (textBoxPatronymic.Enabled)
                                if (past_name != patronymic)
                                    command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, patronymic);
                                else
                                    command.Parameters.AddWithValue("@patronymic", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, ""); // убирание отчества, если было

                            if (past_name != gender)
                                command.Parameters.AddWithValue("@gender", NpgsqlDbType.Text, gender);
                            else
                                command.Parameters.AddWithValue("@gender", DBNull.Value);

                            if (past_name != group_degree)
                                command.Parameters.AddWithValue("@group_degree", NpgsqlDbType.Text, group_degree);
                            else
                                command.Parameters.AddWithValue("@group_degree", DBNull.Value);

                            if (past_name != email)
                                command.Parameters.AddWithValue("@email", NpgsqlDbType.Text, email);
                            else
                                command.Parameters.AddWithValue("@email", DBNull.Value);
                        }

                        command.ExecuteNonQuery();

                        if (data_type == "студент")
                        {
                            if (list_group != null && list_group.Count > 0)
                            {
                                if (list_group.Contains(group_degree))
                                {
                                    DialogResult = DialogResult.OK; // для обновления группы, выведенной на главной форме
                                    data_new_group = group_degree;
                                }
                                else
                                    DialogResult = DialogResult.Yes; // обновление списка групп из-за появления новой
                            }
                            else
                                DialogResult = DialogResult.Yes; // если групп не существовало
                        }
                        else
                            DialogResult = DialogResult.Yes; // обновление списка руководителей

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
                    MessageBox.Show("Выберите пол студента!",
                            "Ошибка данных поля \"пол\"",
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

        private void FormInfoPeopleEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Yes && DialogResult != DialogResult.OK || DialogResult == DialogResult.None)
                DialogResult = DialogResult.No;
        }
    }
}
