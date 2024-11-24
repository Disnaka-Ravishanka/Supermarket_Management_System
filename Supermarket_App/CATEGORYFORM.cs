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

namespace Supermarket_app
{
    public partial class CATEGORYFORM : KryptonForm
    {
        public CATEGORYFORM()
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

        private void CATEGORYFORM_Load(object sender, EventArgs e)
        {
            populate();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\Documents\SUPERMARKET3.mdf;Integrated Security=True;Connect Timeout=30 ");

        private void kryptonButton10_Click(object sender, EventArgs e)
        {

        }

        private void CatDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatDescTb.Text = CatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void kryptonButton10_Click_1(object sender, EventArgs e)
        {

        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {

        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}