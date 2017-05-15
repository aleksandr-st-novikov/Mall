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
using DAL.EFContext;
using DAL.Entities;
using System.Data.Entity;

namespace Mall.Directory.General
{
    public partial class ModalDictionary : DevExpress.XtraEditors.XtraForm
    {
        private DictionaryEFContext dictionaryContext;
        public bool isEdit;
        public int dictionaryId;

        public ModalDictionary()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private async void ModalDictionary_FormClosing(object sender, FormClosingEventArgs e)
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

        private async Task SaveDataAsync()
        {
            using (var dbContextTransaction = dictionaryContext.context.Database.BeginTransaction())
            {
                try
                {
                    Dictionary dictionary = new Dictionary()
                    {
                        Id = dictionaryId,
                        Name = textEdit1.Text,
                        Commentary = textEdit3.Text
                    };
                    var tmp = await dictionaryContext.AddOrUpdateAsync(dictionary, dictionary.Id);
                    dictionaryId = tmp == null ? -1 : tmp.Id;

                    if (!isEdit)
                    {
                        foreach (var d in dictionaryContext.context.DictionaryTable.Local)
                        {
                            d.DictionaryId = dictionaryId;
                        }
                    }
                    await dictionaryContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    //интерфейс
                    textEdit2.Text = dictionaryId.ToString();
                    isEdit = true;
                    simpleButtonSave.Enabled = false;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        private async void ModalDictionary_Load(object sender, EventArgs e)
        {
            dictionaryContext = new DictionaryEFContext();
            await dictionaryContext.context.DictionaryTable.Where(t => t.DictionaryId == dictionaryId).LoadAsync();
            dictionaryTableBindingSource.DataSource = dictionaryContext.context.DictionaryTable.Local.ToBindingList();
            if (isEdit) simpleButtonSave.Enabled = false;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["DictionaryId"], dictionaryId);
            view.SetRowCellValue(e.RowHandle, view.Columns["IsTranslate"], true);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            await SaveDataAsync();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }
    }
}