﻿using SHOPLITE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOPLITE.ModalForms
{
    public partial class frnNewUnit : Form
    {
        public frnNewUnit()
        {
            InitializeComponent();
        }
        public string CreatedUnitCode { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUnitcd.Text.Trim()))
            {
                MessageBox.Show("Please enter Unit Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnitcd.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtUnitnm.Text.Trim()))
            {
                MessageBox.Show("Please enter Unit Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnitnm.Focus();
                return;
            }
            Unit unit = new Unit();
            unit.UnitCd = txtUnitcd.Text.ToUpper();
            unit.UnitNm = txtUnitnm.Text.ToUpper();
            unit.CreatedBy = "Test";
            UnitRepository repository = new UnitRepository();
            if (repository.GetUnit(txtUnitcd.Text) != null)
            {
                MessageBox.Show("Sorry, Unit Code provided already exists in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUnitcd.Focus();
                return;
            }
            if (repository.AddUnit(unit))
            {
                CreatedUnitCode = txtUnitcd.Text;
                MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnitcd.Text = txtUnitnm.Text = "";
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Occurred, Please try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUnitcd.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
