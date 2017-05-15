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
using DevExpress.XtraSplashScreen;

namespace Mall.Docs.CardProduct
{
    public partial class JournalCardProduct : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Карточки товаров - Журнал";
        private DocumentEFContext documentContext;
        private TemplateEFContext templateContext;
        private int rowHandle = 0;

        public JournalCardProduct()
        {
            InitializeComponent();
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barLargeButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddData();
        }

        private void AddData()
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(WaitFormMain));
                SplashScreenManager.Default.SetWaitFormDescription("создание документа...");

                if (!childWindowIsOpen(ModalCardProductAdd.formText))
                {
                    ModalCardProductAdd newMDIChild = new ModalCardProductAdd();
                    newMDIChild.MdiParent = this.MdiParent;
                    newMDIChild.Text = ModalCardProductAdd.formText;

                    newMDIChild.dateEdit1.DateTime = DateTime.Now;
                    newMDIChild.textEdit1.Text = "-1";
                    newMDIChild.cardProductId = -1;
                    newMDIChild.isEdit = false;
                    newMDIChild.Show();
                }
            }
            finally
            {
                SplashScreenManager.CloseForm();
            }
        }

        private bool childWindowIsOpen(string formText)
        {
            for (int i = 0; i < this.MdiParent.MdiChildren.Count(); i++)
            {
                if (this.MdiParent.MdiChildren[i].Text == formText)
                {
                    this.MdiParent.MdiChildren[i].Activate();
                    return true;
                }
            }
            return false;
        }

        private async void JournalCardProduct_Load(object sender, EventArgs e)
        {
            barEditItem1.EditValue = new DateTime(DateTime.Now.AddDays(-15).Year, DateTime.Now.AddDays(-15).Month, DateTime.Now.AddDays(-15).Day, 0, 0, 0);
            barEditItem2.EditValue = new DateTime(DateTime.Now.AddDays(15).Year, DateTime.Now.AddDays(15).Month, DateTime.Now.AddDays(15).Day, 0, 0, 0);

            documentContext = new DocumentEFContext();
            await documentContext.context.Document.Where(d => d.DateDocument >= (DateTime)barEditItem1.EditValue
                && d.DateDocument <= (DateTime)barEditItem2.EditValue).LoadAsync();
            documentBindingSource.DataSource = documentContext.context.Document.Local.ToBindingList();

            templateContext = new TemplateEFContext();
            await templateContext.context.Template.LoadAsync();
            templateBindingSource.DataSource = templateContext.context.Template.Local.ToBindingList();
        }

        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.gridView1.FocusedRowHandle < 0) return;

                SplashScreenManager.ShowForm(typeof(WaitFormMain));
                SplashScreenManager.Default.SetWaitFormDescription("открытие документа...");

                EditData();
            }
            finally
            {
                SplashScreenManager.CloseForm();
            }
        }

        private void EditData()
        {
            if (!childWindowIsOpen("Карточки товаров №" + this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]).ToString()))
            {
                rowHandle = this.gridView1.FocusedRowHandle;

                ModalCardProductAdd newMDIChild = new ModalCardProductAdd();
                newMDIChild.MdiParent = this.MdiParent;
                newMDIChild.Text = "Карточки товаров №" + this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]).ToString();

                newMDIChild.dateEdit1.DateTime = (DateTime)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["DateDocument"]);
                newMDIChild.textEdit1.Text = this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]).ToString();
                newMDIChild.cardProductId = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                newMDIChild.lookUpEdit1.EditValue = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["TemplateId"]);
                newMDIChild.isEdit = true;
                newMDIChild.Show();
            }
        }

        private async void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await DeleteDataAsync();

        }

        private async Task DeleteDataAsync()
        {
            if (XtraMessageBox.Show("Удалить Карточки товаров №" + gridView1.GetFocusedRowCellValue("Id").ToString() + "?", DAL.Data.appName,
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (this.gridView1.FocusedRowHandle < 0) return;
                rowHandle = gridView1.FocusedRowHandle - 1;

                using (var dbContextTransaction = documentContext.context.Database.BeginTransaction())
                {
                    try
                    {
                        SplashScreenManager.ShowForm(typeof(WaitFormMain));
                        SplashScreenManager.Default.SetWaitFormDescription("удаление документа...");

                        int id = (int)this.gridView1.GetFocusedRowCellValue(this.gridView1.Columns["Id"]);
                        await documentContext.DeleteByIdAsync(id);
                        await documentContext.context.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                    finally
                    {
                        SplashScreenManager.CloseForm();
                    }
                }
                gridView1.FocusedRowHandle = rowHandle;
            }
        }

        private async void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                await DeleteDataAsync();
            }
            if (e.KeyCode == Keys.Return)
            {
                EditData();
            }
            if (e.KeyCode == Keys.Insert)
            {
                AddData();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        private async void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await RefreshData();
        }

        private async Task RefreshData()
        {
            if (documentContext != null)
            {
                documentContext = new DocumentEFContext();
            }
            await documentContext.context.Document.Where(d => d.DateDocument >= (DateTime)barEditItem1.EditValue
                && d.DateDocument <= (DateTime)barEditItem2.EditValue).LoadAsync();
            documentBindingSource.DataSource = documentContext.context.Document.Local.ToBindingList();
        }

        private async void JournalCardProduct_Activated(object sender, EventArgs e)
        {
            await RefreshData();
        }

    }
}