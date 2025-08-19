using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace ResearchLink
{
    public partial class FormInfoResearch : Form
    {
        private NpgsqlConnection connection;
        private NpgsqlCommand command = new NpgsqlCommand();
        private DataTable dt = new DataTable();
        private bool superUser;
        private string title;

        public FormInfoResearch(NpgsqlConnection connection, string title)
        {
            InitializeComponent();

            this.connection = connection;
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            superUser = connection.UserName == "supervisor";
            this.title = title;
        }

        private void FormInfoResearch_Load(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point((workingArea.Width - 700) / 2 - 75, (workingArea.Height - 500) / 2 - 25);
            
            contextMenuStripStudentUpdate.Visible = superUser;
            contextMenuStripSupervisorsUpdate.Visible = superUser;
            contextMenuStripPublicationsUpdate.Visible = superUser;
            contextMenuStripFundingUpdate.Visible = superUser;

            GetResearch();
        }

        private void FormInfoResearch_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                float scaleFactor = Math.Min((float)ClientSize.Width / 820, (float)ClientSize.Height / 620);
                Dictionary<Control, byte> elementsFontSizes = new Dictionary<Control, byte> {
                { labelTitle, 14 }, { labelDescription, 14 }, { labelAuthor, 14 },
                { labelStatus, 14 }, { labelPublications, 14 }, { labelFunding, 14 },
                { textBoxDescription, 9 }, { labelStudents, 12 }, { labelSupervisors, 12 },
                { listBoxStudents, 9 }, { listBoxSupervisors, 9 }, { labelStatusValue, 14 },
                { listBoxPublications, 9 }, { listBoxFunding, 9 }
                };

                foreach (var kvp in elementsFontSizes)
                {
                    Control element = kvp.Key;
                    element.Font = new Font(element.Font.FontFamily, kvp.Value * scaleFactor, element.Font.Style);
                }
            }
        }

        private void listBoxStudents_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxStudents.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxStudents.SelectedIndex = index;
                    contextMenuStripStudent.Show(listBoxStudents, e.Location);
                }
                else
                    contextMenuStripStudent.Hide();
            }
        }

        private void listBoxSupervisors_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxSupervisors.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxSupervisors.SelectedIndex = index;
                    contextMenuStripSupervisors.Show(listBoxSupervisors, e.Location);
                }
                else
                    contextMenuStripSupervisors.Hide();
            }
        }

        private void listBoxPublications_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxPublications.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxPublications.SelectedIndex = index;
                    contextMenuStripPublications.Show(listBoxPublications, e.Location);
                }
                else
                    contextMenuStripPublications.Hide();
            }
        }

        private void listBoxFunding_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxFunding.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxFunding.SelectedIndex = index;
                    contextMenuStripFunding.Show(listBoxFunding, e.Location);
                }
                else
                    contextMenuStripFunding.Hide();
            }
        }

        private void contextMenuStripStudent_Click(object sender, EventArgs e)
        {
            ToolStripItem contextMenuItem = sender as ToolStripItem;

            string[] full_name = listBoxStudents.SelectedItem.ToString().Split(' ').Skip(1).ToArray();

            string name, surname, patronymic;

            surname = full_name[0];
            name = full_name[1];
            patronymic = string.IsNullOrEmpty(full_name[2]) ? "-" : full_name[2];

            command.CommandText = "SELECT * FROM students " +
                "WHERE name = @name AND surname = @surname AND ";

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, name);
            command.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, surname);
            if (patronymic == "-")
                command.CommandText += "patronymic IS NULL";
            else
            {
                command.CommandText += "patronymic = @patronymic";
                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, patronymic);
            }

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);

                DataRow row = dt.Rows[0];

                if (contextMenuItem.Text == "Посмотреть данные...")
                {
                    FormInfoPeople formStudent = new FormInfoPeople(
                        "Информация о студенте",
                        surname, name, patronymic, row["gender"].ToString(), row["group_name"].ToString(), row["email"].ToString()
                    );

                    formStudent.ShowDialog();
                    formStudent.Dispose();
                }
                else
                {
                    string email = row["email"].ToString();

                    FormInfoPeopleEdit formStudent = new FormInfoPeopleEdit(
                        connection, "Редактирование данных студента", "студент",
                        Convert.ToInt32(row["id"]), surname, name, patronymic, row["gender"].ToString(),
                        row["group_name"].ToString(), email.Substring(0, email.IndexOf('@'))
                    );

                    DialogResult result = formStudent.ShowDialog();

                    if (result == DialogResult.Yes || result == DialogResult.Ignore)
                        GetResearchStudents(true);

                    formStudent.Dispose();
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void contextMenuStripSupervisors_Click(object sender, EventArgs e)
        {
            ToolStripItem contextMenuItem = sender as ToolStripItem;

            string[] full_name = listBoxSupervisors.SelectedItem.ToString().Split(' ').Skip(1).ToArray();

            string name, surname, patronymic;

            surname = full_name[0];
            name = full_name[1];
            patronymic = string.IsNullOrEmpty(full_name[2]) ? "-" : full_name[2];

            command.CommandText = "SELECT * FROM supervisors " +
                "WHERE name = @name AND surname = @surname AND ";

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, name);
            command.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, surname);
            if (patronymic == "-")
                command.CommandText += "patronymic IS NULL";
            else
            {
                command.CommandText += "patronymic = @patronymic";
                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, patronymic);
            }

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);

                DataRow row = dt.Rows[0];

                Form formSupervisor;

                if (contextMenuItem.Text == "Посмотреть данные...")
                {
                    formSupervisor = new FormInfoPeople(
                        "Информация о научном руководителе",
                        surname, name, patronymic,
                        row["gender"].ToString(), row["academic_degree"].ToString(), row["email"].ToString()
                    );
                }
                else
                {
                    string email = row["email"].ToString();

                    formSupervisor = new FormInfoPeopleEdit(
                        connection, "Редактирование данных научного руководителя", "руководитель",
                        Convert.ToInt32(row["id"]), surname, name, patronymic,
                        row["gender"].ToString(), row["academic_degree"].ToString(), email.Substring(0, email.IndexOf('@'))
                    );
                }

                DialogResult result = formSupervisor.ShowDialog();
                formSupervisor.Dispose();

                if (result == DialogResult.Yes)
                    GetResearchSupervisors(true);

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void contextMenuStripPublicationsUpdate_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) «(?<name>[^»]+)»(?: \(([^)]+)\))?$");
            Match match = regex.Match(listBoxPublications.SelectedItem.ToString());

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

                FormInfoDetailsEdit formResearch = new FormInfoDetailsEdit(
                    connection, "Редактирование данных публикации", "публикация",
                    Convert.ToInt32(row["id"]), row["type"].ToString(), row["name"].ToString(), date_string, link_string
                );

                DialogResult result = formResearch.ShowDialog();
                formResearch.Dispose();

                if (result == DialogResult.Yes)
                    GetResearchPublication(true);

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void contextMenuStripFundingUpdate_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^(?<date>\d{4}-\d{2}-\d{2}) - (?<type>[^ ]+?(?: [^ ]+)*) (?<amount>\d+,\d+) руб\. от «(?<source>[^»]+)»(?:.*)?$");
            Match match = regex.Match(listBoxFunding.SelectedItem.ToString());

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

                FormInfoDetailsEdit formFunding = new FormInfoDetailsEdit(
                    connection, "Редактирование данных финансирования", "финансирование",
                    Convert.ToInt32(row["id"]), row["type"].ToString(), row["source"].ToString(), date_string, 
                    Convert.ToDouble(row["amount"]), details
                );

                DialogResult result = formFunding.ShowDialog();
                formFunding.Dispose();

                if (result == DialogResult.Yes)
                    GetResearchFunding(true);

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void GetResearch()
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);

            command.CommandText = "SELECT * FROM researches WHERE title = @title";

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);
                DataRow row = dt.Rows[0];

                labelTitle.Text = $"{row["type"]}: \"{row["title"]}\"";
                textBoxDescription.Text = row["description"] == DBNull.Value ? "Описание отсутствует" : row["description"].ToString();

                bool complete = Convert.ToBoolean(row["complete"]);
                labelStatusValue.Text = complete ? "Завершена" : "В процессе выполнения";
                labelStatusValue.ForeColor = complete ? Color.Green : Color.Orange;

                dt.Rows.Clear();
                dt.Columns.Clear();
            }

            GetResearchStudents(false);
            GetResearchSupervisors(false);
            GetResearchPublication(false);
            GetResearchFunding(false);
        }

        private void GetResearchStudents(bool single)
        {
            if (single)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
            }

            command.CommandText = "SELECT s.surname, s.name, s.patronymic FROM researches r " +
                "JOIN link_students_researches lsr ON r.id = lsr.research_id JOIN students s ON s.id = lsr.student_id " +
                "WHERE r.title = @title";

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (listBoxStudents.Items.Count > 0)
                    listBoxStudents.Items.Clear();

                dt.Load(reader);

                dt.DefaultView.Sort = "surname ASC, name ASC, patronymic ASC";
                dt = dt.DefaultView.ToTable();

                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                    listBoxStudents.Items.Add(string.Join(" ", new string[] { $"{count})", surname, name, patronymic }));
                    count++;
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void GetResearchSupervisors(bool single)
        {
            if (single)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
            }

            command.CommandText = "SELECT s.surname, s.name, s.patronymic FROM researches r " +
                "JOIN link_supervisors_researches lsr ON r.id = lsr.research_id JOIN supervisors s ON s.id = lsr.supervisor_id " +
                "WHERE r.title = @title";

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (listBoxSupervisors.Items.Count > 0)
                    listBoxSupervisors.Items.Clear();

                dt.Load(reader);

                dt.DefaultView.Sort = "surname ASC, name ASC, patronymic ASC";
                dt = dt.DefaultView.ToTable();

                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                    listBoxSupervisors.Items.Add(string.Join(" ", new string[] { $"{count})", surname, name, patronymic }));
                    count++;
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void GetResearchPublication(bool single)
        {
            if (single)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
            }

            command.CommandText = "SELECT p.type, p.name, p.date, p.link FROM researches r " +
                "JOIN publications p ON r.id = p.research_id " +
                "WHERE r.title = @title";

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (listBoxPublications.Items.Count > 0)
                    listBoxPublications.Items.Clear();

                dt.Load(reader);

                dt.DefaultView.Sort = "date ASC, type ASC, name ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow row in dt.Rows)
                {
                    string date = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                    string link = row["link"] == DBNull.Value ? "" : $" ({row["link"]})";
                    listBoxPublications.Items.Add($"{date} - {row["type"]} «{row["name"]}»{link}");
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void GetResearchFunding(bool single)
        {
            if (single)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
            }

            command.CommandText = "SELECT f.amount, f.type, f.source, f.date, f.details FROM researches r " +
                "JOIN funding f ON r.id = f.research_id " +
                "WHERE r.title = @title";

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (listBoxFunding.Items.Count > 0)
                    listBoxFunding.Items.Clear();

                dt.Load(reader);

                dt.DefaultView.Sort = "date ASC, type ASC, source ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow row in dt.Rows)
                {
                    string date = Convert.ToDateTime(row["date"]).ToString("yyyy-MM-dd");
                    string desc = row["details"] == DBNull.Value ? "" : $": {row["details"]}";
                    listBoxFunding.Items.Add($"{date} - {row["type"]} {row["amount"]} руб. от «{row["source"]}»{desc}");
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }
    }
}
