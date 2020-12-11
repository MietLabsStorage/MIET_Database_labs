using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    class Database
    {
        public static OleDbConnection Cn { get; private set; }
        
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
                return res;
            }
            finally
            {
                Cn.Close();
            }
        }

        /// <summary>
        /// таблица как список строк
        /// </summary>
        /// <param name="query">запрос на получение таблицы</param>
        /// <returns>список строк талицы</returns>
        public static  List<String> GetTableContents(OleDbCommand cmd, LevelOffTableDelegate _lofDelegate)
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
                return res;
            }
            finally
            {
                Cn.Close();
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
            finally
            {
                Cn.Close();
            }
            if(isNameExist)
            {
                return $"SELECT * FROM {name}";
            }
            else
            {
                return "";
            }
            
        }


    }
}
