using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace ResearchLink
{
    public partial class FormMain : Form
    {
        private NpgsqlConnection connection;
        private NpgsqlCommand command = new NpgsqlCommand();
        private DataTable dt = new DataTable();
        private bool superUser, sessionClose = false;
        private string last_author;

        public FormMain(NpgsqlConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            superUser = connection.UserName == "supervisor";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point((workingArea.Width - 1000) / 2 - 175, (workingArea.Height - 450) / 2 - 25);

            menuItemDataAdd.Visible = superUser;
            menuItemDataUpdate.Visible = superUser;
            menuItemDataDelete.Visible = superUser;
            contextMenuStripStudentUpdate.Visible = superUser;
            contextMenuStripStudentDelete.Visible = superUser;
            contextMenuStripSupervisorsUpdate.Visible = superUser;
            contextMenuStripSupervisorsDelete.Visible = superUser;
            contextMenuStripResearchesUpdate.Visible = superUser;
            contextMenuStripResearchesDelete.Visible = superUser;

            GetGroups();
            GetSupervisors();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                float scaleFactor = Math.Min((float)ClientSize.Width / 820, (float)ClientSize.Height / 500);
                Dictionary<Control, byte> elementsFontSizes = new Dictionary<Control, byte> {
                { comboBoxStudents, 11 },
                { listBoxStudents, 9 }, { listBoxResearches, 9 }, { listBoxSupervisors, 9 },
                { labelStudents, 12 }, { labelResearches, 12 }, { labelSupervisors, 12 }
                };

                foreach (var kvp in elementsFontSizes)
                {
                    Control element = kvp.Key;
                    element.Font = new Font(element.Font.FontFamily, kvp.Value * scaleFactor, element.Font.Style);
                }
            }
        }

        private void menuItemSessionClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите завершить сеанс?", "Подтверждение завершения сеанса",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                sessionClose = true;
                connection.Close();
                Application.OpenForms[0].Show();
                Close();
            }
        }

        private void menuItemSessionExit_Click(object sender, EventArgs e)
        {
            FormMain_FormClosing(sender, new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        private void menuItemData_Click(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedIndex != ListBox.NoMatches)
                contextMenuStripStudent_Click(sender, e);
            else if (listBoxSupervisors.SelectedIndex != ListBox.NoMatches)
                contextMenuStripSupervisors_Click(sender, e);
            else
                contextMenuStripResearches_Click(sender, e);
        }

        private void menuItemDataDelete_Click(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedIndex != ListBox.NoMatches)
                contextMenuStripStudentDelete_Click(sender, e);
            else if (listBoxSupervisors.SelectedIndex != ListBox.NoMatches)
                contextMenuStripSupervisorsDelete_Click(sender, e);
            else
                contextMenuStripResearchesDelete_Click(sender, e);
        }

        private void menuItemDataAddPeople_Click(object sender, EventArgs e)
        {
            bool is_student = (sender as ToolStripDropDownItem).Text == "Добавить студента...";

            FormInfoPeopleEdit form = is_student ?
                new FormInfoPeopleEdit(connection, "Добавление записи о студенте", "студент")
                :
                new FormInfoPeopleEdit(connection, "Добавление записи о научном руководителе", "руководитель");

            DialogResult result = form.ShowDialog();

            if (result == DialogResult.Yes)
                if (is_student)
                    GetGroups();
                else
                    GetSupervisors();
            else if (result == DialogResult.OK)
                if (form.GetGroupNewData() == comboBoxStudents.SelectedItem.ToString())
                    GetGroupStudents();

            form.Dispose();
        }

        private void menuItemDataAddResearch_Click(object sender, EventArgs e)
        {
            FormInfoResearchEdit form = new FormInfoResearchEdit(connection, "Добавление записи о научной работе");
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.Yes)
            {
                bool update_list = false;

                List<int> student_id_list = form.GetAuthorStudents();
                List<int> supervisor_id_list = form.GetAuthorSupervisors();
                int author_id;

                if (last_author != null)
                {
                    GetStudent(last_author);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        author_id = reader.GetInt32(0);
                    }

                    foreach (int student_id in student_id_list)
                    {
                        if (student_id == author_id)
                        {
                            update_list = true;
                            break;
                        }
                    }

                    if (!update_list)
                    {
                        GetSupervisor(last_author);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            author_id = reader.GetInt32(0);
                        }

                        foreach (int supervisor_id in supervisor_id_list)
                        {
                            if (supervisor_id == author_id)
                            {
                                update_list = true;
                                break;
                            }
                        }
                    }

                    if (update_list)
                        GetResearches();
                }
            }
            form.Dispose();
        }

        private void menuItemDataAddDetails_Click(object sender, EventArgs e)
        {              
            Form form = (sender as ToolStripDropDownItem).Text == "Добавить публикацию работы..." ?
                new FormInfoDetailsEdit(connection, "Добавление записи о публикации", "публикация")
                :
                new FormInfoDetailsEdit(connection, "Добавление записи о финансировании", "финансирование");

            form.ShowDialog();
            form.Dispose();
        }

        private void menuItemDataListElement_click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            
            string label_name = menuItem.Text;
            string table_name;

            if (label_name == "Научные работы")
                table_name = "researches";
            else if (label_name == "Публикации")
                table_name = "publications";
            else
                table_name = "funding";

            FormDataList form = new FormDataList(connection, label_name, table_name)
            {
                WindowState = WindowState,
                MinimumSize = new Size(820, 500)
            };
            form.ShowDialog();
            form.Dispose();
        }
        
        private void menuItemExtraHelp_Click(object sender, EventArgs e)
        {
            FormHelp form = new FormHelp();
            form.ShowDialog();
            form.Dispose();
        }

        private void menuItemExtraAbout_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
            form.Dispose();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGroupStudents();
        }

        private void listBoxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedIndex != ListBox.NoMatches)
            {
                if (menuItemDataShow.Enabled == false)   menuItemDataShow.Enabled = true;
                if (menuItemDataUpdate.Enabled == false) menuItemDataUpdate.Enabled = true;
                if (menuItemDataDelete.Enabled == false) menuItemDataDelete.Enabled = true;
                if (listBoxSupervisors.SelectedIndex != ListBox.NoMatches)
                    listBoxSupervisors.ClearSelected();
                if (listBoxResearches.SelectedIndex != ListBox.NoMatches)
                    listBoxResearches.ClearSelected();

                GetResearches("student");

                string full_name = listBoxStudents.SelectedItem.ToString();
                last_author = $"Студент {comboBoxStudents.SelectedItem} {full_name.Substring(full_name.IndexOf(')') + 2)}";
            }
        }

        private void listBoxSupervisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSupervisors.SelectedIndex != ListBox.NoMatches)
            {
                if (menuItemDataShow.Enabled == false)   menuItemDataShow.Enabled = true;
                if (menuItemDataUpdate.Enabled == false) menuItemDataUpdate.Enabled = true;
                if (menuItemDataDelete.Enabled == false) menuItemDataDelete.Enabled = true;
                if (listBoxStudents.SelectedIndex != ListBox.NoMatches)
                    listBoxStudents.ClearSelected();
                if (listBoxResearches.SelectedIndex != ListBox.NoMatches)
                    listBoxResearches.ClearSelected();

                GetResearches("supervisor");

                string full_name = listBoxSupervisors.SelectedItem.ToString();
                last_author = $"Руководитель {full_name.Substring(full_name.IndexOf(')') + 2)}";
            } 
        }

        private void listBoxResearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxResearches.SelectedIndex != ListBox.NoMatches)
            {
                if (listBoxStudents.SelectedIndex != ListBox.NoMatches)
                    listBoxStudents.ClearSelected();
                if (listBoxSupervisors.SelectedIndex != ListBox.NoMatches)
                    listBoxSupervisors.ClearSelected();
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

        private void listBoxResearches_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxResearches.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxResearches.SelectedIndex = index;
                    contextMenuStripResearch.Show(listBoxResearches, e.Location);
                }
                else
                    contextMenuStripResearch.Hide();
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

        private void contextMenuStripStudent_Click(object sender, EventArgs e)
        {
            GetStudent();

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);

                DataRow row = dt.Rows[0];

                string patronymic = row["patronymic"] == DBNull.Value ? "-" : row["patronymic"].ToString();

                if (((sender as ToolStripItem).Text == "Посмотреть данные...")
                    ||
                    ((sender as ToolStripDropDownItem).Text == "Посмотреть данные записи..."))
                {
                    FormInfoPeople form = new FormInfoPeople(
                        "Информация о студенте",
                        row["surname"].ToString(), row["name"].ToString(), patronymic,
                        row["gender"].ToString(), row["group_name"].ToString(), row["email"].ToString()
                    );

                    form.ShowDialog();
                    form.Dispose();

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }
                else
                {
                    string email = row["email"].ToString();

                    FormInfoPeopleEdit form = new FormInfoPeopleEdit(
                        connection, "Редактирование данных студента", "студент",
                        Convert.ToInt32(row["id"]), row["surname"].ToString(), row["name"].ToString(), patronymic,
                        row["gender"].ToString(), row["group_name"].ToString(), email.Substring(0, email.IndexOf('@'))
                    );

                    DialogResult result = form.ShowDialog();
                    form.Dispose();

                    dt.Rows.Clear();
                    dt.Columns.Clear();

                    if (result == DialogResult.Yes)
                        GetGroups();
                    else if (result == DialogResult.OK)
                        if (form.GetGroupNewData() == comboBoxStudents.SelectedItem.ToString())
                            GetGroupStudents();
                }
            }
        }

        private void contextMenuStripStudentDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы точно хотите удалить запись о студенте?", 
                "Удаление записи о студенте",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                GetStudent();

                int id = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = "CALL delete_student(@id)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                command.ExecuteNonQuery();

                GetGroupStudents();

                labelResearches.Text = "Научная(-ые) работа(-ы) автора:";
            }
        }

        private void contextMenuStripSupervisors_Click(object sender, EventArgs e)
        {
            GetSupervisor();

            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);

                DataRow row = dt.Rows[0];

                string patronymic = row["patronymic"] == DBNull.Value ? "-" : row["patronymic"].ToString();

                Form form;

                if (((sender as ToolStripItem).Text == "Посмотреть данные...")
                    ||
                    ((sender as ToolStripDropDownItem).Text == "Посмотреть данные записи..."))
                {
                    form = new FormInfoPeople(
                        "Информация о научном руководителе",
                        row["surname"].ToString(), row["name"].ToString(), patronymic,
                        row["gender"].ToString(), row["academic_degree"].ToString(), row["email"].ToString()
                    );
                }
                else
                {
                    string email = row["email"].ToString();

                    form = new FormInfoPeopleEdit(
                        connection, "Редактирование данных научного руководителя", "руководитель",
                        Convert.ToInt32(row["id"]), row["surname"].ToString(), row["name"].ToString(), patronymic,
                        row["gender"].ToString(), row["academic_degree"].ToString(), email.Substring(0, email.IndexOf('@'))
                    );
                }

                DialogResult result = form.ShowDialog();
                form.Dispose();

                dt.Rows.Clear();
                dt.Columns.Clear();

                if (result == DialogResult.Yes)
                    GetSupervisors();   
            }
        }

        private void contextMenuStripSupervisorsDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы точно хотите удалить запись о научном преподавателе?", 
                "Удаление записи о научном преподавателе",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                GetSupervisor();

                int id = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = "CALL delete_supervisor(@id)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                command.ExecuteNonQuery();

                GetSupervisors();

                labelResearches.Text = "Научная(-ые) работа(-ы) автора:";
            }
        }

        private void contextMenuStripResearches_Click(object sender, EventArgs e)
        {
            string full_name = listBoxResearches.SelectedItem.ToString();
            string title = full_name.Substring(full_name.IndexOf(':') + 1).Trim().Trim('"');

            Form form;

            if (((sender as ToolStripItem).Text == "Посмотреть данные...")
                    ||
                    ((sender as ToolStripDropDownItem).Text == "Посмотреть данные записи..."))
            {
                form = new FormInfoResearch(connection, title)
                {
                    WindowState = WindowState,
                    MinimumSize = new Size(820, 620)
                };

                form.ShowDialog();
            }
            else
            {
                command.CommandText = "SELECT id, type, complete, description FROM researches WHERE title = @title";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);

                int id;
                string type, description;
                bool complete;

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);

                    DataRow row = dt.Rows[0];

                    id = Convert.ToInt32(row["id"]);
                    type = row["type"].ToString();
                    description = row["description"].ToString();
                    complete = Convert.ToBoolean(row["complete"]);

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }

                command.CommandText = "SELECT s.group_name, s.surname, s.name, s.patronymic FROM students s " +
                    "JOIN link_students_researches lsr ON s.id = lsr.student_id " +
                    "JOIN researches r ON r.id = lsr.research_id " +
                    "WHERE title = @title";

                List<string> list_students = new List<string>();

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["name"].ToString();
                        string surname = row["surname"].ToString();
                        string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                        string group_name = row["group_name"].ToString();
                        list_students.Add(string.Join(" ", new string[] { group_name, surname, name, patronymic }));
                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }

                command.CommandText = "SELECT s.surname, s.name, s.patronymic FROM supervisors s " +
                    "JOIN link_supervisors_researches lsr ON s.id = lsr.supervisor_id " +
                    "JOIN researches r ON r.id = lsr.research_id " +
                    "WHERE title = @title";

                List<string> list_supervisors = new List<string>();

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["name"].ToString();
                        string surname = row["surname"].ToString();
                        string patronymic = row["patronymic"] == DBNull.Value ? "" : row["patronymic"].ToString();
                        list_supervisors.Add(string.Join(" ", new string[] { surname, name, patronymic }));
                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }

                form = new FormInfoResearchEdit(
                        connection, "Редактирование данных научной работы",
                        id, type, title, complete, description, list_students, list_supervisors
                        );

                DialogResult result = form.ShowDialog();
                if (result == DialogResult.Yes)
                    GetResearches();
            }

            form.Dispose();
        }

        private void contextMenuStripResearchesDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы точно хотите удалить запись о научной работе?\n" +
                "Удаление также затронет связанные записи публикаций и финансирования.",
                "Удаление записи о научной работе",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                GetResearch();

                int id = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = "CALL delete_research(@id)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                command.ExecuteNonQuery();

                GetResearches();
            }
        }

        private void GetGroups()
        {
            command.CommandText = "SELECT group_name FROM students";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    dt.Load(reader);

                    List<string> group_names = dt.AsEnumerable().Select(row => row.Field<string>("group_name")).Distinct().OrderBy(group_name => group_name).ToList();

                    foreach (string group_name in group_names)
                        comboBoxStudents.Items.Add(group_name);

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }
                else
                    MessageBox.Show("В данный момент список студентов пуст.",
                        "Пустой список студентов",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void GetGroupStudents()
        {
            command.CommandText = $"SELECT name, surname, patronymic FROM get_group_students(\'{comboBoxStudents.SelectedItem}\')";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
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
                else
                    MessageBox.Show($"В данный момент список группы студентов {comboBoxStudents.SelectedItem} отсутствует.",
                        "Пустая группа",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void GetStudent(string student = null)
        {
            string[] full_name;
            string name, surname, patronymic, group_name;

            if (student == null)
            {
                full_name = listBoxStudents.SelectedItem.ToString().Split(' ').Skip(1).ToArray();
                surname = full_name[0];
                name = full_name[1];
                patronymic = string.IsNullOrEmpty(full_name[2]) ? "-" : full_name[2];
                group_name = comboBoxStudents.SelectedItem.ToString();
            }
            else
            {
                full_name = student.Split(' ').Skip(1).ToArray();
                group_name = full_name[0];
                surname = full_name[1];
                name = full_name[2];
                patronymic = string.IsNullOrEmpty(full_name[3]) ? "-" : full_name[3];
            }

            command.CommandText = "SELECT * FROM students " +
                "WHERE name = @name AND surname = @surname AND group_name = @group_name AND ";

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@name", NpgsqlDbType.Text, name);
            command.Parameters.AddWithValue("@surname", NpgsqlDbType.Text, surname);
            command.Parameters.AddWithValue("@group_name", NpgsqlDbType.Text, group_name);

            if (patronymic == "-")
                command.CommandText += "patronymic IS NULL";
            else
            {
                command.CommandText += "patronymic = @patronymic";
                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, patronymic);
            }
        }

        private void GetSupervisors()
        {
            command.CommandText = "SELECT surname, name, patronymic FROM supervisors";
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
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
                else
                    MessageBox.Show("В данный момент список научных руководителей пуст.",
                        "Пустой список научных руководителей",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void GetSupervisor(string supervisor = null)
        {
            string[] full_name = supervisor == null ?
                listBoxSupervisors.SelectedItem.ToString().Split(' ').Skip(1).ToArray()
                :
                supervisor.Split(' ').Skip(1).ToArray();

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
        }

        private void GetResearches(string author = null)
        {
            string[] full_name;
            string name, surname, patronymic, group_name;

            if (listBoxStudents.SelectedIndex == ListBox.NoMatches && listBoxSupervisors.SelectedIndex == ListBox.NoMatches)
            {
                full_name = last_author.Split(' ').ToArray();

                if (full_name[0] == "Студент")
                {
                    author = "student";
                    group_name = full_name[1];
                    surname = full_name[2];
                    name = full_name[3];
                    patronymic = string.IsNullOrEmpty(full_name[4]) ? "-" : full_name[4];
                }
                else
                {
                    author = "supervisor";
                    surname = full_name[1];
                    name = full_name[2];
                    patronymic = string.IsNullOrEmpty(full_name[3]) ? "-" : full_name[3];
                }

                full_name = full_name.Skip(1).ToArray();
            }
            else
            {
                ListBox listBox = author == "student" ? listBoxStudents : listBoxSupervisors;
                full_name = listBox.SelectedItem.ToString().Split(' ').Skip(1).ToArray();
                surname = full_name[0];
                name = full_name[1];
                patronymic = string.IsNullOrEmpty(full_name[2]) ? "-" : full_name[2];
            }

            string label_name = string.Join(" ", full_name);
            labelResearches.Text = author == "student" ?
                $"Научная(-ые) работа(-ы) студента\n\"{label_name}\":"
                :
                $"Научная(-ые) работа(-ы) руководителя\n\"{label_name}\":";

            string query = author == "student" ?
                "JOIN link_students_researches lsr ON r.id = lsr.research_id JOIN students s ON s.id = lsr.student_id "
                :
                "JOIN link_supervisors_researches lsr ON r.id = lsr.research_id JOIN supervisors s ON s.id = lsr.supervisor_id ";
            command.CommandText = "SELECT r.type, r.title FROM researches r " + query +
                "WHERE s.name = @name AND s.surname = @surname AND ";

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
                if (listBoxResearches.Items.Count > 0)
                    listBoxResearches.Items.Clear();

                if (reader.HasRows)
                {
                    dt.Load(reader);

                    dt.DefaultView.Sort = "type ASC, title ASC";
                    dt = dt.DefaultView.ToTable();

                    int count = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        listBoxResearches.Items.Add($"{count}) {row["type"]}: \"{row["title"]}\"");
                        count++;
                    }

                    dt.Rows.Clear();
                    dt.Columns.Clear();
                }
            }
        }

        private void GetResearch()
        {
            string full_name = listBoxResearches.SelectedItem.ToString();
            string title = full_name.Substring(full_name.IndexOf(':') + 1).Trim().Trim('"');

            command.CommandText = "SELECT id FROM researches WHERE title = @title";

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!sessionClose)
            {
                DialogResult result = MessageBox.Show("Вы точно хотите закрыть программу?", "Подтверждение выхода",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                    Application.OpenForms[0].Close();
                else
                    e.Cancel = true;
            }
        }
    }
}
