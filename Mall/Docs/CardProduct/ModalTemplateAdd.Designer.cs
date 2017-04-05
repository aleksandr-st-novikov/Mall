namespace Mall.Docs.CardProduct
{
    partial class ModalTemplateAdd
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bindingSourceTemplate = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFieldIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFieldOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrefix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPostfix = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTranslate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.textEdit3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.textEdit2);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(799, 61);
            this.panelControl1.TabIndex = 0;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(88, 32);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(365, 20);
            this.textEdit3.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Комментарий";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(395, 9);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(58, 20);
            this.textEdit2.TabIndex = 2;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(88, 9);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(301, 20);
            this.textEdit1.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Название";
            // 
            // bindingSourceTemplate
            // 
            this.bindingSourceTemplate.DataSource = typeof(DAL.Entities.TemplateTable);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButtonSave);
            this.panelControl2.Controls.Add(this.simpleButtonClose);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 397);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(799, 39);
            this.panelControl2.TabIndex = 2;
            // 
            // simpleButtonSave
            // 
            this.simpleButtonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButtonSave.Location = new System.Drawing.Point(634, 9);
            this.simpleButtonSave.Name = "simpleButtonSave";
            this.simpleButtonSave.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSave.TabIndex = 1;
            this.simpleButtonSave.Text = "Сохранить";
            this.simpleButtonSave.Click += new System.EventHandler(this.simpleButtonSave_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonClose.Location = new System.Drawing.Point(715, 9);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonClose.TabIndex = 0;
            this.simpleButtonClose.Text = "Закрыть";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSourceTemplate;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 61);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(799, 336);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colTemplateId,
            this.colFieldIn,
            this.colFieldOut,
            this.colOrder,
            this.colPrefix,
            this.colPostfix,
            this.colTranslate,
            this.colTemplate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            // 
            // colTemplateId
            // 
            this.colTemplateId.FieldName = "TemplateId";
            this.colTemplateId.Name = "colTemplateId";
            this.colTemplateId.Visible = true;
            this.colTemplateId.VisibleIndex = 1;
            // 
            // colFieldIn
            // 
            this.colFieldIn.FieldName = "FieldIn";
            this.colFieldIn.Name = "colFieldIn";
            this.colFieldIn.Visible = true;
            this.colFieldIn.VisibleIndex = 2;
            // 
            // colFieldOut
            // 
            this.colFieldOut.FieldName = "FieldOut";
            this.colFieldOut.Name = "colFieldOut";
            this.colFieldOut.Visible = true;
            this.colFieldOut.VisibleIndex = 3;
            // 
            // colOrder
            // 
            this.colOrder.FieldName = "Order";
            this.colOrder.Name = "colOrder";
            this.colOrder.Visible = true;
            this.colOrder.VisibleIndex = 4;
            // 
            // colPrefix
            // 
            this.colPrefix.FieldName = "Prefix";
            this.colPrefix.Name = "colPrefix";
            this.colPrefix.Visible = true;
            this.colPrefix.VisibleIndex = 5;
            // 
            // colPostfix
            // 
            this.colPostfix.FieldName = "Postfix";
            this.colPostfix.Name = "colPostfix";
            this.colPostfix.Visible = true;
            this.colPostfix.VisibleIndex = 6;
            // 
            // colTranslate
            // 
            this.colTranslate.FieldName = "Translate";
            this.colTranslate.Name = "colTranslate";
            this.colTranslate.Visible = true;
            this.colTranslate.VisibleIndex = 7;
            // 
            // colTemplate
            // 
            this.colTemplate.FieldName = "Template";
            this.colTemplate.Name = "colTemplate";
            // 
            // ModalTemplateAdd
            // 
            this.AcceptButton = this.simpleButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButtonClose;
            this.ClientSize = new System.Drawing.Size(799, 436);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModalTemplateAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModalTemplateAdd";
            this.Load += new System.EventHandler(this.ModalTemplateAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.BindingSource bindingSourceTemplate;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSave;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateId;
        private DevExpress.XtraGrid.Columns.GridColumn colFieldIn;
        private DevExpress.XtraGrid.Columns.GridColumn colFieldOut;
        private DevExpress.XtraGrid.Columns.GridColumn colOrder;
        private DevExpress.XtraGrid.Columns.GridColumn colPrefix;
        private DevExpress.XtraGrid.Columns.GridColumn colPostfix;
        private DevExpress.XtraGrid.Columns.GridColumn colTranslate;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplate;
        public DevExpress.XtraEditors.TextEdit textEdit2;
        public DevExpress.XtraEditors.TextEdit textEdit1;
        public DevExpress.XtraEditors.TextEdit textEdit3;
    }
}