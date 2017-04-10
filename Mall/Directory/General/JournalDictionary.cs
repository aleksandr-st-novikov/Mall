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
            await BindingData();
        }

        private async void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rowHandle = this.gridView1.FocusedRowHandle;
            dictionaryContext.Dispose();
            await BindingData();
            this.gridView1.FocusedRowHandle = rowHandle;
        }

        private async Task BindingData()
        {
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
                modalDictionary.textEdit1.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Name"]).ToString();
                result = modalDictionary.ShowDialog();
                await ReloadEntryAsync(modalDictionary.dictionaryId);
            }
            this.gridView1.FocusedRowHandle = rowHandle;
        }
    }
}