using CrystalDecisions.CrystalReports.Engine;
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
    public partial class ReportEdit : System.Web.UI.Page
    {
        ReportData _CurrentReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetCurrentReport();
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
                    if (_CurrentReport == null)
                    {
                        lbl_error2.Text = "تقرير غير موجود"; btn_Send.Visible = false;
                    }
                    int empid = 0;
                    int.TryParse(Session["emp"].ToString(), out empid);
                    Employee emp = Employee.GetById(empid);
                    if (emp == null & !Request.Url.AbsoluteUri.ToLower().Contains("login"))
                        Response.Redirect("Login");
                    if (emp.Status > 1)
                        Response.Redirect("Login");
                    if (emp.RoleId == 1)
                    {
                        if (_CurrentReport.Status == 1)
                        {
                            if (_CurrentReport.Maker != emp.Id)
                            {
                                List<Employee> empcommittee = Employee.GetByReport(_CurrentReport.Id);
                                bool incommittee = false;
                                foreach (var empchk in empcommittee)
                                {
                                    if (empchk.Id == emp.Id)
                                    {
                                        incommittee = true; ReportData.SetCommitteeView(emp.Id, _CurrentReport.Id);
                                    }
                                }
                                if (!incommittee)
                                {
                                    lbl_error2.Text = "ليس لديك صلاحيات هل هذا التقرير!"; return;
                                }
                                else
                                    Pnl_Committee.Enabled = true;
                                Pnl_AttachList.Visible = true;
                                Pnl_Upload1.Visible = Pnl_Upload2.Visible = false;
                                Pnl_Emp.Enabled = Pnl_Emp2.Enabled = Pnl_Emp3.Enabled = false;
                            }
                            else
                            {
                                //if (_CurrentReport.IsNew && _CurrentReport.Status == 1)
                                //{
                                //    ReportData.SetReportView(_CurrentReport.Id);

                                //}
                            }
                        }

                        if (_CurrentReport.Status > 1)
                        {
                            Pnl_Save.Enabled = false;
                            btn_Upload.Enabled = btn_fileFacts.Enabled = btn_fileProc.Enabled = false;
                        }
                        Pnl_Ceo.Enabled = Pnl_Manager.Enabled = false;
                    }
                    else
                    {
                        Pnl_AttachList.Visible = true;
                        Pnl_Upload1.Visible = Pnl_Upload2.Visible = false;

                        Pnl_Emp.Enabled = Pnl_Emp2.Enabled = Pnl_Emp3.Enabled = Pnl_Committee.Enabled = false;
                        if (emp.RoleId == 2)
                        {
                            //if (_CurrentReport.IsNew && _CurrentReport.Status == 2)
                            //    ReportData.SetReportView(_CurrentReport.Id);

                            if (_CurrentReport.Status != 2)
                            {
                                Pnl_Save.Enabled = false;
                            }
                            else
                                btn_back.Visible = true;
                            Pnl_Manager.Enabled = true; Pnl_Ceo.Enabled = false;
                        }
                        else if (emp.RoleId == 3)
                        {
                            if (_CurrentReport.Status != 3)
                            {
                                Pnl_Save.Enabled = false;
                            }
                            Pnl_Ceo.Enabled = true; Pnl_Manager.Enabled = false;
                        }
                        if (_CurrentReport.Status == 1)
                            Pnl_Manager.Enabled = Pnl_Ceo.Enabled = false;
                    }
                    SetData();
                }
            }
        }
        public void GetCurrentReport()
        {
            if (Session["emp"] == null) Response.Redirect("Login");
            int rptId;
            lbl_error.Text = lbl_error2.Text = string.Empty;
            if (Request.QueryString["rpt"] != null && int.TryParse((string)Request.QueryString["rpt"], out rptId))
            {
                _CurrentReport = ReportData.GetById(rptId);
            }
            if (_CurrentReport == null)
            {
                lbl_error2.Text = "تقرير غير موجود"; btn_Send.Visible = false;
            }
            else
                HiddenField_Id.Value = _CurrentReport.Id.ToString();
            //  RakheesAll.UserTrack(Request.Url.AbsoluteUri, 1, "موظف", _CurrentEmployee.Code);

        }
        public void SetData()
        {
            if (_CurrentReport == null)
            {
                lbl_error2.Text = "تقرير غير موجود"; return;
            }
            prntLink.NavigateUrl = "print?reportid=" + _CurrentReport.Id;
            txt_Outsidenotes.Text = _CurrentReport.OutsideNotes;
            lbl_Id.Text = _CurrentReport.Id.ToString();
            lbl_Makedate.Text = _CurrentReport.MakeDate.ToString("d/M/yyyy  tt HH:mm");
            lbl_makername.Text = _CurrentReport.MakerName + " | " + _CurrentReport.MakerDescription;
            txt_basedon.Text = _CurrentReport.BasedOn;
            txt_Facts.Text = _CurrentReport.Facts;
            txt_issueDate.Date = _CurrentReport.IssueDate;
            txt_IssueNo.Text = _CurrentReport.IssueNo;
            txt_procedures.Text = _CurrentReport.Procedures;
            txt_recommend.Text = _CurrentReport.Recommend;
            txt_results.Text = _CurrentReport.Results;
            txt_subject.Text = _CurrentReport.Subject;
            rb_priority.SelectedValue = _CurrentReport.Priority.ToString();
            rb_Type.SelectedValue = _CurrentReport.Type.ToString();
            List<ReportFiles> filelist = ReportFiles.GetByReport(_CurrentReport.Id);
            rep_Attach_1.DataSource = filelist.Where(fl => fl.Type == 1);
            rep_Attach_1.DataBind();
            rep_Attach_2.DataSource = filelist.Where(fl => fl.Type == 2);
            rep_Attach_2.DataBind();
            List<Employee> empcommitteeList = Employee.GetByReport(_CurrentReport.Id);
            GridView_emp.DataSource = empcommitteeList;
            GridView_emp.DataBind();
            if (_CurrentReport.Status == 2)
            { btn_Send.Text = "إرسال الى الرئيس التنفيذي"; lbl_Send.Text = "تم الارسال الى المدير"; }
            if (_CurrentReport.Status == 3)
            { btn_Send.Text = "اعتماد"; lbl_Send.Text = "تم الارسال الى الرئيس التنفيذي"; }
            if (_CurrentReport.Status > 3)
            {
                btn_fileFacts.Enabled = btn_Upload.Enabled = btn_fileProc.Enabled = Pnl_Save.Enabled = false; lbl_Send.Text = "تقرير مغلق";
            }
            if (_CurrentReport.LastEditor > 0)
                lbl_Edit.Text = "آخر تعديل تم في " + _CurrentReport.LastEditDate.ToString("d/M/yyyy tt hh:mm") + " | بواسطة: " + _CurrentReport.EditorName;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
            if (Session["emp"] == null) Response.Redirect("Login");
            //
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            lbl_error.Text = lbl_error2.Text = "";
            if (Session["CheckRefresh"] == null || ViewState["CheckRefresh"] == null) return;
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                return;
            }
            ViewState["CheckRefresh"] = null;
            if (Session["emp"] == null) Response.Redirect("Login");
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
            if (_CurrentReport == null)
            {
                lbl_error.Text = "يجب ادخال تاريخ المعاملة!"; return;
            }
            _CurrentReport.BasedOn = txt_basedon.Text;
            _CurrentReport.Facts = txt_Facts.Text;
            _CurrentReport.IssueDate = txt_issueDate.Date;
            _CurrentReport.IssueNo = txt_IssueNo.Text;
            _CurrentReport.Priority = Convert.ToByte(rb_priority.SelectedValue);
            _CurrentReport.Procedures = txt_procedures.Text;
            _CurrentReport.Recommend = txt_recommend.Text;
            _CurrentReport.Results = txt_results.Text;
            _CurrentReport.Subject = txt_subject.Text;
            _CurrentReport.Type = Convert.ToByte(rb_Type.SelectedValue);
            _CurrentReport.LastEditDate = DateTime.Now;
            _CurrentReport.LastEditor = emp.Id;
            _CurrentReport.OutsideNotes = txt_Outsidenotes.Text;
            if (_CurrentReport.Save())
            {
                //save File

                //Add Committee 
                List<object> keyValues = GridLookup.GridView.GetSelectedFieldValues("Id");
                foreach (var empTo in keyValues)
                {
                    if (Convert.ToInt32(empTo) != empid)
                    {
                        ReportData.SetCommittee(Convert.ToInt32(empTo), _CurrentReport.Id);
                    }
                }
                Response.Redirect("ReportEdit?saved=true&rpt=" + _CurrentReport.Id);
            }
        }


        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            lbl_error.Text = lbl_error2.Text = "";
            if (_CurrentReport != null)
                Response.Redirect("ReportEdit?rpt=" + _CurrentReport.Id);
        }

        protected void hlink_Click(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            if (id > 0)
            {
                if (Session["emp"] == null) Response.Redirect("Login");
                ReportFiles file = ReportFiles.GetById(id);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = file.ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Title);
                Response.BinaryWrite(file.FileBinary);
                Response.Flush();
                Response.End();
            }
        }

        protected void lnk_DeletePic_Click(object sender, EventArgs e)
        {
            if (Session["emp"] == null) Response.Redirect("Login");
            if (_CurrentReport != null && _CurrentReport.Status == 1)
            {
                int fileid = 0;
                int.TryParse((sender as LinkButton).CommandArgument, out fileid);
                ReportFiles file = ReportFiles.GetById(fileid);
                if (file != null && file.ReportId == _CurrentReport.Id)
                {
                    ReportFiles.Delete(file.Id);
                    rep_Attach_1.DataSource = ReportFiles.GetByReport(_CurrentReport.Id);
                    rep_Attach_1.DataBind();
                }
            }
        }


        protected void lnk_Deleteemp_Click(object sender, EventArgs e)
        {
            if (_CurrentReport != null && _CurrentReport.Status == 1)
            {
                int empid = 0;
                int.TryParse((sender as LinkButton).CommandArgument, out empid);
                ReportData.DeleteCommittee(empid, _CurrentReport.Id);
                GridView_emp.DataSource = Employee.GetByReport(_CurrentReport.Id);
                GridView_emp.DataBind();
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            if (_CurrentReport != null)
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");
                if (emp.RoleId == 1)
                {

                    if (_CurrentReport.Status == 1)
                    {
                        _CurrentReport.Status = 2;
                        _CurrentReport.IsNew = true;
                        btn_Save_Click(null, null);
                        btn_Send.Enabled = false;
                        btn_Cancel_Click(null, null);
                    }
                }
                else if (emp.RoleId == 2)
                {
                    if (_CurrentReport.Status == 2)
                    {
                        ReportData.SetManagerSign(emp.Id, txt_ManagerNotes.Text, DateTime.Now, true, _CurrentReport.Id, 3);

                        lbl_Send.Text = "تم الارسال الى الرئيس التنفيذي";
                        btn_Send.Enabled = false;
                    }
                }
                else if (emp.RoleId == 3)
                {
                    if (_CurrentReport.Status == 3)
                    {
                        ReportData.SetCeoSign(emp.Id, txt_CeoNotes.Text, DateTime.Now, true, _CurrentReport.Id, 4);

                        lbl_Send.Text = "تم الاعتماد وغلق التقرير";
                        btn_Send.Enabled = false;
                    }
                }
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (_CurrentReport != null)
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");

                if (emp.RoleId == 2)
                {
                    if (_CurrentReport.Status == 2)
                    {
                        ReportData.SetManagerSign(emp.Id, txt_ManagerNotes.Text, DateTime.Now, true, _CurrentReport.Id, 1);

                        lbl_Send.Text = "تم الارجاع للموظف";
                        btn_Send.Enabled = false;
                    }
                }

            }
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            try
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");
                if (_CurrentReport != null && _CurrentReport.Status == 1)
                {
                    if (FileUpload_Subject.HasFile)
                    {
                        lbl_error.Text = lbl_error2.Text = "";
                        ReportFiles file = new ReportFiles(_CurrentReport.Id, 1, emp.Id, DateTime.Now, FileUpload_Subject.FileName);
                        file.FileBinary = FileUpload_Subject.FileBytes;
                        file.Title = FileUpload_Subject.FileName;
                        file.ContentType = "";
                        if (file.Save())
                        {
                            rep_Attach_1.DataSource = ReportFiles.GetByReport(_CurrentReport.Id).Where(fl => fl.Type == 1);
                            rep_Attach_1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }
        protected void btn_fileFacts_Click(object sender, EventArgs e)
        {
            try
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");
                if (_CurrentReport != null && _CurrentReport.Status == 1)
                {
                    if (FileUpload_Facts.HasFile)
                    {
                        lbl_error.Text = lbl_error2.Text = "";
                        ReportFiles file = new ReportFiles(_CurrentReport.Id, 2, emp.Id, DateTime.Now, FileUpload_Facts.FileName);
                        file.FileBinary = FileUpload_Facts.FileBytes;
                        file.Title = FileUpload_Facts.FileName;
                        file.ContentType = "";
                        if (file.Save())
                        {
                            rep_Attach_2.DataSource = ReportFiles.GetByReport(_CurrentReport.Id).Where(fl => fl.Type == 2);
                            rep_Attach_2.DataBind();
                        }
                        //file.ContentType = FileUpload_Facts.ContentType;

                        //string name = e.UploadedFile.FileName;

                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btn_prnt_Click(object sender, EventArgs e)
        {
            using (ReportDocument rpt = new ReportDocument())
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");
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

        protected void btn_fileProc_Click(object sender, EventArgs e)
        {
            try
            {
                int empid = 0;
                int.TryParse(Session["emp"].ToString(), out empid);
                Employee emp = Employee.GetById(empid);
                if (emp == null)
                    Response.Redirect("Login");
                if (emp.Status > 1)
                    Response.Redirect("Login");
                if (_CurrentReport != null && _CurrentReport.Status == 1)
                {
                    if (FileUpload_Proc.HasFile)
                    {
                        lbl_error.Text = lbl_error2.Text = "";
                        ReportFiles file = new ReportFiles(_CurrentReport.Id, 3, emp.Id, DateTime.Now, FileUpload_Proc.FileName);
                        file.FileBinary = FileUpload_Proc.FileBytes;
                        file.Title = FileUpload_Proc.FileName;
                        file.ContentType = "";
                        if (file.Save())
                        {
                            rep_Attach_3.DataSource = ReportFiles.GetByReport(_CurrentReport.Id).Where(fl => fl.Type == 3);
                            rep_Attach_3.DataBind();
                        }
                        //file.ContentType = FileUpload_Proc.ContentType;

                        //string name = e.UploadedFile.FileName;

                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }
    }
}