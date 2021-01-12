using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net.Mail;
using System.Net;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace NonRBOC
{
    public partial class EmailForm : Form
    {
        public EmailForm()
        {
            InitializeComponent();
        }

        public void clearForm()
        {
            txtTo.Clear();
            txtSubject.Clear();
            txtBody.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearForm();

            //hide form and call form 1
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            f1.Left = this.Left;
            f1.Top = this.Top;
            f1.Size = this.Size;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            { 
                Outlook.Application _app = new Outlook.Application();
                Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                mail.To = txtTo.Text;
                mail.Subject = txtSubject.Text;
                mail.Body = txtBody.Text;
                
                ((Outlook._MailItem)mail).Send();
                MessageBox.Show("Message has been sent!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearForm();
  
                //hide form and call form 1
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
                f1.Left = this.Left;
                f1.Top = this.Top;
                f1.Size = this.Size;

                //recheck form
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
