using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace BD.BdClasses
{
    [Table(Name = "Группа")]
    class Group
    {
        public delegate void fun1(string isSucces);
        private static fun1 qFun;

        [Column(Name = "Номер_группы")]
        public string Number { get; set; }

        [Column(Name = "Идентификатор_кафедры")]
        public string IdCatherda { get; set; }

        [Column(Name = "id_старосты")]
        public string IdMonitor { get; set; }
        
        [Column(Name = "id_профорга")]
        public string IdLaborUn { get; set; }
        
        [Column(Name = "Количество_студентов")]
        public string StudentAmount { get; set; }

        /// <summary>
        /// Группы в которых больше 25 студентов
        /// </summary>
        /// <param name="qFun">функция вызываемая при отлове ошибок</param>
        /// <returns></returns>
        public static List<List<string>> More25Students(fun1 qFun)
        {
            string connectionString = @"Data Source=C:\\Users\\maksi\\source\\repos\\BD\\InvisibleYellowViolet;Initial Catalog=usersdb;Integrated Security=True";
            DataContext db = new DataContext(connectionString);
            List<List<string>> lst = new List<List<string>>();
            qFun += qFun;
            try
            {
                var groups = from gr in db.GetTable<Group>()
                             where int.Parse(gr.StudentAmount) > 25
                             orderby gr.Number
                             select gr;
                if(groups != null)
                {
                    foreach(Group str in groups)
                    {
                        List<string> strlst = new List<string>();
                        strlst.Add(str.Number);
                        strlst.Add(str.IdCatherda);
                        strlst.Add(str.IdMonitor);
                        strlst.Add(str.IdLaborUn);
                        strlst.Add(str.StudentAmount);
                        lst.Add(strlst);
                    }
                }
                qFun(" ");
                return lst;
            }
            catch (Exception ex)
            {
                qFun(ex.ToString());
                return lst;
            }

        }
    }
}
