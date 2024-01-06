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
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
namespace Baki
{
    public partial class Hasta : Form
    {
        public Hasta()
        {
            InitializeComponent();
        }
        int Id = 0;
        SqlConnection baglanti = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Belgeler\WonderDb.mdf;Integrated Security = True; Connect Timeout = 30; ");

        private void uyeler()

        {
            baglanti.Open();
            string query = "Select * from HastaTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            HastaDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        public void id()
        {
            baglanti.Open();



            SqlCommandBuilder builder = new SqlCommandBuilder();
            string query = "SELECT top 1 HId  FROM HastaTbl order by HId desc";

            using (SqlCommand command = new SqlCommand(query, baglanti))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // İlk satırdaki seri numarasını al
                        string a = reader["HId"].ToString();
                        int.TryParse(a, out int b);
                        b++;
                        Id_Text.Text = b.ToString();





                    }
                    else
                    {
                        MessageBox.Show("Test");
                    }
                }
            }





            baglanti.Close();


        }
        private void uyeekle()
        {
            baglanti.Open();

            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
            SqlCommand komut = new SqlCommand("insert into HastaTbl(HAd,HTelefon,HCinsiyet) values('" + HAdSoyadTb.Text + "' ,'" + HTelefonTb.Text + "'  ,   '" + HCinsiyetCb.SelectedItem.ToString() +  "')", baglanti);
            komut.ExecuteNonQuery();
            MessageBox.Show("Üye Bilgileri Eklendi");
            baglanti.Close();

        }
        private void Hasta_Load(object sender, EventArgs e)
        {
            uyeler();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {

            if (HAdSoyadTb.Text == "" || HTelefonTb.Text == "" || HCinsiyetCb.Text == "")
            {
                MessageBox.Show("Alanları Doldurun");
            }
            else
            {
                id();
                uyeekle();
                MessageBox.Show("Üye Kayıt Edilmiştir");

            }uyeler();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (Id == 0 || HAdSoyadTb.Text == "" || HTelefonTb.Text == "" || HCinsiyetCb.Text == "")
            {
                MessageBox.Show("Boş bırakılan yerler var!");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "update HastaTbl set HAd='" + HAdSoyadTb.Text + "',HTelefon ='" + HTelefonTb.Text + "',HCinsiyet='" + HCinsiyetCb + "'where HId=" + Id + ";";

                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Seçilen Üye Güncellendi");
                    baglanti.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void HastaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());

            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            HCinsiyetCb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                MessageBox.Show("Üye Seçin");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "delete from HastaTbl where HId=" + Id + ";";
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Seçilen Üye Silindi");
                    baglanti.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }
    }
}
