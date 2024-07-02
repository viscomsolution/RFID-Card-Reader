using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CR208Wrapper;

namespace CR208_Card_Reader
{
    public partial class Form1 : Form
    {
        CR208 cardReader = new CR208();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cardReader.Disconnect();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_connect_Click(object sender, EventArgs e)
        {
            bool connectSuccess = cardReader.Connect(txt_port.Text);
            if (connectSuccess)
            {
                btn_read.Enabled = true;
                btn_beep.Enabled = true;
                btn_led.Enabled = true;

                lbl_message.Text = "Connected";
            }
            else
            {
                btn_read.Enabled = false;
                btn_beep.Enabled = false;
                btn_led.Enabled = false;

                lbl_message.Text = "Cannot connect";
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_read_Click(object sender, EventArgs e)
        {
            int delay = 0;
            if (chk_enableBeep.Checked)
                delay = (int)numericUpDown1.Value;


            string cardNumber = cardReader.Read(delay, chk_turnOnLED.Checked);
            txt_cardNumber.Text = cardNumber;

            if (cardNumber == "")
                lbl_message.Text = "Cannot read";
            else
                lbl_message.Text = "Read success";
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_beep_Click(object sender, EventArgs e)
        {
            int delay = (int)numericUpDown1.Value;
            cardReader.Beep(delay);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_led_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                cardReader.TurnLed(false, false);
            else if (radioButton2.Checked)
                cardReader.TurnLed(true, false);
            else if (radioButton3.Checked)
                cardReader.TurnLed(false, true);
            else if (radioButton4.Checked)
                cardReader.TurnLed(true, true);
        }
    }
}
