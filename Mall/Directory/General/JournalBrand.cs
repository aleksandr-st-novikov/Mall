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

namespace Mall.Directory.General
{
    public partial class JournalBrand : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Бренды - Справочники";
        private BrandEFContext brandContext;

        public JournalBrand()
        {
            InitializeComponent();
        }

        private async void JournalBrand_Load(object sender, EventArgs e)
        {
            brandContext = new BrandEFContext();
            await brandContext.context.Brand.LoadAsync();
            gridControl1.DataSource = brandContext.context.Brand.Local.ToBindingList();
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private async void barLargeButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var dbContextTransaction = brandContext.context.Database.BeginTransaction())
            {
                try
                {
                    await brandContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}