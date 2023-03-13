﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComSciProject
{
    public partial class Menu : Form
    {
        public FlashcardsMenu fMenu;
        public CoursesMenu cMenu;
        int cmId;
        public Menu(int id)
        {
            cmId = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            cMenu = new CoursesMenu(cmId);
            cMenu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fMenu = new FlashcardsMenu(cmId);
            fMenu.Show();
            this.Hide();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
