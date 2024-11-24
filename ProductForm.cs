using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Supermarket_app
{
    public partial class ProductForm : KryptonForm
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=DisNaka\SQLEXPRESS;Initial Catalog=SUPERMARKET3;Integrated Security=True;");

        private void fillcombo()
        {
           con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Catname from CategoryTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "CatName";
            CatCb.DataSource = dt;
            // SearchCb.ValueMember = "CatName";  // Corrected property name to ValueMember
            //  SearchCb.DataSource = dt;

            con.Close();
        }

        private void populate()
        {
            con.Open();
            String query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBoxBunifu1__TextChanged(object sender, EventArgs e)
        {
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            CATEGORYFORM3 cat = new CATEGORYFORM3();
            cat.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellerForm seller = new SellerForm();
            seller.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonTextBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*  try
              {
                  con.Open();
                  string query = "select * from ProductTbl where ProdCat='" + SearchCb.SelectedValue.ToString() + "'";
                  SqlDataAdapter sda = new SqlDataAdapter(query, con);
                  SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                  var ds = new DataSet();
                  sda.Fill(ds);
                  ProdDGV.DataSource = ds.Tables[0];
                  con.Close();
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }*/
            /*  con.Open();
              string query = "select ProdCat from ProductTbl where ProdCat='" + CatCb.SelectedValue.ToString() + "'";
              SqlDataAdapter sda = new SqlDataAdapter(query, con);
              SqlCommandBuilder builder = new SqlCommandBuilder(sda);
              var ds = new DataSet();
              sda.Fill(ds);
              ProdDGV.DataSource = ds.Tables[0];
              con.Close();*/
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO ProductTbl VALUES(" + ProdId.Text + ", '" + ProdName.Text + "', " + ProdQty.Text + "," + ProdPrice.Text + ",'" + CatCb.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");

                //con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                populate();
                ProdId.Text = "";
                ProdName.Text = "";
                ProdQty.Text = "";
                ProdPrice.Text = "";
                CatCb.SelectedValue = "";
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "" || ProdName.Text == "" || ProdQty.Text == "" || ProdPrice.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    con.Open();
                    string query = "UPDATE ProductTbl SET ProdName='" + ProdName.Text + "', ProdQty='" + ProdQty.Text + "', ProdPrice='" + ProdPrice.Text + "', ProdCat='" + CatCb.SelectedValue.ToString() + "' WHERE ProdId=" + ProdId.Text;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Table Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                populate();
                con.Close();
                populate();
                ProdId.Text = "";
                ProdName.Text = "";
                ProdQty.Text = "";
                ProdPrice.Text = "";
                CatCb.SelectedValue = "";
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "")
                {
                    MessageBox.Show("Select The Product to delete");
                }
                else
                {
                    con.Open();

                    // Check if the record with the given ProdId exists
                    string checkQuery = "SELECT COUNT(*) FROM ProductTbl WHERE ProdId = " + ProdId.Text;
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    int recordCount = (int)checkCmd.ExecuteScalar();

                    if (recordCount > 0)
                    {
                        // The record exists, proceed with deletion
                        string deleteQuery = "DELETE FROM ProductTbl WHERE ProdId = " + ProdId.Text;
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Product Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Product with specified ProdId does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                populate();

                // Clear textboxes and set combo box to an appropriate default value
                ProdId.Text = "";
                ProdName.Text = "";
                ProdQty.Text = "";
                ProdPrice.Text = "";
                CatCb.SelectedIndex = 0; // Set to an appropriate default index or use null if needed
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
        }

        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdId.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdName.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdQty.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProdPrice.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCb.SelectedValue = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellingForm sell = new SellingForm();
            sell.Show();
        }
    }
}

//product form eka catogory eken blalagem afill karanna