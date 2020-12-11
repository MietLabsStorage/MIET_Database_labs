using BD.BdClasses;
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
    public partial class AddOrUpdateInGroupForm : Form
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
        public AddOrUpdateInGroupForm(OleDbConnection _cn, bool isAdd, fun showFun, fun1 qFun)
        {
            InitializeComponent();
            this.showFun += showFun;
            this.qFun += qFun;
            button1.Click += Add;
            this.isAdd = isAdd;
            if (!isAdd)
            {
                this.Text = "Обновить в Группа";
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
                    cmd.CommandText = "INSERT INTO [Группа] VALUES (@ID, @Cathedra, @Monitor, @LaborUn, @Quantity)";
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Cathedra", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Monitor", textBox3.Text);
                    cmd.Parameters.AddWithValue("@LaborUn", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Quantity", textBox5.Text);
                }
                else
                {
                    cmd.CommandText = "UPDATE Группа SET " +
                        "Идентификатор_кафедры = @Cathedra, id_старосты = @Monitor, id_профорга = @LaborUn, Количество_студентов = @Quantity " +
                        "WHERE Номер_группы LIKE @ID";
                    cmd.Parameters.AddWithValue("@Cathedra", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Monitor", textBox3.Text);
                    cmd.Parameters.AddWithValue("@LaborUn", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Quantity", textBox5.Text);
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
