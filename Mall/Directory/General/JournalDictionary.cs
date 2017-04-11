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
using System.Data.Entity;
using DAL.Entities;

namespace Mall.Directory.General
{
    public partial class JournalDictionary : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Словари - Журнал";
        private DictionaryEFContext dictionaryContext;
        private int rowHandle;

        public JournalDictionary()
        {
            InitializeComponent();
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private async void barLargeButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result;
            using (var modalDictionary = new ModalDictionary())
            {
                modalDictionary.textEdit2.Text = "-1";
                modalDictionary.dictionaryId = -1;
                modalDictionary.isEdit = false;
                result = modalDictionary.ShowDialog();
                await ReloadEntryAsync(modalDictionary.dictionaryId);
            }
        }

        private async void JournalDictionary_Load(object sender, EventArgs e)
        {
            await BindingDataAsync();
        }

        private async void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rowHandle = this.gridView1.FocusedRowHandle;
            dictionaryContext.Dispose();
            await BindingDataAsync();
            this.gridView1.FocusedRowHandle = rowHandle;
        }

        private async Task BindingDataAsync()
        {
            if (dictionaryContext != null) dictionaryContext.Dispose();
            dictionaryContext = new DictionaryEFContext();
            await dictionaryContext.context.Dictionary.OrderBy(d => d.Name).LoadAsync();
            dictionaryBindingSource.DataSource = dictionaryContext.context.Dictionary.Local.ToBindingList();
        }

        private async Task ReloadEntryAsync(int dictionaryId)
        {
            Dictionary entry = await dictionaryContext.GetDictionaryByIdAsync(dictionaryId);
            await dictionaryContext.context.Entry(entry).ReloadAsync();
        }
  
        private async void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await EditDataAsync();
        }

        private async Task EditDataAsync()
        {
            rowHandle = this.gridView1.FocusedRowHandle;
            DialogResult result;
            using (var modalDictionary = new ModalDictionary())
            {
                modalDictionary.dictionaryId = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                modalDictionary.textEdit2.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]).ToString();
                modalDictionary.isEdit = true;
                modalDictionary.textEdit1.Text = (string)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Name"]);
                modalDictionary.textEdit3.Text = (string)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Commentary"]);
                result = modalDictionary.ShowDialog();
                await ReloadEntryAsync(modalDictionary.dictionaryId);
            }
            this.gridView1.FocusedRowHandle = rowHandle;
        }

        private async void JournalDictionary_Activated(object sender, EventArgs e)
        {
            await BindingDataAsync();
            if (rowHandle != 0)
            {
                this.gridView1.FocusedRowHandle = rowHandle;
            }
            else
            {
                this.gridControl1.ForceInitialize();
            }
        }

        private async void JournalDictionary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                await DeleteDataAsync();
            }
            if (e.KeyCode == Keys.Return)
            {
                await EditDataAsync();
            }
        }

        private async Task DeleteDataAsync()
        {
            if (XtraMessageBox.Show("Удалить словарь " + gridView1.GetFocusedRowCellValue("Name").ToString() + "?", DAL.Data.appName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (this.gridView1.FocusedRowHandle < 0) return;
                rowHandle = gridView1.FocusedRowHandle - 1;

                using (var dbContextTransaction = dictionaryContext.context.Database.BeginTransaction())
                {
                    try
                    {
                        int id = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                        await dictionaryContext.DeleteDictionaryAsync(id);
                        await dictionaryContext.context.SaveChangesAsync();
                        dbContextTransaction.Commit();
                        await BindingDataAsync();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                gridView1.FocusedRowHandle = rowHandle;
            }
        }

        private async void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await DeleteDataAsync();
        }

        private async void gridView1_DoubleClick(object sender, EventArgs e)
        {
            await EditDataAsync();
        }
    }
}