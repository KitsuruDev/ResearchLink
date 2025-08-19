namespace ResearchLink
{
    partial class FormMain
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
            this.listBoxResearches = new System.Windows.Forms.ListBox();
            this.listBoxStudents = new System.Windows.Forms.ListBox();
            this.comboBoxStudents = new System.Windows.Forms.ComboBox();
            this.labelResearches = new System.Windows.Forms.Label();
            this.labelStudents = new System.Windows.Forms.Label();
            this.labelSupervisors = new System.Windows.Forms.Label();
            this.listBoxSupervisors = new System.Windows.Forms.ListBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuItemSession = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSessionClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSessionExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataShow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAddStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAddSupervisor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAddResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAddPublication = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAddFunding = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDataList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataListResearches = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataListPublications = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataListFunding = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExtra = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExtraHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExtraAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripStudent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripStudentShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripStudentUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripStudentDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripResearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripResearchesShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripResearchesUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripResearchesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSupervisors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripSupervisorsShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSupervisorsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSupervisorsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.menu.SuspendLayout();
            this.contextMenuStripStudent.SuspendLayout();
            this.contextMenuStripResearch.SuspendLayout();
            this.contextMenuStripSupervisors.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 5;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.94187F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.219574F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.39339F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.201201F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.37838F));
            this.tableLayoutPanel.Controls.Add(this.listBoxResearches, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.listBoxStudents, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.comboBoxStudents, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelResearches, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.labelStudents, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelSupervisors, 4, 0);
            this.tableLayoutPanel.Controls.Add(this.listBoxSupervisors, 4, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 41);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.21378F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.21378F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.57245F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1332, 414);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // listBoxResearches
            // 
            this.listBoxResearches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxResearches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxResearches.FormattingEnabled = true;
            this.listBoxResearches.HorizontalScrollbar = true;
            this.listBoxResearches.ItemHeight = 18;
            this.listBoxResearches.Location = new System.Drawing.Point(364, 45);
            this.listBoxResearches.Name = "listBoxResearches";
            this.tableLayoutPanel.SetRowSpan(this.listBoxResearches, 2);
            this.listBoxResearches.ScrollAlwaysVisible = true;
            this.listBoxResearches.Size = new System.Drawing.Size(571, 366);
            this.listBoxResearches.TabIndex = 6;
            this.listBoxResearches.SelectedIndexChanged += new System.EventHandler(this.listBoxResearches_SelectedIndexChanged);
            this.listBoxResearches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxResearches_MouseUp);
            // 
            // listBoxStudents
            // 
            this.listBoxStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxStudents.FormattingEnabled = true;
            this.listBoxStudents.ItemHeight = 18;
            this.listBoxStudents.Location = new System.Drawing.Point(3, 87);
            this.listBoxStudents.Name = "listBoxStudents";
            this.listBoxStudents.ScrollAlwaysVisible = true;
            this.listBoxStudents.Size = new System.Drawing.Size(339, 324);
            this.listBoxStudents.TabIndex = 1;
            this.listBoxStudents.SelectedIndexChanged += new System.EventHandler(this.listBoxStudents_SelectedIndexChanged);
            this.listBoxStudents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxStudents_MouseUp);
            // 
            // comboBoxStudents
            // 
            this.comboBoxStudents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStudents.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxStudents.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStudents.FormattingEnabled = true;
            this.comboBoxStudents.Location = new System.Drawing.Point(3, 45);
            this.comboBoxStudents.Name = "comboBoxStudents";
            this.comboBoxStudents.Size = new System.Drawing.Size(339, 30);
            this.comboBoxStudents.TabIndex = 2;
            this.comboBoxStudents.Text = "Выберете учебную группу...";
            this.comboBoxStudents.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // labelResearches
            // 
            this.labelResearches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelResearches.AutoSize = true;
            this.labelResearches.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResearches.Location = new System.Drawing.Point(364, 0);
            this.labelResearches.Name = "labelResearches";
            this.labelResearches.Size = new System.Drawing.Size(571, 25);
            this.labelResearches.TabIndex = 5;
            this.labelResearches.Text = "Научная(-ые) работа(-ы) автора:";
            this.labelResearches.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStudents
            // 
            this.labelStudents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStudents.AutoSize = true;
            this.labelStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStudents.Location = new System.Drawing.Point(3, 0);
            this.labelStudents.Name = "labelStudents";
            this.labelStudents.Size = new System.Drawing.Size(339, 25);
            this.labelStudents.TabIndex = 3;
            this.labelStudents.Text = "Список студентов:";
            this.labelStudents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSupervisors
            // 
            this.labelSupervisors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSupervisors.AutoSize = true;
            this.labelSupervisors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSupervisors.Location = new System.Drawing.Point(956, 0);
            this.labelSupervisors.Name = "labelSupervisors";
            this.labelSupervisors.Size = new System.Drawing.Size(373, 25);
            this.labelSupervisors.TabIndex = 7;
            this.labelSupervisors.Text = "Список научных руководителей:";
            this.labelSupervisors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxSupervisors
            // 
            this.listBoxSupervisors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSupervisors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxSupervisors.FormattingEnabled = true;
            this.listBoxSupervisors.ItemHeight = 18;
            this.listBoxSupervisors.Location = new System.Drawing.Point(956, 45);
            this.listBoxSupervisors.Name = "listBoxSupervisors";
            this.tableLayoutPanel.SetRowSpan(this.listBoxSupervisors, 2);
            this.listBoxSupervisors.ScrollAlwaysVisible = true;
            this.listBoxSupervisors.Size = new System.Drawing.Size(373, 366);
            this.listBoxSupervisors.TabIndex = 8;
            this.listBoxSupervisors.SelectedIndexChanged += new System.EventHandler(this.listBoxSupervisors_SelectedIndexChanged);
            this.listBoxSupervisors.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxSupervisors_MouseUp);
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSession,
            this.menuItemData,
            this.menuItemExtra});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1332, 28);
            this.menu.TabIndex = 4;
            this.menu.Text = "menuStrip1";
            // 
            // menuItemSession
            // 
            this.menuItemSession.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSessionClose,
            this.menuItemSessionExit});
            this.menuItemSession.Name = "menuItemSession";
            this.menuItemSession.Size = new System.Drawing.Size(64, 24);
            this.menuItemSession.Text = "Сеанс";
            // 
            // menuItemSessionClose
            // 
            this.menuItemSessionClose.Name = "menuItemSessionClose";
            this.menuItemSessionClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.menuItemSessionClose.Size = new System.Drawing.Size(271, 26);
            this.menuItemSessionClose.Text = "Завершить сеанс";
            this.menuItemSessionClose.Click += new System.EventHandler(this.menuItemSessionClose_Click);
            // 
            // menuItemSessionExit
            // 
            this.menuItemSessionExit.Name = "menuItemSessionExit";
            this.menuItemSessionExit.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.menuItemSessionExit.Size = new System.Drawing.Size(271, 26);
            this.menuItemSessionExit.Text = "Выйти";
            this.menuItemSessionExit.Click += new System.EventHandler(this.menuItemSessionExit_Click);
            // 
            // menuItemData
            // 
            this.menuItemData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDataShow,
            this.menuItemDataUpdate,
            this.menuItemDataDelete,
            this.menuItemDataAdd,
            this.menuItemDataSeparator,
            this.menuItemDataList});
            this.menuItemData.Name = "menuItemData";
            this.menuItemData.Size = new System.Drawing.Size(78, 24);
            this.menuItemData.Text = "Данные";
            // 
            // menuItemDataShow
            // 
            this.menuItemDataShow.Enabled = false;
            this.menuItemDataShow.Name = "menuItemDataShow";
            this.menuItemDataShow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemDataShow.Size = new System.Drawing.Size(358, 26);
            this.menuItemDataShow.Text = "Посмотреть данные записи...";
            this.menuItemDataShow.Click += new System.EventHandler(this.menuItemData_Click);
            // 
            // menuItemDataUpdate
            // 
            this.menuItemDataUpdate.Enabled = false;
            this.menuItemDataUpdate.Name = "menuItemDataUpdate";
            this.menuItemDataUpdate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.menuItemDataUpdate.Size = new System.Drawing.Size(358, 26);
            this.menuItemDataUpdate.Text = "Изменить данные записи...";
            this.menuItemDataUpdate.Visible = false;
            this.menuItemDataUpdate.Click += new System.EventHandler(this.menuItemData_Click);
            // 
            // menuItemDataDelete
            // 
            this.menuItemDataDelete.Enabled = false;
            this.menuItemDataDelete.Name = "menuItemDataDelete";
            this.menuItemDataDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuItemDataDelete.Size = new System.Drawing.Size(358, 26);
            this.menuItemDataDelete.Text = "Удалить запись...";
            this.menuItemDataDelete.Click += new System.EventHandler(this.menuItemDataDelete_Click);
            // 
            // menuItemDataAdd
            // 
            this.menuItemDataAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDataAddStudent,
            this.menuItemDataAddSupervisor,
            this.menuItemDataAddResearch,
            this.menuItemDataAddPublication,
            this.menuItemDataAddFunding});
            this.menuItemDataAdd.Name = "menuItemDataAdd";
            this.menuItemDataAdd.Size = new System.Drawing.Size(358, 26);
            this.menuItemDataAdd.Text = "Добавить запись";
            // 
            // menuItemDataAddStudent
            // 
            this.menuItemDataAddStudent.Name = "menuItemDataAddStudent";
            this.menuItemDataAddStudent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuItemDataAddStudent.Size = new System.Drawing.Size(456, 26);
            this.menuItemDataAddStudent.Text = "Добавить студента...";
            this.menuItemDataAddStudent.Click += new System.EventHandler(this.menuItemDataAddPeople_Click);
            // 
            // menuItemDataAddSupervisor
            // 
            this.menuItemDataAddSupervisor.Name = "menuItemDataAddSupervisor";
            this.menuItemDataAddSupervisor.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.menuItemDataAddSupervisor.Size = new System.Drawing.Size(456, 26);
            this.menuItemDataAddSupervisor.Text = "Добавить научного руководителя...";
            this.menuItemDataAddSupervisor.Click += new System.EventHandler(this.menuItemDataAddPeople_Click);
            // 
            // menuItemDataAddResearch
            // 
            this.menuItemDataAddResearch.Name = "menuItemDataAddResearch";
            this.menuItemDataAddResearch.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.menuItemDataAddResearch.Size = new System.Drawing.Size(456, 26);
            this.menuItemDataAddResearch.Text = "Добавить научную работу...";
            this.menuItemDataAddResearch.Click += new System.EventHandler(this.menuItemDataAddResearch_Click);
            // 
            // menuItemDataAddPublication
            // 
            this.menuItemDataAddPublication.Name = "menuItemDataAddPublication";
            this.menuItemDataAddPublication.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.menuItemDataAddPublication.Size = new System.Drawing.Size(456, 26);
            this.menuItemDataAddPublication.Text = "Добавить публикацию работы...";
            this.menuItemDataAddPublication.Click += new System.EventHandler(this.menuItemDataAddDetails_Click);
            // 
            // menuItemDataAddFunding
            // 
            this.menuItemDataAddFunding.Name = "menuItemDataAddFunding";
            this.menuItemDataAddFunding.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.menuItemDataAddFunding.Size = new System.Drawing.Size(456, 26);
            this.menuItemDataAddFunding.Text = "Добавить финансирование работы...";
            this.menuItemDataAddFunding.Click += new System.EventHandler(this.menuItemDataAddDetails_Click);
            // 
            // menuItemDataSeparator
            // 
            this.menuItemDataSeparator.Name = "menuItemDataSeparator";
            this.menuItemDataSeparator.Size = new System.Drawing.Size(355, 6);
            // 
            // menuItemDataList
            // 
            this.menuItemDataList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDataListResearches,
            this.menuItemDataListPublications,
            this.menuItemDataListFunding});
            this.menuItemDataList.Name = "menuItemDataList";
            this.menuItemDataList.Size = new System.Drawing.Size(358, 26);
            this.menuItemDataList.Text = "Полный список записей";
            // 
            // menuItemDataListResearches
            // 
            this.menuItemDataListResearches.Name = "menuItemDataListResearches";
            this.menuItemDataListResearches.Size = new System.Drawing.Size(214, 26);
            this.menuItemDataListResearches.Text = "Научные работы";
            this.menuItemDataListResearches.Click += new System.EventHandler(this.menuItemDataListElement_click);
            // 
            // menuItemDataListPublications
            // 
            this.menuItemDataListPublications.Name = "menuItemDataListPublications";
            this.menuItemDataListPublications.Size = new System.Drawing.Size(214, 26);
            this.menuItemDataListPublications.Text = "Публикации";
            this.menuItemDataListPublications.Click += new System.EventHandler(this.menuItemDataListElement_click);
            // 
            // menuItemDataListFunding
            // 
            this.menuItemDataListFunding.Name = "menuItemDataListFunding";
            this.menuItemDataListFunding.Size = new System.Drawing.Size(214, 26);
            this.menuItemDataListFunding.Text = "Финансирование";
            this.menuItemDataListFunding.Click += new System.EventHandler(this.menuItemDataListElement_click);
            // 
            // menuItemExtra
            // 
            this.menuItemExtra.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExtraHelp,
            this.menuItemExtraAbout});
            this.menuItemExtra.Name = "menuItemExtra";
            this.menuItemExtra.Size = new System.Drawing.Size(81, 24);
            this.menuItemExtra.Text = "Справка";
            // 
            // menuItemExtraHelp
            // 
            this.menuItemExtraHelp.Name = "menuItemExtraHelp";
            this.menuItemExtraHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuItemExtraHelp.Size = new System.Drawing.Size(211, 26);
            this.menuItemExtraHelp.Text = "Помощь";
            this.menuItemExtraHelp.Click += new System.EventHandler(this.menuItemExtraHelp_Click);
            // 
            // menuItemExtraAbout
            // 
            this.menuItemExtraAbout.Name = "menuItemExtraAbout";
            this.menuItemExtraAbout.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuItemExtraAbout.Size = new System.Drawing.Size(211, 26);
            this.menuItemExtraAbout.Text = "О программе";
            this.menuItemExtraAbout.Click += new System.EventHandler(this.menuItemExtraAbout_Click);
            // 
            // contextMenuStripStudent
            // 
            this.contextMenuStripStudent.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripStudent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripStudentShow,
            this.contextMenuStripStudentUpdate,
            this.contextMenuStripStudentDelete});
            this.contextMenuStripStudent.Name = "contextMenuStripStudent";
            this.contextMenuStripStudent.ShowImageMargin = false;
            this.contextMenuStripStudent.Size = new System.Drawing.Size(222, 76);
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
            // contextMenuStripStudentDelete
            // 
            this.contextMenuStripStudentDelete.Name = "contextMenuStripStudentDelete";
            this.contextMenuStripStudentDelete.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripStudentDelete.Text = "Удалить запись...";
            this.contextMenuStripStudentDelete.Click += new System.EventHandler(this.contextMenuStripStudentDelete_Click);
            // 
            // contextMenuStripResearch
            // 
            this.contextMenuStripResearch.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripResearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripResearchesShow,
            this.contextMenuStripResearchesUpdate,
            this.contextMenuStripResearchesDelete});
            this.contextMenuStripResearch.Name = "contextMenuStripStudent";
            this.contextMenuStripResearch.ShowImageMargin = false;
            this.contextMenuStripResearch.Size = new System.Drawing.Size(222, 76);
            // 
            // contextMenuStripResearchesShow
            // 
            this.contextMenuStripResearchesShow.Name = "contextMenuStripResearchesShow";
            this.contextMenuStripResearchesShow.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripResearchesShow.Text = "Посмотреть данные...";
            this.contextMenuStripResearchesShow.Click += new System.EventHandler(this.contextMenuStripResearches_Click);
            // 
            // contextMenuStripResearchesUpdate
            // 
            this.contextMenuStripResearchesUpdate.Name = "contextMenuStripResearchesUpdate";
            this.contextMenuStripResearchesUpdate.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripResearchesUpdate.Text = "Редактировать данные...";
            this.contextMenuStripResearchesUpdate.Click += new System.EventHandler(this.contextMenuStripResearches_Click);
            // 
            // contextMenuStripResearchesDelete
            // 
            this.contextMenuStripResearchesDelete.Name = "contextMenuStripResearchesDelete";
            this.contextMenuStripResearchesDelete.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripResearchesDelete.Text = "Удалить запись...";
            this.contextMenuStripResearchesDelete.Click += new System.EventHandler(this.contextMenuStripResearchesDelete_Click);
            // 
            // contextMenuStripSupervisors
            // 
            this.contextMenuStripSupervisors.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripSupervisors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripSupervisorsShow,
            this.contextMenuStripSupervisorsUpdate,
            this.contextMenuStripSupervisorsDelete});
            this.contextMenuStripSupervisors.Name = "contextMenuStripStudent";
            this.contextMenuStripSupervisors.ShowImageMargin = false;
            this.contextMenuStripSupervisors.Size = new System.Drawing.Size(222, 76);
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
            // contextMenuStripSupervisorsDelete
            // 
            this.contextMenuStripSupervisorsDelete.Name = "contextMenuStripSupervisorsDelete";
            this.contextMenuStripSupervisorsDelete.Size = new System.Drawing.Size(221, 24);
            this.contextMenuStripSupervisorsDelete.Text = "Удалить запись...";
            this.contextMenuStripSupervisorsDelete.Click += new System.EventHandler(this.contextMenuStripSupervisorsDelete_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 453);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Главное окно";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.contextMenuStripStudent.ResumeLayout(false);
            this.contextMenuStripResearch.ResumeLayout(false);
            this.contextMenuStripSupervisors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ListBox listBoxStudents;
        private System.Windows.Forms.ComboBox comboBoxStudents;
        private System.Windows.Forms.Label labelStudents;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuItemExtra;
        private System.Windows.Forms.ToolStripMenuItem menuItemExtraHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemExtraAbout;
        private System.Windows.Forms.Label labelResearches;
        private System.Windows.Forms.ListBox listBoxResearches;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripStudent;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripStudentShow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripResearch;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripResearchesShow;
        private System.Windows.Forms.ToolStripMenuItem menuItemSession;
        private System.Windows.Forms.Label labelSupervisors;
        private System.Windows.Forms.ListBox listBoxSupervisors;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSupervisors;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripSupervisorsShow;
        private System.Windows.Forms.ToolStripMenuItem menuItemSessionClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemSessionExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemData;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataList;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataListResearches;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataListPublications;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataListFunding;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataShow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripStudentUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataUpdate;
        private System.Windows.Forms.ToolStripSeparator menuItemDataSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAdd;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAddStudent;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAddSupervisor;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAddResearch;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAddPublication;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAddFunding;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripSupervisorsUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripStudentDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripSupervisorsDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripResearchesDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripResearchesUpdate;
    }
}