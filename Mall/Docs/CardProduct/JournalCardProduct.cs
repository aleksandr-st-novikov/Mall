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
    }
}