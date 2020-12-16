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

namespace BD.AddForms
{
    public partial class AddOrUpdateSubjectForm : Form
    {
        private OleDbConnection cn;
        private Database.fun showFun;
        private Database.fun1 qFun;
        private bool isAdd;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="isAdd">true if add, false if update</param>
        /// <param name="showFun">обновлять Listbox</param>
        /// <param name="qFun">вызывается при ошибке</param>
        public AddOrUpdateSubjectForm(OleDbConnection _cn, bool isAdd, Database.fun showFun, Database.fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Add;
            this.isAdd = isAdd;
            if (!isAdd)
            {
                this.Text = "Обновить в Предмет";
                button1.Text = "Обновить";
            }
            cn = _cn;
        }

        public void Add(object sender, EventArgs e)
        {
            try
            {
                if (isAdd)
                {
                    Database.Add(showFun, qFun, 5, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                }
                else
                {
                    Database.Update(showFun, qFun, 5, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                qFun(ex.Message);
            }

        }

    }
}
