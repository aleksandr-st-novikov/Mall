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
using Mall.Docs.CardProduct;
using DAL.Entities;
using System.Data.Entity;

namespace Mall.Docs.CardProduct
{
    public partial class JournalTemplate : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Шаблоны загрузки - Журнал";
        private int rowHandle;
        private TemplateEFContext templateContext = null;

        public JournalTemplate()
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
            using (var modalTemplateAdd = new ModalTemplateAdd())
            {
                modalTemplateAdd.textEdit2.Text = "-1";
                modalTemplateAdd.templateId = -1;
                modalTemplateAdd.Text = "Создание шаблона";
                modalTemplateAdd.isEdit = false;
                result = modalTemplateAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    await LoadDataAsync();
                }
            }
        }

        private async void JournalTemplate_Load(object sender, EventArgs e)
        {
            templateContext = new TemplateEFContext();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            gridControl1.DataSource = await templateContext.context.Template.OrderBy(t => t.Name).ToListAsync();
        }

        private async void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await EditDataAsync();
        }

        private async Task EditDataAsync()
        {
            rowHandle = this.gridView1.FocusedRowHandle;
            DialogResult result;
            using (var modalTemplateAdd = new ModalTemplateAdd())
            {
                modalTemplateAdd.templateId = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                modalTemplateAdd.textEdit2.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]).ToString();
                modalTemplateAdd.Text = "Редактирование шаблона";
                modalTemplateAdd.isEdit = true;
                modalTemplateAdd.textEdit1.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Name"]).ToString();
                modalTemplateAdd.textEdit3.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Commentary"]).ToString();
                result = modalTemplateAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    templateContext = new TemplateEFContext();
                    await LoadDataAsync();
                    templateContext.Dispose();
                }
            }
            this.gridView1.FocusedRowHandle = rowHandle;
        }

        private void JournalTemplate_FormClosed(object sender, FormClosedEventArgs e)
        {
            templateContext.Dispose();
        }

        private async void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await LoadDataAsync();
        }

        private async void JournalTemplate_KeyDown(object sender, KeyEventArgs e)
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

        private async void JournalTemplate_Activated(object sender, EventArgs e)
        {
            await LoadDataAsync();
            if (rowHandle != 0)
            {
                this.gridView1.FocusedRowHandle = rowHandle;
            }
            else
            {
                this.gridControl1.ForceInitialize();
            }
        }

        private async void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await DeleteDataAsync();
        }

        private async Task DeleteDataAsync()
        {
            if (XtraMessageBox.Show("Удалить шаблон " + gridView1.GetFocusedRowCellValue("Name").ToString() + "?", DAL.Data.appName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (this.gridView1.FocusedRowHandle < 0) return;
                rowHandle = gridView1.FocusedRowHandle - 1;

                using (var dbContextTransaction = templateContext.context.Database.BeginTransaction())
                {
                    try
                    {
                        int id = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                        await templateContext.DeleteTemplateAsync(id);
                        await templateContext.context.SaveChangesAsync();
                        dbContextTransaction.Commit();
                        await LoadDataAsync();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                gridView1.FocusedRowHandle = rowHandle;
            }
        }

        private async void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            await EditDataAsync();
        }
    }
}