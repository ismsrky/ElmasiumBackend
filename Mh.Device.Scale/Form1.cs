using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mh.Device.Scale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string deger = serialPort1.ReadExisting();

                if (string.IsNullOrEmpty(deger) || !deger.StartsWith("U")) return;

                deger = deger.Substring(2, deger.Length - 6);

                decimal degerDec = Convert.ToDecimal(deger);

                SendKeys.SendWait("^(a)");
                if (degerDec > 0)
                    SendKeys.SendWait(degerDec.ToString() + "x");
                else
                    SendKeys.SendWait("{DEL 4}");
            }
            catch (Exception ex)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}