using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD.DeleteForms
{
    public partial class DelSemestrForm : Form
    {
        OleDbConnection cn;
        private Database.fun showFun;
        private Database.fun1 qFun;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="showFun">для вывода в listbox</param>
        /// <param name="qFun">для вывода при ошибке</param>
        public DelSemestrForm(OleDbConnection _cn, Database.fun showFun, Database.fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Delete;
            cn = _cn;
        }

        private void Delete(object sender, EventArgs e)
        {
            try
            {
                Database.Delete(showFun, qFun, 4, textBox1.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                qFun(ex.Message);
            }
        }

    }
}
