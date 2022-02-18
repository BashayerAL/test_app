using HealthAPP.AppCode.BLL;
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
    public partial class ReportCreate : System.Web.UI.Page
    {
        //  Employee _CurrentEmp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emp"] == null) Response.Redirect("Login");
            if (!IsPostBack)
            {
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

                Session["attachlistCreate"] = null;
                if (Request.QueryString["saved"] != null)
                {
                    lbl_error2.Text = lbl_error.Text = "تم الحفظ بنجاح !!";
                }
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
                    if (emp.Status > 1)
                        Response.Redirect("Login");
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
            if (Session["emp"] == null) Response.Redirect("Login");

        }
        protected void UploadControlPic_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                lbl_error.Text = lbl_error2.Text = "";
                ReportFiles file = new ReportFiles(0, 1, 0, DateTime.Now, e.UploadedFile.FileName);
                //Random rnd = new Random();
                //file.Id = rnd.Next(1, 200);
                file.FileBinary = e.UploadedFile.FileBytes;
                file.Title = e.UploadedFile.FileName;
                file.ContentType = e.UploadedFile.ContentType;
                //List<ReportFiles> filelist = new List<ReportFiles>();
                //if (Session["attachlistCreate"] != null)
                //    filelist = Session["attachlistCreate"] as List<ReportFiles>;
                //filelist.Add(file);
                Session["attachlistCreate"] = file;

                string name = e.UploadedFile.FileName;
                long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
                string sizeText = sizeInKilobytes.ToString() + " KB";
                e.CallbackData = name + "|" + "/#" + "|" + sizeText;
            }
            catch (Exception ex)
            {
                //lbl_msg.Text = "الملف مساحته كبيرة جدا";
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            lbl_error.Text = lbl_error2.Text = "";
            if (Session["CheckRefresh"] == null || ViewState["CheckRefresh"] == null) return;
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            ViewState["CheckRefresh"] = null;
            int empid = 0;
            int.TryParse(Session["emp"].ToString(), out empid);
            Employee emp = Employee.GetById(empid);
            if (emp == null)
                Response.Redirect("Login");
            if (emp.Status > 1)
                Response.Redirect("Login");
            if (string.IsNullOrEmpty(txt_issueDate.Text) | txt_issueDate.Date <= DefaultValues.GetDateTimeMinValue())
            {
                lbl_error.Text = "يجب ادخال تاريخ المعاملة!"; return;
            }
            ReportData report = new ReportData(emp.Id, DateTime.Now, Convert.ToByte(rb_Type.SelectedValue), txt_IssueNo.Text.Trim(), txt_issueDate.Date,
                Convert.ToByte(rb_priority.SelectedValue), txt_subject.Text, txt_basedon.Text, txt_Facts.Text, txt_procedures.Text, txt_results.Text, txt_recommend.Text, 1, true);
            report.OutsideNotes = txt_Outsidenotes.Text;
            if (report.Save())
            {
                //save File
                if (Session["attachlistCreate"] != null)
                {
                    ReportFiles file = Session["attachlistCreate"] as ReportFiles; 
                    file.Uploader = emp.Id;
                    file.ReportId = report.Id;
                    file.Save();
                }
                //Add Committee 
                List<object> keyValues = GridLookup.GridView.GetSelectedFieldValues("Id");
                //if (keyValues.Count == 0)
                //{
                //    lbl_error.Text = lbl_error2.Text = "اختار مستخدم اولا"; return;
                //}
                foreach (var empTo in keyValues)
                {
                    if (Convert.ToInt32(empTo) != empid)
                    {
                        ReportData.SetCommittee(Convert.ToInt32(empTo), report.Id);
                    }
                }
                Response.Redirect("ReportCreate?saved=true");
            }
        }

        byte[] toArray(Stream sourceStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            lbl_error.Text = lbl_error2.Text = "";
            //Session["attachlist"] = null;
            Response.Redirect(Request.Url.AbsolutePath);
        }
    }
}