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
        //flag to identify either search by product id or supplier id
        private bool bProduct;
        //flat to identify either add or update the record in group box
        private bool addNew;

        private Supplier supplier;  //for one particular supplier
        private Product product;    //for one particular product

        //list of products and suppliers
        private List<Product> products = new List<Product>(); 
        private List<Supplier> suppliers = new List<Supplier>();

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

            this.ClearControls();
            //set focus for input
            txtInput.Focus();
        }
        //search for supplier id
        private void rdoSupplier_CheckedChanged(object sender, EventArgs e)
        {
            bProduct = false;
            //clear all content in text box
            this.ClearControls();

            gboxInfo.Text = "Products";
            lblName.Text = "Suppliers List";
        }
        //search for product id
        private void rdoProduct_CheckedChanged(object sender, EventArgs e)
        {
            bProduct = true;
            //clear all content in text box
            this.ClearControls();

            gboxInfo.Text = "Suppliers";
            lblName.Text = "Products List";
        }

        private void ClearControls()
        {
            txtInput.Text = "";
            txtSearchName.Text = "";
            txtId.Text = "";
            txtName.Text = "";
            cboLoadList.Visible = false;
            btnUpdate.Enabled= false;        
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            txtInput.Focus();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtInput) &&
                Validator.IsNumeric(txtInput))
            {
                //hiding combox
                lblName.Text = "";
                cboLoadList.Visible = false;

                //read only for group box controls and search result
                txtName.ReadOnly = true;
                txtId.ReadOnly = true;

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
                    {
                        this.DisplayProduct();
                        //get suppliers list and show in text box fields
                        this.LoadSuppliers();
                        if (suppliers.LongCount() > 0)
                        {
                            supplier = suppliers[0];
                            txtId.Text = supplier.Id.ToString();
                            txtName.Text = supplier.Name;
                            this.EnableEdit();
                        }
                        else 
                        {
                            MessageBox.Show("There is no supplier available for this product", "No record");
                        }

                    }
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
                    {
                        this.DisplaySupplier();
                        //get product list and show in text box fields
                        this.LoadProducts();
                        //more than one products
                        if (products.LongCount() > 0)
                        {
                            product = products[0];
                            txtId.Text = product.ProductId.ToString();
                            txtName.Text = product.ProdName;
                            this.EnableEdit();
                        }
                        else
                        {
                            MessageBox.Show("There is no product available for this product", "No record");
                        }
                        
                    }
                }
                
                
            }
        }
        //if there are more than one record showing in group box
        //allow update and delete and save
        private void EnableEdit()
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;

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

        //get product by product id
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //If Product Radiobutton selected, Add new Product
            if (rdoProduct.Checked)
            {
               
                Product newProduct = new Product();
                newProduct.ProdName = txtSearchName.Text;
                int NewID = ProductDB.AddProduct(newProduct);

                txtInput.Text = Convert.ToString(NewID);
                txtSearchName.Text = newProduct.ProdName;
                return;
            }
            // If Supplie Radiobutton selected, Add new Supplier
            else if (rdoSupplier.Checked)
            {
                Supplier newSupplier = new Supplier();
                newSupplier.Name = txtSearchName.Text;
                newSupplier.Id = Convert.ToInt32(txtInput.Text);
                SupplierDB.AddSupplier(newSupplier);
                return;
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            addNew = false;     //update the record

            //text box in group box is editable for update            
            txtId.ReadOnly = true;      //update not allowed to change ID
            txtInput.ReadOnly = true;   //update not allowed to change ID

            //update allowed to change name of product/supplier
            txtName.ReadOnly = false; 

            if (bProduct)
            {
                lblName.Text = "Suppliers List";
                lblName.Visible = true;
                cboLoadList.Visible = true;
                //load list of suppliers here                
                //have to define which supplier is located first before change it

                this.LoadSuppliers();
                this.ShowSupplierCombox();
            }
            else
            {
                lblName.Text = "Products List";
                lblName.Visible = true;
                cboLoadList.Visible = true;
                //load list products here                
                //have to define which product is located first before change it

                this.LoadProducts();
                this.ShowProductCombox();
            }

            //TODO: update record code here
        }

        //load products list 
        private void LoadProducts()
        {
            //List<Product> products = new List<Product>();
            if (Validator.IsPresent(txtInput) && Validator.IsNumeric(txtInput))
            {
                int inputID = Convert.ToInt32(txtInput.Text.Trim());
                try
                {
                    products = Product_SupplierDB.GetAllProducts(inputID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        //load suppliers list
        private void LoadSuppliers()
        {
            //List<Supplier> suppliers = new List<Supplier>();
            if (Validator.IsPresent(txtInput) && Validator.IsNumeric(txtInput))
            {
                int inputID = Convert.ToInt32(txtInput.Text.Trim());
                try
                {
                    suppliers = Product_SupplierDB.GetAllSuppliers(inputID);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        //show suppliers in combo box
        private void ShowSupplierCombox()
        {
            cboLoadList.DataSource = suppliers;
            cboLoadList.DisplayMember = "Name";
            cboLoadList.ValueMember = "Id";
        }

        //show products in combo box
        private void ShowProductCombox()
        {
            cboLoadList.DataSource = products;
            cboLoadList.DisplayMember = "ProdName";
            cboLoadList.ValueMember = "ProductId";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int index;

            //show suppliers list
            if (bProduct)
            {
                index = suppliers.IndexOf(supplier);
                index += 1;
                if (index < suppliers.LongCount())
                {
                    supplier = suppliers[index];
                    txtId.Text = supplier.Id.ToString();
                    txtName.Text = supplier.Name;
                }
                else 
                {                    
                    MessageBox.Show("You reach the last record of suppliers list","Out of Range");
                }
            }
            else
            {
                //show products list
                index = products.IndexOf(product);
                index += 1;
                if (index < products.LongCount())
                {
                    product = products[index];
                    txtId.Text = product.ProductId.ToString();
                    txtName.Text = product.ProdName;
                }
                else
                {
                    MessageBox.Show("You reach the last record of products list", "Out of Range");
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int index;

            //show suppliers list
            if (bProduct)
            {
                index = suppliers.IndexOf(supplier);
                index -= 1;
                if (index >= 0)
                {
                    supplier = suppliers[index];
                    txtId.Text = supplier.Id.ToString();
                    txtName.Text = supplier.Name;
                }
                else
                {
                    
                    MessageBox.Show("You reach the first record of suppliers list", "Out of Range");
                }
            }
            else
            {
                //show products list
                index = products.IndexOf(product);
                index -= 1;
                if (index >= 0)
                {
                    product = products[index];
                    txtId.Text = product.ProductId.ToString();
                    txtName.Text = product.ProdName;
                }
                else
                {                    
                    MessageBox.Show("You reach the first record of products list", "Out of Range");
                }
            }
        }

        //event when save button is pressed
        private void btnSave_Click(object sender, EventArgs e)
        {
            //save the record to tables
            //if Product ID is chosen and no ID is input, only save to products table
            //if Product ID is chosen and there is ID in the input for search, save into 
            //three tables(products, suppliers, and products_suppliers)

            //if Supplier ID is chosen and no ID is input, only save to suppliers table
            //if Supplier ID is chosen and there is ID in the input for search, save into
            //three tables(products, suppliers, and products_suppliers)

            this.ClearControls();   //reset the controls in form

        }

        //this part of the form coded by Andew
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rdoProduct.Checked == true)
            {
                DialogResult result = MessageBox.Show("Delete " + supplier.Name + "?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SupplierDB.DeleteSupplier(supplier);
                }
            }
            else if (rdoSupplier.Checked == true)
            {
                DialogResult result = MessageBox.Show("Delete " + product.ProdName + "?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ProductDB.DeleteProduct(product);
                }
            }
        }
    }
}
