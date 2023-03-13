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
using System.Security.Cryptography;

namespace ComSciProject
{
    public partial class QuizForm : Form
    {
        SqlConnection con = new SqlConnection(databaseCon.getCon());
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        SqlCommand cmd;
        SqlDataReader reader;
        String readQuery;
        public Course loadedCourse;
        public List<Flashcard> caste1;
        public List<Flashcard> caste2;
        int mid;
        public QuizForm(int cId, int id)
        {
            mid = id;
            InitializeComponent();
            loadCourse(cId);
        }

        public QuizForm(int cId, bool reset)
        {
            InitializeComponent();
            resetCourse(cId);
            loadCourse(cId);
        }

        public void resetCourse(int cId)
        {
            loadedCourse = new Course(cId);
            for(int i = 0; i < loadedCourse.courseSize; i++)
            {
                loadedCourse.flashcards[i].box = false;
            }
        }

        public void loadCourse(int cId)
        {
            loadedCourse = new Course(cId);
            caste1 = loadedCourse.caste1;
            caste2 = loadedCourse.caste2;
            
        }
        CoursesMenu courseMenu;

        public void endSession()
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (caste1[0] == null)
            {
                endSession();
                return;
            }
            caste1[0].box = true;
            caste2.Add(caste1[0]);
            caste1.Remove(caste1[0]);
            this.label1.Text = caste1[0].front;
            this.label2.Text = "...";
            label3.Text = updateProgress();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            courseMenu = new CoursesMenu(mid);
            courseMenu.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = caste1[0].back;
        }

        private void QuizForm_Load(object sender, EventArgs e)
        {
            if (caste1.Count() >= 1) label1.Text = caste1[0].front;
            else label1.Text = "Course empty. No flashcards found";
        }
        public String updateProgress()
        {
            return $"{caste1.Count}/{caste2.Count}";
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Flashcard f = caste1[0];
            caste1.Remove(caste1[0]);
            caste1.Add(f);
            this.label1.Text = caste1[0].front;
            this.label2.Text = "...";
        }
    }
}
