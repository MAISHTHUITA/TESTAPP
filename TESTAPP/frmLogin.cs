﻿using SHOPLITE.Models;
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

namespace SHOPLITE
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //txtPassword.Text = "PASSWORD";
            //txtUsername.Text = "USERNAME";
            //txtUsername.Focus();
        }
        public User user { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            //if (txtUsername.Text.ToLower()== "USERNAME".ToLower())
            //{
            //    txtUsername.Text = "";
            //}
            lblError.Text = "";
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            //if (txtPassword.Text.ToLower() == "PASSWORD".ToLower())
            //{
            //    txtPassword.Text = "";
            //    txtPassword.PasswordChar =Convert.ToChar("*");
            //}
            lblError.Text = "";
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {

            //if (txtUsername.Text.ToLower() == "")
            //{
            //    txtUsername.Text = "USERNAME";
            //}
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            //if (txtPassword.Text.ToLower() == "")
            //{
            //    txtPassword.PasswordChar = Convert.ToChar("*");
            //    txtPassword.Text = "PASSWORD";
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserRepository repository = new UserRepository();
            
            if (repository.Login(txtUsername.Text,txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.user.UserName = txtUsername.Text;
                getvalues();
                this.Close();
            }
            else
            {
                lblError.Text = "Invalid Login Credentials.";
            }
        }
        private void getvalues()
        {
            using (SqlConnection  con=new SqlConnection(DbCon.connection))
            {
                SqlCommand cmd = new SqlCommand("SpGetValues",con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Company = rdr["Company"].ToString();
                        Branch = rdr["Branch"].ToString();
                    }
                }
            }
        }
    }
}
