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
    public partial class AddOrUpdateSemestrForm : Form
    {
        private OleDbConnection cn;
        public delegate void fun();
        private fun showFun;
        public delegate void fun1(string isSucces);
        private fun1 qFun;
        private bool isAdd;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cn"></param>
        /// <param name="isAdd">true if add, false if update</param>
        /// <param name="showFun">обновлять Listbox</param>
        /// <param name="qFun">вызывается при ошибке</param>
        public AddOrUpdateSemestrForm(OleDbConnection _cn, bool isAdd, fun showFun, fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Add;
            this.isAdd = isAdd;
            if (!isAdd)
            {
                this.Text = "Обновить в Семестр";
                button1.Text = "Обновить";
            }
            cn = _cn;
        }

        public void Add(object sender, EventArgs e)
        {
            cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                if (isAdd)
                {
                    cmd.CommandText =
                    "INSERT INTO [Семестр] VALUES (@ID, @Begin, @End, @Time)";

                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Begin", textBox2.Text);
                    cmd.Parameters.AddWithValue("@End", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Time", textBox4.Text);
                }
                else
                {
                    cmd.CommandText = "UPDATE Семестр SET " +
                        "Начало = @Begin, Конец = @End, Количество_недель = @Time" +
                        "WHERE Номер_семестра LIKE @ID";
                    cmd.Parameters.AddWithValue("@Begin", textBox2.Text);
                    cmd.Parameters.AddWithValue("@End", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Time", textBox4.Text);
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);

                }
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
