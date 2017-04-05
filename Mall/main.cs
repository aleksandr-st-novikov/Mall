using DevExpress.XtraEditors;
using Mall.Docs.CardProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mall
{
    public partial class main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("Выйти из программы?", DAL.Data.appName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!childWindowIsOpen(JournalCardProduct.formText))
            {
                JournalCardProduct newMDIChild = new JournalCardProduct();
                newMDIChild.MdiParent = this;
                newMDIChild.Text = JournalCardProduct.formText;
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!childWindowIsOpen(JournalTemplate.formText))
            {
                JournalTemplate newMDIChild = new JournalTemplate();
                newMDIChild.MdiParent = this;
                newMDIChild.Text = JournalTemplate.formText;
                newMDIChild.Show();
            }
        }
    }
}
