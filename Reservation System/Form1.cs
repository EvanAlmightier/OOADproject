﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation_System
{
   public partial class Form1 : Form
   {

        ReservationManager RM = new ReservationManager();
        
      public Form1()
      {
         InitializeComponent();
      }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void IDtext_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if(IDtext.Text != "" && passwordText.Text != "")
            {
                if(true)
                {
                    pnlLogin.SendToBack();

                    /*
                    if(RM.GetUser(int.Parse(IDtext.Text)) != null
                        && passwordText.Text == "password")
                        pnlLogin.SendToBack();
                    */
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
