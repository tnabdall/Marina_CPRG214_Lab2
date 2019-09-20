using Marina_CPRG214_Lab2.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marina_CPRG214_Lab2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                List<Dock> allDocks = DockDB.GetDocks(); // Gets all docks
                                                         // Cycle through all docks in drop down list
                for (int i = 0; i < dockDropDownList.Items.Count; i++)
                {
                    bool enabled = true; // Assume enabled to start
                                         // Get dock id
                    int dockId = Convert.ToInt32(dockDropDownList.Items[i].Value);
                    // Go through all docks until we find a match
                    for (int j = 0; j < allDocks.Count; j++)
                    {
                        if (dockId == allDocks[j].DockId) // Match is found
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
            catch(Exception)
            {
                errorLabel.Visible = true;
            }
        }
    }
}
