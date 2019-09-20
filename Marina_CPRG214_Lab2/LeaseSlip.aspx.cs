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
        private int customerId;
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


        protected void filterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool needElectricalService = false;
            bool needWaterService = false;
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
                    break;
            }
            List<Dock> allDocks = DockDB.GetDocks();
            for (int i = 0; i < dockDropDownList.Items.Count; i++)
            {
                bool enabled = true;
                int dockId = Convert.ToInt32(dockDropDownList.Items[i].Value);
                for (int j = 0; j < allDocks.Count; j++)
                {
                    if (dockId == allDocks[j].DockId)
                    {
                        if (needElectricalService && allDocks[j].ElectricalService == false)
                        {
                            enabled = false;
                        }
                        if (needWaterService && allDocks[j].WaterService == false)
                        {
                            enabled = false;
                        }
                    }
                }
                dockDropDownList.Items[i].Enabled = enabled;
            }
            dockDropDownList.SelectedIndex = 0;
        }

        protected void slipsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // For lease click
            if (e.CommandName == "New")
            {
                if (customerId < 0)
                {
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = slipsGridView.Rows[index];

                int slipId = Convert.ToInt32(row.Cells[0].Text);

                LeaseDB.LeaseSlip(customerId, slipId);

                leasedSlipsGridView.DataBind();
                slipsGridView.DataBind();
            }
        }

        protected void LeasedSlipsObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["CustomerId"] = Convert.ToInt32(customerId);
        }
    }
}