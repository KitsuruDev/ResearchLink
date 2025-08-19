using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ResearchLink
{
    public partial class FormDataList : Form
    {
        private NpgsqlConnection connection;
        private NpgsqlCommand command = new NpgsqlCommand();
        private DataTable dt = new DataTable();
        private string table_name;
        private bool superUser;

        public FormDataList(NpgsqlConnection connection, string label_name, string table_name)
        {
            InitializeComponent();

            this.connection = connection;
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            superUser = connection.UserName == "supervisor";
            label.Text = label_name;
            this.table_name = table_name;
        }

        private void FormDataList_Load(object sender, EventArgs e)
        {
            menuItemDataAdd.Visible = superUser;
            menuItemDataUpdate.Visible = superUser;
            menuItemDataDelete.Visible = superUser;
            menuItemDataSeparator.Visible = superUser;
            contextMenuStripUpdate.Visible = superUser;
            contextMenuStripDelete.Visible = superUser;

            GetData();
        }

        private void FormDataList_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                float scaleFactor = Math.Min((float)ClientSize.Width / 820, (float)ClientSize.Height / 500);

                label.Font = new Font(label.Font.FontFamily, 14 * scaleFactor, label.Font.Style);
                listBox.Font = new Font(listBox.Font.FontFamily, 10 * scaleFactor, listBox.Font.Style);
            }
        }

        private void menuStripDataAdd_Click(object sender, EventArgs e)
        {
            Form form;

            if (table_name == "researches")
                form = new FormInfoResearchEdit(connection, "Добавление записи о научной работе");
            else if (table_name == "publications")
                form = new FormInfoDetailsEdit(connection, "Добавление записи о публикации", "публикация");
            else
                form = new FormInfoDetailsEdit(connection, "Добавление записи о финансировании", "финансирование");

            DialogResult result = form.ShowDialog();
            form.Dispose();

            if (result == DialogResult.Yes)
                GetData();
        }

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBox.SelectedIndex = index;
                    contextMenuStrip.Show(listBox, e.Location);
                }
                else
                    contextMenuStrip.Hide();
            }
        }

        private void contextMenuStripResearchShow_Click(object sender, EventArgs e)
        {
            string title;

            if (table_name == "researches")
            {
                string full_name = listBox.SelectedItem.ToString();
                title = full_name.Substring(full_name.IndexOf(':') + 1).Trim().Trim('"');
            }
            else
            {
                Regex regex;
                Match match;

                command.CommandText = "SELECT r.title FROM researches r ";

                if (table_name == "publications")
                {
                    regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) «(?<name>[^»]+)»(?: \(([^)]+)\))?$");
                    match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText += "JOIN publications p ON r.id = p.research_id " +
                        "WHERE p.type = @type AND p.name = @name AND p.date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, match.Groups["name"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);
                }
                else
                {
                    regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) (?<amount>\d+,\d+) руб\. от «(?<source>[^»]+)»(?:.*)?$");
                    match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText += "JOIN funding f ON r.id = f.research_id " +
                        "WHERE f.type = @type AND f.source = @source AND f.date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@source", NpgsqlDbType.Text, match.Groups["source"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);
                }

                title = command.ExecuteScalar().ToString();
            }

            FormInfoResearch form = new FormInfoResearch(connection, title)
            {
                WindowState = WindowState,
                MinimumSize = new Size(820, 620)
            };
            form.ShowDialog();
            form.Dispose();
        }

        private void contextMenuStripUpdate_Click(object sender, EventArgs e)
        {
            if (table_name == "researches")
            {
                string full_name = listBox.SelectedItem.ToString();
                string title = full_name.Substring(full_name.IndexOf(':') + 1).Trim().Trim('"');

                int type_start = full_name.IndexOf(')') + 2;
                int type_end = full_name.IndexOf(':') - type_start;
                string type = full_name.Substring(type_start, type_end);

                int id;
                bool complete;
                string description;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);

                command.CommandText = "SELECT * FROM researches WHERE title = @title";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                    DataRow row = dt.Rows[0];

                    id = Convert.ToInt32(row["ID"]);
                    complete = Convert.ToBoolean(row["complete"]);
                    description = row["description"] == DBNull.Value ? "" : row["description"].ToString();

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }


                List<string> students = new List<string>();

                command.CommandText = "SELECT s.surname, s.name, s.patronymic, s.group_name FROM researches r " +
                    "JOIN link_students_researches lsr ON r.id = lsr.research_id " +
                    "JOIN students s ON s.id = lsr.student_id " +
                    "WHERE r.title = @title";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);

                    dt.DefaultView.Sort = "group_name ASC, surname ASC, name ASC, patronymic ASC";
                    dt = dt.DefaultView.ToTable();

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["name"].ToString();
                        string surname = row["surname"].ToString();
                        string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                        string group_name = row["group_name"].ToString();
                        students.Add(string.Join(" ", new string[] { group_name, surname, name, patronymic }));
                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }


                List<string> supervisors = new List<string>();

                command.CommandText = "SELECT s.surname, s.name, s.patronymic FROM researches r " +
                    "JOIN link_supervisors_researches lsr ON r.id = lsr.research_id " +
                    "JOIN supervisors s ON s.id = lsr.supervisor_id " +
                    "WHERE r.title = @title";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);

                    dt.DefaultView.Sort = "surname ASC, name ASC, patronymic ASC";
                    dt = dt.DefaultView.ToTable();

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["name"].ToString();
                        string surname = row["surname"].ToString();
                        string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                        supervisors.Add(string.Join(" ", new string[] { surname, name, patronymic }));
                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }

                FormInfoResearchEdit form = new FormInfoResearchEdit(
                    connection, "Редактирование данных научной работы", 
                    id, type, title, complete, description, students, supervisors
                    );

                DialogResult result = form.ShowDialog();
                form.Dispose();

                if (result == DialogResult.Yes)
                    GetData();
            }
            else
            {
                if (table_name == "publications")
                {
                    Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) «(?<name>[^»]+)»(?: \(([^)]+)\))?$");
                    Match match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText = "SELECT * FROM publications " +
                        "WHERE type = @type AND name = @name AND date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, match.Groups["name"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);

                        DataRow row = dt.Rows[0];

                        string date_string = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                        string link_string = row["link"] == DBNull.Value ? "" : row["link"].ToString();

                        FormInfoDetailsEdit form = new FormInfoDetailsEdit(
                            connection, "Редактирование данных публикации", "публикация",
                            Convert.ToInt32(row["id"]), row["type"].ToString(), row["name"].ToString(), date_string, link_string
                        );

                        DialogResult result = form.ShowDialog();
                        form.Dispose();

                        dt.Rows.Clear();
                        dt.Columns.Clear();

                        if (result == DialogResult.Yes)
                            GetData();
                    }
                }
                else
                {
                    Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) (?<amount>\d+,\d+) руб\. от «(?<source>[^»]+)»(?:.*)?$");
                    Match match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText = "SELECT * FROM funding " +
                        "WHERE type = @type AND source = @source AND date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@source", NpgsqlDbType.Text, match.Groups["source"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);

                        DataRow row = dt.Rows[0];

                        string date_string = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                        string details = row["details"] == DBNull.Value ? "" : row["details"].ToString();

                        FormInfoDetailsEdit form = new FormInfoDetailsEdit(
                            connection, "Редактирование данных финансирования", "финансирование",
                            Convert.ToInt32(row["id"]), row["type"].ToString(), row["source"].ToString(), date_string,
                            Convert.ToDouble(row["amount"]), details
                        );

                        DialogResult result = form.ShowDialog();
                        form.Dispose();

                        dt.Rows.Clear();
                        dt.Columns.Clear();

                        if (result == DialogResult.Yes)
                            GetData();
                    }
                }
            }
        }

        private void contextMenuStripDelete_Click(object sender, EventArgs e)
        {
            string message = table_name == "researches" ?
                "Вы точно хотите удалить запись о научной работе?\n" +
                "Удаление также затронет связанные записи публикаций и финансирования."
                :
                "Вы точно хотите удалить запись?";

            DialogResult result = MessageBox.Show(
                 message,
                 "Удаление записи о научной работе",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                int id;

                if (table_name == "researches")
                {
                    command.CommandText = "SELECT id FROM researches WHERE title = @title";

                    string title = listBox.SelectedItem.ToString();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title.Substring(title.IndexOf(':') + 1).Trim());

                    id = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "CALL delete_research(@id)";
                }
                else if (table_name == "publications")
                {
                    Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) «(?<name>[^»]+)»(?: \(([^)]+)\))?$");
                    Match match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText = "SELECT id FROM publications " +
                        "WHERE type = @type AND name = @name AND date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, match.Groups["name"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);

                    id = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "CALL delete_publication(@id)";
                }
                else
                {
                    Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) (?<amount>\d+,\d+) руб\. от «(?<source>[^»]+)»(?:.*)?$");
                    Match match = regex.Match(listBox.SelectedItem.ToString());

                    command.CommandText = "SELECT * FROM funding " +
                        "WHERE type = @type AND source = @source AND date = @date";

                    DateTime date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, match.Groups["type"].Value);
                    command.Parameters.AddWithValue("@source", NpgsqlDbType.Text, match.Groups["source"].Value);
                    command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, date);

                    id = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "CALL delete_funding(@id)";
                }

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
                command.ExecuteNonQuery();

                GetData();
            }
        }

        private void GetData()
        {
            command.CommandText = $"SELECT * FROM {table_name}";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (listBox.Items.Count > 0)
                        listBox.Items.Clear();

                    dt.Load(reader);

                    if (table_name == "researches")
                    {
                        dt.DefaultView.Sort = "type ASC, title ASC";
                        dt = dt.DefaultView.ToTable();

                        int count = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            listBox.Items.Add($"{count}) {row["type"]}: \"{row["title"]}\"");
                            count++;
                        }

                    }
                    else if (table_name == "publications")
                    {
                        dt.DefaultView.Sort = "date ASC, type ASC, name ASC";
                        dt = dt.DefaultView.ToTable();

                        foreach (DataRow row in dt.Rows)
                        {
                            string date = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                            string link = row["link"] == DBNull.Value ? "" : $" ({row["link"]})";
                            listBox.Items.Add($"{date} - {row["type"]} «{row["name"]}»{link}");
                        }

                    }
                    else
                    {
                        dt.DefaultView.Sort = "date ASC, type ASC, source ASC";
                        dt = dt.DefaultView.ToTable();

                        foreach (DataRow row in dt.Rows)
                        {
                            string date = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                            string desc = row["details"] == DBNull.Value ? "" : $": {row["details"]}";
                            listBox.Items.Add($"{date} - {row["type"]} {row["amount"]} руб. от «{row["source"]}»{desc}");
                        }

                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }
                else
                    MessageBox.Show($"В данный момент список \"{label.Text}\".",
                        $"Пустой список \"{label.Text}\".",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }
    }
}
