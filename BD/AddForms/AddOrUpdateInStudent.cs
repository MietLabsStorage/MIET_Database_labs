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
        public AddOrUpdateInStudent(OleDbConnection _cn, bool isAdd, fun showFun, fun1 qFun)
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
            cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = cn;
                if (isAdd)
                {
                    cmd.CommandText = "INSERT INTO [Студент] VALUES (@p1, @p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";

                    cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                    cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                    cmd.Parameters.AddWithValue("@p4", textBox4.Text);
                    cmd.Parameters.AddWithValue("@p5", textBox5.Text);
                    cmd.Parameters.AddWithValue("@p6", textBox6.Text);
                    cmd.Parameters.AddWithValue("@p7", textBox7.Text);
                    cmd.Parameters.AddWithValue("@p8", textBox8.Text);
                    cmd.Parameters.AddWithValue("@p9", textBox9.Text);
                    cmd.Parameters.AddWithValue("@p10", textBox10.Text);
                }
                else
                {
                    cmd.CommandText = "UPDATE Группа SET " +
                        "Номер_группы = @p2, Фамилия = @p3, Имя = @p4, Отчество = @p5, Дата_Рождения = @p6, Пол = @p7, Регион = @p8, Проживание в общежитии = @p9, Школа = @p10" +
                        "WHERE Номер_студенческого_билета LIKE @p1";
                    cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                    cmd.Parameters.AddWithValue("@p4", textBox4.Text);
                    cmd.Parameters.AddWithValue("@p5", textBox5.Text);
                    cmd.Parameters.AddWithValue("@p6", textBox6.Text);
                    cmd.Parameters.AddWithValue("@p7", textBox7.Text);
                    cmd.Parameters.AddWithValue("@p8", textBox8.Text);
                    cmd.Parameters.AddWithValue("@p9", textBox9.Text);
                    cmd.Parameters.AddWithValue("@p10", textBox10.Text);
                    cmd.Parameters.AddWithValue("@p1", textBox1.Text);

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
