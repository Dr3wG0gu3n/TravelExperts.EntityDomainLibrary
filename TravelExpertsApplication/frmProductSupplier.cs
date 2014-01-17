using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExperts.EntityDomainLibrary;
using TravelExperts;

/*********************************************************************
 * Author: Vishnu & Andrew & Chris & Yu Wen
 * Group: Team 2
 * Date: Jan 16, 2014
 * Task: Workshop 2-CMPP248
 * main form of the project
 * *******************************************************************/

namespace TravelExpertsApplication
{
    public partial class frmProductSupplier : Form
    {
        private bool bProduct;      //identify either search by product id or supplier id
        private Supplier supplier;
        private Product product;

        public frmProductSupplier()
        {
            InitializeComponent();
        }

        //exit application
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //load form 
        private void frmProductSupplier_Load(object sender, EventArgs e)
        {
            //set default to check search by product id
            rdoProduct.Checked = true;
            bProduct = true;
            gboxInfo.Text = "Suppliers";

            //hiding combox until add button is clicked
            lblName.Text = "";
            cboLoadList.Visible = false;
            //set focus for input
            txtInput.Focus();
        }
        //search for supplier id
        private void rdoSupplier_CheckedChanged(object sender, EventArgs e)
        {
            bProduct = false;
        }
        //search for product id
        private void rdoProduct_CheckedChanged(object sender, EventArgs e)
        {
            bProduct = true;
        }

        private void ClearControls()
        {
            txtInput.Text = "";
            txtSearchName.Text = "";
            txtId.Text = "";
            txtName.Text = "";
            cboLoadList.Enabled = false;
            cboLoadList.Visible = false;
            btnUpdate.Enabled= false;
            btnDelete.Enabled = false;
            txtInput.Focus();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtInput) &&
                Validator.IsNumeric(txtInput))
            {
                int inputID = Convert.ToInt32(txtInput.Text);
                //search by product id
                if (bProduct)
                {
                    //search by supplier id
                    this.GetProduct(inputID);
                    if (product == null)
                    {
                        MessageBox.Show("No product found with this ID. " +
                             "Please try again.", "Product Not Found");
                        this.ClearControls();
                    }
                    else
                        this.DisplayProduct();      
                }
                else                 
                {
                    //search by supplier id
                    this.GetSupplier(inputID);
                    if (supplier == null)
                    {
                        MessageBox.Show("No supplier found with this ID. " +
                             "Please try again.", "Supplier Not Found");
                        this.ClearControls();
                    }
                    else
                        this.DisplaySupplier();                        
                }
                
                
            }
        }
        //get supplier by supplier id
        private void GetSupplier(int supplierID)
        {
            try
            {
                supplier = SupplierDB.GetSupplierById(supplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        //display supplier name in the text box
        private void DisplaySupplier()
        {
            txtSearchName.Text = supplier.Name;
        }

        //get supplier by supplier id
        private void GetProduct(int productID)
        {
            try
            {
                product = ProductDB.GetProduct(productID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        //display supplier name in the text box
        private void DisplayProduct()
        {
            txtSearchName.Text = product.ProdName;
        }
    }
}
