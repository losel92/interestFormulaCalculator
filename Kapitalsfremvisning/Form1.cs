using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kapitalsfremvisning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Transforms all the values in the txtBoxes to doubles
        double k0Num = 0;
        double rNum = 0;
        double nNum = 0;
        double knNum = 0;

        void calculateKn() //Calculates for Kn
        {
            knNum = k0Num * Math.Pow(1 + rNum, nNum);
            kn.Text = Convert.ToString(knNum);
        }
        void calculateK0() //Calculates for K0
        {
            k0Num = knNum / Math.Pow(1 + rNum, nNum);
            k0.Text = Convert.ToString(k0Num);
        }
        void calculateR() //Calculates for r
        {
            rNum = Math.Pow(knNum/k0Num, 1/nNum) - 1;
            r.Text = Convert.ToString(rNum);
        }
        void calculateN() //Calculates for n
        {
            nNum = Math.Log(knNum/k0Num) / Math.Log(1 + rNum);
            n.Text = Convert.ToString(nNum);
        }

        //Piece of code from StackOverflow
        //Only accepts numbers and one decimal point in the textboxes
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void calculateIt(object sender, EventArgs e)
        {
            //Checks if at least three of the textboxes are filled and gets us the one that isn't (aka the one we need to calculate for)
            //It also assigns the num doubles to whatever is in the textbox, so long it isn't an empty string
            int filledCount = 0;
            var numToFind = k0;
            if (k0.Text != "") { filledCount++; k0Num = Convert.ToDouble(k0.Text); } else { numToFind = k0; }
            if (kn.Text != "") { filledCount++; knNum = Convert.ToDouble(kn.Text); } else { numToFind = kn; }
            if (r.Text != "") { filledCount++; rNum = Convert.ToDouble(r.Text); } else { numToFind = r; }
            if (n.Text != "") { filledCount++; nNum = Convert.ToDouble(n.Text); } else { numToFind = n; }

            if (filledCount == 3)
            {
                if (numToFind == k0) calculateK0();
                else if (numToFind == kn) calculateKn();
                else if (numToFind == r) calculateR();
                else if (numToFind == n) calculateN();
            }
            else //If the number of textboxes filled is not equal to three, it will throw an error
            {
                MessageBox.Show("Error", "Please fill out exactly three values", MessageBoxButtons.OK);
            }
        }

        //Resets all the textboxes
        private void resetValues(object sender, EventArgs e)
        {
            k0.Text = "";
            kn.Text = "";
            r.Text = "";
            n.Text = "";
        }
    }
}
