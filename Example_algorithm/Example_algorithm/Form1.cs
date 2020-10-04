using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example_algorithm
{
    public partial class Form1 : Form
    {
        bool parameter_ok;


        decimal a;
        decimal b;
        decimal c;
        decimal d;

        decimal x = 5 * 10;
        decimal y;  //10+d
        decimal e = 7 / 1;

        string y_str;
        string x_str;
        string e_str;

        string a_str;
        string b_str;
        string c_str;
        string d_str;
        string total_str;

        int a_i;
        int b_i;
        int c_i;
        int d_i;

        decimal minEv;
        decimal maxEv;

        string minEvstring;
        string maxEvstring;

        public void CreateParameter()
        {
             bool parameter_ok;


            decimal a;
            decimal b;
            decimal c;
            decimal d;

            decimal x = 5 * 10;
            decimal y;  //10+d
            decimal e = 7 / 1;

            string y_str;
            string x_str;
            string e_str;

            string a_str;
            string b_str;
            string c_str;
            string d_str;
            string total_str;

            int a_i;
            int b_i;
            int c_i;
            int d_i;

            decimal minEv;
            decimal maxEv;

            string minEvstring;
            string maxEvstring;
        }


        private void CheckParameter()///Проверяем текстбоксы на пустую строку или считываем введенные параметры
        {

            ///переменная A
            if (txbx_a.Text == "")
            {
                Error_Form F2 = new Error_Form();                
                F2.ShowDialog();
                parameter_ok = false;
            }
            else
            {
                a_str = "";
                a_str = txbx_a.Text;
                a = Convert.ToDecimal(a_str);
                parameter_ok = true;
            }
            ///переменная B
            if (txbx_b.Text == "")
            {
                Error_Form F2 = new Error_Form();
                F2.ShowDialog();
                parameter_ok = false;
            }
            else
            {
                b_str = "";
                b_str = txbx_b.Text;
                b = Convert.ToDecimal(b_str);
                parameter_ok = true;
            }
            ///переменная C
            if (txbx_c.Text == "")
            {
                Error_Form F2 = new Error_Form();
                F2.ShowDialog();
                parameter_ok = false;
            }
            else
            {
                c_str = "";
                c_str = txbx_c.Text;
                c = Convert.ToDecimal(c_str);
                parameter_ok = true;
            }
            ///переменная D
            if (txbx_d.Text == "")
            {
                Error_Form F2 = new Error_Form();
                F2.ShowDialog();
                parameter_ok = false;
            }
            else
            {
                d_str = "";
                d_str = txbx_d.Text;
                d = Convert.ToDecimal(d_str);
                parameter_ok = true;
                y = d + 10;
                y_str = Convert.ToString(y);



            }
            if (parameter_ok == true)
            {
                Do_MinMax();

            }
            else
            {
                Error_Form F2 = new Error_Form();
                F2.ShowDialog();
            }


        }

        private void Do_MinMax()
        {

            minEv = 0;
            maxEv = 0;
            maxEvstring = "";
            if (a>b)
            {
                minEv = b;
            }
            else
            {
                minEv = a;
            }


            y_str= Convert.ToString(y);
            e_str = Convert.ToString(e);
             

            String[] ScoreString = { y_str,e_str,c_str,a_str};
            List<Decimal> ScoreList = new List<Decimal>();
            Decimal mySum = 0;
            foreach (string s in ScoreString)
            {
                ScoreList.Add(Convert.ToDecimal(s));
                mySum += Convert.ToDecimal(s);
            }
            decimal resultMax = (ScoreList.ToArray().Max());
            //Console.Write(resultMax);
            maxEv = resultMax;



            /* decimal[] numbers = { SByte.MinValue, y, e, c, a, SByte.MaxValue };
             decimal result;

             foreach (decimal number in numbers)
             {
                 result = Convert.ToDecimal(number);

                 maxEv = result;
                 maxEvstring = Convert.ToString(maxEv);
             }*/

            Result();
        }

        private void Result()
        {
            decimal SummMinEvAndx = minEv + x;
            decimal total;
            total = SummMinEvAndx - maxEv;
            total_str = Convert.ToString(total);
            lb_result.Text = total_str;


        }

        public Form1()
        {

            InitializeComponent();
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            CreateParameter();
            CheckParameter();
        }

        
    }
}
