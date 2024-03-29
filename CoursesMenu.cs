﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComSciProject
{
    public partial class CoursesMenu : Form

    {

        public QuizForm quiz;
        public Menu menu;
        public CourseCreateDialouge cadd;
        public int mid;

        public CoursesMenu(int cmId)
        {
            mid = cmId;
            InitializeComponent();
            downloadCourses(mid);
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
                    this.checkedListBox1.Items.Add(reader.GetString(0));
                }
                reader.NextResult();
            }
            con.Close();
            #endregion

            #region fill flashcard Array
            //fillCourseArray(mId, courseAmount);
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

        

        private void button1_Click(object sender, EventArgs e)
        {
            cadd = new CourseCreateDialouge(mid);
            this.Hide();
            cadd.Show();        
        }

        private void CoursesMenu_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedItems.Count > 1 || this.checkedListBox1.SelectedItems.Count == 0) {
                
                label2.Visible = true;
                return;
            }
            else
            {
             

                String readQuery;
                int courseId = 0;

                SqlConnection con = new SqlConnection(databaseCon.getCon());
                SqlCommand cmd;
                SqlDataReader reader;

                readQuery = $"SELECT cId FROM Courses WHERE cName = '{this.checkedListBox1.SelectedItem.ToString()}'";
                cmd = new SqlCommand(readQuery, con);
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courseId = (int)reader.GetInt32(0);
                }
                con.Close();

                if (courseId <= 0) label2.Text = "no course found with that ID.";
                else
                {
                    quiz = new QuizForm(courseId, false);
                    this.Hide();
                    quiz.Show();
                }
            }
        }
    }
}
