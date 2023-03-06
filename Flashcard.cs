using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ComSciProject
{
    public class Flashcard
    {
        public int flashcardId;
        public int memId;
        public int couId;
        public String front;
        public String back;
        public bool box;
        public Flashcard(int i) {
            loadFlashcardByFid(i);
         }

        public void loadFlashcardByFid(int i)
        {
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlCommand cmd;
            SqlDataReader reader;
            //load flashcard with id = i from database
            String ReadQuery = $"SELECT COUNT(fId) FROM Flashcards";
            
            
                flashcardId = i;

                #region loadfront
                ReadQuery = $"SELECT Question FROM Flashcards WHERE fId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    front = reader.GetString(0);
                }
                con.Close();
                #endregion

                #region loadBack
                ReadQuery = $"SELECT Answer FROM Flashcards WHERE fId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    back = reader.GetString(0);
                }
                con.Close();
                #endregion

                #region loadBox
                ReadQuery = $"SELECT Box FROM Flashcards WHERE fId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                   if(!reader.IsDBNull(0)) box = reader.GetBoolean(0);
                }
                con.Close();
                #endregion

                #region loadMemId

                ReadQuery = $"SELECT mId FROM Flashcards WHERE fId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    memId = reader.GetInt32(0);
                }

                con.Close();
                #endregion

                #region loadCouId

                ReadQuery = $"SELECT cId FROM Flashcards WHERE fId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if(!reader.IsDBNull(0)) couId = reader.GetInt32(0);
                }

                con.Close();
                #endregion
            
            
        }


    }
}
