using System;
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
    public partial class endScreen : Form
    {
        public Menu mMenu;
        public CoursesMenu cMenu;
        public QuizForm quiz;
        int cId;
        int mid;

        public endScreen(int courseId, int id)
        {
            mid = id;
            InitializeComponent();
            cId = courseId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mMenu = new Menu(mid);
            mMenu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cMenu = new CoursesMenu(mid);
            cMenu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            quiz = new QuizForm(cId, true);
        }
    }
}
