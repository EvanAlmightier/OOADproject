using System;
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
            if (IDtext.Text != "" && passwordText.Text != "")
            {
                if (true)
                {
                    //pnlLogin.SendToBack();

                    if (RM.GetUser(int.Parse(IDtext.Text)) != null
                        && passwordText.Text == "password")
                    {
                        RM.SetActiveUser(int.Parse(IDtext.Text));
                        if (RM.GetUser(RM.activeUser).GetType() == "Administrator")
                            administratorToolStripMenuItem.Visible = true;
                        pnlLogin.SendToBack();
                    }
                    else
                    {
                        txtIncorrectLogin.Visible = true;
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IDtext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void managingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlManage.BringToFront();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlSearch.BringToFront();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            lstboxAvailable.Items.Add(RM.GetAvailable(cbType.Text,dtpTime.Value,int.Parse(nudDuration.Value.ToString())));
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbCreateReservable.SelectedItem.ToString() == "Computer")
            {
                lstboxCreateComputerRoomList.Items.Clear();
                lstboxCreateComputerRoomList.Visible = true;
                int[] IDs = RM.GetRoomIDs();
                foreach(int i in IDs)
                {
                    lstboxCreateComputerRoomList.Items.Add(i);
                }
            }
            else
            {
                lstboxCreateComputerRoomList.Visible = false;
            }
        }

        private void btnCreateReservable_Click(object sender, EventArgs e)
        {
            if (cbCreateReservable.SelectedItem.ToString() == "Room")
            {
                RM.AddReservable(cbCreateReservable.SelectedItem.ToString(), 0);
                txtNoRoomSelected.Visible = false;
            }
            else
            {
                if (lstboxCreateComputerRoomList.SelectedIndex != -1)
                {
                    RM.AddReservable(cbCreateReservable.SelectedItem.ToString(), int.Parse(lstboxCreateComputerRoomList.SelectedItem.ToString()));
                    txtNoRoomSelected.Visible = false;
                }
                else
                    txtNoRoomSelected.Visible = true;
            }
        }

      private void cbEditReservable_SelectedValueChanged(object sender, EventArgs e)
      {
         gbEditReservable.Visible = false;
         if (cbEditReservable.SelectedItem.ToString() == "Computer")
         {
            lstboxEditReservable.Items.Clear();
            int[] CIDs = RM.GetComputerIDs();
            foreach (int i in CIDs)
            {
               lstboxEditReservable.Items.Add(i);
            }
         }
         else
         {
            lstboxEditReservable.Items.Clear();
            int[] RIDs = RM.GetRoomIDs();
            foreach (int i in RIDs)
            {
               lstboxEditReservable.Items.Add(i);
            }
         }
      }

      private void btnDeleteReservable_Click(object sender, EventArgs e)
      {
         if(lstboxEditReservable.SelectedIndex != -1)
         {
            RM.RemoveReservable(int.Parse(lstboxEditReservable.SelectedItem.ToString()));
            if (cbEditReservable.SelectedItem.ToString() == "Computer")
            {
               lstboxEditReservable.Items.Clear();
               int[] CIDs = RM.GetComputerIDs();
               foreach (int i in CIDs)
               {
                  lstboxEditReservable.Items.Add(i);
               }
            }
            else
            {
               lstboxEditReservable.Items.Clear();
               int[] RIDs = RM.GetRoomIDs();
               foreach (int i in RIDs)
               {
                  lstboxEditReservable.Items.Add(i);
               }
            }
         }
      }

      private void lstboxEditReservable_SelectedIndexChanged(object sender, EventArgs e)
      {
         if(lstboxEditReservable.SelectedIndex != -1)
         {
            Reservable r = RM.GetReservable(int.Parse(lstboxEditReservable.SelectedItem.ToString()));
            if(r.GetType() == "Room")
            {
               gbEditReservable.Visible = true;
               int[] IDArray = ((Room)r).GetComputerIDs();
               lstboxEditComputersInRoom.Items.Clear();
               foreach(int i in IDArray)
               {
                  lstboxEditComputersInRoom.Items.Add(i);
               }
               int[] IDRooms = RM.GetRoomIDs();
               lstboxMoveToRoom.Items.Clear();
               foreach (int i in IDRooms)
               {
                  lstboxMoveToRoom.Items.Add(i);
               }
               lstboxMoveToRoom.Items.RemoveAt(lstboxEditReservable.SelectedIndex);
            }
         }
      }

      private void btnDeleteSelectedComputer_Click(object sender, EventArgs e)
      {
         if(lstboxEditComputersInRoom.SelectedIndex != -1)
         {
            RM.RemoveReservable(int.Parse(lstboxEditComputersInRoom.SelectedItem.ToString()));
            if (lstboxEditReservable.SelectedIndex != -1)
            {
               Reservable r = RM.GetReservable(int.Parse(lstboxEditReservable.SelectedItem.ToString()));
               if (r.GetType() == "Room")
               {
                  gbEditReservable.Visible = true;
                  int[] IDArray = ((Room)r).GetComputerIDs();
                  lstboxEditComputersInRoom.Items.Clear();
                  foreach (int i in IDArray)
                  {
                     lstboxEditComputersInRoom.Items.Add(i);
                  }
               }
            }
         }
      }
      
      private void btnMoveSelectedComputer_Click(object sender, EventArgs e)
      {
         if (lstboxEditComputersInRoom.SelectedIndex != -1
            && lstboxEditReservable.SelectedIndex != -1
            && lstboxMoveToRoom.SelectedIndex != -1)
         {
            RM.MoveReservable(int.Parse(lstboxEditComputersInRoom.SelectedItem.ToString()),int.Parse(lstboxMoveToRoom.SelectedItem.ToString()));
            lstboxEditComputersInRoom.Items.RemoveAt(lstboxEditComputersInRoom.SelectedIndex);
         }
      }

      private void btnCreateUser_Click(object sender, EventArgs e)
      {
         lblUserWarning.Visible = true;
         if (cbCreateUser.SelectedIndex != -1 && cbCreateUser.SelectedItem.ToString() != "" && txtUserName.Text != "" && txtUserEmail.Text != "")
         {
            if (txtUserEmail.Text.Contains("@gmail.com") || txtUserEmail.Text.Contains("@yahoo.com"))
            {
               RM.AddUser(cbCreateUser.SelectedItem.ToString(), txtUserName.Text, txtUserEmail.Text);
               lblUserWarning.Text = "User: " + txtUserName.Text + " Added to System";
            }
            else
            {
               lblUserWarning.Text = "Enter a valid email address.";
            }
         }
         else
         {
            lblUserWarning.Text = "Please fill in all fields.";
         }
      }

      private void cbEditUser_SelectedIndexChanged(object sender, EventArgs e)
      {
         if(cbEditUser.SelectedIndex != -1)
         {
            lstboxUsers.Items.Clear();
            int[] tempArray = RM.GetUserIDs();
            foreach(int i in tempArray)
            {
               if (RM.GetUser(i).GetType() == cbEditUser.SelectedItem.ToString())
               lstboxUsers.Items.Add(i);
            }
         }
      }

      private void lstboxUsers_SelectedIndexChanged(object sender, EventArgs e)
      {
         if(lstboxUsers.SelectedIndex != -1)
         {
            gbEditUser.Visible = true;
         }
      }

      private void btnEditUser_Click(object sender, EventArgs e)
      {
         User tempUser = RM.GetUser(int.Parse(lstboxUsers.SelectedItem.ToString()));
         RM.RemoveUser(tempUser.id);
         string type = tempUser.GetType();
         string name = tempUser.name;
         string email = tempUser.email;
         if(cbChangeUser.SelectedItem.ToString() != "")
         {
            type = cbChangeUser.SelectedItem.ToString();
         }
         if(txtEditName.Text != "")
         {
            name = txtEditName.Text;
         }
         if(txtEditEmail.Text != "")
         {
            email = txtEditEmail.Text;
         }
         RM.AddUser(type, name, email);
      }
   }
}
