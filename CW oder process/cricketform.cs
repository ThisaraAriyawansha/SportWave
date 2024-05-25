﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_oder_process
{
    public partial class cricketform : Form
    {
        int xx;
        DataSet ds;
        public cricketform()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cm_Click(object sender, EventArgs e)
        {
            cashier x = new cashier();
            x.Show();
            this.Hide();
        }

        private void cricketform_Load(object sender, EventArgs e)
        {
            try
            {
                quantity.Visible = false;
                fquantity.Visible = false;
                //Create a connection with MS SQL Server
                string cs = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                //Create a Command
                string sql = "SELECT Equipment_name From cricket ";
                SqlCommand com = new SqlCommand(sql, con);


                //Acccess data
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    this.cat.Items.Add(dr.GetValue(0));
                }

                //Disconnect server
                con.Close();

                //Create a connection with MS SQL Server
                string css = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                SqlConnection conn = new SqlConnection(css);
                conn.Open();

                //Create a Command
                string sqll = "SELECT max(cid) From cids ";
                SqlCommand comm = new SqlCommand(sqll, conn);
                comm.Parameters.AddWithValue("", xx);

                SqlDataReader drr = comm.ExecuteReader();
                drr.Read();

                this.ciii.Text = drr.GetValue(0).ToString();
                xx = Convert.ToInt32(this.ciii.Text);
                ciii.Text = xx.ToString("000");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cqun_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void ccatergory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Create a connection with MS SQL Server
                string cs = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                //Create a Command
                string sql = "SELECT Price From cricket WHERE Equipment_name=@iname";
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@iname", this.cat.Text);

                //Acccess data
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();

                this.ccprice.Text = dr.GetValue(0).ToString();


                //Disconnect server
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(this.quantity.Text) >= Convert.ToDouble(this.cqunn.Text))
                {
                    Double pr, qt, tot;
                    pr = Convert.ToDouble(this.ccprice.Text);
                    qt = Convert.ToDouble(this.cqunn.Text);
                    tot = pr * qt;
                    this.tott.Text = tot.ToString();

                    string G = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                    SqlConnection c = new SqlConnection(G);
                    c.Open();
                    string sql = "INSERT INTO Bill1(Customerid,Catergory,Price,Quantity,Total) VALUES(@ci,@cat,@pr,@qu,@to)";
                    SqlCommand com = new SqlCommand(sql, c);

                    com.Parameters.AddWithValue("@ci", this.ciii.Text);
                    com.Parameters.AddWithValue("@cat", this.cat.Text);
                    com.Parameters.AddWithValue("@pr", this.ccprice.Text);
                    com.Parameters.AddWithValue("@qu", this.cqunn.Text);
                    com.Parameters.AddWithValue("@to", this.tott.Text);


                    int ret = com.ExecuteNonQuery();
                    c.Close();

                    string css = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                    SqlConnection comm = new SqlConnection(css);

                    comm.Open();

                    string sq = "SELECT * From Bill1 WHERE Customerid=@cid";
                    SqlCommand coe = new SqlCommand(sq, comm);
                    coe.Parameters.AddWithValue("@cid", this.ciii.Text);

                    //Acccess data
                    SqlDataAdapter dap = new SqlDataAdapter(coe);
                    ds = new DataSet();
                    dap.Fill(ds);
                    this.dataGridView1.DataSource = ds.Tables[0];

                    comm.Close();

                    Double fqun;
                    fqun = Convert.ToDouble(this.quantity.Text) - Convert.ToDouble(this.cqunn.Text);
                    this.fquantity.Text = fqun.ToString();
                    //Create a connection with MS SQL Server
                    string cc = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                    SqlConnection ccc = new SqlConnection(cc);
                    ccc.Open();

                    //Create a Command
                    string s = "UPDATE cricket SET Quantity=@qun WHERE Equipment_name=@iname ";
                    SqlCommand co = new SqlCommand(s, ccc);
                    co.Parameters.AddWithValue("@iname", this.cat.Text);
                    co.Parameters.AddWithValue("@qun", this.fquantity.Text);
                    int rett = co.ExecuteNonQuery();

                    string ss = "UPDATE alldetail SET Quantity=@qun WHERE Name=@iname ";
                    SqlCommand coo = new SqlCommand(ss, ccc);
                    coo.Parameters.AddWithValue("@iname", this.cat.Text);
                    coo.Parameters.AddWithValue("@qun", this.fquantity.Text);
                    int rettt = coo.ExecuteNonQuery();
                    //Disconnect server
                    ccc.Close();
                }
                else
                {
                    MessageBox.Show("Out Of Stock", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ctot1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbuy_Click(object sender, EventArgs e)
        {
            Finall_Bill fx = new Finall_Bill();
            fx.Show();
            this.Hide();
        }

        private void ccprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Create a connection with MS SQL Server
                string cc = "Data Source=MSI;Initial Catalog=Sport;Integrated Security=True";
                SqlConnection c = new SqlConnection(cc);
                c.Open();

                //Create a Command
                string sq = "SELECT Quantity From cricket WHERE Equipment_name=@iname";
                SqlCommand co = new SqlCommand(sq, c);
                co.Parameters.AddWithValue("@iname", this.cat.Text);

                //Acccess data
                SqlDataReader drrr = co.ExecuteReader();
                drrr.Read();

                this.quantity.Text = drrr.GetValue(0).ToString();


                //Disconnect server
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
