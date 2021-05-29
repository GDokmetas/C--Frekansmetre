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
using Microsoft.Win32;

namespace frekansokuyucu
{
    
    public partial class Form1 : Form
    {
        string txt;
        public Form1()
        {
            InitializeComponent();

        }
private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            txt = serialPort1.ReadExisting().ToString();
            SetText(txt.ToString());
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string baglantiNoktalari = "Hardware\\Devicemap\\SerialComm";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(baglantiNoktalari);
            foreach (var portlar in registryKey.GetValueNames())
            {
                comboBox1.Items.Add(registryKey.GetValue(portlar));
            }
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            serialPort1.Open(); 
            label3.Text = "Bağlandı...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            label3.Text = "Bağlantı Kesildi...";
        }
    }
}
