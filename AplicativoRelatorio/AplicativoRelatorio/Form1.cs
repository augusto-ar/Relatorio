using AplicativoRelatorio.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using AplicativoRelatorio.Type;

namespace AplicativoRelatorio
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {          
            InitializeComponent();          
        }

        public void GeraRelatorio(object sender, System.EventArgs e)
        {
            string msg = "";
            var item = this.comboBox1.SelectedValue;
            RelatorioType.TipoRelatorio tipo = (RelatorioType.TipoRelatorio)item;
            MessageBox.Show(msg);

        }
    }
}
