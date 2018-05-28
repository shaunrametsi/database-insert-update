using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace InsertAndUpdateData
{
    public partial class Form1 : Form
    {
        static OleDbConnection connection = new OleDbConnection();
        static OleDbCommand    command = new OleDbCommand();
        
        public Form1()
        {
            InitializeComponent();

            connection.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0;" +
                                          @"Data Source= ../data.accdb";
            command.Connection = connection ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet3.Gamer_Details' table. You can move, or remove it, as needed.
            this.gamer_DetailsTableAdapter2.Fill(this.dataDataSet3.Gamer_Details);
            // TODO: This line of code loads data into the 'dataDataSet2.Gamer_Details' table. You can move, or remove it, as needed.
            this.gamer_DetailsTableAdapter1.Fill(this.dataDataSet2.Gamer_Details);
            // TODO: This line of code loads data into the 'dataDataSet1.Gamer_Details' table. You can move, or remove it, as needed.
            this.gamer_DetailsTableAdapter.Fill(this.dataDataSet1.Gamer_Details);
            try
            {
                connection.Open();
                command.CommandText = "SELECT DISTINCT GENRE FROM Gamer_Details";
                
                OleDbDataReader read = command.ExecuteReader();
                while(read.Read())
                {
                  cmbGenre.Items.Add(read.GetString(0));
                }
                read.Close();

                command.CommandText = "SELECT DISTINCT GENDER FROM Gamer_Details";
                
                read = command.ExecuteReader();
                while(read.Read())
                {
                  cmbGender.Items.Add(read.GetString(0));
                }
                read.Close();


                
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                string[] info = textBox1.Text.Split(' ');
                string name = info[0];
                string surname = info[1];
                string prGame = textBox2.Text;
                string genre = cmbGenre.SelectedItem.ToString();
                string gender = cmbGender.SelectedItem.ToString();

                command.CommandText = "INSERT INTO Gamer_Details(First_Name, Last_Name, PrefGame, Genre, Gender)" + 
                                      " Values('" + name +  "', '" + surname +  "', '" + prGame +  "', '" + genre +  "', '" + gender +  "')";


                command.ExecuteNonQuery();

                command.CommandText = "SELECT COUNT(First_Name) FROM Gamer_Details";
                MessageBox.Show("Your Gamer ID is No." + command.ExecuteScalar().ToString());

            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                
                command.CommandText = "UPDATE Gamer_Details SET PrefGame = '" + textBox3.Text + "' WHERE First_Name = '" + comboBox3.Text + "'"; ;
                command.ExecuteNonQuery();

                
                MessageBox.Show("uPDATE successful");

            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.gamer_DetailsTableAdapter2.FillBy(this.dataDataSet3.Gamer_Details);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
