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
    public partial class frmNew : Form
    {
        public frmNew()
        {
            InitializeComponent();
        }

        public bool addCustomer;
        public Customer customer;

        private void btnNew_Click(object sender, EventArgs e)
        {
            customer = new Customer();
            this.putCustomerData(customer);
            try
            {
                customer.Id = CustomerDB.AddCustomer(customer);
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void putCustomerData(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.NickName = txtNickName.Text;
        }
    }
}
