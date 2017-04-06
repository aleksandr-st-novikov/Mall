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

namespace Mall.Docs.CardProduct
{
    public partial class JournalTemplate : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Шаблоны загрузки - Журнал";
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
                result = modalTemplateAdd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    
                    //await SaveDataAsync(template);
                }
                else
                {
                    return;
                }
            }
        }

    }
}