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

namespace BD.QueriesForms
{
    public partial class SubjWithMoreHours : Form
    {
        private OleDbConnection cn;
        public delegate void fun(OleDbCommand cmd);
        private fun showFun;
        public delegate void fun1(string isSucces);
        private fun1 qFun;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="isAdd">true if add, false if update</param>
        /// <param name="showFun">обновлять Listbox</param>
        /// <param name="qFun">вызывается при ошибке</param>
        public SubjWithMoreHours(OleDbConnection _cn, fun showFun, fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Query;
            cn = _cn;
        }

        private void Query(object sender, EventArgs e)
        {
            cn.Open();
            OleDbCommand cmd = new OleDbCommand();
            try
            {

                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("[Кол-во часов]", textBox1.Text);
                cmd.CommandText = "SELECT * FROM ПредметыБольшеСтолькиЧасов";
                cmd.ExecuteNonQuery();
                cn.Close();
                this.showFun(cmd);
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
