using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marina_CPRG214_Lab2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MarinaEntities db = new MarinaEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerNames = from c in db.Customers select c.FirstName + " " + c.LastName;
            testDropDownList.DataSource = customerNames.ToList();
            testDropDownList.DataBind();
        }
    }
}