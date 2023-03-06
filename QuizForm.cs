using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ComSciProject
{
    public partial class QuizForm : Form
    {
        SqlConnection con = new SqlConnection(databaseCon.getCon());
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        SqlCommand cmd;
        SqlDataReader reader;
        String readQuery;
        public Flashcard[] caste1;
        public Flashcard[] caste2;
        public QuizForm(int cId)
        {
            logMan = new loginmanager();
            InitializeComponent();
            loadCourse(cId);
        }



        public void loadCourse(int cId)
        {

        }
        CoursesMenu courseMenu;
        loginmanager logMan;
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            courseMenu = new CoursesMenu(logMan.getCurLID());
            courseMenu.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
