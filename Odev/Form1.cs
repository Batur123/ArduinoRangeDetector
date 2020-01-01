using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
namespace Odev
{
    public partial class Form1 : Form
    {
        public SqlConnection baglanti;
        public SqlCommand komut;
        public SqlDataAdapter da;
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void getir()
        {
            baglanti = new SqlConnection("server=BATUR\\BATUR; Initial Catalog=mesafe;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM mesafe", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OperatorGrid();
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            label2.Text = "Bağlantı Kapalı";
            getir();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadExisting();
                label1.Text = sonuc;
            }
            catch (Exception msg)
            {

            }




            if (serialPort1.IsOpen == true)
            {
                string sorgu = "INSERT INTO mesafe (mesafe) VALUES (@mesafe)";
                komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@mesafe", label1.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                getir();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                serialPort1.BaudRate = 9600;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                label2.Text = "Bağlantı Açık";
                timer1.Start();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hatası:" + hata.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TextBoxTemizle(this);
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();

            }
        }

        #region Textbox'ları Temizleme Fonksiyonu
        public void TextBoxTemizle(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
                if (c.Controls.Count > 0)
                {
                    TextBoxTemizle(c);
                }
            }
        }
        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }


        public void guncelle()
        {
            //bool result = false;
            //using (var baglanti = Veritabani.GetConnection())
            //{
            //    baglanti.Open();
            //    string secmeSorgusu = "SELECT * from mesafe where id=@id";
            //    SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, baglanti);
            //    secmeKomutu.Parameters.AddWithValue("@id", textBox1.ToString());
            //    SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
            //    SqlDataReader dr = secmeKomutu.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        string id = dr["id"].ToString();
            //        string urunadkod = dr["mesafe"].ToString();

            //        dr.Close();
            //        DialogResult durum = MessageBox.Show(id + " numaralı " + urunadkod + " santimetre olarak kayıtlı mesafeyi güncellemek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
            //        if (DialogResult.Yes == durum)
            //        {

            //            string kayit = "UPDATE mesafe SET mesafe=@mesafed where id=@id";
            //            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //            komut.Parameters.AddWithValue("@mesafe", textBox2.ToString());
            //            komut.ExecuteNonQuery();

            //            MessageBox.Show("Mesafe veritabanı bilgileri başarıyla güncellendi.");

            //        }

            //        baglanti.Close();


            //    }
            //    else
            //    {
            //        MessageBox.Show("Böyle bir mesafe ölçümü bulunamadı.");
            //    }

            //    return result;
            //}
        }

        SqlConnection cnn = new SqlConnection("server=BATUR\\BATUR; Initial Catalog=mesafe;Integrated Security=SSPI");

        private void guncbuton_Click(object sender, EventArgs e)
        {
            

            if (!string.IsNullOrEmpty(textBox1.Text)) // ID'nin Boş olup olmadığının kontrolü
            {
                string sql = "update mesafe set mesafe = '" + textBox2.Text + "' where id = '" + Convert.ToInt32(textBox1.Text) + "' ";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                if (cnn.State != ConnectionState.Open) // bağlantı açık değilse açtırıyoruz.
                {
                    cnn.Open();
                }
                cmd.ExecuteNonQuery(); // sql sorgusunu işleme koyuyoruz.
                cnn.Close(); // bağlantıyı kapatıyoruz
            }

            //    try
            //{
            //    SqlConnection sql_con;


            //    SqlCommand sql_cmd;




            //    string sqlPath = "server=BATUR\\BATUR; Initial Catalog=mesafe;Integrated Security=SSPI";

            //    // Komut isminde bir string değişken tanımlıyoruz.

            //    string komut = "";

            //    //  oluşturduğumuz bağlatıya SqlConnection bağlantımıza server adresini tanımlıyoruz.

            //    sql_con = new SqlConnection(sqlPath);

            //    // oluşturduğumuz SQL komutumuza komutumuzu ve bağlantımızı tanımlıyoruz

            //    sql_cmd = new SqlCommand(komut, sql_con);

            //    // Bağlatımızı açıyoruz

            //    sql_con.Open();
            //    komut = "UPDATE mesafe SET mesafe=@mesafe where id=@id";

            //    SqlParameterCollection param = sql_cmd.Parameters;
            //    param.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
            //    param.AddWithValue("@mesafe", textBox2.Text);





            //    // Çalıştırılmak istenen SQL sorgusu
            //    sql_cmd.CommandText = komut;

            //    // SqlCommand ın kullanacağı bağlantı

            //    sql_cmd.Connection = sql_con;

            //    // Çalıştırılıcak komutun türü sql sorgusu yada türü

            //    sql_cmd.CommandType = CommandType.Text;

            //    // Bağlantımızı kapatıyoruz

            //    sql_con.Close();

            //    //   guncelle();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Hata: " + ex);
            //}

            TextBoxTemizle(this);
            OperatorGrid();

        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)) // ID'nin Boş olup olmadığının kontrolü yapıyoruz yine yukarılarda ID isimli bir string değer tanımlanmış olmalıdır.
            {
                string sql = "delete from mesafe where id = '" + Convert.ToInt32(textBox1.Text) + "' ";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                if (cnn.State != ConnectionState.Open) // Bağlantı açık değilse açtırıyoruz.
                {
                    cnn.Open();
                }
                cmd.ExecuteNonQuery(); // Silme işlemini yapan komut
                cnn.Close(); // bağlantıyı kapat
            }

            TextBoxTemizle(this);
            OperatorGrid();
        }

        private void OperatorGrid()
        {

            try
            {
           //     using (var connection = Veritabani.GetConnection())
           //     {
                    var connection = "server=BATUR\\BATUR; Initial Catalog=mesafe;Integrated Security=SSPI";

                    // WHERE KullaniciAdi='" + KullaniciAdi + "'and Sifre='" + Sifre + "'"


                    var select = "SELECT * FROM mesafe";

                    var dataAdapter = new SqlDataAdapter(select, connection);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    dataGridView1.ReadOnly = true;
                    dataGridView1.DataSource = ds.Tables[0];

                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Columns[0].HeaderCell.Value = "ID";
                    dataGridView1.Columns[1].HeaderCell.Value = "Mesafe Ölçümü(Santimetre)";


                    dataGridView1.Columns[0].Width = 150;
                    dataGridView1.Columns[1].Width = 150;

           //     }
            }
            catch (Exception msg)
            {
                MessageBox.Show("Veritabanı bağlantınız kurulamadı veya sistemde teknik bir hata oluştu. Lütfen programı kapatıp açınız." + "\n" + msg);
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            OperatorGrid();
        }
    }
}

