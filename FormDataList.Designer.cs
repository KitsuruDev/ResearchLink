namespace ResearchLink
{
    partial class FormDataList
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStripData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDataSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDataShow = new System.Windows.Forms.ToolStripMenuItem();
            this.label = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripResearchShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.menuStrip, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.listBox, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.19048F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(802, 453);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripData});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(802, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuStripData
            // 
            this.menuStripData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDataAdd,
            this.menuItemDataUpdate,
            this.menuItemDataDelete,
            this.menuItemDataSeparator,
            this.menuItemDataShow});
            this.menuStripData.Name = "menuStripData";
            this.menuStripData.Size = new System.Drawing.Size(78, 24);
            this.menuStripData.Text = "Данные";
            // 
            // menuItemDataAdd
            // 
            this.menuItemDataAdd.Name = "menuItemDataAdd";
            this.menuItemDataAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemDataAdd.Size = new System.Drawing.Size(375, 26);
            this.menuItemDataAdd.Text = "Добавить запись...";
            this.menuItemDataAdd.Click += new System.EventHandler(this.menuStripDataAdd_Click);
            // 
            // menuItemDataUpdate
            // 
            this.menuItemDataUpdate.Name = "menuItemDataUpdate";
            this.menuItemDataUpdate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.menuItemDataUpdate.Size = new System.Drawing.Size(375, 26);
            this.menuItemDataUpdate.Text = "Редактировать данные записи...";
            this.menuItemDataUpdate.Click += new System.EventHandler(this.contextMenuStripUpdate_Click);
            // 
            // menuItemDataDelete
            // 
            this.menuItemDataDelete.Name = "menuItemDataDelete";
            this.menuItemDataDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuItemDataDelete.Size = new System.Drawing.Size(375, 26);
            this.menuItemDataDelete.Text = "Удалить запись...";
            this.menuItemDataDelete.Click += new System.EventHandler(this.contextMenuStripDelete_Click);
            // 
            // menuItemDataSeparator
            // 
            this.menuItemDataSeparator.Name = "menuItemDataSeparator";
            this.menuItemDataSeparator.Size = new System.Drawing.Size(372, 6);
            // 
            // menuItemDataShow
            // 
            this.menuItemDataShow.Name = "menuItemDataShow";
            this.menuItemDataShow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemDataShow.Size = new System.Drawing.Size(375, 26);
            this.menuItemDataShow.Text = "Посмотреть научную работу...";
            this.menuItemDataShow.Click += new System.EventHandler(this.contextMenuStripResearchShow_Click);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(3, 43);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(796, 64);
            this.label.TabIndex = 0;
            this.label.Text = "Полный список выбранного элемента:";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.ItemHeight = 20;
            this.listBox.Location = new System.Drawing.Point(3, 110);
            this.listBox.Name = "listBox";
            this.listBox.ScrollAlwaysVisible = true;
            this.listBox.Size = new System.Drawing.Size(796, 340);
            this.listBox.TabIndex = 1;
            this.listBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStripResearchShow,
            this.contextMenuStripUpdate,
            this.contextMenuStripDelete});
            this.contextMenuStrip.Name = "contextMenuStripStudent";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(275, 76);
            // 
            // contextMenuStripResearchShow
            // 
            this.contextMenuStripResearchShow.Name = "contextMenuStripResearchShow";
            this.contextMenuStripResearchShow.Size = new System.Drawing.Size(274, 24);
            this.contextMenuStripResearchShow.Text = "Посмотреть научную работу...";
            this.contextMenuStripResearchShow.Click += new System.EventHandler(this.contextMenuStripResearchShow_Click);
            // 
            // contextMenuStripUpdate
            // 
            this.contextMenuStripUpdate.Name = "contextMenuStripUpdate";
            this.contextMenuStripUpdate.Size = new System.Drawing.Size(274, 24);
            this.contextMenuStripUpdate.Text = "Редактировать данные записи...";
            this.contextMenuStripUpdate.Click += new System.EventHandler(this.contextMenuStripUpdate_Click);
            // 
            // contextMenuStripDelete
            // 
            this.contextMenuStripDelete.Name = "contextMenuStripDelete";
            this.contextMenuStripDelete.Size = new System.Drawing.Size(274, 24);
            this.contextMenuStripDelete.Text = "Удалить запись...";
            this.contextMenuStripDelete.Click += new System.EventHandler(this.contextMenuStripDelete_Click);
            // 
            // FormDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 453);
            this.Controls.Add(this.tableLayoutPanel);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormDataList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Полный список";
            this.Load += new System.EventHandler(this.FormDataList_Load);
            this.Resize += new System.EventHandler(this.FormDataList_Resize);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripResearchShow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripUpdate;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStripDelete;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuStripData;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataShow;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataDelete;
        private System.Windows.Forms.ToolStripMenuItem menuItemDataAdd;
        private System.Windows.Forms.ToolStripSeparator menuItemDataSeparator;
    }
}