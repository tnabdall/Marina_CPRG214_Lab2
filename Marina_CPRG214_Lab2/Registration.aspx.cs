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
            // Makes form login/register as it was before on post back
            if (IsPostBack && ViewState["isRegistrationForm"]!=null)
            {
                isRegistrationForm = (bool) ViewState["isRegistrationForm"];
            }
        }

        /// <summary>
        /// Processes form layout change to registration form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void registerButton_Click(object sender, EventArgs e)
        {
            // All appropriate fields are visible
            firstNameLabel.Visible = true;
            firstNameTextBox.Visible = true;
            lastNameLabel.Visible = true;
            lastNameTextBox.Visible = true;
            phoneLabel.Visible = true;
            phoneTextBox.Visible = true;
            cityLabel.Visible = true;
            cityTextBox.Visible = true;
            

            // All validators are enabled
            firstNameRequiredValidator.Enabled = true;
            lastNameRequiredValidator.Enabled = true;
            phoneRegularExpressionValidator.Enabled = true;
            phoneRequiredValidator.Enabled = true;
            cityRequiredValidator.Enabled = true;

            // Hide register button, show cancel button
            cancelButton.Visible = true;
            registerButton.Visible = false;
            failedLabel.Visible = false;

            // Holds registration form status in view state
            isRegistrationForm = true; 
            ViewState["isRegistrationForm"] = isRegistrationForm;
        }

        /// <summary>
        /// Cancels registration. Reverts to login form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cancelButton_Click(object sender, EventArgs e)
        {            
            // Hides all appropriate fields
            firstNameLabel.Visible = false;
            firstNameTextBox.Visible = false;
            lastNameLabel.Visible = false;
            lastNameTextBox.Visible = false;
            phoneLabel.Visible = false;
            phoneTextBox.Visible = false;
            cityLabel.Visible = false;
            cityTextBox.Visible = false;

            // Disables all irrelevant validators
            firstNameRequiredValidator.Enabled = false;
            lastNameRequiredValidator.Enabled = false;
            phoneRegularExpressionValidator.Enabled = false;
            phoneRequiredValidator.Enabled = false;
            cityRequiredValidator.Enabled = false;

            // Show register button, hide cancel button
            cancelButton.Visible = false;
            registerButton.Visible = true;
            failedLabel.Visible = false;

            // Holds login form status in view state
            isRegistrationForm = false;
            ViewState["isRegistrationForm"] = isRegistrationForm;
        }

        /// <summary>
        /// Process login/register request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Process register request
                if (isRegistrationForm)
                {
                    // Attempts to register customer in DB. If successful, continue to lease slip page
                    if (CustomerDB.RegisterCustomer(new Customer(-1, firstNameTextBox.Text.Trim(), lastNameTextBox.Text.Trim(), phoneTextBox.Text.Trim(), cityTextBox.Text.Trim(), usernameTextBox.Text.Trim()), passwordTextBox.Text.Trim()))
                    {
                        // Gets registerd customer id (by logging in)
                        Customer loggedInCustomer = CustomerDB.VerifyLogin(usernameTextBox.Text.Trim(), passwordTextBox.Text.Trim());
                        if (loggedInCustomer != null) // If login is successful, proceed to lease slip page
                        {
                            // Attaches two session variables to hold customer username and id
                            Session["loggedInCustomer"] = loggedInCustomer.Username;
                            Session["loggedInCustomerId"] = loggedInCustomer.CustomerId;
                            Response.Redirect("~/LeaseSlip.aspx");
                        }
                    }
                    else
                    {
                        failedLabel.Text = "Unable to register customer. Username already exists.";
                        failedLabel.Visible = true;
                    }
                }
                else // Process login request
                {
                    // Attempts to login customer
                    Customer loggedInCustomer = CustomerDB.VerifyLogin(usernameTextBox.Text.Trim(), passwordTextBox.Text.Trim());
                    if (loggedInCustomer != null) // If login is successful
                    {
                        // Attaches two session variables to hold customer username and id
                        Session["loggedInCustomer"] = loggedInCustomer.Username;
                        Session["loggedInCustomerId"] = loggedInCustomer.CustomerId;
                        Response.Redirect("~/LeaseSlip.aspx"); // Go to lease slip page
                    }
                    else
                    {
                        failedLabel.Text = "Failed to login. Please check username and password.";
                        failedLabel.Visible = true;
                    }
                }
            }
            catch(Exception)
            {
                failedLabel.Text = "Error in DB. Please try again later.";
                failedLabel.Visible = true;
            }
        }
    }
}