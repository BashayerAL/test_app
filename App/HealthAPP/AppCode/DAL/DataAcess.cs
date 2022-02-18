using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using HealthAPP.BLL;

namespace HealthAPP.DAL
{
    public abstract class DataAccess
    {
        /*** PROPERTIES ***/
        protected string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["HealthCS"] == null)
                    throw (new NullReferenceException("ConnectionString configuration is missing from you web.config. It should contain  <connectionStrings> <add key=\"aspnet_staterKits_TimeTracker\" value=\"Server=(local);Integrated Security=True;Database=Issue_Tracker\" </connectionStrings>"));

                string connectionString = ConfigurationManager.ConnectionStrings["HealthCS"].ConnectionString;

                if (String.IsNullOrEmpty(connectionString))
                    throw (new NullReferenceException("ConnectionString configuration is missing from you web.config. It should contain  <connectionStrings> <add key=\"aspnet_staterKits_TimeTracker\" value=\"Server=(local);Integrated Security=True;Database=Issue_Tracker\" </connectionStrings>"));
                else
                    return (connectionString);
            }
        }

        /*** METHODS  ***/
        //* Employee ///////
        public abstract int CreateNewEmployee(Employee newEmployee);
        public abstract bool UpdateEmployee(Employee newEmployee);
        public abstract List<Employee> GetAllEmployees();
        public abstract List<Employee> GetByRoleId(byte role);
        public abstract Employee GetEmployeeById(int Id);
        public abstract Employee GetByUserName(string username);
        public abstract List<Employee> GetByIsActive(byte isactive);
        public abstract List<Employee> GetEmployeeByReport(int report);

        //* ReportData ////////////////
        public abstract int CreateNewReportData(ReportData newReportData);
        public abstract bool UpdateReportData(ReportData newReportData);
        public abstract List<ReportData> GetAllReportDatas();
        public abstract List<ReportData> GetReportByCommittee(int empid);
        public abstract List<ReportData> GetReportByCommitteeIsNew(int empid);
        public abstract List<ReportData> GetReportDataByMaker(int maker);
        public abstract List<ReportData> GetReportByMakerIsNew(int maker);
        public abstract List<ReportData> GetReportByStatus(byte status);
        public abstract List<ReportData> GetReportByStatusIsNew(byte status);
        public abstract bool SetReportCeoSign(int ceosign, string ceonotes, DateTime ceosigndate, bool isnew, int id, byte status);
        public abstract bool SetReportManagerSign(int managersign, string managernotes, DateTime managersigndate, bool isnew, int id, byte status);
        public abstract bool SetReportCommitteeView(int empid, int report);
        public abstract bool SetReportView(int report);
        public abstract ReportData GetReportDataById(int id);
        public abstract bool SetReportCommittee(int empid, int report);
        public abstract bool DeleteReportCommittee(int empid, int report);
        public abstract List<ReportData> GetReportByStatusMaker(byte status, int maker);

        //* ReportFiles ///////////
        public abstract ReportFiles GetReportFilesById(int Id);
        public abstract List<ReportFiles> GetReportFilesByReport(int report);
        public abstract bool DeleteReportFiles(int ID);
        public abstract int CreateNewReportFiles(ReportFiles newReportFiles);

    }
}