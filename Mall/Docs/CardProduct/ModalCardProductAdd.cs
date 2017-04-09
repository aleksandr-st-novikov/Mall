using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DAL.EFContext;
using DAL.Entities;
using DAL.ViewModel;
using System.Data.Entity;
using Mall.Helpers;
using System.Reflection;

namespace Mall.Docs.CardProduct
{
    public partial class ModalCardProductAdd : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Добавление - Карточки товаров";
        private MallDBContext context = null;
        public int cardProductId;
        public bool isEdit;
        private List<TemplateForDocumentView> templateDV = null;
        private List<DocumentTable> documentTableList = null;

        public ModalCardProductAdd()
        {
            InitializeComponent();
            context = new MallDBContext();
        }

        private async void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //очищаем табличную часть


            //пересоздаем грид
            using (TemplateEFContext templateContext = new TemplateEFContext())
            {
                templateDV = await templateContext.GetTemplateForDocumentByNameAsync(comboBoxEdit1.Text);
                if (templateDV.Count() == 0)
                    return;
            }
            await context.DocumentTable.Where(t => t.DocumentId == cardProductId).LoadAsync();
            gridControl1.DataSource = context.DocumentTable.Local.ToBindingList();
            InitGrid(templateDV);
        }

        private void InitGrid(List<TemplateForDocumentView> templateDV)
        {
            gridView1.Columns["Id"].Visible = gridView1.Columns["DocumentId"].Visible = gridView1.Columns["Document"].Visible = false;
            for (int i = 1; i <= 10; i++)
            {
                string field = "F" + i.ToString().PadLeft(3, '0');
                var tmpl = templateDV.FirstOrDefault(t => t.FieldOut.ToUpper() == field);
                if (tmpl == null)
                {
                    gridView1.Columns[field].Visible = false;
                    continue;
                }
                gridView1.Columns[field].Visible = true;
                gridView1.Columns[field].Caption = tmpl.FieldOutName;
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit1.Text = openFileDialog1.FileName;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = IO.GetFileFields(buttonEdit1.Text);
            DocumentTable documentTable = null;
            TemplateForDocumentView tmp = null;
            PropertyInfo propertyInfo = null;
            List<CellDataView> cellValues = null;
            documentTableList = new List<DocumentTable>();
            for (int r = 0; r <= dt.Rows.Count - 1; r++)
            {
                if (cellValues != null) cellValues = null;
                cellValues = new List<CellDataView>();

                if (documentTable != null) documentTable = null;
                documentTable = new DocumentTable()
                {
                    DocumentId = cardProductId
                };
                for (int c = 0; c <= dt.Columns.Count - 1; c++)
                {
                    if (templateDV.FirstOrDefault(t => t.FileldIn == dt.Columns[c].ColumnName) != null)
                    {
                        //сначала собираем все нужные значения по отдельным полям
                        tmp = SetTempValue(dt, tmp, cellValues, r, c);
                    }
                }
                propertyInfo = PopulateResultCell(documentTable, propertyInfo, cellValues);
                if (documentTableList.FirstOrDefault(d => d.Document == documentTable.Document
                     && d.F001 == documentTable.F001
                     && d.F002 == documentTable.F002
                     && d.F003 == documentTable.F003
                     && d.F004 == documentTable.F004
                     && d.F005 == documentTable.F005
                     && d.F006 == documentTable.F006
                     && d.F007 == documentTable.F007
                     && d.F008 == documentTable.F008
                     && d.F009 == documentTable.F009
                     && d.F010 == documentTable.F010) == null)
                {
                    documentTableList.Add(documentTable);
                }
            }

            //gridControl1.DataSource = context.DocumentTable.Local.ToBindingList();
            gridControl1.DataSource = documentTableList.ToList();
            if (documentTableList != null) documentTableList = null;

            //gridControl1.Enabled = true;
            //gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }

        private static PropertyInfo PopulateResultCell(DocumentTable documentTable, PropertyInfo propertyInfo, List<CellDataView> cellValues)
        {
            cellValues.GroupBy(cell => cell.FieldOut).ToList().ForEach(cell =>
            {
                //переводим

                //объединяем в результирующие поля
                if (propertyInfo != null) propertyInfo = null;
                propertyInfo = documentTable.GetType().GetProperty(cell.Key.ToUpper());
                string value = "";
                foreach (var v in cell.OrderBy(cl => cl.Order))
                {
                    value += v.Prefix + v.Value + v.Postfix;
                }
                propertyInfo.SetValue(documentTable, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            });
            return propertyInfo;
        }

        private TemplateForDocumentView SetTempValue(DataTable dt, TemplateForDocumentView tmp, List<CellDataView> cellValues, int r, int c)
        {
            if (tmp != null) tmp = null;
            tmp = templateDV.FirstOrDefault(t => t.FileldIn.ToUpper() == dt.Columns[c].ColumnName.ToUpper());
            if (tmp != null)
            {
                cellValues.Add(new CellDataView()
                {
                    FieldOut = tmp.FieldOut,
                    Order = tmp.Order,
                    Prefix = tmp.Prefix,
                    Value = dt.Rows[r].ItemArray.GetValue(c).ToString(),
                    Postfix = tmp.Postfix
                });
            }

            return tmp;
        }

        private void ModalCardProductAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
    }
}