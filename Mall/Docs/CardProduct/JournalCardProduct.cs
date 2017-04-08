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

namespace Mall.Docs.CardProduct
{
    public partial class JournalCardProduct : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Карточки товаров - Журнал";

        public JournalCardProduct()
        {
            InitializeComponent();
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private async void barLargeButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!childWindowIsOpen(ModalCardProductAdd.formText))
            {
                ModalCardProductAdd newMDIChild = new ModalCardProductAdd();
                newMDIChild.MdiParent = this.MdiParent;
                newMDIChild.Text = ModalCardProductAdd.formText;

                //заполняем шаблонами
                using (TemplateEFContext templateContext = new TemplateEFContext())
                {
                    newMDIChild.comboBoxEdit1.Properties.Items.AddRange(await templateContext.GetListTemplateNameAsync());
                }
                newMDIChild.dateEdit1.DateTime = DateTime.Now;
                newMDIChild.textEdit1.Text = "-1";
                newMDIChild.cardProductId = -1;
                newMDIChild.isEdit = false;
                newMDIChild.Show();
            }
        }

        private bool childWindowIsOpen(string formText)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                if (MdiChildren[i].Text == formText)
                {
                    MdiChildren[i].Activate();
                    return true;
                }
            }
            return false;
        }
    }
}