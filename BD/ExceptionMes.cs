using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class ExceptionMes : Form
    {
        public ExceptionMes(string mes)
        {
            InitializeComponent();
            for(int i = 0; i<mes.Length/48; i++)
            {
                listBox1.Items.Add(mes.Substring(i*48, 48));
            }
            
        }
    }
}
