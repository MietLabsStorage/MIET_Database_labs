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

namespace BD
{
    public partial class AddOrUpdateInCathedraForm : Form
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
        public AddOrUpdateInCathedraForm(OleDbConnection _cn, bool isAdd, fun showFun, fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Add;
            this.isAdd = isAdd;
            if (!isAdd)
            {
                this.Text = "Обновить в Кафедра";
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
                    cmd.CommandText = "INSERT INTO [Выпускающая кафедра] VALUES (@ID, @FullName, @ShortName, @Decan)";

                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@FullName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ShortName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Decan", textBox4.Text);
                }
                else
                {
                    cmd.CommandText = "UPDATE [Выпускающая кафедра] SET " +
                        "Полное_название = @FullName, Короткое_название = @ShortName, Декан = @Decan " +
                        "WHERE Идентификатор_кафедры = @ID";
                    cmd.Parameters.AddWithValue("@FullName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ShortName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Decan", textBox4.Text);
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
