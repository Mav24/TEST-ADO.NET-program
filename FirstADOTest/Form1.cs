using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstADOTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Customer customer;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //int customerId = Convert.ToInt32(txtID.Text);
            if (!Int32.TryParse(txtID.Text, out int customerId))
            {
                MessageBox.Show("Sorry \"" + txtID.Text + "\"is not a numeric value try again", "Error");
                txtID.Clear();
                txtID.Focus();

            }
            else
            GetCustomer(customerId);
        }

        private void GetCustomer(int customerId)
        {
            try
            {
                customer = CustomerDB.GetCustomer(customerId);
                if (customer == null)
                {
                    MessageBox.Show("Sorry not Customer with that ID", "Customer Not found");
                }
                else
                {
                    DisplayCustomer();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        
        private void DisplayCustomer()
        {
            txtName.Text = customer.Name;
            txtNicName.Text = customer.NickName;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNew frmNew = new frmNew();
            frmNew.addCustomer = true;
            DialogResult result = frmNew.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = frmNew.customer;
                txtID.Text = customer.Id.ToString();
                DisplayCustomer();
            }

        }
    }
}
