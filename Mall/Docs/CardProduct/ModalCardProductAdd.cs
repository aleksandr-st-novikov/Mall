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
using DAL.ViewModel;

namespace Mall.Docs.CardProduct
{
    public partial class ModalCardProductAdd : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Добавление - Карточки товаров";

        public ModalCardProductAdd()
        {
            InitializeComponent();
        }

        private async void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //очищаем табличную часть


            //пересоздаем грид
            List<TemplateForDocumentView> templateDV = null;
            using (TemplateEFContext templateContext = new TemplateEFContext())
            {
                templateDV = await templateContext.GetTemplateForDocumentByNameAsync(comboBoxEdit1.Text);
                if (templateDV.Count() == 0)
                    return;
            }
            
        }
    }
}