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

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Btn_Verzameling_Verzameling(object sender, EventArgs e)
        {
            if (comboBoxDB.Text == "")
            {
                MessageBox.Show("Please select a collection", "No name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                VerzamelPage DB_Name = new VerzamelPage(comboBoxDB.Text);
                DB_Name.ShowDialog();

                Hide();
            }
        }



        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("Select * From Test", cnn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);


            using (SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True"))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", con))
                {
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        comboBoxDB.Items.Clear();
                        while (reader.Read())
                        {
                            comboBoxDB.Items.Add((string)reader["TABLE_NAME"]);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void debugInstructionsLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBoxDB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxDB.Text == "")
            {
                MessageBox.Show("Please enter a name for a collection to delete.", "No name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBoxDB.Items.Contains("" + textBoxDB.Text + ""))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete '" + textBoxDB.Text + "'", "No name", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    MessageBox.Show("'" + textBoxDB.Text + "' has not been deleted.", "Delete aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Succesfully Deleted " + textBoxDB.Text + "", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using (SqlConnection connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True"))
                    {
                        SqlCommand command = new SqlCommand("DROP TABLE [" + textBoxDB.Text + "]", connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        comboBoxDB.Items.Clear();
                        SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
                        SqlCommand cmd = new SqlCommand("Select * From Test", cnn);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);

                        using (SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True"))
                        {
                            con.Open();
                            using (SqlCommand com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", con))
                            {
                                using (SqlDataReader reader = com.ExecuteReader())
                                {
                                    comboBoxDB.Items.Clear();
                                    while (reader.Read())
                                    {
                                        comboBoxDB.Items.Add((string)reader["TABLE_NAME"]);
                                    }
                                }
                            }
                        }
                    }
                    textBoxDB.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No collection exist with that name.", "Wrong name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxDB_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBoxDB.Text == "")
            {
                MessageBox.Show("Please enter a name for your Collection!", "No name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBoxDB.Items.Contains("" + textBoxDB.Text + ""))
            {
                MessageBox.Show("That name already Exists!", "Existing Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                using (SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True"))
                {
                    try
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand("create table [" + textBoxDB.Text + "](ID int IDENTITY(1,1) not null,Titel nvarchar(max),Omschrijving nvarchar(max),Datum nvarchar(max));", con))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Collection succesfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBoxDB.Text = "";
            }
            comboBoxDB.Items.Clear();
            SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("Select * From Test", cnn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            using (SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True"))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", con))
                {
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        comboBoxDB.Items.Clear();
                        while (reader.Read())
                        {
                            comboBoxDB.Items.Add((string)reader["TABLE_NAME"]);
                        }
                    }
                }
            }
        }

        private void comboBoxDB_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
