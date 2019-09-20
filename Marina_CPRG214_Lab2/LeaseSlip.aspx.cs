using Marina_CPRG214_Lab2.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marina_CPRG214_Lab2
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private int customerId; // Holds customer Id for customer who logged into this page
        protected void Page_Load(object sender, EventArgs e)
        {
            // First attempt to login. Gets rid of second session variable so user cannot try to enter the url again    
            if (!IsPostBack && Session["loggedInCustomerId"] != null && Session["loggedInCustomer"] != null)
            {
                customerId = Convert.ToInt32(Session["loggedInCustomerId"]);
                Session.Remove("loggedInCustomer");
            }
            else if(!IsPostBack) // If either check fails above, redirect
            {
                Session.Clear();
                Response.Redirect("~/Registration.aspx");
            }
            else if (IsPostBack && Session["loggedInCustomerId"]==null) // If you have post back but dont have customer id, redirect
            {
                Session.Clear();
                Response.Redirect("~/Registration.aspx");
            }
            else // Reassign customer id
            {
                customerId = Convert.ToInt32(Session["loggedInCustomerId"]);
                
            }
            
        }

        /// <summary>
        /// Filters the available docks by electrical service and water service drop down list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool needElectricalService = false; // Filter for electrical service
            bool needWaterService = false; // Filter for water service
            // Assigns filter variables based on value of drop down list item selected
            switch (filterDropDownList.SelectedValue)
            {
                case "ES":
                    needElectricalService = true;
                    break;
                case "WS":
                    needWaterService = true;
                    break;
                case "ESWS":
                    needElectricalService = true;
                    needWaterService = true;
                    break;
                default:
                    break; // No filter
            }
            try
            {
                List<Dock> allDocks = DockDB.GetDocks();
                // Cycle through all docks in drop down list
                for (int i = 0; i < dockDropDownList.Items.Count; i++)
                {
                    bool enabled = true; // Assume enabled
                                         // Get dock id
                    int dockId = Convert.ToInt32(dockDropDownList.Items[i].Value);
                    // Go through all docks until we find a match
                    for (int j = 0; j < allDocks.Count; j++)
                    {
                        if (dockId == allDocks[j].DockId)
                        {
                            // Check for electrical service. Disable if required but not available.
                            if (needElectricalService && allDocks[j].ElectricalService == false)
                            {
                                enabled = false;
                            }
                            // Check for water service. Disable if required but not available.
                            if (needWaterService && allDocks[j].WaterService == false)
                            {
                                enabled = false;
                            }
                        }
                    }
                    // Sets enabled property of dropdownlist item
                    dockDropDownList.Items[i].Enabled = enabled;
                }
                // Selects first item in drop down list
                dockDropDownList.SelectedIndex = 0;
            }
            catch
            {
                errorLabel2.Visible = true;
            }
        }

        /// <summary>
        /// Processes all click events within slip grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void slipsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // For lease click (assigned to new button)
                if (e.CommandName == "New")
                {
                    if (customerId < 0) // If customer id is invalid dont run method
                    {
                        return;
                    }
                    // Get slip id from active row
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = slipsGridView.Rows[index];
                    int slipId = Convert.ToInt32(row.Cells[0].Text);

                    // Lease slip in DB
                    LeaseDB.LeaseSlip(customerId, slipId);

                    // Refresh grid views
                    leasedSlipsGridView.DataBind();
                    slipsGridView.DataBind();
                }
            }
            catch (Exception)
            {
                errorLabel2.Visible = true;
            }
        }

        /// <summary>
        /// Assigns customer ID to grid view before it runs its select statement (which needs customer Id)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LeasedSlipsObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["CustomerId"] = Convert.ToInt32(customerId);
        }

        /// <summary>
        /// Clears session and logsout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Registration.aspx");
        }
    }
}