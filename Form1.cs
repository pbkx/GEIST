using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace GEIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            searchBox = new TextBox
            {
                Text = "Search Income/Expense Data",
                ForeColor = Color.Gray,
                BackColor = Color.Black,
                BorderStyle = BorderStyle.Fixed3D,
                Width = 1047,
                
                Location = new Point(167, 3)
            };

            searchBox.Enter += searchBox_Enter;
            searchBox.Leave += searchBox_Leave;

            Controls.Add(searchBox);

            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawLinesPoint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == "Search Income/Expense Data")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.White;
            }
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "🔍 Search Income/Expense Data";
                searchBox.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private Point[] graphPoints(object sender, EventArgs e, int[] amounts, int[] dates)
        {

            int mindate = int.MaxValue;
            int maxdate = int.MinValue;

            for (int i = 0; i < dates.Length; i++) {
                if (dates[i] > maxdate) {
                    maxdate = dates[i];
                }
                if (dates[i] < mindate) {
                    mindate = dates[i];
                }
            }

            int minamount = int.MaxValue;
            int maxamount = int.MinValue;

            for (int i = 0; i < amounts.Length; i++) {
                if (amounts[i] > maxamount)
                {
                    maxamount = amounts[i];
                }
                if (amounts[i] < minamount)
                {
                    minamount = amounts[i];
                }
            }


            // CHECK DESMOS
           // Point[] points = 
                
            return null;
        }
        
        private void DrawLinesPoint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 1);

            // Create array of points that define lines to draw.
            Point[] points =
                        {
            new Point(150, 600),
            new Point(300, 560),
            new Point(450, 580),
            new Point(600, 590)
        };

            //Draw lines to screen.
            e.Graphics.DrawLines(pen, points);
        }

    }
}
