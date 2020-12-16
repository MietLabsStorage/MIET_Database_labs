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
    public partial class AddOrUpdateInStudent : Form
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
        public AddOrUpdateInStudent(OleDbConnection _cn, bool isAdd, Database.fun showFun, Database.fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Add;
            this.isAdd = isAdd;
            if (!isAdd)
            {
                this.Text = "Обновить в Студент";
                button1.Text = "Обновить";
            }
            cn = _cn;
        }

        public void Add(object sender, EventArgs e)
        {
            try
            {
                string isHost = radioButton1.Checked ? "1" : "0";
                if (isAdd)
                {
                    Database.Add(showFun, qFun, 2, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, isHost, textBox10.Text);
                }
                else
                {
                    Database.Update(showFun, qFun, 2, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, isHost, textBox10.Text);
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
