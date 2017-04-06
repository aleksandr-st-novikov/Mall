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

namespace Mall.Docs.CardProduct
{
    public partial class ModalSettingDoc : DevExpress.XtraEditors.XtraForm
    {
        private SettingDocEFContext settingDocContext = null;
        public ModalSettingDoc()
        {
            InitializeComponent();
        }

        private async void ModalSettingDoc_Load(object sender, EventArgs e)
        {
            settingDocContext = new SettingDocEFContext();
            using (var dbContextTransaction = settingDocContext.context.Database.BeginTransaction())
            {
                try
                {
                    await settingDocContext.PopulateFiledsAsync(10);
                    await settingDocContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    XtraMessageBox.Show(ex.Message);
                }
            }

            settingDocContext.context.SettingDoc.Load();
            gridControl1.DataSource = settingDocContext.context.SettingDoc.Local.ToBindingList();
        }

        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            using (var dbContextTransaction = settingDocContext.context.Database.BeginTransaction())
            {
                try
                {
                    await settingDocContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }
    }
}