using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Supermarket_app
{
    public partial class CATEGORYFORM3 : KryptonForm
    {
        public CATEGORYFORM3()
        {
            InitializeComponent();
        }

        private void populate()
        {
            con.Open();
            String query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=DisNaka\SQLEXPRESS;Initial Catalog=SUPERMARKET3;Integrated Security=True;");

        private void CATEGORYFORM3_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO CategoryTbl VALUES(" + CatIdTb.Text + ", '" + CatNameTb.Text + "', '" + CatDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");

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

                CatDescTb.Text = "";
                CatIdTb.Text = "";
                CatNameTb.Text = "";
            }
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "")

                    MessageBox.Show("Select The Product to delete");
                else
                {
                    con.Open();
                    string query = "delete from CategoryTbl where CatId =" + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("catogery Deleted Successfully");

                    /*con.Close();
                      populate(); */
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

                CatDescTb.Text = "";
                CatIdTb.Text = "";
                CatNameTb.Text = "";
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "" || CatNameTb.Text == "" || CatDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information ");
                }
                else
                {
                    con.Open();
                    string query = "update CategoryTbl set CatName ='" + CatNameTb.Text + "',CatDesc ='" + CatDescTb.Text + "' where CatId = " + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Table Updated Successfully");

                    /* con.Close();
                       populate(); */
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

                CatDescTb.Text = "";
                CatIdTb.Text = "";
                CatNameTb.Text = "";
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            ProductForm prd = new ProductForm();
            prd.Show();
            this.Hide();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellerForm seller = new SellerForm();
            seller.Show();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellingForm sale = new SellingForm();
            sale.Show();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
/* using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Supermarket_app
{
    public partial class CATEGORYFORM3 : KryptonForm
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SUPRMARKET.mdf;Integrated Security=True;Connect Timeout=30 ";
        private SqlConnection con = new SqlConnection(ConnectionString);

        public CATEGORYFORM3()
        {
            InitializeComponent();
        }

        private void CATEGORYFORM3_Load(object sender, EventArgs e)
        {
            PopulateCategories();
        }

        private void PopulateCategories()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from CategoryTbl", con);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CatIdTb.Text) || string.IsNullOrEmpty(CatNameTb.Text) || string.IsNullOrEmpty(CatDescTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CategoryTbl VALUES(@CatId, @CatName, @CatDesc)", con);
                cmd.Parameters.AddWithValue("@CatId", CatIdTb.Text);
                cmd.Parameters.AddWithValue("@CatName", CatNameTb.Text);
                cmd.Parameters.AddWithValue("@CatDesc", CatDescTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                PopulateCategories();
                ClearInputFields();
            }
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void deleteCategoryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CatIdTb.Text))
            {
                MessageBox.Show("Select The Category to delete");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from CategoryTbl where CatId = @CatId", con);
                cmd.Parameters.AddWithValue("@CatId", CatIdTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                PopulateCategories();
                ClearInputFields();
            }
        }

        private void updateCategoryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CatIdTb.Text) || string.IsNullOrEmpty(CatNameTb.Text) || string.IsNullOrEmpty(CatDescTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName = @CatName, CatDesc = @CatDesc where CatId = @CatId", con);
                cmd.Parameters.AddWithValue("@CatId", CatIdTb.Text);
                cmd.Parameters.AddWithValue("@CatName", CatNameTb.Text);
                cmd.Parameters.AddWithValue("@CatDesc", CatDescTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                PopulateCategories();
                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            CatDescTb.Text = "";
            CatIdTb.Text = "";
            CatNameTb.Text = "";
        }

      private void NavigateToProductFormButton_Click(object sender, EventArgs e)
{
    NavigateToForm(new ProductForm());
}

private void NavigateToLoginFormButton_Click(object sender, EventArgs e)
{
    NavigateToForm(new Login());
}

private void NavigateToSellerFormButton_Click(object sender, EventArgs e)
{
    NavigateToForm(new SellerForm());
}

private void NavigateToSellingFormButton_Click(object sender, EventArgs e)
{
    NavigateToForm(new SellingForm());
}

private void NavigateToForm(Form form)
{
    this.Hide();
    form.Show();
}

private void ExitApplicationButton_Click(object sender, EventArgs e)
{
    Application.Exit();
}
    
}*/