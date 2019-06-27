using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DesktopApp1
{
    public partial class VerzamelPage : Form
    {
        public VerzamelPage(string Tbl_Name)
        {
            InitializeComponent();
            textBox4.Text = Tbl_Name;
        }
		DataTable dt;
		//De terug-knop
		private void VerzamelPage_terug(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }

        //Zorgt ervoor dat het kruisje rechtbovenin daadwerkelijk de app afsluit
        private void VerzamelPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
		
		private void VerzamelPage_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
			comboBox1.SelectedIndex = 0;

			SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("Select * From [" + textBox4.Text + "]", cnn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ID"].Visible = false;
        }

        public void Btn_toevoegen_Click(object sender, EventArgs e)
        {

            if(textBox1.Text == "")
            {
                MessageBox.Show("Please enter a title!", "No title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if(textBox2.Text == "")
            {
                MessageBox.Show("Please enter a description!", "No description", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
                cnn.Open();
                SqlCommand cmd = new SqlCommand("Insert Into [" + textBox4.Text + "] (Titel,Omschrijving,Datum) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Text + "')", cnn);
                cmd.ExecuteNonQuery();
                VerzamelPage_Load(sender, e);

                textBox1.Text = "";
                textBox2.Text = "";
            }


            /*
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
            dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
            dataGridView1.Rows[n].Cells[2].Value = dateTimePicker1.Text;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Items";
            dt.Columns.Add("Titel");
            dt.Columns.Add("Omschrijving");
            dt.Columns.Add("Datum");
            ds.Tables.Add(dt);

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                DataRow row = ds.Tables["Items"].NewRow();
                row["Titel"] = r.Cells[0].Value;
                row["Omschrijving"] = r.Cells[1].Value;
                row["Datum"] = r.Cells[2].Value;
                ds.Tables["Items"].Rows.Add(row);
            }
            ds.WriteXml("C:\\Users\\HP\\source\\repos\\Collectors-Edition\\DesktopApp1\\DesktopApp1\\Data.xml");
       */
        }

        private void Btn_verwijderen_Click(object sender, EventArgs e)
        {

            SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
            if (dataGridView1.SelectedRows.Count == 0)
            {

                    MessageBox.Show("No record selected to delete");

                }
                else
                {
                    DialogResult del = MessageBox.Show("Are you sure you want to delete " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + " ", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (del == DialogResult.Yes)
                    {
                        cnn.Open();

                        SqlCommand cmd = new SqlCommand("DELETE from [" + textBox4.Text + "] WHERE (ID='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "')", cnn);
                       int  j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            MessageBox.Show("Record deleted successfully!");

                        }
                    VerzamelPage_Load(sender, e);
                        cnn.Close();

                    }
                    else
                    {
                        this.Show();

                    }
                }
          





            /*
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowIndex);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Items";
            dt.Columns.Add("Titel");
            dt.Columns.Add("Omschrijving");
            dt.Columns.Add("Datum");
            ds.Tables.Add(dt);

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                DataRow row = ds.Tables["Items"].NewRow();
                row["Titel"] = r.Cells[0].Value;
                row["Omschrijving"] = r.Cells[1].Value;
                row["Datum"] = r.Cells[2].Value;
                ds.Tables["Items"].Rows.Add(row);
            }
            ds.WriteXml("C:\\Users\\HP\\source\\repos\\Collectors-Edition\\DesktopApp1\\DesktopApp1\\Data.xml");
        */
        }

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
        
		private void textBox3_TextChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text == "All")
			{
				SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
				SqlCommand cmd = new SqlCommand("Select * From [" + textBox4.Text + "] WHERE Titel like'%" + textBox3.Text + "%' OR Omschrijving like'%" + textBox3.Text + "%' OR Datum like'%" + textBox3.Text + "%'", cnn);
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				dt = new DataTable();
				sda.Fill(dt);
				dataGridView1.DataSource = dt;
			}
			else if (comboBox1.Text == "Title")
			{
				SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
				SqlCommand cmd = new SqlCommand("Select * From [" + textBox4.Text + "] where Titel like '%" + textBox3.Text + "%'", cnn);
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				dt = new DataTable();
				sda.Fill(dt);
				dataGridView1.DataSource = dt;
			}
			else if (comboBox1.Text == "Description")
			{
				SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
				SqlCommand cmd = new SqlCommand("Select * From [" + textBox4.Text + "] where Omschrijving like '%" + textBox3.Text + "%'", cnn);
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				dt = new DataTable();
				sda.Fill(dt);
				dataGridView1.DataSource = dt;
			}
			else if (comboBox1.Text == "Date")
			{
				SqlConnection cnn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename ='|DataDirectory|\\Database1.mdf'; Integrated Security = True");
				SqlCommand cmd = new SqlCommand($"Select * From [" + textBox4.Text + "] where Datum like '%{textBox3.Text}%'", cnn);
				SqlDataAdapter sda = new SqlDataAdapter(cmd);
				dt = new DataTable();
				sda.Fill(dt);
				dataGridView1.DataSource = dt;
			}
		}

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

    }



