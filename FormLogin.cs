using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace ResearchLink
{
    public partial class FormLogin : Form
    {
        private NpgsqlConnection connection = null;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point((workingArea.Width - Width) / 2 - 75, (workingArea.Height - Height) / 2 - 25);

            MinimumSize = new Size(820, 500);
        }

        private void FormLogin_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                float scaleFactor = Math.Min((float)ClientSize.Width / 820, (float)ClientSize.Height / 500);
                Dictionary<Control, byte> elementsFontSizes = new Dictionary<Control, byte> {
                { labelMain, 15 }, { labelLogin, 14 }, { labelPassword, 14 },
                { textBoxLogin, 14 }, { textBoxPassword, 14 },
                { button, 14 }
                };

                foreach (var kvp in elementsFontSizes)
                {
                    Control element = kvp.Key;
                    element.Font = new Font(element.Font.FontFamily, kvp.Value * scaleFactor, element.Font.Style);
                }
            }
        }

        private void textBoxLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxPassword.Text.Length == 0)
                    textBoxPassword.Focus();
                else
                    button_Click(sender, e);
                e.Handled = true;
            }
        }

        private void textBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxLogin.Text.Length == 0)
                    textBoxLogin.Focus();
                else
                    button_Click(sender, e);
                e.Handled = true;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) && string.IsNullOrEmpty(textBoxPassword.Text))
                MessageBox.Show("Пожалуйста, введите логин и пароль!",
                    "Ошибка входа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
            {
                try
                {
                    connection = new NpgsqlConnection($"Server = localhost; Port = 5432; Database = SRWS; User ID = {textBoxLogin.Text}; Password = {textBoxPassword.Text}");
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        FormMain formMain = new FormMain(connection)
                        {
                            WindowState = WindowState,
                            MinimumSize = new Size(1350, 500)
                        };

                        Hide();
                        textBoxLogin.Clear();
                        textBoxPassword.Clear();

                        formMain.ShowDialog();
                        formMain.Dispose();
                        Show();
                    }
                    else
                        MessageBox.Show("Не удалось зарегестрироваться в системе. Используйте логин и пароль зарегестрированного в системе пользователя",
                            "Ошибка регистрации",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show($"Ошибка сессии: {ex.Message}",
                        "Ошибка подключения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                connection.Dispose();
            }
        }
    }
}
