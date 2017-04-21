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
using DevExpress.XtraEditors;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Mall.Docs.CardProduct
{
    public partial class ModalCardProductAdd : DevExpress.XtraEditors.XtraForm
    {
        public static string formText = "Добавление - Карточки товаров";
        private DocumentEFContext documentContext;
        private TemplateEFContext templateContext;
        private DictionaryEFContext dictionaryContext;
        public int cardProductId;
        public bool isEdit;
        private bool isEmpty;
        private List<TemplateForDocumentView> templateDV;
        private List<DocumentTable> documentTableList;

        public ModalCardProductAdd()
        {
            InitializeComponent();
            documentContext = new DocumentEFContext();
            templateContext = new TemplateEFContext();
        }

        /// <summary>
        /// Инициализация грида
        /// </summary>
        /// <param name="templateDV"></param>
        private void InitGrid(List<TemplateForDocumentView> templateDV)
        {
            gridView1.Columns["Id"].Visible = gridView1.Columns["DocumentId"].Visible = gridView1.Columns["Document"].Visible = false;
            for (int i = 1; i <= DAL.Data.appCountFields; i++)
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

        /// <summary>
        /// Выбор файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                buttonEdit1.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void simpleButton1_Click(object sender, EventArgs e)
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
                        tmp = await SetTempValueAsync(dt, tmp, cellValues, r, c);
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

            documentContext.context.DocumentTable.AddRange(documentTableList);
            if (documentTableList != null) documentTableList = null;
            isEmpty = false;
        }

        /// <summary>
        /// Объединяем в результиющую ячейку временные данные
        /// </summary>
        /// <param name="documentTable"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="cellValues"></param>
        /// <returns></returns>
        private static PropertyInfo PopulateResultCell(DocumentTable documentTable, PropertyInfo propertyInfo, List<CellDataView> cellValues)
        {
            cellValues.GroupBy(cell => cell.FieldOut).ToList().ForEach(cell =>
            {
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

        /// <summary>
        /// Заполняем данными одну запись - с одной строки
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tmp"></param>
        /// <param name="cellValues"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private async Task<TemplateForDocumentView> SetTempValueAsync(DataTable dt, TemplateForDocumentView tmp, List<CellDataView> cellValues, int r, int c)
        {
            if (tmp != null) tmp = null;
            tmp = templateDV.FirstOrDefault(t => t.FileldIn.ToUpper() == dt.Columns[c].ColumnName.ToUpper());
            if (tmp != null)
            {
                //перевод
                string value = await TranslateAsync(dt.Rows[r].ItemArray.GetValue(c).ToString(), tmp, r, c);

                cellValues.Add(new CellDataView()
                {
                    FieldOut = tmp.FieldOut,
                    Order = tmp.Order,
                    Prefix = tmp.Prefix,
                    //Value = dt.Rows[r].ItemArray.GetValue(c).ToString(),
                    Value = value,
                    Postfix = tmp.Postfix
                });
            }

            return tmp;
        }

        /// <summary>
        /// Перевод ячеек по словам и полностью
        /// </summary>
        /// <param name="original"></param>
        /// <param name="tmp"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private async Task<string> TranslateAsync(string original, TemplateForDocumentView tmp, int r, int c)
        {
            string value = original;
            if (tmp.TranslateByWord && tmp.DictionaryId != null)
            {
                string pattern = "((?:[a-zA-Z]+[-']?)*[a-zA-Z]+)";
                MatchCollection words = Regex.Matches(original, pattern);
                foreach (Match word in words)
                {
                    string translate = await dictionaryContext.GetTranslateAsync(word.ToString(), (int)tmp.DictionaryId);
                    if (!String.IsNullOrEmpty(translate))
                    {
                        value = value.Replace(word.ToString(), translate);
                    }
                }
            }

            if (tmp.Translate && tmp.DictionaryId != null)
            {
                string translate = await dictionaryContext.GetTranslateAsync(original, (int)tmp.DictionaryId);
                if (!String.IsNullOrEmpty(translate))
                {
                    value = value.Replace(original, translate);
                }
            }

            return value;
        }

        /// <summary>
        /// Кнопка Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void simpleButtonSave_Click(object sender, EventArgs e)
        {
            await SaveDataAsync();
        }

        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <returns></returns>
        private async Task SaveDataAsync()
        {
            using (var dbContextTransaction = documentContext.context.Database.BeginTransaction())
            {
                try
                {
                    Document document = new Document()
                    {
                        Id = cardProductId,
                        DateDocument = (DateTime)dateEdit1.EditValue,
                        TemplateId = (int)lookUpEdit1.EditValue
                    };
                    cardProductId = await documentContext.SaveDocumentAsync(document);

                    if (!isEdit)
                    {
                        foreach (var d in documentContext.context.DocumentTable.Local)
                        {
                            d.DocumentId = cardProductId;
                        }
                    }
                    await documentContext.context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    //интерфейс
                    textEdit1.Text = cardProductId.ToString();
                    this.Text = "Карточки товаров №" + textEdit1.Text;
                    isEdit = true;
                    simpleButtonSave.Enabled = false;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        private async void ModalCardProductAdd_Load(object sender, EventArgs e)
        {
            dictionaryContext = new DictionaryEFContext();
            templateBindingSource.DataSource = await templateContext.GetListTemplateAsync();

            await documentContext.context.DocumentTable.Where(t => t.DocumentId == cardProductId).LoadAsync();
            gridControl1.DataSource = documentContext.context.DocumentTable.Local.ToBindingList();

            simpleButtonSave.Enabled = false;
        }

        /// <summary>
        /// Выбор шаблона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //очищаем табличную часть
            if (isEdit || !isEmpty)
            {
                //var itemsToDelete = documentContext.context.Set<DocumentTable>();
                //documentContext.context.DocumentTable.RemoveRange(itemsToDelete);
                documentContext.context.DocumentTable.Local.Clear();
            }

            //пересоздаем грид
            templateDV = await templateContext.GetTemplateForDocumentByIdAsync((int)lookUpEdit1.EditValue);
            if (templateDV.Count() == 0) return;

            await documentContext.context.DocumentTable.Where(t => t.DocumentId == cardProductId).LoadAsync();
            gridControl1.DataSource = documentContext.context.DocumentTable.Local.ToBindingList();
            InitGrid(templateDV);

            simpleButtonSave.Enabled = true;
        }

        /// <summary>
        /// Очистка табличной части
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            documentContext.context.DocumentTable.Local.Clear();
            simpleButtonSave.Enabled = true;
        }

        /// <summary>
        /// Экспорт в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gridControl1.ExportToXls(saveFileDialog1.OpenFile());
            }
        }

        /// <summary>
        /// Поиск перевода слова
        /// </summary>
        /// <param name="original"></param>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        private async Task<string> TranslateWordAsync(string original, int dictionaryId)
        {
            string translate = original;
            using (DictionaryEFContext dictionaryContext = new DictionaryEFContext())
            {
                string entry = await dictionaryContext.GetTranslateAsync(original, dictionaryId);
                translate = entry != null ? entry : translate;
            }
            return translate;
        }

        #region Служебные

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void ModalCardProductAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (simpleButtonSave.Enabled)
            {
                switch (XtraMessageBox.Show("Сохранить документ?", DAL.Data.appName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        await SaveDataAsync();
                        e.Cancel = false;
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void ModalCardProductAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }

        #endregion

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.gridView1.DeleteSelectedRows();
                simpleButtonSave.Enabled = true;
            }
            //if (e.KeyCode == Keys.Return)
            //{
            //    await EditDataAsync();
            //}
        }
    }
}