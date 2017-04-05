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
using System.Data.Entity;
using DAL.EFContext;
using DAL.Entities;

namespace Mall.Docs.CardProduct
{
    public partial class ModalTemplateAdd : DevExpress.XtraEditors.XtraForm
    {
        private DAL.Entities.MallDBContext dbContextSpec = new DAL.Entities.MallDBContext();
        public int templateId;

        public ModalTemplateAdd()
        {
            InitializeComponent();
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["TemplateId"], int.Parse(textEdit2.Text));
        }

        private void ModalTemplateAdd_Load(object sender, EventArgs e)
        {
            dbContextSpec.TemplateTable.Where(t => t.TemplateId == templateId).Load();
            gridControl1.DataSource = dbContextSpec.TemplateTable.Local.ToBindingList();
        }

        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            using (TemplateEFContext context = new TemplateEFContext())
            {
                Template template = new Template()
                {
                    Name = textEdit1.Text,
                    Commentary = textEdit3.Text
                };
                templateId = await context.AddTemplateAsync(template);
            }

            foreach (var d in dbContextSpec.TemplateTable.Local)
            {
                d.TemplateId = templateId;
            }
            await dbContextSpec.SaveChangesAsync();
        }
    }
}