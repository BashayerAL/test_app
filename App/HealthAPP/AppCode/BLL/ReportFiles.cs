using HealthAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAPP.BLL
{

    public class ReportFiles
    {
        int _Id;
        int _ReportId;
        byte _Type;
        DateTime _UploadDate;
        int _Uploader;
        string _Title;
        byte[] _FileBinary;
        string _ContentType;
        public string TypeName
        {
            get
            {
                if (_Type == 1) return "الموضوع";
                else if (_Type == 2) return "الوقائع";
                else if (_Type == 3) return "الاجراءات";
                else return "";
            }
        }

        public int Id { get => _Id; set => _Id = value; }
        public int ReportId { get => _ReportId; set => _ReportId = value; }
        public byte Type { get => _Type; set => _Type = value; }
        public DateTime UploadDate { get => _UploadDate; set => _UploadDate = value; }
        public int Uploader { get => _Uploader; set => _Uploader = value; }
        public string Title { get => _Title; set => _Title = value; }
        public byte[] FileBinary { get => _FileBinary; set => _FileBinary = value; }
        public string ContentType { get => _ContentType; set => _ContentType = value; }

        public ReportFiles(int reportid, byte type, int uploader, DateTime uploaddate, string title)
            : this( 0,  reportid,  type,  uploader,  uploaddate,  title)
        { }
        public ReportFiles(int id, int reportid, byte type, int uploader, DateTime uploaddate, string title)
        {
            _Id = id;
            _ReportId = reportid;
            _Uploader = uploader;
            _UploadDate = uploaddate;
            _Title = title;
            _Type = type;
        }

        //* Methods ////
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (Id <= 0)
            {
                int TempId = DALLayer.CreateNewReportFiles(this);
                if (TempId > 0)
                {
                    _Id = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (false);
        }

        public static bool Delete(int id)
        {
            DataAccess DALLAYER = DataAccessHelper.GetDataAccess();
            return (DALLAYER.DeleteReportFiles(id));
        }
        public static List<ReportFiles> GetByReport(int report)
        {
            DataAccess DALLAYER = DataAccessHelper.GetDataAccess();
            return (DALLAYER.GetReportFilesByReport(report));
        }
        public static ReportFiles GetById(int id)
        {
            if (id <= 0)
                return (null);
            DataAccess DALLAYER = DataAccessHelper.GetDataAccess();
            return (DALLAYER.GetReportFilesById(id));
        }
      
    }

}