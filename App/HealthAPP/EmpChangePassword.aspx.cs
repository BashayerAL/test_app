using HealthAPP.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthAPP
{
    public partial class EmpChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emp"] == null) Response.Redirect("Login");
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
                    lbl_Username.Text = emp.UserName;
                }
            }

        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            int empid = 0;
            int.TryParse(Session["emp"].ToString(), out empid);
            Employee emp = Employee.GetById(empid);
            if (emp == null & !Request.Url.AbsoluteUri.ToLower().Contains("login"))
                Response.Redirect("Login");
            if (lbl_Username.Text != emp.UserName)
            { lbl_error2.Text = "حدث خطأ يرجي اعادة المحاولة!";return; }
            if(string.IsNullOrEmpty(txt_PassNew.Text)| string.IsNullOrEmpty(txt_PassCurrent.Text))
            { lbl_error2.Text = "يجب ادخال كلمة المرور القديمة والجديدة!"; return; }
            if (txt_PassCurrent.Text != emp.Password)
            { lbl_error2.Text = "كلمة المرور الحالية غير صحيحة!"; return; }
            emp.Password = txt_PassNew.Text;
          if(  emp.Save())
            {
                txt_PassNew.Text = txt_PassNew2.Text = txt_PassCurrent.Text = "";
                lbl_error2.Text = "تم تغيير كلمة المرور بنجاح";
                Timer1.Enabled = true;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session["emp"] = null;
            Response.Redirect("Login");
        }
    }
}