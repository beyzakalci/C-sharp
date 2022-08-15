using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.Data.OleDb kütüphanesinin eklenmesi

using System.Data.OleDb;

namespace otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Veritabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        //Formlar arası veri aktarımında kullanılacak değişkenler

        public static string tcno, adi, soyadi, yetki;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar= char.Parse ("*");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                if (radioButton1.Checked == true)
                {
                    if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text &&
                        kayitokuma["yetki"].ToString() == "Yönetici")
                    {
                        durum = true;
                        tcno = kayitokuma.GetValue(0).ToString();
                        adi = kayitokuma.GetValue(1).ToString();
                        soyadi = kayitokuma.GetValue(2).ToString();
                        yetki = kayitokuma.GetValue(3).ToString();
                        this.Hide();
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        break;


                    }
                }
                if (radioButton2.Checked == true)
                {
                    if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text &&
                        kayitokuma["yetki"].ToString() == "Kullanıcı")
                    {
                        durum = true;
                        tcno = kayitokuma.GetValue(0).ToString();
                        adi = kayitokuma.GetValue(1).ToString();
                        soyadi = kayitokuma.GetValue(2).ToString();
                        yetki = kayitokuma.GetValue(3).ToString();
                        this.Hide();
                        Form3 frm3 = new Form3();
                        frm3.Show();
                        break;



                    }
                }
                
            }
            

        }

        //Yerel yani yalnızca bu formda geçerli olacak değişkenler

        bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1;
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen; //Gelecek ekranın ortada gelmesini sağlar
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}
