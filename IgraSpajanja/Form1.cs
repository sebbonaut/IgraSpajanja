using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IgraSpajanja
{
    public partial class Form1 : Form
    {
        public static int vrijeme = 700;
        Random random = new Random();
        Label prvaOdabrana = null, drugaOdabrana = null;
        /// <summary>
        /// Svaku ikonu iz liste stavimo kao tekst nekoj labeli.
        /// </summary>
        private void RaspodijeliIkone()
        {
            List<string> ikone = new List<string>()
            {
                "c", "c", "!", "!", "N", "N", "b", "b",
                "v", "v", "w", "w", "z", "z", ",", ","
            };
            tableLayoutPanel1.SuspendLayout();
            foreach (Control kontrola in tableLayoutPanel1.Controls)
            {
                if (kontrola is Label labela)
                {
                    int randomBroj = random.Next(ikone.Count);
                    labela.Text = ikone[randomBroj];
                    labela.ForeColor = labela.BackColor;
                    ikone.RemoveAt(randomBroj);
                }
            }
            tableLayoutPanel1.ResumeLayout(false);
        }
        public Form1()
        {
            InitializeComponent();
            RaspodijeliIkone();
        }

        private void ProvjeriPobjedu()
        {
            foreach (Control kontrola in tableLayoutPanel1.Controls)
                if (kontrola is Label labela && labela.ForeColor
                    == labela.BackColor)
                    return;
            if (MessageBox.Show("Želite li opet igrati?", "Čestitamo!",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                    RaspodijeliIkone();
            else
                Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            prvaOdabrana.ForeColor = prvaOdabrana.BackColor;
            drugaOdabrana.ForeColor = drugaOdabrana.BackColor;
            prvaOdabrana = drugaOdabrana = null;
        }

        private void uputeZaIgruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pronađite sve parove ikona!",
                "Cilje igre");
        }

        private void postavkeIgreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        void Oboji(Color boja)
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.BackColor = boja;
            foreach (Control kontrola in tableLayoutPanel1.Controls)
            {
                if(kontrola is Label labela)
                {
                    labela.BackColor = boja;
                    if(labela.ForeColor != Color.Black)
                    {
                        labela.ForeColor = labela.BackColor;
                    }
                }
            }
            tableLayoutPanel1.ResumeLayout(false);
        }
        private void odabirBojeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (colorDialog1.Color == Color.Black)
                    MessageBox.Show("Izaberite neku drugu!");
                else
                    Oboji(colorDialog1.Color);
            }
        }

        private void promijeniVrijemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        /// <summary>
        /// Kod za obradu svakog događaja klika na neku labelu.
        /// </summary>
        /// <param name="sender">Labela koja je kliknuta.</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            if (sender is Label labela && timer1.Enabled == false)
            {
                if (labela.ForeColor == labela.BackColor)
                {
                    if (prvaOdabrana == null)
                    {
                        prvaOdabrana = labela;
                        labela.ForeColor = Color.Black;
                    }
                    else
                    {
                        drugaOdabrana = labela;
                        drugaOdabrana.ForeColor = Color.Black;
                        ProvjeriPobjedu();
                        if (prvaOdabrana.Text == drugaOdabrana.Text)
                            prvaOdabrana = drugaOdabrana = null;
                        else
                        {
                            timer1.Interval = vrijeme;
                            timer1.Start();
                        }
                    }
                }
            }
        }
    }
}
