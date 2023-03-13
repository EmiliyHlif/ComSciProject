using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComSciProject
{
    public partial class CourseCreateDialouge : Form
    {
        int mid;
        public CourseCreateDialouge(int cmid)
        {
            mid = cmid;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        public void createCourses(String cName, String cDescription)
        {
            
                SqlConnection con = new SqlConnection(databaseCon.getCon());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand cmd;
                SqlDataReader reader;
                int ncId = -1;
                int cmId = -1;

                #region set new flashcard ID

                String countFIDQuery = $"SELECT COUNT(cId) FROM courses";
                con.Open();
                cmd = new SqlCommand(countFIDQuery, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ncId = reader.GetInt32(0);
                    ncId++;
                }
                con.Close();

                #endregion

                #region get MemberId


            #endregion

            bool shouldRepeate = true;
            int i = 1;

            String checkQuery = $"SELECT cName FROM courses WHERE cName = '{cName}'";

            while (shouldRepeate)
            {
                

                con.Open();
                cmd = new SqlCommand(checkQuery, con);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    i++;
                    checkQuery = $"SELECT cName FROM courses WHERE cName = '{cName} ({i})'";
                }
                else
                {
                    cName = $"{cName} ({i})";
                    shouldRepeate = false;
                }
                con.Close();

            }
            

            String uploadQuery = $"INSERT INTO Courses(cId, mId, cName, cDescription) VALUES({ncId}, {mid}, '{cName}', '{cDescription}')";
                con.Open();
                cmd = new SqlCommand(uploadQuery, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

            
        }
        CoursesMenu courseMenu;
        private void button1_Click(object sender, EventArgs e)
        {
            createCourses(this.textBox1.Text, this.textBox2.Text);
            courseMenu = new CoursesMenu(mid);
            courseMenu.Show();
            this.Hide();
        }
    }
}
