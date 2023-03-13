using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace ComSciProject
{
    public partial class FlashcardsMenu : Form {

        public int mid;

        public EditFlashcardDialouge eFcDia;
        public FlashcardsAddDialouge fcDia;
        public Menu menu;
        public Flashcard selectedFlashcard = null;
        public Flashcard[] flashcardArray;

        public SqlCommand cmd;
        public SqlDataReader reader;
        public SqlConnection con = new SqlConnection(databaseCon.getCon());

        public FlashcardsMenu(int cmId)
        {
            mid = cmId;
            InitializeComponent();
            downloadFlashcards(mid);
        }

        void fillFlashcardArray(int mId, int flashcardAmount)
        {
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd;
            SqlDataReader reader;
            String readQuery;
            flashcardArray = new Flashcard[flashcardAmount];


            readQuery = $"SELECT fId FROM Flashcards WHERE mId = {mId}";
            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();
            int fcA = 0;
            while (reader.HasRows)
            {

                while (reader.Read())
                {
                    Flashcard tempFlashcard = new Flashcard(reader.GetInt32(0));
                    if(tempFlashcard != null)flashcardArray[fcA] = tempFlashcard;
                }
                reader.NextResult();
                fcA++;
            }
            con.Close();
        }

        public void downloadFlashcards(int mId)
        {
            int flashcardAmount = 0;
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand cmd;
            SqlDataReader reader;
            String readQuery;
            //downloads Flashcards into Arraylist

            #region get Flashcard amount
            readQuery = $"SELECT COUNT(fId) FROM Flashcards WHERE mId = {mId}";
            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                flashcardAmount = (int)reader.GetInt32(0);
            }
            con.Close();
            #endregion


            #region fill Questionlist GUI
            readQuery = $"SELECT Question, fId FROM flashcards WHERE mId = {mId}";

            
            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();
           for(int i = 0; i<flashcardAmount; i++)
            {
              
              while (reader.Read()) {
                    this.checkedListBox1.Items.Add(reader.GetInt32(1) + " | " + reader.GetString(0));
                   }
                reader.NextResult();
            }
            con.Close();
            #endregion

            #region fill flashcard Array
           fillFlashcardArray(mId, flashcardAmount);
           #endregion


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            menu = new Menu(mid);
            menu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedFlashcard != null)
            {
                eFcDia = new EditFlashcardDialouge();
                eFcDia.Show();
            }
        }

        public void deleteFlashcard(String[] Question)
        {
            
            for (int i = Question.Length -1; i > 1; i--)
            {
                if (i >= 0)
                {
                    String deleteQuery = $"DELETE FROM Flashcards WHERE Question = '{Question[i].Remove(1,3)}'";
                    cmd = new SqlCommand(deleteQuery, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public int getSelectedFlashcardId() {
            int selFId = 0;


            return selFId;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String[] checkedItemsList = new String[this.checkedListBox1.CheckedItems.Count];
            for (int i = 0; i<this.checkedListBox1.CheckedItems.Count; i++)
            {
                checkedItemsList[i] = this.checkedListBox1.CheckedItems[i].ToString();
                System.Diagnostics.Debug.WriteLine(checkedItemsList[i]);
            }

            deleteFlashcard(checkedItemsList);
        }
        

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)

        {
            fcDia = new FlashcardsAddDialouge(mid);
            this.Close();
            fcDia.Show();
        }

        private void FlashcardsMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
