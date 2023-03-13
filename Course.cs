using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;

namespace ComSciProject
{
    public class Course 
    {
        public Flashcard[] flashcards;
        public int creatorId;
        public int courseId;
        public String courseName;
        public String courseDesc;
        public List<Flashcard> caste1;
        public List<Flashcard> caste2;
        public int courseSize;

        public Course(int i)
        {
            caste1 = new List<Flashcard>();
            caste2 = new List<Flashcard>();
            loadCourseByCid(i);
            downloadFlashcards();
            sortFlashcards();

        }

        public void loadCourseByCid(int i)
        {
            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlCommand cmd;
            SqlDataReader reader;
            //load Course with id = i from database
            String ReadQuery = $"SELECT COUNT(cId) FROM Courses";
            cmd = new SqlCommand(ReadQuery, con);
            con.Open();

            reader = cmd.ExecuteReader();

            int courseCount = 0;
            while (reader.Read())
            {
                courseCount = reader.GetInt32(0);
            }
            con.Close();

            if(courseCount >= i)
            {
                courseId = i;
                #region loadCourseSize
                ReadQuery = $"SELECT COUNT(fId) FROM Flashcards WHERE cId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    courseSize = reader.GetInt32(0);
                }
                con.Close();
                #endregion

                #region loadCourseName
                ReadQuery = $"SELECT cName FROM courses WHERE cId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courseName = reader.GetString(0);
                }
                con.Close();
                #endregion

                #region loadCourseDescription
                ReadQuery = $"SELECT cDescription FROM courses WHERE cId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courseDesc = reader.GetString(0);
                }
                con.Close();
                #endregion

                #region loadMemberId
                ReadQuery = $"SELECT mId FROM courses WHERE cId = {i}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    creatorId = reader.GetInt32(0);
                }
                con.Close();
                #endregion
            }
        }

        public void downloadFlashcards()
        {
            int[] flashcardIds = new int[courseSize];

            SqlConnection con = new SqlConnection(databaseCon.getCon());
            SqlCommand cmd;
            SqlDataReader reader;
            //downloads Flashcards into Arraylist

            #region import flashcard ids into array
            
            for(int c = 0; c< flashcardIds.Length; c++)
            {

                String ReadQuery = $"SELECT fId FROM Flashcards WHERE cId = {courseId}";
                cmd = new SqlCommand(ReadQuery, con);
                con.Open();

                reader = cmd.ExecuteReader();

                
                while (reader.Read())
                {
                    flashcardIds[c] = reader.GetInt16(c);
                }
                con.Close();
            }
            #endregion

            #region load Flashcard ArrayList
            flashcards = new Flashcard[flashcardIds.Length];
            Flashcard f = new Flashcard(1);
            for(int b = 0; b < flashcardIds.Length; b++)
            {
                f.loadFlashcardByFid(flashcardIds[b]);
                flashcards[b] = f;
            }

            #endregion


        }


            

        public void sortFlashcards()
        {
            for(int i = 0; i<flashcards.Length; i++)
            {
                if (flashcards[i].box == true) {
                    caste1.Add(flashcards[i]);
                }
                else
                {
                    caste2.Add(flashcards[i]); 
                }
            }
        }
    }
}
