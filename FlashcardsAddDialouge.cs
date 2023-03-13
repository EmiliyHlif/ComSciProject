using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ComSciProject
{
    public partial class FlashcardsAddDialouge : Form
    {
        
        int mId;
        FlashcardsMenu flashcardMenu;
        public FlashcardsAddDialouge(int id)
        {

            mId = id;
            InitializeComponent();
            downloadCourses(mId);
        }


        public void downloadCourses(int mId)
        {
            int courseAmount = 0;
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd;
            SqlDataReader reader;
            String readQuery;
            //downloads Flashcards into Arraylist


            #region get course amount
            readQuery = $"SELECT COUNT(cId) FROM Courses WHERE mId = {mId}";
            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                courseAmount = (int)reader.GetInt32(0);
            }
            con.Close();
            #endregion


            #region fill course list GUI
            readQuery = $"SELECT cName, cid FROM Courses WHERE mId = {mId}";


            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();
            for (int i = 0; i < courseAmount; i++)
            {

                while (reader.Read())
                {
                    this.comboBox1.Items.Add(reader.GetString(0));
                }
                reader.NextResult();
            }
            con.Close();
            #endregion

            #region fill flashcard Array
            //fillCourseArray(mId, courseAmount);
            #endregion


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void uploadFlashcard(String qString, String aString) {
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd;
            SqlDataReader reader;
            int nfId = -1;
            int cmId = -1;

            #region set new flashcard ID

            String countFIDQuery = $"SELECT COUNT(fId) FROM Flashcards";
            con.Open();
            cmd = new SqlCommand(countFIDQuery, con);
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                nfId = reader.GetInt32(0);
                nfId++;
            }
            con.Close();

            #endregion

            #region get cid
            String cidQuery = $"Select cId from courses where cName = '{comboBox1.SelectedItem.ToString()}'";
            int cId = 0;
            con.Open();
            cmd = new SqlCommand(cidQuery, con);
            reader = cmd.ExecuteReader();
            while(reader.Read())cId = reader.GetInt32(0);
            con.Close();

            #endregion

            String uploadQuery;
            if (comboBox1.SelectedItem == null) uploadQuery = $"INSERT INTO flashcards(fId, mId, Question, Answer) VALUES({nfId}, {mId}, '{qString}', '{aString}')";
            else uploadQuery = $"Insert into flashcards(fId, mId, Question, Answe, cId) Values({nfId}, {mId}, '{qString}', '{aString}, {cId})";
            con.Open();
            cmd = new SqlCommand(uploadQuery, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //upload Flashcard to database
            uploadFlashcard(this.textBox1.Text, this.textBox2.Text);
            flashcardMenu = new FlashcardsMenu(mId);
            flashcardMenu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            flashcardMenu = new FlashcardsMenu(mId);
            flashcardMenu.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FlashcardsAddDialouge_Load(object sender, EventArgs e)
        {

        }
    }
}
