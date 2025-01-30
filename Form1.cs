using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        private double[,] graphPoints(int[] amounts, int[] dates)
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


            double[] scaledDates = new double[dates.Length];


            for (int z = 0; z < scaledDates.Length; z++) {
                scaledDates[z] = ((double)(dates[z] - mindate) / (maxdate-mindate));
            }

            double[] scaledAmounts = new double[amounts.Length];

            for (int y = 0; y < scaledAmounts.Length; y++)
            {
                scaledAmounts[y] = ((double)(amounts[y] - minamount) / (maxamount-minamount));
            }

            foreach (double p in scaledAmounts)
            {
                Console.WriteLine(p.ToString());
            }

            foreach (double p in scaledDates)
            {
                Console.WriteLine(p.ToString());
            }
           
            double[,] Points = new double[dates.Length,2];
            for (int p = 0; p < dates.Length; p++) {
                Points[p,0] = scaledDates[p];
                Points[p, 1] = scaledAmounts[p];
            }
           
            return Points;
        }
        
        private void DrawLinesPoint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 2);

            int[] d = { 5, 10, 15, 20, 25 };
            int[] a = { 100, 10, 200, 5, 300 };

            double[,] POINTS = graphPoints(a, d);

            foreach (double p in POINTS)
            {
                Console.WriteLine(p.ToString());
            }

            // Create array of points that define lines to draw.

            Point[] points = new Point[POINTS.Length/2];

            int xcoord = 0;
            int ycoord = 0;

            for (int i = 0; i < POINTS.Length/2; i++)
            {
                xcoord = (int)(1080 * POINTS[i, 0] + 134);
                ycoord = (int)(678 - 221 * POINTS[i, 1] - 49);

                Console.WriteLine("----");
                Console.WriteLine(xcoord);
                Console.WriteLine(ycoord);

                points[i] = new Point(xcoord, ycoord);
            }

            //Draw lines to screen.
            e.Graphics.DrawLines(pen, points);
        }

    }
}
