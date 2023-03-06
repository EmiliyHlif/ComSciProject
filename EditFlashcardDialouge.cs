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
    public partial class EditFlashcardDialouge : Form
    {
        public EditFlashcardDialouge()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load available courses
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update flashcard
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EditFlashcardDialouge_Load(object sender, EventArgs e)
        {

        }
    }
}
