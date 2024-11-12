using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndmebaasTARpv23_V.A
{
    public partial class Form1 : Form
    {
        SqlConnection conn= new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Andmebaas;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public Form1()
        {
            InitializeComponent();
            NaitaAndmed();
        }
        public void NaitaAndmed()
        {
            conn.Open();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT * FROM Toode", conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void lisa_btn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("Insert into Toode(Nimetus, Kogus, Hind) Values (@toode,@kogus,@hind)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid");
            }
        }

        private void kustuta_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int deletedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Toode WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", deletedId);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();

                    MessageBox.Show("Kirje kustutatud");
                }
                catch (Exception)
                {
                    MessageBox.Show("Viga kustutamisel");
                }
            }
            else
            {
                MessageBox.Show("Valige kustutamiseks kirje");
            }
        }

        private void uuenda_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

