using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kursovaya_ShD
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            string connectString = "Data Source=DESKTOP-88P1P3S;Initial Catalog=Kursovaya_ShD;" + "Integrated Security=True;";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query_M = "SELECT * FROM Tovar_spisok";
            SqlCommand command = new SqlCommand(query_M, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();
            myConnection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
