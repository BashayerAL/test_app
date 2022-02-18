using HealthAPP.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthAPP
{
    public partial class EmpCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emp"] == null) Response.Redirect("Login");
            // lbl_Error.Text = "";
            if (!IsPostBack)
            {
                if (Session["emp"] == null)
                {
                    if (!Request.Url.AbsoluteUri.ToLower().Contains("login"))
                        Response.Redirect("Login");

                }
                else
                {
                    int empid = 0;
                    int.TryParse(Session["emp"].ToString(), out empid);
                    Employee emp = Employee.GetById(empid);
                    if (emp == null & !Request.Url.AbsoluteUri.ToLower().Contains("login"))
                        Response.Redirect("Login");
                    if (emp.RoleId < 2 | emp.RoleId > 3)
                        Response.Redirect("Login");
                }
            }

        }

        protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            if (e.AffectedRecords <= 0)
            {
                lbl_Error.Text = "اسم مستخدم موجود من قبل!";
            }
        }
    }
}