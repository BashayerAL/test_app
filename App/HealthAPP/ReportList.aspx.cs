using CrystalDecisions.CrystalReports.Engine;
using HealthAPP.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthAPP
{
    public partial class ReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emp"] == null) Response.Redirect("Login");
            if (Session["emp"] == null) Response.Redirect("Login");
            if (!IsPostBack)
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.RoleId > 1)
                    Response.Redirect("ReportListManager");
            }
        }

        protected void EditLink_Click(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            if (id > 0)
            {
                if (Session["emp"] == null) Response.Redirect("Login");
                using (ReportDocument rpt = new ReportDocument())
                {
                    int empid = 0;
                    int.TryParse(Session["emp"].ToString(), out empid);
                    Employee emp = Employee.GetById(empid);
                    if (emp == null)
                        Response.Redirect("Login");
                    if (emp.Status > 1)
                        Response.Redirect("Login");
                    ReportData _CurrentReport = ReportData.GetById(id);
                    if (_CurrentReport != null)
                    {
                        List<ReportData> rptlist = new List<ReportData>();
                        rptlist.Add(_CurrentReport);
                        string path = Server.MapPath("Report/Reportrpt.rpt");
                        rpt.Load(path);
                        rpt.SetDataSource(rptlist);
                        string committee = "";
                        List<Employee> emplist = Employee.GetByReport(_CurrentReport.Id);
                        foreach (var item in emplist)
                        {
                            if (!string.IsNullOrEmpty(committee)) committee += "  |  ";
                            committee += item.SignName + " - " + item.Notes;
                        }
                        rpt.SetParameterValue("@committee", committee);
                        rpt.SetParameterValue("@emp", emp.SignName);
                        string editinfo = " ";
                        if (_CurrentReport.LastEditor > 0)
                            editinfo = "آخر تعديل تم في " + _CurrentReport.LastEditDate.ToString("d/M/yyyy tt hh:mm") + " | بواسطة: " + _CurrentReport.EditorName;
                        rpt.SetParameterValue("@Editinfo", editinfo);

                        Stream stream2 = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        rpt.Close();
                        rpt.Clone();
                        rpt.Dispose();
                        //  UberAll.ReportExport(stream2, "pdf", false);

                        System.Web.HttpContext.Current.Response.Clear();
                        System.Web.HttpContext.Current.Response.ContentType = "application/" + "pdf";
                        System.Web.HttpContext.Current.Response.AddHeader("Accept-Header", stream2.Length.ToString());
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", ("Attachment") + "; filename=" + "exportReport_" + _CurrentReport.Id + "." + "pdf");
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Length", stream2.Length.ToString());
                        //Response.ContentEncoding = System.Text.Encoding.Default;
                        // stream = stream2 as MemoryStream;
                        System.Web.HttpContext.Current.Response.BinaryWrite(UberAll.ReadFully(stream2));
                        System.Web.HttpContext.Current.Response.End();
                    }
                    // RakheesAll.UserTrack(Request.Url.AbsoluteUri, 5, "طباعة تايم كارد كود  " + emp.Code + " - " + month.NameYear + " -" + rb_Status.SelectedItem.Text, 0);
                }
            }
        }
    }
}