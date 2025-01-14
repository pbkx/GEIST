﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            searchBox = new TextBox
            {
                Text = "Search here...",
                ForeColor = Color.Gray,
                Width = 1090,
                Location = new Point(132, 3)
            };

            searchBox.Enter += searchBox_Enter;
            searchBox.Leave += searchBox_Leave;

            Controls.Add(searchBox);
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
            if (searchBox.Text == "Search here...")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;
            }
        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search here...";
                searchBox.ForeColor = Color.Gray;
            }
        }
    }
}
