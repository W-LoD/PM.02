using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kursovaya_ShD
{
    public partial class Form1 : Form
    {
        int count_records = 1;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            string connectString = "Data Source=DESKTOP-88P1P3S;Initial Catalog=Kursovaya_ShD;" + "Integrated Security=True;";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query_M = "SELECT * FROM new_pokupka";
            SqlCommand command = new SqlCommand(query_M, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = comboBox1.Items[(int)reader[0] - 2].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = comboBox3.Items[(int)reader[2] - 2].ToString();
                data[data.Count - 1][3] = comboBox2.Items[(int)reader[3] - 2].ToString();
                count_records++;
            }
            reader.Close();
            myConnection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-88P1P3S;Initial Catalog=Kursovaya_ShD;" + "Integrated Security=True;");
            try
            {
                List<string[]> data = new List<string[]>();
                data.Add(new string[4]);
                data[data.Count - 1][0] = comboBox1.Text;
                data[data.Count - 1][1] = numericUpDown1.Text;
                data[data.Count - 1][2] = comboBox3.Text;
                if (data[data.Count - 1][0] != "" && data[data.Count - 1][1] != "0" && data[data.Count - 1][2] != "")
                {
                    int current_id = comboBox1.SelectedIndex + 2;
                    int current_kolvo = (int)numericUpDown1.Value;
                    int pokupatel_id = comboBox3.SelectedIndex + 2;
                    data[data.Count - 1][3] = comboBox2.Items[current_id-2].ToString();
                    foreach (string[] s in data)
                        dataGridView1.Rows.Add(s);
                    string query = "set identity_insert Pokupka on;" +
                        "INSERT INTO Pokupka (Pokupka_ID, ID_M, ID_S, ID_P, ID_T, Kolvo, ID_Pok)" +
                        " VALUES (@Pokupka_ID, @ID_M, @ID_S, @ID_P, @ID_T, @Kolvo, @ID_Pok)" +
                        "set identity_insert Pokupka off;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Pokupka_ID", count_records++);
                    command.Parameters.AddWithValue("ID_M", current_id);
                    command.Parameters.AddWithValue("ID_S", current_id);
                    command.Parameters.AddWithValue("ID_P", current_id);
                    command.Parameters.AddWithValue("ID_T", current_id);
                    command.Parameters.AddWithValue("Kolvo", current_kolvo);
                    command.Parameters.AddWithValue("ID_Pok", pokupatel_id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                    MessageBox.Show("Заказ добавлен", "Все прошло успешно!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else MessageBox.Show("Введите данные");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-88P1P3S;Initial Catalog=Kursovaya_ShD;" + 
                "Integrated Security=True;");
            try
            {
                DialogResult dr = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Cancel)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows) 
                    {
                        int a = row.Index + 2;
                        string query = " delete Pokupka where Pokupka_ID = " + a;
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        dataGridView1.Rows.Remove(row); 
                    }    
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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
