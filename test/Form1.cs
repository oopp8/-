using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        SynoAPIExHelper fh = new SynoAPIExHelper();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.PerformClick();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var v = fh.OpenDevice();
            label1.Text = v.ToString();
            if (v == ReturnValue.PS_OK)
            {
                foreach (var item in this.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        (item as Button).Enabled = true;
                    }
                }
                button1.Enabled = false;
                button6.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = fh.CloseDevice().ToString();
            foreach (var item in this.Controls)
            {
                if (item.GetType() == typeof(Button))
                {
                    (item as Button).Enabled = false;
                }
            }
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "finger.bmp";
            if (pictureBox1.BackgroundImage != null)
                pictureBox1.BackgroundImage.Dispose();
            pictureBox1.BackgroundImage = null;
            File.Delete(path);
            label1.Text = fh.SaveFigerBmp(path).ToString();
            if (File.Exists(path))
            {
                Bitmap b = new Bitmap(path);
                pictureBox1.BackgroundImage = b;
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                pictureBox1.BackgroundImage = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = 0;
            label1.Text = fh.AddFinger(out n).ToString();
            label2.Text = n.ToString();
            button6.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n = 0;
            label1.Text = fh.FindFinger(out n).ToString();
            label2.Text = n.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<int> ll = new List<int>();
            label1.Text = fh.GetAllFinger(out ll).ToString();
            foreach (var item in ll)
            {
                listBox1.Items.Add(item);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            label1.Text = fh.ClearAllFinger().ToString();
            button6.PerformClick();
        }
    }
}
