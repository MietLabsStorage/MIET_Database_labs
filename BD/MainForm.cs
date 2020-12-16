using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using BD.DeleteForms;
using BD.AddForms;
using BD.QueriesForms;
using BD.BdClasses;

namespace BD
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            button1.Click += ShowTable;
            button2.Click += AddInTable;
            button3.Click += DeleteInTable;
            button4.Click += UpdateInTable;

            /*label1.Location = new Point(label1.Location.X, label1.Location.Y + 70);
            textBoxTable.Location = new Point(textBoxTable.Location.X, textBoxTable.Location.Y + 70);
            button1.Location = new Point(button1.Location.X, button1.Location.Y + 70);
            button2.Location = new Point(button2.Location.X, button2.Location.Y + 70);
            button3.Location = new Point(button3.Location.X, button3.Location.Y + 70);
            button4.Location = new Point(button4.Location.X, button4.Location.Y + 70);*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// показывает в textBoxException сообщение ошибки
        /// </summary>
        /// <param name="exception">сообщение об ошибке</param>
        private void QueryException(string exception)
        {
            new ExceptionMes(exception).Show();
        }

        /// <summary>
        /// выравнивает таблицу по ширине столбцов
        /// </summary>
        /// <param name="tableStr">невыровненная таблица</param>
        /// <param name="columns">число столбцов</param>
        /// <returns></returns>
        private List<String> LevelOffTable(List<List<string>> tableStr, int columns)
        {
            List<String> res = new List<String>();
            for (int i = 0; i < columns; i++)
            {
                int maxLen = 0;
                foreach (List<string> lststr in tableStr)
                {
                    if (lststr[i].Length > maxLen) { maxLen = lststr[i].Length; }
                }
                foreach (List<string> lststr in tableStr)
                {
                    int lenadd = maxLen - lststr[i].Length;
                    for (int k = 0; k < lenadd; k++)
                    {
                        lststr[i] += " ";
                    }
                    lststr[i] += " | ";
                }
            }
            foreach (List<string> lststr in tableStr)
            {
                string resstr = "";
                for (int i = 0; i < columns; i++)
                {
                    resstr += lststr[i];
                }
                res.Add(resstr);
            }
            return res;
        }

        /// <summary>
        /// Метод не прописан!!!
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       /* private string SelectParamQuery(string name) { return "SELECT * FROM [" + name + "]"; }*/

        /// <summary>
        /// вывести таблицу на listBox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTable(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (String i in Database.GetTableContents(Database.SelectQuery(textBoxTable.Text), LevelOffTable))
                listBox1.Items.Add(i);
        }

        /// <summary>
        /// добавление в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInTable(object sender, EventArgs e)
        {
            switch (textBoxTable.Text)
            {
                case "Выпускающая кафедра":
                    new AddOrUpdateInCathedraForm(Database.Cn, true, delegate() { ShowTable(sender, e); } , delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Группа":
                    new AddOrUpdateInGroupForm(Database.Cn, true, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Предмет":
                    new AddOrUpdateSubjectForm(Database.Cn, true, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Семестр":
                    new AddOrUpdateSemestrForm(Database.Cn, true, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Студент":
                    new AddOrUpdateInStudent(Database.Cn, true, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Текущая_успеваемость":
                    new AddOrUpdateMarksForm(Database.Cn, true, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
            }
        }

        /// <summary>
        /// удалить из таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteInTable(object sender, EventArgs e)
        {
            switch (textBoxTable.Text)
            {
                case "Выпускающая кафедра":
                    new DelInCathedraForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Группа":
                    new DelInGroupForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Предмет":
                    new DelSubjectForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Семестр":
                    new DelSemestrForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Студент":
                    new DelInStudentForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Текущая_успеваемость":
                    new DelMarksForm(Database.Cn, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
            }
        }

        /// <summary>
        /// обновить в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInTable(object sender, EventArgs e)
        {
            switch (textBoxTable.Text)
            {
                case "Выпускающая кафедра":
                    new AddOrUpdateInCathedraForm(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Группа":
                    new AddOrUpdateInGroupForm(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Предмет":
                    new AddOrUpdateSubjectForm(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Семестр":
                    new AddOrUpdateSemestrForm(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Студент":
                    new AddOrUpdateInStudent(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
                case "Текущая_успеваемость":
                    new AddOrUpdateMarksForm(Database.Cn, false, delegate () { ShowTable(sender, e); }, delegate (string str) { QueryException(str); }).Show();
                    break;
            }
        }

        /// <summary>
        /// первый запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow1(object sender, EventArgs e)
        {
            Database.QueriesWithoutParams(
                delegate (List<string> str)
                {
                    listBox1.Items.Clear();
                    foreach (String i in str)
                        listBox1.Items.Add(i);
                }, delegate (string str) { QueryException(str); }, 0, LevelOffTable);
        }

        /// <summary>
        /// второй запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow2(object sender, EventArgs e)
        {
            Database.QueriesWithoutParams(
               delegate (List<string> str)
               {
                   listBox1.Items.Clear();
                   foreach (String i in str)
                       listBox1.Items.Add(i);
               }, delegate (string str) { QueryException(str); }, 1, LevelOffTable);
        }

        /// <summary>
        /// третий запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow3(object sender, EventArgs e)
        {
            Database.QueriesWithoutParams(
               delegate (List<string> str)
               {
                   listBox1.Items.Clear();
                   foreach (String i in str)
                       listBox1.Items.Add(i);
               }, delegate (string str) { QueryException(str); }, 2, LevelOffTable);
        }

        /// <summary>
        /// четвертый запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow4(object sender, EventArgs e)
        {
            new StudsFromCity(Database.Cn, delegate (OleDbCommand cmd)
            {
                listBox1.Items.Clear();
                foreach (String i in Database.GetTableContents(cmd, LevelOffTable))
                    listBox1.Items.Add(i);
            }, delegate (string str) { QueryException(str); }).Show();
        }

        /// <summary>
        /// пятый запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow5(object sender, EventArgs e)
        {
            new GroupsFromCath(Database.Cn, delegate (OleDbCommand cmd)
            {
                listBox1.Items.Clear();
                foreach (String i in Database.GetTableContents(cmd, LevelOffTable))
                    listBox1.Items.Add(i);
            }, delegate (string str) { QueryException(str); }).Show();
        }

        /// <summary>
        /// шестой запрос без параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryShow6(object sender, EventArgs e)
        {
            new SubjWithMoreHours(Database.Cn, delegate (OleDbCommand cmd)
            {
                listBox1.Items.Clear();
                foreach (String i in Database.GetTableContents(cmd, LevelOffTable))
                    listBox1.Items.Add(i);
            }, delegate (string str) { QueryException(str); }).Show();
        }

        /// <summary>
        /// запрос обновление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryUpdate(object sender, EventArgs e)
        {

            try
            {
                textBoxTable.Text = "Предмет";
                Database.QueriesOther(delegate (string str) { QueryException(str); }, 0);
                ShowTable(sender, e);
            }
            catch (Exception ex)
            {
                QueryException(ex.Message);
            }
        }

        /// <summary>
        /// запрос добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryAdd(object sender, EventArgs e)
        {
            try
            {
                textBoxTable.Text = "Студент";
                Database.QueriesOther(delegate (string str) { QueryException(str); }, 1);
                ShowTable(sender, e);
            }
            catch (Exception ex)
            {
                QueryException(ex.Message);
            }

        }

        /// <summary>
        /// запрос удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDel(object sender, EventArgs e)
        {
            try
            {
                textBoxTable.Text = "Студент";
                Database.QueriesOther(delegate (string str) { QueryException(str); }, 2);
                ShowTable(sender, e);
            }
            catch (Exception ex)
            {
                QueryException(ex.Message);
            }
        }

        /// <summary>
        /// Запрос с помощью Data.Link - не работает!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryLink(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (String i in LevelOffTable(Group.More25Students(delegate (string str) { QueryException(str); }), 5))
                listBox1.Items.Add(i);
        }

        /// <summary>
        /// выполнить собственный запрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuerySelect(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (String i in Database.GetTableContents(textBoxQuery.Text, LevelOffTable))
                listBox1.Items.Add(i);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
