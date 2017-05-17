using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;
using DAL.EFContext;
using DAL.Entities;
using System.Data.OleDb;
using Mall.Helpers;

namespace Mall.Docs.CardProduct
{
    public partial class ModalTemplateAdd : DevExpress.XtraEditors.XtraForm
    {
        private TemplateEFContext templateContext;
        private DictionaryEFContext dictionaryContext;
        public int templateId;
        public bool isEdit;

        public ModalTemplateAdd()
        {
            InitializeComponent();
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["TemplateId"], int.Parse(textEdit2.Text));
            view.SetRowCellValue(e.RowHandle, view.Columns["Order"], 1);
        }

        private async void ModalTemplateAdd_Load(object sender, EventArgs e)
        {
            templateContext = new TemplateEFContext();
            await templateContext.context.TemplateTable.Where(t => t.TemplateId == templateId).LoadAsync();
            gridControl1.DataSource = templateContext.context.TemplateTable.Local.ToBindingList();

            List<SettingDoc> lookupData = await templateContext.context.SettingDoc.Where(s => s.IsActive == true && !String.IsNullOrEmpty(s.Descr)).ToListAsync();
            settingDocBindingSource.DataSource = lookupData;

            if(isEdit)
            {
                gridControl1.Enabled = true;
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                simpleButtonSave.Enabled = false;
            }

            dictionaryContext = new DictionaryEFContext();
            await dictionaryContext.context.Dictionary.OrderBy(t => t.Name).LoadAsync();
            dictionaryBindingSource.DataSource = dictionaryContext.context.Dictionary.Local.ToBindingList();
        }

        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            await SaveDataAsync();
        }

        private async Task SaveDataAsync()
        {
            using (var dbContextTransaction = templateContext.context.Database.BeginTransaction())
            {
                try
                {
                    Template template = new Template()
                    {
                        Id = templateId,
                        Name = textEdit1.Text,
                        Commentary = textEdit3.Text
                    };
                    var tmp = await templateContext.AddOrUpdateAsync(template, template.Id);
                    templateId = tmp == null ? -1 : tmp.Id;

                    if (!isEdit)
                    {
                        foreach (var d in templateContext.context.TemplateTable.Local)
                        {
                            d.TemplateId = templateId;
                        }
                    }
                    await templateContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    //интерфейс
                    textEdit2.Text = templateId.ToString();
                    isEdit = true;
                    simpleButtonSave.Enabled = false;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit1.Text = openFileDialog1.FileName;

                DataTable dt = IO.GetFileFields(buttonEdit1.Text);
                List<string> comboData = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    comboData.Add(column.ColumnName);
                }
                repositoryItemComboBox1.Items.AddRange(comboData);

                gridControl1.Enabled = true;
                gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private async void ModalTemplateAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (simpleButtonSave.Enabled)
            {
                switch (XtraMessageBox.Show("Сохранить документ?", DAL.Data.appName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        await SaveDataAsync();
                        e.Cancel = false;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                switch (XtraMessageBox.Show("Удалить запись?", DAL.Data.appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        this.gridView1.DeleteSelectedRows();
                        simpleButtonSave.Enabled = true;
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

    }
}