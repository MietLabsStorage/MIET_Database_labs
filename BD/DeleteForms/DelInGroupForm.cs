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
    public partial class DelInGroupForm : Form
    {
        OleDbConnection cn; 
        public delegate void fun();
        public delegate void fun1(string isSucces);
        private fun1 qFun;
        private fun showFun;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="showFun">для вывода в listbox</param>
        /// <param name="qFun">для вывода при ошибке</param>
        public DelInGroupForm(OleDbConnection _cn, fun showFun, fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Delete;
            cn = _cn;
        }

        private void Delete(object sender, EventArgs e)
        {
            cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                cmd.CommandText = "DELETE FROM [Группа] WHERE [Номер_группы]=@ID";
                cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                cmd.ExecuteNonQuery();
                cn.Close();
                this.showFun();
                this.qFun(" ");
                this.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                this.qFun(ex.ToString());
                this.Close();
            }
        }
    }
}
