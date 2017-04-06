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
using System.Data.OleDb;

namespace Mall.Docs.CardProduct
{
    public partial class ModalTemplateAdd : DevExpress.XtraEditors.XtraForm
    {
        private TemplateEFContext templateContext = null;
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

        private async void ModalTemplateAdd_Load(object sender, EventArgs e)
        {
            templateContext = new TemplateEFContext();
            templateContext.context.TemplateTable.Where(t => t.TemplateId == templateId).Load();
            gridControl1.DataSource = templateContext.context.TemplateTable.Local.ToBindingList();

            DataTable dt = GetFileFields();
            List<string> comboData = new List<string>();
            foreach (DataColumn column in dt.Columns)
            {
                comboData.Add(column.ColumnName);
            }
            repositoryItemComboBox1.Items.AddRange(comboData);

            List<SettingDoc> lookupData = await templateContext.context.SettingDoc.Where(s => s.IsActive == true && !String.IsNullOrEmpty(s.Descr)).ToListAsync();
            settingDocBindingSource.DataSource = lookupData;
        }

        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            using (var dbContextTransaction = templateContext.context.Database.BeginTransaction())
            {
                try
                {
                    Template template = new Template()
                    {
                        Name = textEdit1.Text,
                        Commentary = textEdit3.Text
                    };
                    templateId = await templateContext.AddTemplateAsync(template);

                    foreach (var d in templateContext.context.TemplateTable.Local)
                    {
                        d.TemplateId = templateId;
                    }
                    await templateContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        private DataTable GetFileFields()
        {
            try
            {
                // Строка подключения
                string file = @"e:\VS\Mall\Data\Книга1.xls";
                string ConnectionString = "";
                if (System.IO.Path.GetExtension(file).ToUpper() == ".XLS")
                {
                    ConnectionString = String.Format(
                        "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=Yes\";Data Source={0}", file);
                }
                //пока не работает
                else if (System.IO.Path.GetExtension(file).ToUpper() == ".XLSX")
                {
                    ConnectionString = String.Format(
                        "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties =\"Excel 12.0 Xml;HDR=YES\";Data Source={0}", file);
                }

                // Открываем соединение
                DataSet ds = new DataSet("EXCEL");
                OleDbConnection cn = new OleDbConnection(ConnectionString);
                cn.Open();

                DataTable schemaTable =
                        cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                new object[] { null, null, null, "TABLE" });

                // Берем название первого листа
                string sheet1 = (string)schemaTable.Rows[0].ItemArray[2];
                // Выбираем все данные с листа
                string select = String.Format("SELECT * FROM [{0}]", sheet1);
                OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
                ad.Fill(ds);
                DataTable tb = ds.Tables[0];
                return tb;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null;
            }
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}