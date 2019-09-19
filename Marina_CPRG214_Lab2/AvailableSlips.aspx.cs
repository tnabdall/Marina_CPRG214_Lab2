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
            for(int i = 0; i<dockDropDownList.Items.Count; i++)
            {
                bool enabled = true;
                int dockId = Convert.ToInt32(dockDropDownList.Items[i].Value);
                for (int j = 0; j<allDocks.Count; j++)
                {
                    if (dockId == allDocks[j].DockId)
                    {
                        if (needElectricalService && allDocks[j].ElectricalService==false)
                        {
                            enabled = false;                            
                        }
                        if(needWaterService && allDocks[j].WaterService == false)
                        {
                            enabled = false;                            
                        }
                    }
                }
                dockDropDownList.Items[i].Enabled = enabled;
            }
            dockDropDownList.SelectedIndex = 0;
        }
    }
}
