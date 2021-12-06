using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO.Ports;

namespace WebCamControllerV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                comboBox2.Items.Add(port);
            }
            comboBox2.SelectedIndex = 0;
        }

        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        private void Form1_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) { serialPort1.Close(); }

            if(FinalFrame.IsRunning == true){FinalFrame.Stop();}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox2.SelectedItem.ToString();
            serialPort1.BaudRate = 115200;
            serialPort1.Open();

            if (serialPort1.IsOpen == true) { MessageBox.Show("Ligado"); }

            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Write("w");
            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            serialPort1.Write("a");
            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            serialPort1.Write("s");
            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("d");
            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            serialPort1.Write("p");
            listBox1.Items.Clear();
            string line = serialPort1.ReadLine();
            listBox1.Items.Add(line);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int c=1;
            Bitmap pic = new Bitmap(pictureBox2.Image);
            string n = c.ToString();
            string namefile = n + ".png";
            pic.Save(namefile, System.Drawing.Imaging.ImageFormat.Png);
            c++;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://github.com/Moisesfprodrigues/PanTilt");
        }

        private void arduinoCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://github.com/Moisesfprodrigues/PanTilt");
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
