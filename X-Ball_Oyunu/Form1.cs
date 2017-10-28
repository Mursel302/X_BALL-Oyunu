using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace X_Ball_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int level = 1, qutuSayi = 10;
        ArrayList qutucuqlar = new ArrayList();
        int yerX = 5, yerY = 5, cehd = 3;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            pbControl.Left = e.X;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ballAction();
            crashControl();
            qalanCehd(sender, e);
            isTheGameEnd(sender, e);
        }

        private void qutucuqYarat()
        {
            for (int j = 0; j < level; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    PictureBox pb = new PictureBox();
                    if (level == 1)
                        pb.ImageLocation = "1.png";
                    else if (level == 2)
                        pb.ImageLocation = "2.png";
                    else if (level == 3)
                        pb.ImageLocation = "3.png";
                    pb.Name = i.ToString();
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Size = new Size(70,65);
                    pb.Location = new Point(20+i*120,j*90+20);
                    pb.BackColor = Color.Transparent;
                    this.Controls.Add(pb);
                    qutucuqlar.Add(pb);
                }
            }
        }
        private void ballAction()
        {
            if (this.ClientSize.Width <= pbBall.Right)
                yerX = yerX * -1;
            else if(0>pbBall.Left)
                yerX = yerX * -1;
            if (this.ClientSize.Height <= pbBall.Bottom)
                yerY = yerY * -1;
            else if (0 > pbBall.Top)
                yerY = yerY * -1;
            else if (pbBall.Bottom >= pbControl.Top && pbBall.Right >= pbControl.Left && pbBall.Left <= pbControl.Right)
                yerY = yerY * -1;
            pbBall.Location = new Point(pbBall.Left+yerX,pbBall.Top+yerY);
        }
        private void Refresh()
        {
            qutuSayi = qutuSayi * level;
            Random rnd = new Random();
            int x = rnd.Next(0,50);
            int y = rnd.Next(300,350);
            pbBall.Location = new Point(x, y);
            timer1.Enabled = true;
            timer1.Interval = 5;
            yerX = 1 + level * 1;
            yerY = -1 - level * 1;
        }
        private void crashControl()
        {
            Rectangle r = new Rectangle();
            Rectangle t = new Rectangle();
            t.X = pbBall.Left;
            t.Y = pbBall.Top;
            t.Height = pbBall.Height;
            t.Width = pbBall.Width;
            for (int i = 0; i < qutucuqlar.Count; i++)
            {
                PictureBox pb = ((PictureBox)qutucuqlar[i]);
                r.X = pb.Left;
                r.Y = pb.Top;
                r.Height = pb.Height;
                r.Width = pb.Width;
                if (r.IntersectsWith(t))
                {
                    qutuSayi--;
                    qutucuqlar.RemoveAt(i);
                    if (yerY>0&&yerX>0)
                    {
                        if (pb.Top <= pbBall.Bottom && pb.Left < pbBall.Right)
                            yerY = yerY * -1;
                        else
                            yerX = yerX * -1;
                    }
                    else
                        if(yerY>0 && yerX < 0)
                          {
                        if (pb.Top <= pbBall.Bottom && pb.Right > pbBall.Left)
                            yerY = yerY * -1;
                        else
                            yerX = yerX * -1;
                          }
                    else
                        if(yerY<0&&yerX>0)
                       {
                        if (pb.Bottom >= pbBall.Top && pb.Left < pbBall.Right)
                            yerY = yerY * -1;
                        else
                            yerX = yerX * -1;
                        }
                    else
                        if(yerY<0&&yerX<0)
                    {
                        if (pb.Bottom >= pbBall.Top && pb.Right > pbBall.Left)
                            yerY = yerY * -1;
                        else
                            yerX = yerX * -1;
                    }
                    pb.Dispose();

                }
            }
        }
        private void qalanCehd(object sender,EventArgs e)
        {
            if(pbBall.Bottom>pbControl.Bottom&&!(pbBall.Right>=pbControl.Left&&pbBall.Left<=pbControl.Right))
            {
                cehd--;
                for (int i = 0; i < qutucuqlar.Count; i++)
                {
                    PictureBox pb = ((PictureBox)qutucuqlar[i]);
                    ((PictureBox)qutucuqlar[i]).Dispose();
                }
                qutucuqlar.Clear();
                timer1.Enabled = false;
                DialogResult dr = DialogResult.None;
                if (cehd > 0)
                {
                    dr = MessageBox.Show(cehd.ToString()+" Cehdiniz qaldi");
                    if (dr == DialogResult.OK)
                        Form1_Load(sender, e);
                }
                else if (cehd == 0)
                {
                    MessageBox.Show("Meglub Oldunuz:(");
                    this.Close();
                }
            }
        }
        private void isTheGameEnd(object sender,EventArgs e)
        {
            DialogResult dr = DialogResult.None;
            if (qutuSayi == 0)
            {
                timer1.Enabled = false;
                dr = MessageBox.Show("Tebrikler!!! Seviyye " + level + " Bitirdiniz...");
                
            }
            if (dr == DialogResult.OK)
            {
                level++;
                Form1_Load(sender, e);
                timer1.Enabled = true;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Qalan cehd = " + cehd.ToString() + " Level = " + level.ToString();
            Refresh();
            qutucuqYarat();
        }
    }
}
