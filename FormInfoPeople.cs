using System;
using System.Drawing;
using System.Windows.Forms;

namespace ResearchLink
{
    public partial class FormInfoPeople : Form
    {
        public FormInfoPeople(
            string Text, string surname, string name, string patronymic, string gender, string group_degree, string email
        )
        {
            InitializeComponent();

            this.Text = Text;
            labelSurnameData.Text = surname;
            labelNameData.Text = name;
            labelPatronymicData.Text = patronymic;
            labelGenderData.Text = gender;
            labelGroupDegreeData.Text = group_degree;
            labelEmailData.Text = email;

            using (Graphics g = CreateGraphics())
            {
                SizeF textSizeGroupDegree = g.MeasureString(labelGroupDegreeData.Text, labelEmailData.Font);
                SizeF textSizeEmail = g.MeasureString(labelEmailData.Text, labelEmailData.Font);
                Width += Math.Max((int)textSizeGroupDegree.Width - 50, (int)textSizeEmail.Width - 100);
            }
        }
    }
}
