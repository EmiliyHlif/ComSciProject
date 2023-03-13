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
    public partial class LoginScreen : Form
    {
        public Menu mainMenu;
        public UserRegistration uReg;

        SqlConnection con = new SqlConnection(databaseCon.getCon());
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        SqlCommand cmd;
        SqlDataReader reader;
        String readQuery;

        bool userExist;

        public LoginScreen()
        {
            InitializeComponent();
        }

        public void userLogin(String username, String Password)
        {
            readQuery = $"SELECT mId FROM Logins WHERE Username = '{username}' AND Passwordhash = '{Password}'";
            cmd = new SqlCommand(readQuery, con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                    
                    mainMenu = new Menu(reader.GetInt32(0));
                    mainMenu.Show();
                    this.Hide();

                    
                    
                
            }
            if (this.Visible)
            {
                label3.Visible = true;
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userLogin(this.textBox1.Text, this.textBox2.Text);
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            uReg = new UserRegistration();
            
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
