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
    public partial class FormInfoResearchEdit : Form
    {
        private NpgsqlCommand command = new NpgsqlCommand();
        private DataTable dt = new DataTable();
        private List<int> past_student_id_list, past_supervisor_id_list, new_student_id_list, new_supervisor_id_list;
        private int data_id;
        private string past_type, past_title, past_description;
        private bool past_complete, data_new;

        private FormInfoResearchEdit(NpgsqlConnection connection)
        {
            InitializeComponent();

            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }

        public FormInfoResearchEdit(NpgsqlConnection connection, string Text) : this (connection)
        {
            this.Text = Text;

            buttonOK.Text = "Добавить";

            data_new = true;
        }

        public FormInfoResearchEdit(NpgsqlConnection connection, string Text,
            int id, string type, string title, bool complete, string description, List<string> students, List<string> supervisors
            ) : this(connection)
        {
            this.Text = Text;

            data_id = id;
            comboBoxType.SelectedItem = type;
            textBoxTitle.Text = title;
            radioButtonStatusProcess.Checked = !complete;
            radioButtonStatusComplete.Checked = complete;
            textBoxDescription.Text = description;

            past_student_id_list = new List<int>();
            past_supervisor_id_list = new List<int>();

            foreach (string student in students)
            {
                listBoxAuthorStudent.Items.Add(student);
                past_student_id_list.Add(GetStudentID(student));
            }
                

            foreach (string supervisor in supervisors)
            {
                listBoxAuthorSupervisor.Items.Add(supervisor);
                past_supervisor_id_list.Add(GetSupervisorID(supervisor));
            }
               

            past_type = type;
            past_title = title;
            past_complete = complete;
            past_description = description;

            data_new = false;
        }

        public List<int> GetAuthorStudents() => new_student_id_list;
        public List<int> GetAuthorSupervisors() => new_supervisor_id_list;

        private void FormInfoResearchEdit_Load(object sender, EventArgs e)
        {
            command.CommandText = "SELECT name, surname, patronymic, group_name FROM students";
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
                    comboBoxAuthorStudent.Items.Add(string.Join(" ", new string[] { group_name, surname, name, patronymic }));
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }

            command.CommandText = "SELECT name, surname, patronymic FROM supervisors";
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
                    comboBoxAuthorSupervisor.Items.Add(string.Join(" ", new string[] { surname, name, patronymic }));
                }

                dt.Rows.Clear();
                dt.Columns.Clear();
            }
        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBoxTitle.Text, @"^[А-ЯЁа-яё\-]+(?: [A-Za-zА-ЯЁа-яё\-]+)*$"))
                errorProvider.SetError(textBoxTitle, "");
            else
                errorProvider.SetError(textBoxTitle, "Неверный формат. Пример: Длинное-предлинное название");
        }

        private void listBoxAuthorStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAuthorStudent.SelectedIndex != ListBox.NoMatches)
                if (listBoxAuthorSupervisor.SelectedIndex != ListBox.NoMatches)
                    listBoxAuthorSupervisor.ClearSelected();
        }

        private void listBoxAuthorSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAuthorSupervisor.SelectedIndex != ListBox.NoMatches)
                if (listBoxAuthorStudent.SelectedIndex != ListBox.NoMatches)
                    listBoxAuthorStudent.ClearSelected();
        }

        private void listBoxAuthorStudent_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxAuthorStudent.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxAuthorStudent.SelectedIndex = index;
                    contextMenuStripStudent.Show(listBoxAuthorStudent, e.Location);
                }
                else
                    contextMenuStripStudent.Hide();
            }
        }

        private void listBoxAuthorSupervisor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxAuthorSupervisor.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBoxAuthorSupervisor.SelectedIndex = index;
                    contextMenuStripSupervisor.Show(listBoxAuthorSupervisor, e.Location);
                }
                else
                    contextMenuStripSupervisor.Hide();
            }
        }

        private void comboBoxAuthorStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string student = comboBoxAuthorStudent.SelectedItem.ToString();
            if (!listBoxAuthorStudent.Items.Contains(student))
                listBoxAuthorStudent.Items.Add(student);
        }

        private void comboBoxAuthorSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string supervisor = comboBoxAuthorSupervisor.SelectedItem.ToString();
            if (!listBoxAuthorSupervisor.Items.Contains(supervisor))
                listBoxAuthorSupervisor.Items.Add(supervisor);
        }

        private void contextMenuStripStudentDelete_Click(object sender, EventArgs e)
        {
            int index = listBoxAuthorStudent.SelectedIndex;
            if (index != ListBox.NoMatches)
                listBoxAuthorStudent.Items.RemoveAt(index);
        }

        private void contextMenuStripSupervisorDelete_Click(object sender, EventArgs e)
        {
            int index = listBoxAuthorSupervisor.SelectedIndex;
            if (index != ListBox.NoMatches)
                listBoxAuthorSupervisor.Items.RemoveAt(index);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool errors = false, empty = false;

            if (!string.IsNullOrEmpty(errorProvider.GetError(textBoxTitle)))
                errors = true;
            else if (string.IsNullOrEmpty(textBoxTitle.Text) && radioButtonStatusComplete.Checked || 
                listBoxAuthorStudent.Items.Count == 0 || listBoxAuthorSupervisor.Items.Count == 0 ||
                radioButtonStatusProcess.Checked == false && radioButtonStatusComplete.Checked == false)
                empty = true;


            if (!errors && !empty)
            {
                if (comboBoxType.SelectedItem != null)
                {
                    try
                    {
                        string type, title, description;
                        bool complete;

                        type = comboBoxType.Text;
                        title = textBoxTitle.Text;
                        description = textBoxDescription.Text;
                        complete = radioButtonStatusComplete.Checked;

                        command.Parameters.Clear();

                        if (data_new)
                        {
                            command.CommandText = "CALL add_research(@complete, @type, @title, @description)";

                            command.Parameters.AddWithValue("@complete", NpgsqlDbType.Boolean, complete);
                            command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, type);
                            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
                            if (string.IsNullOrEmpty(description))
                                command.Parameters.AddWithValue("@description", DBNull.Value);
                            else
                                command.Parameters.AddWithValue("@description", NpgsqlDbType.Text, description);
                        }
                        else
                        {
                            command.CommandText = "SELECT id FROM researches WHERE title = @title";
                            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
                            int id = Convert.ToInt32(command.ExecuteScalar());

                            command.CommandText = "CALL update_research(@id, @complete, @type, @title, @description)";

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, data_id);

                            if (past_complete != complete)
                                command.Parameters.AddWithValue("@complete", NpgsqlDbType.Boolean, complete);
                            else
                                command.Parameters.AddWithValue("@complete", DBNull.Value);

                            if (past_type != type)
                                command.Parameters.AddWithValue("@type", NpgsqlDbType.Text, type);
                            else
                                command.Parameters.AddWithValue("@type", DBNull.Value);

                            if (past_title != title)
                                command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
                            else
                                command.Parameters.AddWithValue("@title", DBNull.Value);

                            if (past_description != description && !string.IsNullOrEmpty(description))
                                command.Parameters.AddWithValue("@description", NpgsqlDbType.Text, description);
                            else
                                command.Parameters.AddWithValue("@description", DBNull.Value);
                        }

                        command.ExecuteNonQuery();

                        if (data_new)
                        {
                            command.CommandText = "SELECT id FROM researches WHERE title = @title";
                            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, title);
                            data_id = Convert.ToInt32(command.ExecuteScalar());
                        }
                            
                        List<int> student_id_list = new List<int>();
                        List<int> supervisor_id_list = new List<int>();

                        foreach (string student in listBoxAuthorStudent.Items)
                            student_id_list.Add(GetStudentID(student));

                        foreach (string supervisor in listBoxAuthorSupervisor.Items)
                            supervisor_id_list.Add(GetSupervisorID(supervisor));

                        command.CommandText = "CALL assign_research(@student_id_list, @supervisor_id_list, @id)";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@student_id_list", NpgsqlDbType.Array | NpgsqlDbType.Integer, student_id_list.ToArray());
                        command.Parameters.AddWithValue("@supervisor_id_list", NpgsqlDbType.Array | NpgsqlDbType.Integer, supervisor_id_list.ToArray());
                        command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, data_id);

                        command.ExecuteNonQuery();

                        new_student_id_list = student_id_list;
                        new_supervisor_id_list = supervisor_id_list;

                        if (!data_new)
                        {
                            foreach (int student_id in past_student_id_list) // отвязка убранных из списка студентов
                            {
                                if (!new_student_id_list.Contains(student_id))
                                {
                                    command.CommandText = "DELETE FROM link_students_researches " +
                                        "WHERE research_id = @research_id AND student_id = @student_id";

                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@research_id", NpgsqlDbType.Integer, data_id);
                                    command.Parameters.AddWithValue("@student_id", NpgsqlDbType.Integer, student_id);

                                    command.ExecuteNonQuery();
                                }
                            }

                            foreach (int supervisor_id in past_supervisor_id_list) // отвязка убранных из списка руководителей
                            {
                                if (!new_supervisor_id_list.Contains(supervisor_id))
                                {
                                    command.CommandText = "DELETE FROM link_supervisor_researches " +
                                        "WHERE research_id = @research_id AND supervisor_id = @supervisor_id";

                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@research_id", NpgsqlDbType.Integer, data_id);
                                    command.Parameters.AddWithValue("@supervisor_id", NpgsqlDbType.Integer, supervisor_id);

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

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
                    MessageBox.Show("Выберите тип научной работы из выпадающего списка!",
                        "Ошибка данных поля \"тип работы\"",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Заполните все поля данными и исправьте их формат,\n" +
                                "если у них отображается значок ошибки!",
                    "Ошибка в заполнении данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInfoResearchEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Yes)
                DialogResult = DialogResult.No;
        }

        private int GetStudentID(string student)
        {
            string[] full_student = student.Split(' ').ToArray();
            string name, surname, patronymic, group_name;

            group_name = full_student[0];
            surname = full_student[1];
            name = full_student[2];
            patronymic = string.IsNullOrEmpty(full_student[3]) ? "-" : full_student[3];

            command.CommandText = "SELECT id FROM students WHERE " +
                "name = @name AND surname = @surname AND group_name = @group_name AND ";

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
            
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private int GetSupervisorID(string supervisor)
        {
            string[] full_supervisor = supervisor.Split(' ').ToArray();
            string name, surname, patronymic;

            surname = full_supervisor[0];
            name = full_supervisor[1];
            patronymic = string.IsNullOrEmpty(full_supervisor[2]) ? "-" : full_supervisor[2];

            command.CommandText = "SELECT id FROM supervisors WHERE " +
                "name = @name AND surname = @surname AND ";

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
            
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
