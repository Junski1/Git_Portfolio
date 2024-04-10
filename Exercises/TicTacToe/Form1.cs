using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool vuoro = true;
        int vuoroMaara = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void PlayerClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (vuoro)
                b.Text = "X";
            else
                b.Text = "O";

            vuoro = !vuoro;
            b.Enabled = false;
            vuoroMaara++;

            CheckforWinner();
        }

        private void CheckforWinner()
        {
            bool voittaja= false;

            //vaaka
            if (A1.Text == A2.Text && A2.Text == A3.Text&& !A1.Enabled)
                voittaja = true;
            
            else if(B1.Text == B2.Text && B2.Text == B3.Text && !B1.Enabled)
                voittaja = true;

            else if(C1.Text == C2.Text && C2.Text == C3.Text && !C1.Enabled)
                voittaja = true;

            //pysty
            if (A1.Text == B1.Text && B1.Text == C1.Text && !A1.Enabled)
                voittaja = true;

            else if (A2.Text == B2.Text && B2.Text == C2.Text && !A2.Enabled)
                voittaja = true;

            else if (A3.Text == B3.Text && B3.Text == C3.Text && !A3.Enabled)
                voittaja = true;

            //risti
            if (A1.Text == B2.Text && B2.Text == C3.Text && !A1.Enabled)
                voittaja = true;

            else if (A3.Text == B2.Text && B2.Text == C1.Text && !C1.Enabled)
                voittaja = true;

            if (voittaja != true)
            {
                if (vuoroMaara == 9)
                    MessageBox.Show("Tasapeli!");

                return;
            }

            DisableButtons();

            string voittoPelaaja = "";

            if (vuoro)
                voittoPelaaja = "pelaaja 2";
            else
                voittoPelaaja = "pelaaja 1";

            MessageBox.Show("Voitto tuli nytten pelaajalle " + voittoPelaaja);
        }

        private void DisableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }

        private void UusiPeliToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Restart();
        }

        private void LopetaPeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Application.Exit();
            
        }
    }
}
