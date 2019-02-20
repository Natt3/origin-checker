using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApp14
{
    public partial class Form1 : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(File.ReadAllLines(openFileDialog1.FileName));
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string sPath = "available.txt";
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            const string sPath = "taken.txt";
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox2.Items)
            {
                SaveFile.WriteLine(item);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "start")
            {
                timer1.Start();
                button5.Text = "stop";
                
            }
            else if (button5.Text == "stop")
            {
                timer1.Stop();
                button5.Text = "start";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                WebClient steam = new WebClient();
                string check = steam.DownloadString("https://signin.ea.com/p/ajax/user/checkOriginId?requestorId=portal&originId=" + comboBox1.SelectedItem.ToString());
                string input = check;
                string taken = @"status(.+?)origin_id_duplicated";
                string open = @"status(.+?)true";
                string vulgar = @"message(.+?)origin_id_not_allowed";
                string small = @"message(.+?)origin_id_too_short";
                string big = @"message(.+?)origin_id_too_long";
                RegexOptions options = RegexOptions.Multiline;
                if (Regex.IsMatch(input, taken, options))
                {
                    listBox2.Items.Add(comboBox1.SelectedItem.ToString());
                    if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                    {
                        comboBox1.SelectedIndex = 0;
                        timer1.Stop();
                    }
                    int selectedIndex = comboBox1.SelectedIndex;
                    comboBox1.SelectedIndex = selectedIndex + 1;
                    listBox2.TopIndex = listBox2.Items.Count - 1;

                }
                 if (Regex.IsMatch(input, open, options))
                {
                    listBox1.Items.Add(comboBox1.SelectedItem.ToString());
                    if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                    {
                        comboBox1.SelectedIndex = 0;
                        timer1.Stop();
                    }
                    int selectedIndex = comboBox1.SelectedIndex;
                    comboBox1.SelectedIndex = selectedIndex + 1;
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                }
                if (Regex.IsMatch(input, vulgar, options))
                {
                    listBox2.Items.Add(comboBox1.SelectedItem.ToString());
                    if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                    {
                        comboBox1.SelectedIndex = 0;
                        timer1.Stop();
                    }
                    int selectedIndex = comboBox1.SelectedIndex;
                    comboBox1.SelectedIndex = selectedIndex + 1;
                    listBox2.TopIndex = listBox2.Items.Count - 1;
                }
                if (Regex.IsMatch(input, small, options))
                {
                    listBox2.Items.Add(comboBox1.SelectedItem.ToString());
                    if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                    {
                        comboBox1.SelectedIndex = 0;
                        timer1.Stop();
                    }
                    int selectedIndex = comboBox1.SelectedIndex;
                    comboBox1.SelectedIndex = selectedIndex + 1;
                    listBox2.TopIndex = listBox2.Items.Count - 1;
                }
                if (Regex.IsMatch(input, big, options))
                {
                    listBox2.Items.Add(comboBox1.SelectedItem.ToString());
                    if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                    {
                        comboBox1.SelectedIndex = 0;
                        timer1.Stop();
                    }
                    int selectedIndex = comboBox1.SelectedIndex;
                    comboBox1.SelectedIndex = selectedIndex + 1;
                    listBox2.TopIndex = listBox2.Items.Count - 1;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError &&
                    ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        listBox1.Items.Add(comboBox1.SelectedItem.ToString());
                        if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
                        {
                            comboBox1.SelectedIndex = 0;
                            timer1.Stop();
                        }
                        int selectedIndex = comboBox1.SelectedIndex;
                        comboBox1.SelectedIndex = selectedIndex + 1;
                        listBox1.TopIndex = listBox1.Items.Count - 1;

                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=SR1vGzEiv90");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("coded and created by @eric", "Origin Checker");
        }
    }
 }
