using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using NonRBOC.Helper;


namespace NonRBOC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string userName = Environment.UserName;


        //LOAD FOR CHECK
        private void Form1_Load(object sender, EventArgs e)
        {
            btnRunProcedure.Enabled = false;

            lblInfo.Text = "Checking Records...";

            GlobalRbocHelper rbocHelper = new GlobalRbocHelper();

            var isLastRunExpired = rbocHelper.IsRbocLastRunDateExpired();

            if (isLastRunExpired)
            {
                btnRunProcedure.Enabled = true;
                lblInfo.Text = "Ready To Run";
                lblInfo.ForeColor = Color.Green;
            }
            else
            {
                //Prompt already ran
                //MessageBox.Show("RBOC Tables have already been updated for this month");
                lblInfo.Text = "Not Ready To Run";
                lblInfo.ForeColor = Color.Red;
            }
        }


        // Button Click - button eneabled if check above passes
        private async void button1_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "Running...";
            lblInfo.Refresh();

            try
                {
                    string connectionString = "server=SLIDER;database=RBOC;UID=sa;password=@cc3ss0n3";
                    SqlConnection conn = new SqlConnection(connectionString);

                    SqlCommand StoredProcedureCommand = new SqlCommand("spMonthlyRBOCCheck", conn);
                    StoredProcedureCommand.CommandTimeout = 0;
                    StoredProcedureCommand.CommandType = CommandType.StoredProcedure;
                    StoredProcedureCommand.Parameters.AddWithValue("@User", Environment.UserName); //passes Windows username as SP parameter
                    SqlParameter param = new SqlParameter();
                    param.Direction = ParameterDirection.ReturnValue;
                    conn.Open();
                    btnRunProcedure.Enabled = false;
                    SqlDataReader reader = await StoredProcedureCommand.ExecuteReaderAsync();
                    

                    while (await reader.ReadAsync())
                    {
                        
                        lblInfo.Text = "Successful";
                        MessageBox.Show("Successful Run on:  " + reader[0].ToString());
                        
                    }

                    lblInfo.Text = "Not Ready To Run";
                    lblInfo.ForeColor = Color.Red;
                    Console.Read();

                    reader.Close();
                    conn.Close();

                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }            
        }

        // redirects to Email form, when button is clicked
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            this.Hide();           
            EmailForm f2 = new EmailForm();
            f2.Show();
            f2.Left = this.Left;
            f2.Top = this.Top;
            f2.Size = this.Size;
        }

        // closes app, when button is clicked
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            f1.Left = this.Left;
            f1.Top = this.Top;
            f1.Size = this.Size;
        }
    }
}
