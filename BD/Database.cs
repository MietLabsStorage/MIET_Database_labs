using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    public class Database
    {
        public static OleDbConnection Cn { get; private set; }

        private OleDbConnection cn;
        public delegate void fun();
        public delegate void fun1(string isSucces);
        public delegate void fun2(List<string> str);
        public delegate void fun3(OleDbCommand cmd);

        static Database()
        {
            Cn = new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source=""C:\\Users\\maksi\\source\\repos\\BD\\InvisibleYellowViolet.accdb"";" +
                @"Jet OLEDB:Create System Database=true;" +
                @"Jet OLEDB:System database=C:\Users\maksi\source\repos\BD\System.mdw");
        }

        public delegate List<String> LevelOffTableDelegate(List<List<string>> tableStr, int columns);


        /// <summary>
        /// таблица как список строк
        /// </summary>
        /// <param name="query">запрос на получение таблицы</param>
        /// <returns>список строк талицы</returns>
        public static List<String> GetTableContents(string query, LevelOffTableDelegate _lofDelegate)
        {
            List<String> res = new List<String>();
            Cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                cmd.CommandText = query;
                OleDbDataReader rd;
                try
                {
                    rd = cmd.ExecuteReader();
                }
                catch (Exception)
                {
                    Cn.Close();
                    return new List<string>() { "No such table" };
                }
                if (rd.HasRows)
                {
                    string str = "";
                    List<List<string>> tableStr = new List<List<string>>();
                    List<string> lst = new List<string>();
                    int columns = rd.FieldCount;
                    for (int i = 0; i < columns; i++)
                    {
                        lst.Add(rd.GetName(i));
                    }
                    tableStr.Add(lst);
                    while (rd.Read())
                    {
                        List<string> lst1 = new List<string>();
                        for (int i = 0; i < columns; i++)
                        {
                            lst1.Add(rd[i].ToString());
                        }
                        tableStr.Add(lst1);
                    }
                    res = _lofDelegate(tableStr, columns);
                }
                Cn.Close();
                return res;
            }
            catch
            {
                Cn.Close();
                return new List<string>();
            }
        }

        /// <summary>
        /// таблица как список строк
        /// </summary>
        /// <param name="query">запрос на получение таблицы</param>
        /// <returns>список строк талицы</returns>
        public static List<String> GetTableContents(OleDbCommand cmd, LevelOffTableDelegate _lofDelegate)
        {
            List<String> res = new List<String>();
            Cn.Open();
            try
            {
                OleDbDataReader rd;
                try
                {
                    rd = cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    return new List<string>() { "No such table" };
                }
                if (rd.HasRows)
                {
                    string str = "";
                    List<List<string>> tableStr = new List<List<string>>();
                    List<string> lst = new List<string>();
                    int columns = rd.FieldCount;
                    for (int i = 0; i < columns; i++)
                    {
                        lst.Add(rd.GetName(i));
                    }
                    tableStr.Add(lst);
                    while (rd.Read())
                    {
                        List<string> lst1 = new List<string>();
                        for (int i = 0; i < columns; i++)
                        {
                            lst1.Add(rd[i].ToString());
                        }
                        tableStr.Add(lst1);
                    }
                    res = _lofDelegate(tableStr, columns);
                }
                Cn.Close();
                return res;
            }
            catch
            {
                Cn.Close();
                return new List<string>();
            }
        }

        /// <summary>
        /// создает запрос вида "SELECT * FROM [" + name + "]"
        /// </summary>
        /// <param name="name">имя таблицы</param>
        /// <returns>"SELECT * FROM [" + name + "]"</returns>
        public static string SelectQuery(string name)
        {
            Cn.Open();
            bool isNameExist = false;
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                cmd.CommandText = "SELECT Name FROM MSysObjects";

                OleDbDataReader rd;
                try
                {
                    rd = cmd.ExecuteReader();
                }
                catch (Exception)
                {
                    return "";
                }
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd[0].Equals(name))
                        {
                            isNameExist = true;
                            break;
                        }
                    }
                }
            }
            catch { }
            finally
            {
                Cn.Close();
            }

            if (isNameExist)
            {
                return $"SELECT * FROM [{name}]";
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// add line in table
        /// </summary>
        /// <param name="n">
        /// which table (
        /// 0 - Cathedra
        /// 1 - Group
        /// 2 - Student
        /// 3 - Marks 
        /// 4 - Semestr
        /// 5 - Subject
        /// )</param>
        public static void Add(fun showFun, fun1 qFun, int n, params string[] textBox)
        {
            Cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                switch (n)
                {
                    case 0:
                        cmd.CommandText = "INSERT INTO [Выпускающая кафедра] VALUES (@ID, @FullName, @ShortName, @Decan)";

                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        cmd.Parameters.AddWithValue("@FullName", textBox[1]);
                        cmd.Parameters.AddWithValue("@ShortName", textBox[2]);
                        cmd.Parameters.AddWithValue("@Decan", textBox[3]);
                        break;

                    case 1:
                        cmd.CommandText = "INSERT INTO [Группа] VALUES (@ID, @Cathedra, @Monitor, @LaborUn, @Quantity)";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        cmd.Parameters.AddWithValue("@Cathedra", textBox[1]);
                        cmd.Parameters.AddWithValue("@Monitor", textBox[2]);
                        cmd.Parameters.AddWithValue("@LaborUn", textBox[3]);
                        cmd.Parameters.AddWithValue("@Quantity", textBox[4]);
                        break;

                    case 2:
                        cmd.CommandText = "INSERT INTO [Студент] VALUES (@p1, @p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";

                        cmd.Parameters.AddWithValue("@p1", textBox[0]);
                        cmd.Parameters.AddWithValue("@p2", textBox[1]);
                        cmd.Parameters.AddWithValue("@p3", textBox[2]);
                        cmd.Parameters.AddWithValue("@p4", textBox[3]);
                        cmd.Parameters.AddWithValue("@p5", textBox[4]);
                        cmd.Parameters.AddWithValue("@p6", textBox[5]);
                        cmd.Parameters.AddWithValue("@p7", textBox[6]);
                        cmd.Parameters.AddWithValue("@p8", textBox[7]);
                        cmd.Parameters.AddWithValue("@p9", textBox[8]);
                        cmd.Parameters.AddWithValue("@p10", textBox[9]);
                        break;

                    case 3:
                        cmd.CommandText =
                            "INSERT INTO [Текущая_успеваемость] VALUES (@p1, @p2,@p3,@p4,@p5,@p6)";

                        cmd.Parameters.AddWithValue("@p1", textBox[0]);
                        cmd.Parameters.AddWithValue("@p2", textBox[1]);
                        cmd.Parameters.AddWithValue("@p3", textBox[2]);
                        cmd.Parameters.AddWithValue("@p4", textBox[3]);
                        cmd.Parameters.AddWithValue("@p5", textBox[4]);
                        cmd.Parameters.AddWithValue("@p6", textBox[5]);
                        break;

                    case 4:
                        cmd.CommandText = "INSERT INTO [Семестр] VALUES (@ID, @Begin, @End, @Time)";

                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        cmd.Parameters.AddWithValue("@Begin", textBox[1]);
                        cmd.Parameters.AddWithValue("@End", textBox[2]);
                        cmd.Parameters.AddWithValue("@Time", textBox[3]);
                        break;

                    case 5:
                        cmd.CommandText =
                            "INSERT INTO [Предмет] VALUES (@ID, @FullName, @ShortName, @Time)";

                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        cmd.Parameters.AddWithValue("@FullName", textBox[1]);
                        cmd.Parameters.AddWithValue("@ShortName", textBox[2]);
                        cmd.Parameters.AddWithValue("@Time", textBox[3]);
                        break;
                }
                cmd.ExecuteNonQuery();
                Cn.Close();
                showFun();
            }
            catch (Exception ex)
            {
                Cn.Close();
                qFun(ex.Message);
            }
        }


        /// <summary>
        /// update line in table
        /// </summary>
        /// <param name="n">
        /// which table (
        /// 0 - Cathedra
        /// 1 - Group
        /// 2 - Student
        /// 3 - Marks 
        /// 4 - Semestr
        /// 5 - Subject
        /// )</param>
        public static void Update(fun showFun, fun1 qFun, int n, params string[] textBox)
        {
            Cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                switch (n)
                {
                    case 0:
                        cmd.CommandText = "UPDATE [Выпускающая кафедра] SET " +
                        "Полное_название = @FullName, Короткое_название = @ShortName, Декан = @Decan " +
                        "WHERE Идентификатор_кафедры = @ID";
                        cmd.Parameters.AddWithValue("@FullName", textBox[1]);
                        cmd.Parameters.AddWithValue("@ShortName", textBox[2]);
                        cmd.Parameters.AddWithValue("@Decan", textBox[3]);
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 1:
                        cmd.CommandText = "UPDATE Группа SET " +
                            "Идентификатор_кафедры = @Cathedra, id_старосты = @Monitor, id_профорга = @LaborUn, Количество_студентов = @Quantity " +
                            "WHERE Номер_группы LIKE @ID";
                        cmd.Parameters.AddWithValue("@Cathedra", textBox[1]);
                        cmd.Parameters.AddWithValue("@Monitor", textBox[2]);
                        cmd.Parameters.AddWithValue("@LaborUn", textBox[3]);
                        cmd.Parameters.AddWithValue("@Quantity", textBox[4]);
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 2:
                        cmd.CommandText = "UPDATE Студент SET " +
    "Номер_группы = @p2, Фамилия = @p3, Имя = @p4, Отчество = @p5, Дата_Рождения = @p6, Пол = @p7, Регион = @p8, [Проживание в общежитии] = @p9, Школа = @p10 " +
    "WHERE Номер_студенческого_билета LIKE @p1";

                        cmd.Parameters.AddWithValue("@p2", textBox[1]);
                        cmd.Parameters.AddWithValue("@p3", textBox[2]);
                        cmd.Parameters.AddWithValue("@p4", textBox[3]);
                        cmd.Parameters.AddWithValue("@p5", textBox[4]);
                        cmd.Parameters.AddWithValue("@p6", textBox[5]);
                        cmd.Parameters.AddWithValue("@p7", textBox[6]);
                        cmd.Parameters.AddWithValue("@p8", textBox[7]);
                        cmd.Parameters.AddWithValue("@p9", textBox[8]);
                        cmd.Parameters.AddWithValue("@p10", textBox[9]);
                        cmd.Parameters.AddWithValue("@p1", textBox[0]);
                        break;

                    case 3:
                        cmd.CommandText = "UPDATE [Текущая_успеваемость] SET " +
    "Номер_семестра = @p2, Идентификатор_предмета = @p3, Номер_студенческого_билета = @p4, Дата_проведения = @p5, Оценка = @p6 " +
    "WHERE Идентификатор_успеваемости LIKE @p1";

                        cmd.Parameters.AddWithValue("@p2", textBox[1]);
                        cmd.Parameters.AddWithValue("@p3", textBox[2]);
                        cmd.Parameters.AddWithValue("@p4", textBox[3]);
                        cmd.Parameters.AddWithValue("@p5", textBox[4]);
                        cmd.Parameters.AddWithValue("@p6", textBox[5]);
                        cmd.Parameters.AddWithValue("@p1", textBox[0]);
                        break;

                    case 4:
                        cmd.CommandText = "UPDATE Семестр SET " +
    "Начало = @Begin, Конец = @End, Количество_недель = @Time " +
    "WHERE Номер_семестра LIKE @ID";

                        cmd.Parameters.AddWithValue("@Begin", textBox[1]);
                        cmd.Parameters.AddWithValue("@End", textBox[2]);
                        cmd.Parameters.AddWithValue("@Time", textBox[3]);
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 5:
                        cmd.CommandText = "UPDATE Предмет SET " +
    "[Название предмета] = @FullName, [Короткое название предмета] = @ShortName, [Количество часов] = @Time " +
    "WHERE [Идентификатор предмета] LIKE @ID";

                        cmd.Parameters.AddWithValue("@FullName", textBox[1]);
                        cmd.Parameters.AddWithValue("@ShortName", textBox[2]);
                        cmd.Parameters.AddWithValue("@Time", textBox[3]);
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;
                }
                cmd.ExecuteNonQuery();
                Cn.Close();
                showFun();
            }
            catch (Exception ex)
            {
                Cn.Close();
                qFun(ex.Message);
            }
        }


        /// <summary>
        /// update line in table
        /// </summary>
        /// <param name="n">
        /// which table (
        /// 0 - Cathedra
        /// 1 - Group
        /// 2 - Student
        /// 3 - Marks 
        /// 4 - Semestr
        /// 5 - Subject
        /// )</param>
        public static void Delete(fun showFun, fun1 qFun, int n, params string[] textBox)
        {
            Cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                switch (n)
                {
                    case 0:
                        cmd.CommandText = "DELETE FROM [Выпускающая кафедра] WHERE [Идентификатор_кафедры]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 1:
                        cmd.CommandText = "DELETE FROM [Группа] WHERE [Номер_группы]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 2:
                        cmd.CommandText = "DELETE FROM [Студент] WHERE [Номер_студенческого_билета]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 3:
                        cmd.CommandText = "DELETE FROM [Текущая_успеваемость] WHERE [Идентификатор_успеваемости]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 4:
                        cmd.CommandText = "DELETE FROM [Семестр] WHERE [Номер_семестра]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;

                    case 5:
                        cmd.CommandText = "DELETE FROM [Предмет] WHERE [Идентификатор предмета]=@ID";
                        cmd.Parameters.AddWithValue("@ID", textBox[0]);
                        break;
                }
                cmd.ExecuteNonQuery();
                Cn.Close();
                showFun();
            }
            catch (Exception ex)
            {
                Cn.Close();
                qFun(ex.Message);
            }
        }

        /// <summary>
        /// queries without params
        /// </summary>
        /// <param name="showFun"></param>
        /// <param name="qFun">
        /// </param>
        /// <param name="n">
        /// 0 - 6QL_Старосты
        /// 1 - УспеваемостьПИН-34
        /// 2 - SQL_студентыИИхКафедры</param>
        /// <param name="_lofDelegate"></param>
        /// <param name="textBox"></param>
        public static void QueriesWithoutParams(fun2 showFun, fun1 qFun, int n, LevelOffTableDelegate _lofDelegate, params string[] textBox)
        {
            try
            {
                switch (n)
                {
                    case 0:                       
                        showFun(Database.GetTableContents(Database.SelectQuery("6QL_Старосты"), _lofDelegate));
                        break;

                    case 1:
                        showFun(Database.GetTableContents(Database.SelectQuery("УспеваемостьПИН-34"), _lofDelegate));
                        break;

                    case 2:
                        showFun(Database.GetTableContents(Database.SelectQuery("SQL_студентыИИхКафедры"), _lofDelegate));
                        break;
                }
            }
            catch (Exception ex)
            {
                qFun(ex.Message);
            }
        }

        /// <summary>
        /// queries without params
        /// </summary>
        /// <param name="showFun"></param>
        /// <param name="qFun">
        /// </param>
        /// <param name="n">
        /// 0 - СтудентИзГорода
        /// 1 - ГруппыКафедры
        /// 2 - ПредметыБольшеСтолькиЧасов</param>
        /// <param name="_lofDelegate"></param>
        /// <param name="textBox"></param>
        public static void QueriesWithParams(fun3 showFun, fun1 qFun, int n, params string[] textBox)
        {
            try
            {
                Cn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Cn;
                switch (n)
                {
                    case 0:
                        cmd.Parameters.AddWithValue("[Введите город]", textBox[0]);
                        cmd.CommandText = "SELECT * FROM СтудентИзГорода";
                        break;

                    case 1:
                        cmd.Parameters.AddWithValue("[ID кафедры]", textBox[0]);
                        cmd.CommandText = "SELECT * FROM ГруппыКафедры";
                        break;

                    case 2:
                        cmd.Parameters.AddWithValue("[Кол-во часов]", textBox[0]);
                        cmd.CommandText = "SELECT * FROM ПредметыБольшеСтолькиЧасов";
                        break;
                }
                cmd.ExecuteNonQuery();
                Cn.Close();
                showFun(cmd);
            }
            catch (Exception ex)
            {
                Cn.Close();
                qFun(ex.Message);
            }
        }


        /// <summary>
        /// queries without params
        /// </summary>
        /// <param name="showFun"></param>
        /// <param name="qFun">
        /// </param>
        /// <param name="n">
        /// 0 - 6QL_Старосты
        /// 1 - УспеваемостьПИН-34
        /// 2 - SQL_студентыИИхКафедры</param>
        /// <param name="_lofDelegate"></param>
        /// <param name="textBox"></param>
        public static void QueriesOther(fun1 qFun, int n)
        {
            Cn.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Database.Cn;
                switch (n)
                {
                    case 0:
                        cmd.CommandText = "EXEC 6QL_обновПредмет";
                        break;

                    case 1:
                        cmd.CommandText = "EXEC 6QL_добСтудент";
                        break;

                    case 2:
                        cmd.CommandText = "EXEC 6QL_удСтудент";
                        break;
                }
                cmd.ExecuteNonQuery();
                Cn.Close();
            }
            catch (Exception ex)
            {
                Cn.Close();
                qFun(ex.Message);
            }
        }
    }
}
