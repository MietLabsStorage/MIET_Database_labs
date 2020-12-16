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
    public partial class GroupsFromCath : Form
    {
        private OleDbConnection cn;
        private Database.fun3 showFun;
        private Database.fun1 qFun;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="isAdd">true if add, false if update</param>
        /// <param name="showFun">обновлять Listbox</param>
        /// <param name="qFun">вызывается при ошибке</param>
        public GroupsFromCath(OleDbConnection _cn, Database.fun3 showFun, Database.fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Query;
            cn = _cn;
        }

        private void Query(object sender, EventArgs e)
        {
            try
            {
                Database.QueriesWithParams(showFun, qFun, 1, textBox1.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                this.qFun(ex.ToString());
            }

        }
    }
}
