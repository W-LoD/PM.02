using System;
using System.Windows.Forms;

namespace Kursovaya_ShD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button1_KeyUp);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Сотрудник" && textBox1.Text == "12345")
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else if (comboBox1.Text == "Покупатель")
            {
                this.Hide();
                Form3 form3 = new Form3();
                form3.Show();
            }
            else
            {
                label2.Text = "Пароль не подходит";
                label2.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
           textBox1.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                button1.PerformClick();
        }
    }
}
