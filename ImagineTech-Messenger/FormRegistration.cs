using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagineTech_Messenger
{
    public partial class FormRegistration : Form
    {
        string password;

        public FormRegistration()
        {
            InitializeComponent();
        }

        private void FormRegistration_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void grbRequestCode_Enter(object sender, EventArgs e)
        {

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please enter your phone number.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhoneNumber.Focus();
                return;
            }
            if(string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Please enter your full name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFullName.Focus();
                return;
            }
            if (WhatsAppApi.Register.WhatsRegisterV2.RequestCode(txtPhoneNumber.Text, out password, "sms"))
            {
                if (!string.IsNullOrEmpty(password))
                    Save();
                else
                {
                    grbRequestCode.Enabled = false;
                    grbConfirmCode.Enabled = true;
                }
            }
            else
                MessageBox.Show("Could not generate password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Save()
        {
            this.grbRequestCode.Enabled = false;
            this.grbConfirmCode.Enabled = false;
            Properties.Settings.Default.PhoneNumber = txtPhoneNumber.Text;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.FullName = txtFullName.Text;
            Properties.Settings.Default.Save();
            if(Globals.DB.Accounts.FindByAccountId(txtPhoneNumber.Text) == null)
            {
                AppData.AccountsRow row = Globals.DB.Accounts.NewAccountsRow();
                row.AccountId = txtPhoneNumber.Text;
                row.FullName = txtFullName.Text;
                row.Password = password;
                Globals.DB.Accounts.AddAccountsRow(row);
                Globals.DB.Accounts.AcceptChanges();
                Globals.DB.Accounts.WriteXml(string.Format("{0}\\accounts.dat", Application.StartupPath));
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSmsCode.Text))
            {
                MessageBox.Show("Please enter your sms code.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSmsCode.Focus();
                return;
            }
            password = WhatsAppApi.Register.WhatsRegisterV2.RegisterCode(txtPhoneNumber.Text, txtSmsCode.Text);
            Save();
        }
    }
}
