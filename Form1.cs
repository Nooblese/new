using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Linq;

namespace _045_Alacantara_Basseg_Fldb
{
    public partial class Form1 : Form
    {
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/j43pc10/Documents/dbPhone.mdb";
        OleDbConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "select * from brand";
            DataTable dt = new DataTable();
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            conn.Close();

            string getValue;
            cmbBrandId.DataSource = dt;
            cmbBrandId.DisplayMember = "brand";
            cmbBrandId.ValueMember = "brandid";
            getValue = cmbBrandId.SelectedIndex.ToString();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into [model](model_desc,price,brandid) values(@modeldesc,@price,@brandid)";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            //int price = int.Parse(txtPrice.Text);
            cmd.Parameters.AddWithValue("@modeldesc", txtModelDesc.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice.Text);
            cmd.Parameters.AddWithValue("@brandid", cmbBrandId.SelectedIndex+1);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtModelDesc.Clear();
            txtPrice.Clear();
            cmbBrandId.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            conn = new OleDbConnection(connStr);
            string query = "select modelid as ModelID, model_desc as Model_Desc, price as Price, brandid as BrandId from [model] where model_desc like '"+txtSearch.Text+"' ";
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            
            adapter.Fill(dt);
            conn.Close();
            grdView.DataSource = dt;
            //DataTable dt = new DataTable();
            //conn = new OleDbConnection(connStr);
            //string query = "select model_desc as MODEL, price as PRICE, brandid as BRAND from model where model_desc ='" + txtSearch.Text + "' or price = '" + txtPrice_search.Text + "' and brandid = '" + cmbBrand_search.SelectedIndex + "'";
            //conn.Open();
            //OleDbDataAdapter adpater = new OleDbDataAdapter(query, conn);
            //adpater.Fill(dt);
            //conn.Close();

            //grdView.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update [model] set model_desc = @modeldesc, price = @price, brandid = @brandid where [modelid]= " + txtModelID_update.Text + "";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@modeldesc", txtModelDesc_update.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice_update.Text);
            cmd.Parameters.AddWithValue("@brandid", cmbBrandID_update.SelectedIndex+1);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "DELETE from model where model_desc = @model";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@model", txtDelete.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("DELETED");
        }
    }
}
