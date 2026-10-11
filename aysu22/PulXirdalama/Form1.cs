using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PulXirdalama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Şəkilləri yükləyirik
            SekilleriYukle();

            // İlkin olaraq şəkildəki kimi bütün əskinaslar və label1..label8 göstərilir
            IlkinGorunusuQur();
        }

        /// <summary>
        /// Əskinas şəkillərini Resources qovluğundan yükləyir
        /// </summary>
        private void SekilleriYukle()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string resDir = Path.Combine(baseDir, "Resources");

                // Əgər bin/Debug daxilindədirsə və tapılmasa, layihə qovluğuna baxırıq
                if (!Directory.Exists(resDir))
                {
                    resDir = Path.Combine(baseDir, "..", "..", "Resources");
                }

                pictureBox1.Image = SekilGetir(resDir, "azn_1.png");
                pictureBox2.Image = SekilGetir(resDir, "azn_5.png");
                pictureBox3.Image = SekilGetir(resDir, "azn_10.png");
                pictureBox4.Image = SekilGetir(resDir, "azn_20.png");
                pictureBox5.Image = SekilGetir(resDir, "azn_50.png");
                pictureBox6.Image = SekilGetir(resDir, "azn_100.png");
                pictureBox7.Image = SekilGetir(resDir, "azn_200.png");
                pictureBox8.Image = SekilGetir(resDir, "azn_500.png");
            }
            catch
            {
                // Şəkillər yüklənməsə belə tətbiqin işi dayanmır
            }
        }

        private Image SekilGetir(string dir, string fileName)
        {
            string fullPath = Path.Combine(dir, fileName);
            if (File.Exists(fullPath))
            {
                return Image.FromFile(fullPath);
            }
            return null;
        }

        /// <summary>
        /// Şəkildəki kimi ilkin vəziyyətdə bütün əskinaslar və label1..label8 göstərilir
        /// </summary>
        private void IlkinGorunusuQur()
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            pictureBox8.Visible = true;

            label1.Visible = true; label1.Text = "label1";
            label2.Visible = true; label2.Text = "label2";
            label3.Visible = true; label3.Text = "label3";
            label4.Visible = true; label4.Text = "label4";
            label5.Visible = true; label5.Text = "label5";
            label6.Visible = true; label6.Text = "label6";
            label7.Visible = true; label7.Text = "label7";
            label8.Visible = true; label8.Text = "label8";
        }

        /// <summary>
        /// Bütün əskinas və label-ləri gizlədir
        /// </summary>
        private void EskinaslariSifirla()
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
        }

        /// <summary>
        /// İstifadəçi məbləğ daxil edərkən ErrorProvider xəbərdarlığını təmizləyir
        /// </summary>
        private void txtMebleg_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMebleg.Text))
            {
                errorProvider1.SetError(txtMebleg, "");
            }
        }

        /// <summary>
        /// Xırdala düyməsi basıldıqda məbləğin ən böyük nominallarla xırdalanması
        /// </summary>
        private void btnXirdala_Click(object sender, EventArgs e)
        {
            // 1. Xananın boş olub-olmamasının ErrorProvider ilə yoxlanması
            if (string.IsNullOrWhiteSpace(txtMebleg.Text))
            {
                errorProvider1.SetError(txtMebleg, "Məbləğ daxil edin");
                EskinaslariSifirla();
                return;
            }
            else
            {
                // Məbləğ daxil edildikdən sonra errorprovider öz işini yekunlaşdırır
                errorProvider1.SetError(txtMebleg, "");
            }

            // 2. Düzgün rəqəm (tam ədəd) olub-olmamasının yoxlanması
            if (!int.TryParse(txtMebleg.Text.Trim(), out int mebleg))
            {
                MessageBox.Show("Zəhmət olmasa düzgün tam ədəd daxil edin!", "Diqqət", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Mənfi və ya sıfır məbləğ yoxlanışı
            if (mebleg <= 0)
            {
                EskinaslariSifirla();
                MessageBox.Show("Mənfi və ya sıfır məbləğ xırdalanmaz", "Diqqət", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // İlkin olaraq əvvəlki nəticələri gizlədirik
            EskinaslariSifirla();

            // 4. Ən böyük nominallardan başlayaraq xırdalama alqoritmi
            int qaliq = mebleg;

            // 500 AZN
            int say500 = qaliq / 500;
            qaliq %= 500;

            // 200 AZN
            int say200 = qaliq / 200;
            qaliq %= 200;

            // 100 AZN
            int say100 = qaliq / 100;
            qaliq %= 100;

            // 50 AZN
            int say50 = qaliq / 50;
            qaliq %= 50;

            // 20 AZN
            int say20 = qaliq / 20;
            qaliq %= 20;

            // 10 AZN
            int say10 = qaliq / 10;
            qaliq %= 10;

            // 5 AZN
            int say5 = qaliq / 5;
            qaliq %= 5;

            // 1 AZN
            int say1 = qaliq / 1;
            qaliq %= 1;

            // 5. Yalnız lazım olan əskinasların və onların saylarının göstərilməsi
            // (Hansı əskinas istifadə olunarsa onun özü və qarşısındakı sayı aktiv olur)

            // 1 AZN -> PictureBox1 və Label1
            if (say1 > 0)
            {
                pictureBox1.Visible = true;
                label1.Visible = true;
                label1.Text = say1.ToString();
            }

            // 5 AZN -> PictureBox2 və Label2
            if (say5 > 0)
            {
                pictureBox2.Visible = true;
                label2.Visible = true;
                label2.Text = say5.ToString();
            }

            // 10 AZN -> PictureBox3 və Label3
            if (say10 > 0)
            {
                pictureBox3.Visible = true;
                label3.Visible = true;
                label3.Text = say10.ToString();
            }

            // 20 AZN -> PictureBox4 və Label4
            if (say20 > 0)
            {
                pictureBox4.Visible = true;
                label4.Visible = true;
                label4.Text = say20.ToString();
            }

            // 50 AZN -> PictureBox5 və Label5
            if (say50 > 0)
            {
                pictureBox5.Visible = true;
                label5.Visible = true;
                label5.Text = say50.ToString();
            }

            // 100 AZN -> PictureBox6 və Label6
            if (say100 > 0)
            {
                pictureBox6.Visible = true;
                label6.Visible = true;
                label6.Text = say100.ToString();
            }

            // 200 AZN -> PictureBox7 və Label7
            if (say200 > 0)
            {
                pictureBox7.Visible = true;
                label7.Visible = true;
                label7.Text = say200.ToString();
            }

            // 500 AZN -> PictureBox8 və Label8
            if (say500 > 0)
            {
                pictureBox8.Visible = true;
                label8.Visible = true;
                label8.Text = say500.ToString();
            }
        }
    }
}
