using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using ComponentFactory.Krypton;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.VisualBasic;

namespace Supermarket_app
{
    public partial class Login : KryptonForm
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string SellerName = "";

        private string adminVerificationCode;

        private SqlConnection con = new SqlConnection(@"Data Source=DisNaka\SQLEXPRESS;Initial Catalog=SUPERMARKET3;Integrated Security=True;");

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            

            if (UnameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {
                    if (RoleCb.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UnameTb.Text == "Admin" && PassTb.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();

                            // Send email notification to admin
                            SendEmail("Admin Login Notification", "Admin has logged in.","disnakaravishanka11@gmail.com");
                           // MessageBox.Show("Email Notification Sent Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                            UnameTb.Text = "";
                            PassTb.Text = "";
                        }
                    }
                    else
                    {
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(8) from SellerTbl where SellerName='" + UnameTb.Text + "' and SellerPass='" + PassTb.Text + "'", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = UnameTb.Text;
                            SellingForm sell = new SellingForm();
                            sell.Show();
                            this.Hide();

                            // Send email notification to admin
                            SendEmail("Seller Login Notification", $"Seller '{SellerName}' has logged in.","disnakaravishanka11@gmail.com");
                            // Do not show the success message for sellers
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                            UnameTb.Text = "";
                            PassTb.Text = "";
                        }

                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select Your Role First ");
                    UnameTb.Text = "";
                    PassTb.Text = "";
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SendEmail(string subject, string body, string to)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("nimsarawickz@gmail.com", "amqycovlanrjqpsr"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage("nimsarawickz@gmail.com", "disnakaravishanka11@gmail.com")
                {
                    Subject = subject,
                    Body = body, 
                    IsBodyHtml = false,
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email notification: {ex.Message}");
            }
        }
    }
}