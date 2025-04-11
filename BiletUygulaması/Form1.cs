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

namespace BiletUygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-UHPV4QF\SQLEXPRESS01;Initial Catalog=DbBilet;Integrated Security=True");
        void seferListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLSEFER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLYOLCU (AD,SOYAD,TELEFON,TC,CINSIYET,MAIL) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yolcu Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnKaptan_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKAPTAN (KAPTANNO,ADSOYAD,TELEFON) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKaptanNo.Text);
            komut.Parameters.AddWithValue("@p2", txtKaptanAd.Text);
            komut.Parameters.AddWithValue("@p3", mskKaptanTelefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaptan Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSeferOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFER (KALKIS,VARIS,TARIH,SAAT,KAPTAN,FIYAT) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKalkis.Text);
            komut.Parameters.AddWithValue("@p2", txtVaris.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", mskKaptan.Text);
            komut.Parameters.AddWithValue("@p6", txtFiyat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sefer Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            seferListesi();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            seferListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtSeferNumara.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtKoltukNo.Text = "9";
        }

        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select TC FROM TBLYOLCU WHERE TC=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", mskYolcuTC.Text);
            SqlDataReader dr2=komut2.ExecuteReader();
            if (dr2.Read())
            {
                dr2.Close();
                if (mskYolcuTC.Text.Length == 11 && mskYolcuTC.Text != "")
                {
                    SqlCommand komut = new SqlCommand("insert into TBLSEFERDETAY (SEFERNO,YOLCUTC,KOLTUK) values (@p1,@p2,@p3)", baglanti);
                    komut.Parameters.AddWithValue("@p1", txtSeferNumara.Text);
                    komut.Parameters.AddWithValue("@p2", mskYolcuTC.Text);
                    komut.Parameters.AddWithValue("@p3", txtKoltukNo.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Koltuk rezervasyonu yapıldı.");
                }
                else
                {
                    MessageBox.Show("TC no eksik lütfen 11 hane giriniz.");
                }
            }
            else
            {
                MessageBox.Show("TC sistemde kayıtlı değil.");
            }
            baglanti.Close();
        }
    }
}