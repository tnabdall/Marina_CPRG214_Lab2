using Marina_CPRG214_Lab2.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marina_CPRG214_Lab2
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        bool isRegistrationForm = false; // If true, use registration layout
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["isRegistrationForm"]!=null)
            {
                isRegistrationForm = (bool) ViewState["isRegistrationForm"];
            }
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            
            firstNameLabel.Visible = true;
            firstNameTextBox.Visible = true;
            lastNameLabel.Visible = true;
            lastNameTextBox.Visible = true;
            phoneLabel.Visible = true;
            phoneTextBox.Visible = true;
            cityLabel.Visible = true;
            cityTextBox.Visible = true;
            cancelButton.Visible = true;

            firstNameRequiredValidator.Enabled = true;
            lastNameRequiredValidator.Enabled = true;
            phoneRegularExpressionValidator.Enabled = true;
            phoneRequiredValidator.Enabled = true;
            cityRequiredValidator.Enabled = true;

            registerButton.Visible = false;

            isRegistrationForm = true;
            ViewState["isRegistrationForm"] = isRegistrationForm;
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            

            firstNameLabel.Visible = false;
            firstNameTextBox.Visible = false;
            lastNameLabel.Visible = false;
            lastNameTextBox.Visible = false;
            phoneLabel.Visible = false;
            phoneTextBox.Visible = false;
            cityLabel.Visible = false;
            cityTextBox.Visible = false;

            firstNameRequiredValidator.Enabled = false;
            lastNameRequiredValidator.Enabled = false;
            phoneRegularExpressionValidator.Enabled = false;
            phoneRequiredValidator.Enabled = false;
            cityRequiredValidator.Enabled = false;

            cancelButton.Visible = false;
            registerButton.Visible = true;

            isRegistrationForm = false;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (isRegistrationForm)
            {
                if(CustomerDB.RegisterCustomer(new Customer(-1, firstNameTextBox.Text, lastNameTextBox.Text, phoneTextBox.Text, cityTextBox.Text, usernameTextBox.Text), passwordTextBox.Text))
                {
                    Customer loggedInCustomer = CustomerDB.VerifyLogin(usernameTextBox.Text, passwordTextBox.Text);
                    if (loggedInCustomer != null)
                    {
                        Session["loggedInCustomer"] = loggedInCustomer.Username;
                        Session["loggedInCustomerId"] = loggedInCustomer.CustomerId;
                        Response.Redirect("~/LeaseSlip.aspx");
                    }
                }
            }
            else
            {
                Customer loggedInCustomer = CustomerDB.VerifyLogin(usernameTextBox.Text, passwordTextBox.Text);
                if (loggedInCustomer != null)
                {
                    Session["loggedInCustomer"] = loggedInCustomer.Username;
                    Session["loggedInCustomerId"] = loggedInCustomer.CustomerId;
                    Response.Redirect("~/LeaseSlip.aspx");
                }
                else
                {
                    usernameTextBox.Text = "Not Logged in";
                }
            }
        }
    }
}