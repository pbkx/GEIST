using Spire.Xls;
using Spire.Xls.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.IO;
using ClosedXML.Excel;
using System.Data.SqlClient;

namespace GEIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {



            InitializeComponent();

            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            this.button3.Click += new EventHandler(button3_Click);

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

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(@"C:\Users\s114150\Downloads\defaultData.xlsx");
            Worksheet worksheet = workbook.Worksheets[0];

            //Export data in the worksheet to a DataTable 

            //This overload enables you to specify the range to be exported along with whether to export column names and the actual values of formulas
            DataTable dt = worksheet.ExportDataTable(worksheet.Range["A1:E7"], true, true);

            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawLinesPoint);

            this.Controls.Add(incomeData);
            incomeData.DataSource = dt;

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                searchBox.Text = "Search Income/Expense Data";
                searchBox.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private double[,] graphPoints(List<int> amounts, List<int> dates)
        {

            int mindate = int.MaxValue;
            int maxdate = int.MinValue;

            for (int i = 0; i < dates.Count; i++)
            {
                if (dates[i] > maxdate)
                {
                    maxdate = dates[i];
                }
                if (dates[i] < mindate)
                {
                    mindate = dates[i];
                }
            }

            int minamount = int.MaxValue;
            int maxamount = int.MinValue;

            for (int i = 0; i < amounts.Count; i++)
            {
                if (amounts[i] > maxamount)
                {
                    maxamount = amounts[i];
                }
                if (amounts[i] < minamount)
                {
                    minamount = amounts[i];
                }
            }

            double[] scaledDates = new double[dates.Count];


            for (int z = 0; z < scaledDates.Length; z++)
            {
                scaledDates[z] = ((double)(dates[z] - mindate) / (maxdate - mindate));
            }

            double[] scaledAmounts = new double[amounts.Count];

            for (int y = 0; y < scaledAmounts.Length; y++)
            {
                scaledAmounts[y] = ((double)(amounts[y] - minamount) / (maxamount - minamount));
            }

            double[,] Points = new double[dates.Count, 2];
            for (int p = 0; p < dates.Count; p++)
            {
                Points[p, 0] = scaledDates[p];
                Points[p, 1] = scaledAmounts[p];
            }

            return Points;
        }

        private void DrawLinesPoint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Green, 2);

            List<int> d = new List<int>();
            List<int> a = new List<int>();

            foreach (DataGridViewRow row in incomeData.Rows)
            {

                if (row.Visible)
                {

                    if (row.Cells[0].Value != null && row.Cells[3].Value != null)
                    {
                        try
                        {
                            d.Add(Int32.Parse(row.Cells[0].Value.ToString()));
                            a.Add(Int32.Parse(row.Cells[3].Value.ToString()));
                        }
                        catch
                        {
                            System.Console.WriteLine("Error.");
                        }

                    }
                }
            }

            double[,] POINTS = graphPoints(a, d);

            // Create array of points that define lines to draw.

            Point[] points = new Point[POINTS.Length / 2];

            int xcoord = 0;
            int ycoord = 0;

            for (int i = 0; i < POINTS.Length / 2; i++)
            {
                xcoord = (int)(1080 * POINTS[i, 0] + 134);
                ycoord = (int)(678 - 221 * POINTS[i, 1] - 49);
                points[i] = new Point(xcoord, ycoord);
            }

            //Draw lines to screen.

            try
            {
                e.Graphics.DrawLines(pen, points);
            }
            catch
            {
                System.Console.WriteLine("Not enough points.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //na

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = searchBox.Text;

            incomeData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            incomeData.AllowUserToAddRows = false;

            CurrencyManager cm = (CurrencyManager)BindingContext[incomeData.DataSource];
            foreach (DataGridViewRow row in incomeData.Rows)
            {
                cm.SuspendBinding();
                row.Visible = false;
                cm.ResumeBinding();
            }

            foreach (DataGridViewRow row in incomeData.Rows)
            {

                if (row.Cells[0].Value.ToString().Equals(searchValue) || row.Cells[1].Value.ToString().Equals(searchValue) || row.Cells[2].Value.ToString().Equals(searchValue) || row.Cells[3].Value.ToString().Equals(searchValue) || row.Cells[4].Value.ToString().Equals(searchValue))
                {
                    var sb = new StringBuilder();

                    cm.SuspendBinding();
                    row.Visible = true;
                    sb.Append(row.Cells[0].Value.ToString());
                    sb.Append(", ");
                    sb.Append(row.Cells[3].Value.ToString());

                    Console.WriteLine(sb.ToString());
                    cm.ResumeBinding();
                }
            }

            foreach (DataGridViewRow row in incomeData.Rows)
            {
                if (searchValue == "Search Income/Expense Data")
                {
                    row.Visible = true;
                }
            }

            cm.SuspendBinding();
            this.Invalidate();
            cm.ResumeBinding();

            incomeData.AllowUserToAddRows = true;




        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Workbook workbook = new Workbook();
            try
            {
                workbook.LoadFromFile(textBox2.Text);
            }
            catch
            {
                System.Console.WriteLine("No excel sheet input");
            }

            Worksheet worksheet = workbook.Worksheets[0];

            try
            {
                DataTable dt = worksheet.ExportDataTable(worksheet.Range[textBox1.Text], true, true);
                incomeData.DataSource = dt;
            }
            catch
            {
                System.Console.WriteLine("No bounds input");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {

            System.Console.WriteLine("Yo");
            DataTable DD = new DataTable();
            incomeData.AllowUserToAddRows = false;
            CurrencyManager cm = (CurrencyManager)BindingContext[incomeData.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewColumn column in incomeData.Columns)
            {
                DD.Columns.Add(column.HeaderText, column.ValueType);
            }


            foreach (DataGridViewRow row in incomeData.Rows)
            {
                DD.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    DD.Rows[DD.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            string folderPath = "C:\\Users\\s114150\\Desktop";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DD, "ExportData");
                System.Console.WriteLine(wb.ToString());
                wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
            }

            cm.ResumeBinding();
            incomeData.AllowUserToAddRows = true;
        }
    }
}

