namespace ResearchLink
{
    partial class FormInfoResearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxFunding = new System.Windows.Forms.ListBox();
            this.labelFunding = new System.Windows.Forms.Label();
            this.listBoxPublications = new System.Windows.Forms.ListBox();
            this.labelPublications = new System.Windows.Forms.Label();
            this.labelStatusValue = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelSupervisors = new System.Windows.Forms.Label();
            this.labelStudents = new System.Windows.Forms.Label();
            this.listBoxSupervisors = new System.Windows.Forms.ListBox();
            this.listBoxStudents = new System.Windows.Forms.ListBox();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.contextMenuStripStudent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripStudentShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripStudentUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSupervisors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripSupervisorsShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSupervisorsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripPublications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripPublicationsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripFunding = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripFundingUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.contextMenuStripStudent.SuspendLayout();
            this.contextMenuStripSupervisors.SuspendLayout();
            this.contextMenuStripPublications.SuspendLayout();
            this.contextMenuStripFunding.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.25F));
            this.tableLayoutPanel.Controls.Add(this.listBoxFunding, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.labelFunding, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.listBoxPublications, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.labelPublications, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.labelStatusValue, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelStatus, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelSupervisors, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.labelStudents, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.listBoxSupervisors, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.listBoxStudents, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.labelAuthor, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelDescription, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.16052F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.48547F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.823973F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.51062F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.6228F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.26443F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.1322F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(802, 573);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // listBoxFunding
            // 
            this.tableLayoutPanel.SetColumnSpan(this.listBoxFunding, 2);
            this.listBoxFunding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFunding.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxFunding.FormattingEnabled = true;
            this.listBoxFunding.HorizontalScrollbar = true;
            this.listBoxFunding.ItemHeight = 18;
            this.listBoxFunding.Location = new System.Drawing.Point(255, 458);
            this.listBoxFunding.Name = "listBoxFunding";
            this.listBoxFunding.ScrollAlwaysVisible = true;
            this.listBoxFunding.Size = new System.Drawing.Size(544, 112);
            this.listBoxFunding.TabIndex = 19;
            this.listBoxFunding.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxFunding_MouseUp);
            // 
            // labelFunding
            // 
            this.labelFunding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFunding.AutoSize = true;
            this.labelFunding.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFunding.Location = new System.Drawing.Point(3, 499);
            this.labelFunding.Name = "labelFunding";
            this.labelFunding.Size = new System.Drawing.Size(246, 29);
            this.labelFunding.TabIndex = 18;
            this.labelFunding.Text = "Финансирование:";
            this.labelFunding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxPublications
            // 
            this.tableLayoutPanel.SetColumnSpan(this.listBoxPublications, 2);
            this.listBoxPublications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPublications.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxPublications.FormattingEnabled = true;
            this.listBoxPublications.HorizontalScrollbar = true;
            this.listBoxPublications.ItemHeight = 18;
            this.listBoxPublications.Location = new System.Drawing.Point(255, 348);
            this.listBoxPublications.Name = "listBoxPublications";
            this.listBoxPublications.ScrollAlwaysVisible = true;
            this.listBoxPublications.Size = new System.Drawing.Size(544, 104);
            this.listBoxPublications.TabIndex = 17;
            this.listBoxPublications.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxPublications_MouseUp);
            // 
            // labelPublications
            // 
            this.labelPublications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPublications.AutoSize = true;
            this.labelPublications.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPublications.Location = new System.Drawing.Point(3, 385);
            this.labelPublications.Name = "labelPublications";
            this.labelPublications.Size = new System.Drawing.Size(246, 29);
            this.labelPublications.TabIndex = 16;
            this.labelPublications.Text = "Публикации:";
            this.labelPublications.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatusValue
            // 
            this.labelStatusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatusValue.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelStatusValue, 2);
            this.labelStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatusValue.Location = new System.Drawing.Point(255, 289);
            this.labelStatusValue.Name = "labelStatusValue";
            this.labelStatusValue.Size = new System.Drawing.Size(544, 29);
            this.labelStatusValue.TabIndex = 15;
            this.labelStatusValue.Text = "Выполнено/В процессе";
            this.labelStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatus.Location = new System.Drawing.Point(3, 289);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(246, 29);
            this.labelStatus.TabIndex = 14;
            this.labelStatus.Text = "Статус:";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSupervisors
            // 
            this.labelSupervisors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSupervisors.AutoSize = true;
            this.labelSupervisors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupervisors.Location = new System.Drawing.Point(529, 159);
            this.labelSupervisors.Name = "labelSupervisors";
            this.labelSupervisors.Size = new System.Drawing.Size(270, 25);
            this.labelSupervisors.TabIndex = 13;
            this.labelSupervisors.Text = "Научные руководители:";
            this.labelSupervisors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStudents
            // 
            this.labelStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStudents.AutoSize = true;
            this.labelStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStudents.Location = new System.Drawing.Point(255, 159);
            this.labelStudents.Name = "labelStudents";
            this.labelStudents.Size = new System.Drawing.Size(268, 25);
            this.labelStudents.TabIndex = 12;
            this.labelStudents.Text = "Студенты:";
            this.labelStudents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxSupervisors
            // 
            this.listBoxSupervisors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSupervisors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxSupervisors.FormattingEnabled = true;
            this.listBoxSupervisors.ItemHeight = 18;
            this.listBoxSupervisors.Location = new System.Drawing.Point(529, 194);
            this.listBoxSupervisors.Name = "listBoxSupervisors";
            this.listBoxSupervisors.ScrollAlwaysVisible = true;
            this.listBoxSupervisors.Size = new System.Drawing.Size(270, 65);
            this.listBoxSupervisors.TabIndex = 11;
            this.listBoxSupervisors.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxSupervisors_MouseUp);
            // 
            // listBoxStudents
            // 
            this.listBoxStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxStudents.FormattingEnabled = true;
            this.listBoxStudents.ItemHeight = 18;
            this.listBoxStudents.Location = new System.Drawing.Point(255, 194);
            this.listBoxStudents.Name = "listBoxStudents";
            this.listBoxStudents.ScrollAlwaysVisible = true;
            this.listBoxStudents.Size = new System.Drawing.Size(268, 65);
            this.listBoxStudents.TabIndex = 10;
            this.listBoxStudents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxStudents_MouseUp);
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAuthor.Location = new System.Drawing.Point(3, 192);
            this.labelAuthor.Name = "labelAuthor";
            this.tableLayoutPanel.SetRowSpan(this.labelAuthor, 2);
            this.labelAuthor.Size = new System.Drawing.Size(246, 29);
            this.labelAuthor.TabIndex = 9;
            this.labelAuthor.Text = "Авторы:";
            this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDescription.Location = new System.Drawing.Point(3, 99);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(246, 29);
            this.labelDescription.TabIndex = 7;
            this.labelDescription.Text = "Описание:";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelTitle, 3);
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(3, 23);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(796, 29);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "Название работы";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel.SetColumnSpan(this.textBoxDescription, 2);
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDescription.Location = new System.Drawing.Point(255, 78);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(544, 71);
            this.textBoxDescription.TabIndex = 8;
            // 
            // contextMenuStripStudent
            // 
            this.contextMenuStripStudent.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripStudent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripStudentShow,
            this.contextMenuStripStudentUpdate});
            this.contextMenuStripStudent.Name = "contextMenuStripStudent";
            this.contextMenuStripStudent.ShowImageMargin = false;
            this.contextMenuStripStudent.Size = new System.Drawing.Size(222, 52);
            // 
            // contextMenuStripStudentShow
            // 
            this.contextMenuStripStudentShow.Name = "contextMenuStripStudentShow";
            this.contextMenuStripStudentShow.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripStudentShow.Text = "Посмотреть данные...";
            this.contextMenuStripStudentShow.Click += new System.EventHandler(this.contextMenuStripStudent_Click);
            // 
            // contextMenuStripStudentUpdate
            // 
            this.contextMenuStripStudentUpdate.Name = "contextMenuStripStudentUpdate";
            this.contextMenuStripStudentUpdate.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripStudentUpdate.Text = "Редактировать данные...";
            this.contextMenuStripStudentUpdate.Click += new System.EventHandler(this.contextMenuStripStudent_Click);
            // 
            // contextMenuStripSupervisors
            // 
            this.contextMenuStripSupervisors.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripSupervisors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripSupervisorsShow,
            this.contextMenuStripSupervisorsUpdate});
            this.contextMenuStripSupervisors.Name = "contextMenuStripStudent";
            this.contextMenuStripSupervisors.ShowImageMargin = false;
            this.contextMenuStripSupervisors.Size = new System.Drawing.Size(222, 52);
            // 
            // contextMenuStripSupervisorsShow
            // 
            this.contextMenuStripSupervisorsShow.Name = "contextMenuStripSupervisorsShow";
            this.contextMenuStripSupervisorsShow.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripSupervisorsShow.Text = "Посмотреть данные...";
            this.contextMenuStripSupervisorsShow.Click += new System.EventHandler(this.contextMenuStripSupervisors_Click);
            // 
            // contextMenuStripSupervisorsUpdate
            // 
            this.contextMenuStripSupervisorsUpdate.Name = "contextMenuStripSupervisorsUpdate";
            this.contextMenuStripSupervisorsUpdate.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripSupervisorsUpdate.Text = "Редактировать данные...";
            this.contextMenuStripSupervisorsUpdate.Click += new System.EventHandler(this.contextMenuStripSupervisors_Click);
            // 
            // contextMenuStripPublications
            // 
            this.contextMenuStripPublications.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripPublications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripPublicationsUpdate});
            this.contextMenuStripPublications.Name = "contextMenuStripStudent";
            this.contextMenuStripPublications.ShowImageMargin = false;
            this.contextMenuStripPublications.Size = new System.Drawing.Size(222, 28);
            // 
            // contextMenuStripPublicationsUpdate
            // 
            this.contextMenuStripPublicationsUpdate.Name = "contextMenuStripPublicationsUpdate";
            this.contextMenuStripPublicationsUpdate.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripPublicationsUpdate.Text = "Редактировать данные...";
            this.contextMenuStripPublicationsUpdate.Click += new System.EventHandler(this.contextMenuStripPublicationsUpdate_Click);
            // 
            // contextMenuStripFunding
            // 
            this.contextMenuStripFunding.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripFunding.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripFundingUpdate});
            this.contextMenuStripFunding.Name = "contextMenuStripStudent";
            this.contextMenuStripFunding.ShowImageMargin = false;
            this.contextMenuStripFunding.Size = new System.Drawing.Size(222, 28);
            // 
            // contextMenuStripFundingUpdate
            // 
            this.contextMenuStripFundingUpdate.Name = "contextMenuStripFundingUpdate";
            this.contextMenuStripFundingUpdate.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripFundingUpdate.Text = "Редактировать данные...";
            this.contextMenuStripFundingUpdate.Click += new System.EventHandler(this.contextMenuStripFundingUpdate_Click);
            // 
            // FormInfoResearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 573);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FormInfoResearch";
            this.Text = "Информация о научной работе";
            this.Load += new System.EventHandler(this.FormInfoResearch_Load);
            this.Resize += new System.EventHandler(this.FormInfoResearch_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.contextMenuStripStudent.ResumeLayout(false);
            this.contextMenuStripSupervisors.ResumeLayout(false);
            this.contextMenuStripPublications.ResumeLayout(false);
            this.contextMenuStripFunding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelSupervisors;
        private System.Windows.Forms.Label labelStudents;
        private System.Windows.Forms.ListBox listBoxSupervisors;
        private System.Windows.Forms.ListBox listBoxStudents;
        private System.Windows.Forms.Label labelStatusValue;
        private System.Windows.Forms.ListBox listBoxFunding;
        private System.Windows.Forms.Label labelFunding;
        private System.Windows.Forms.ListBox listBoxPublications;
        private System.Windows.Forms.Label labelPublications;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStudent;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripStudentShow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSupervisors;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripSupervisorsShow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripStudentUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripSupervisorsUpdate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPublications;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripPublicationsUpdate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFunding;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripFundingUpdate;
    }
}