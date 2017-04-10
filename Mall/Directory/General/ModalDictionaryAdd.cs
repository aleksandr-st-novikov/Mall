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

namespace Mall.Directory.General
{
    public partial class ModalDictionaryAdd : DevExpress.XtraEditors.XtraForm
    {
        public ModalDictionaryAdd()
        {
            InitializeComponent();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
            if(checkEdit1.Checked)
            {
                textEdit2.Enabled = false;
            }
            else
            {
                textEdit2.Enabled = true;
            }
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            simpleButtonSave.Enabled = true;
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }
    }
}