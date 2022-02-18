using HealthAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAPP.BLL
{

    [Serializable]
    public class ReportData
    {

        int _Id;
        int _Maker;
        DateTime _MakeDate;
        byte _Type;
        string _IssueNo;
        DateTime _IssueDate;
        byte _Priority;
        string _Subject;
        string _BasedOn;
        string _Facts;
        string _Procedures;
        string _Results;
        string _Recommend;
        String _OutsideNotes;
        DateTime _LastEditDate;
        int _LastEditor;
        byte _Status;
        bool _IsNew;
        string _ManagerNotes;
        int _ManagerSign;
        DateTime _ManagerSignDate;
        string _CeoNotes;
        int _CeoSign;
        DateTime _CeoSignDate;
        DateTime _ViewDate;

        public string StatusName
        {
            get
            {
                if (Status == 1)
                    return "مفتوح";
                else if (Status == 2)
                    return "ارسال الى المدير";
                else if (Status == 3)
                    return "ارسال الى الرئيس تنفيذي";
                else if (Status == 4)
                    return "مغلق";
                else
                    return "";
            }
        }
        public string TypeName
        {
            get
            {
                if (_Type == 1)
                    return "إداري";
                else if (_Type == 2)
                    return "طبي";
                else
                    return "";
            }
        }
        public string PriorityName
        {
            get
            {
                if (_Priority == 1)
                    return "عادي";
                else if (_Priority == 2)
                    return "سري";
                else if (_Priority == 3)
                    return "عاجل";
                else if (_Priority == 4)
                    return "عاجل جدا";
                else
                    return "";
            }
        }
        List<Employee> _EmpCommitteeList;
        public string ReportTypeName
        {
            get
            {
                if (_EmpCommitteeList == null)
                    _EmpCommitteeList = Employee.GetByReport(_Id);
                if (_EmpCommitteeList == null || _EmpCommitteeList.Count == 0)
                    return "فردي";
                return "لجنة";
            }
        }
        Employee _EditorName;
        public string EditorName
        {
            get
            {
                if (_EditorName == null)
                    _EditorName = Employee.GetById(_LastEditor);
                if (_EditorName == null) return "";
                return _EditorName.SignName;
            }
        }
        Employee _MakerName;
        public string MakerName
        {
            get
            {
                if (_MakerName == null)
                    _MakerName = Employee.GetById(_Maker);
                if (_MakerName == null) return "";
                return _MakerName.SignName;
            }
        }
        Employee _ManagerName;
        public string ManagerName
        {
            get
            {
                if (_ManagerName == null)
                    _ManagerName = Employee.GetById(_ManagerSign);
                if (_ManagerName == null) return "";
                return _ManagerName.SignName;
            }
        }
        Employee _CeoName;
        public string CeoName
        {
            get
            {
                if (_CeoName == null)
                    _CeoName = Employee.GetById(_CeoSign);
                if (_CeoName == null) return "";
                return _CeoName.SignName;
            }
        }
        public string MakerDescription
        {
            get
            {
                if (_MakerName == null)
                    _MakerName = Employee.GetById(_Maker);
                if (_MakerName == null) return "";
                return _MakerName.Notes;
            }
        }

        public int Id { get => _Id; set => _Id = value; }
        public int Maker { get => _Maker; set => _Maker = value; }
        public DateTime MakeDate { get => _MakeDate; set => _MakeDate = value; }
        public byte Type { get => _Type; set => _Type = value; }
        public string IssueNo { get => _IssueNo; set => _IssueNo = value; }
        public DateTime IssueDate { get => _IssueDate; set => _IssueDate = value; }
        public byte Priority { get => _Priority; set => _Priority = value; }
        public string Subject { get => _Subject; set => _Subject = value; }
        public string BasedOn { get => _BasedOn; set => _BasedOn = value; }
        public string Facts { get => _Facts; set => _Facts = value; }
        public string Procedures { get => _Procedures; set => _Procedures = value; }
        public string Results { get => _Results; set => _Results = value; }
        public string Recommend { get => _Recommend; set => _Recommend = value; }
        public DateTime LastEditDate { get => _LastEditDate; set => _LastEditDate = value; }
        public int LastEditor { get => _LastEditor; set => _LastEditor = value; }
        public byte Status { get => _Status; set => _Status = value; }
        public bool IsNew { get => _IsNew; set => _IsNew = value; }
        public string ManagerNotes { get => _ManagerNotes; set => _ManagerNotes = value; }
        public int ManagerSign { get => _ManagerSign; set => _ManagerSign = value; }
        public DateTime ManagerSignDate { get => _ManagerSignDate; set => _ManagerSignDate = value; }
        public string CeoNotes { get => _CeoNotes; set => _CeoNotes = value; }
        public int CeoSign { get => _CeoSign; set => _CeoSign = value; }
        public DateTime CeoSignDate { get => _CeoSignDate; set => _CeoSignDate = value; }
        public DateTime ViewDate { get => _ViewDate; set => _ViewDate = value; }
        public string OutsideNotes { get => _OutsideNotes; set => _OutsideNotes = value; }

        public ReportData()
        { }
        public ReportData(int maker, DateTime makeDate, byte type, string issueNo,
        DateTime issueDate, byte priority, string subject, string basedOn, string facts, string procedures, string results, string recommend, byte status,
        bool isNew)
            : this(0, maker, makeDate, type, issueNo,
         issueDate, priority, subject, basedOn, facts, procedures, results, recommend, status,
         isNew)
        {

        }
        public ReportData(int id, int maker, DateTime makeDate, byte type, string issueNo,
        DateTime issueDate, byte priority, string subject, string basedOn, string facts, string procedures, string results, string recommend, byte status,
        bool isNew)
        {
            _Id = id;
            _Maker = maker;
            _MakeDate = makeDate;
            _Type = type;
            _IssueNo = issueNo;
            _IssueDate = issueDate;
            _Priority = priority;
            _Subject = subject;
            _BasedOn = basedOn;
            _Facts = facts;
            _Procedures = procedures;
            _Results = results;
            _Recommend = recommend;
            _Status = status;
            _IsNew = isNew;
        }

        //* Methods ////////////////
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (Id <= 0)
            {
                int TempId = DALLayer.CreateNewReportData(this);
                if (TempId > 0)
                {
                    Id = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateReportData(this));
        }

        public static List<ReportData> GetAll()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllReportDatas());
        }
        public static ReportData GetById(int ReportDataId)
        {
            if (ReportDataId <= 0)
                return (null);

            DataAccess DALLAYER = DataAccessHelper.GetDataAccess();
            return (DALLAYER.GetReportDataById(ReportDataId));
        }
        public static List<ReportData> GetByCommittee(int emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByCommittee(emp));
        }
        public static List<ReportData> GetByCommitteeIsNew(int emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByCommitteeIsNew(emp));
        }
        public static List<ReportData> GetByMaker(int emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportDataByMaker(emp));
        }
        public static List<ReportData> GetByMakerIsNew(int emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByMakerIsNew(emp));
        }
        public static List<ReportData> GetByStatus(byte status)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByStatus(status));
        }
        public static List<ReportData> GetByStatusMaker(byte status,int maker)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByStatusMaker(status,maker));
        }
        public static List<ReportData> GetByStatusIsNew(byte status)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetReportByStatusIsNew(status));
        }
        public static bool SetCeoSign(int ceosign, string ceonotes, DateTime ceosigndate, bool isnew, int id, byte status)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.SetReportCeoSign(ceosign, ceonotes, ceosigndate, isnew, id, status));
        }
        public static bool SetManagerSign(int managersign, string managernotes, DateTime managersigndate, bool isnew, int id, byte status)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.SetReportManagerSign(managersign, managernotes, managersigndate, isnew, id, status));
        }
        public static bool SetCommitteeView(int empid, int reportid)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.SetReportCommitteeView(empid, reportid));
        }
        public static bool SetCommittee(int empid, int reportid)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.SetReportCommittee(empid, reportid));
        }
        public static bool DeleteCommittee(int empid, int reportid)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.DeleteReportCommittee(empid, reportid));
        }
        public static bool SetReportView(int reportid)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.SetReportView(reportid));
        }
    }
}