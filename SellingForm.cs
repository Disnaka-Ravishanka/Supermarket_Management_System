using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton;
using ComponentFactory.Krypton.Toolkit;
using System.Drawing.Printing;
using System.Security.Cryptography;

namespace Supermarket_app
{
    public partial class SellingForm : KryptonForm
    {
        public SellingForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000; // Set the timer to tick every 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick; // Set the Tick event handler
            timer.Start(); // Start the timer
                           // printDocument1.PrintPage += printDocument1_PrintPage;
        }

        private Timer timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            // This method is called every time the timer ticks (every 1 second)
            DateTime currentDateTime = DateTime.Now;
            Datelbl.Text = $"{currentDateTime.Day}/{currentDateTime.Month}/{currentDateTime.Year} {currentDateTime.Hour}:{currentDateTime.Minute}:{currentDateTime.Second}";
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=DisNaka\SQLEXPRESS;Initial Catalog=SUPERMARKET3;Integrated Security=True;");

        private void populate()
        {
            con.Open();
            String query = "select ProdName,ProdPrice from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void populateBillsDGV()
        {
            con.Open();
            String query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBillsDGV();
            fillcombo();
            SellerNamelbl.Text = Login.SellerName;
        }

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Show current date and time in the format of day/month/year hour:minute:second
            DateTime currentDateTime = DateTime.Now;
            Datelbl.Text = $"{currentDateTime.Day}/{currentDateTime.Month}/{currentDateTime.Year} {currentDateTime.Hour}:{currentDateTime.Minute}:{currentDateTime.Second}";
        }

        private void BillsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private int Grdtotal = 0, n = 0;

        private void ORDERDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (BillID.Text == "")
            {
                MessageBox.Show("Missing Bill ID");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO BillTbl VALUES(" + BillID.Text + ", '" + SellerNamelbl.Text + "', '" + Datelbl.Text + "'," + Amtlbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");

                    //con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    populateBillsDGV(); ;
                }
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            //windows 
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Supermarket Billing System", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID: " + BillsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100, 100));
            e.Graphics.DrawString("Seller Name: " + BillsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100, 130));
            e.Graphics.DrawString("Date: " + BillsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100, 160));
            e.Graphics.DrawString("Total Amount: " + BillsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(100, 190));
            e.Graphics.DrawString("Supermarket Billing System", new Font("Century Gothic", 20, FontStyle.Italic), Brushes.Red, new Point(230, 230));
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            populate();
            CatCb.Text = "Select Catogory";
        }

        /* private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
          {
              con.Open();
              string query = "select ProdName,ProdQty from ProductTbl where ProdCat='" + CatCb.SelectedValue.ToString() + "'";
              SqlDataAdapter sda = new SqlDataAdapter(query, con);
              SqlCommandBuilder builder = new SqlCommandBuilder(sda);
              var ds = new DataSet();
              sda.Fill(ds);
              ProdDGV1.DataSource = ds.Tables[0];
              con.Close();
          }*/

        private void fillcombo()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CategoryTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "CatName";
            CatCb.DataSource = dt;
            // SearchCb.ValueMember = "CatName";
            //  SearchCb.DataSource = dt;

            con.Close();
        }

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select ProdName,ProdPrice from ProductTbl where ProdCat='" + CatCb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Amtlbl_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SellerNamelbl_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQty.Text == "")
            {
                MessageBox.Show("Add Data First");
            }
            else
            {
                // Add a new row to the DataGridView (ORDERDGV)
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ORDERDGV);
                newRow.Cells[0].Value = n + 1;
                n++;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = ProdQty.Text;
                newRow.Cells[4].Value = total; // Use the total variable here
                ORDERDGV.Rows.Add(newRow);
                Grdtotal = Grdtotal + total;
                Amtlbl.Text = "" + Grdtotal;
            }
        }
    }
}