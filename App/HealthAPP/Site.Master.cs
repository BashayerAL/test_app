using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthAPP.BLL;
using System.Management;

namespace HealthAPP
{
    public partial class SiteMaster : MasterPage
    {
        public string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                lnk_Me.Text = "Design By JENAN-ALSHAMMARI";
                lnk_Me.NavigateUrl = "http://www.linkedin.com/in/jenan-alahmad";
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
                    if (emp.Status != 1)
                        Response.Redirect("Login");

                    UserName = emp.Name;
                    Pnl_Admin.Visible = false;
                    //if (emp.RoleId == 1)
                    //{
                    List<ReportData> rptlist = ReportData.GetByMakerIsNew(emp.Id);
                    List<ReportData> rptcommlist = ReportData.GetByCommitteeIsNew(emp.Id);
                    rptlist.AddRange(rptcommlist);
                    //RepeaterTasks.DataSource = rptlist;
                    //RepeaterTasks.DataBind();
                    //if (rptlist.Count > 0)
                    //{
                    //    Taskcountspan.Visible = true;
                    //    lbl_Taskscount.Text = rptlist.Count.ToString();
                    //}
                    //else
                    //    Taskcountspan.Visible = false;
                    // }
                    if (emp.RoleId > 1)
                    {
                        Pnl_Admin.Visible = true;
                        List<ReportData> rptlist2 = ReportData.GetByStatusIsNew(emp.RoleId);
                        rptlist.AddRange(rptlist2);

                    }
                    RepeaterTasks.DataSource = rptlist;
                    RepeaterTasks.DataBind();
                    if (rptlist.Count > 0)
                    {
                        Taskcountspan.Visible = true;
                        lbl_Taskscount.Text = rptlist.Count.ToString();
                    }
                    else
                        Taskcountspan.Visible = false;
                }
                //lnk_Me.Text = "Amr Etman";
                //lnk_Me.NavigateUrl = "https://www.linkedin.com/in/amretman/";
                //lnk_Me.ToolTip = "Designed & Developed by: Amr_Etman@hotmail.com(Not For Resale)";
                //if (HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    txt_Search.Visible = btn_Search.Visible = true;
                //    List<Message> msgList = Message.GetMessageToByIsNew(UberAll.GetCurrentEmp().Id, true);
                //    RepeaterMsg.DataSource = msgList;
                //    RepeaterMsg.DataBind();
                //    if (msgList.Count > 0)
                //    {
                //        msgcountspan.Visible = true;
                //        lbl_MsgCount.Text = msgList.Count.ToString();
                //    }
                //    else
                //        msgcountspan.Visible = false;

                //    List<Tasks> taskList = Tasks.GetTasksToByIsNew(UberAll.GetCurrentEmp().Id, true);
                //    List<Tasks> taskListalert = new List<Tasks>();
                //    foreach (var tsk in taskList)
                //    {
                //        taskListalert.Add(tsk);
                //        if (taskListalert.Count > 3) break;
                //    }

                //    RepeaterTasks.DataSource = taskListalert;
                //    RepeaterTasks.DataBind();
                //    if (taskList.Count > 0)
                //    {
                //        Taskcountspan.Visible = true;
                //        lbl_Taskscount.Text = taskList.Count.ToString();
                //    }
                //    else
                //        Taskcountspan.Visible = false;
                //}
                //else
                //{
                //    txt_Search.Visible = btn_Search.Visible = false;
                //}
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txt_Search.Text))
            //{
            //    // List<Client> clientlist = Client.GetBySearch("%" + txt_Search.Text + "%");
            //    Response.Redirect("~/Search?txt=%" + txt_Search.Text + "%");
            //    //Server.Transfer("Search.aspx");
            //}
        }
        protected void Unnamed_LoggingOut(object sender, EventArgs e)
        {
            // Context.GetOwinContext().Authentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session["emp"] = null;
            Response.Redirect("Login");
        }


    }
}