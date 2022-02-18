using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using HealthAPP.BLL;
using System.Web.Security;

namespace HealthAPP.DAL
{
    public class SQLDataAccess : DataAccess
    {
        /*** DELEGATE ***/

        private delegate void TGenerateListFromReader<T>(SqlDataReader returnData, ref List<T> tempList);

        /*****************************  BASE CLASS IMPLEMENTATION *****************************/
        private TimeZoneInfo timez = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");


        //* Employee ////////////////////
        private const string SP_Employee_CREATE = "Employee_Create";
        private const string SP_Employee_UPDATE = "Employee_Update";
        private const string SP_Employee_GETALL = "Employee_GetAll";
        private const string SP_Employee_GETBYID = "Employee_GetById";
        private const string SP_Employee_GETBYROLE = "Employee_GetByRole";
        private const string SP_Employee_GETBYUSERNAME = "Employee_GetByUserName";
        private const string SP_Employee_GETBYACTIVE = "Employee_GetByActive";
        private const string SP_Employee_GETBYReport = "Employee_GetByReport";
        public override int CreateNewEmployee(Employee newEmployee)
        {
            if (newEmployee == null)
                throw (new ArgumentNullException("newEmployee"));

            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Name", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.Name);
            AddParamToSQLCmd(sqlCmd, "@SignName", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.SignName);
            AddParamToSQLCmd(sqlCmd, "@RoleId", SqlDbType.TinyInt, 0, ParameterDirection.Input, newEmployee.RoleId);
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.UserName);
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 0, ParameterDirection.Input, newEmployee.Password);
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, newEmployee.Status);
            AddParamToSQLCmd(sqlCmd, "@Notes", SqlDbType.NVarChar, 100, ParameterDirection.Input, string.IsNullOrEmpty(newEmployee.Notes) ? string.Empty : newEmployee.Notes);

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_CREATE);
            ExecuteScalarCmd(sqlCmd);

            return (Convert.ToInt32(sqlCmd.Parameters["@ReturnValue"].Value));
        }
        public override bool UpdateEmployee(Employee newEmployee)
        {
            if (newEmployee == null)
                throw (new ArgumentNullException("newEmployee"));
            if (newEmployee.Id <= 0)
                throw (new ArgumentOutOfRangeException("newEmployee.Id"));

            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Name", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.Name);
            AddParamToSQLCmd(sqlCmd, "@SignName", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.SignName);
            AddParamToSQLCmd(sqlCmd, "@RoleId", SqlDbType.TinyInt, 0, ParameterDirection.Input, newEmployee.RoleId);
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 50, ParameterDirection.Input, newEmployee.UserName);
            if (string.IsNullOrEmpty(newEmployee.Password))
            { Employee emppass = Employee.GetById(newEmployee.Id); newEmployee.Password = emppass.Password; }
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NVarChar, 0, ParameterDirection.Input, newEmployee.Password);
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, newEmployee.Status);
            AddParamToSQLCmd(sqlCmd, "@Notes", SqlDbType.NVarChar, 100, ParameterDirection.Input, string.IsNullOrEmpty(newEmployee.Notes) ? string.Empty : newEmployee.Notes);
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, newEmployee.Id);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_UPDATE);
            ExecuteScalarCmd(sqlCmd);

            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override List<Employee> GetAllEmployees()
        {
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETALL);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReader<Employee>, ref EmployeeList);
            return EmployeeList;
        }
        public override List<Employee> GetByRoleId(byte role)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@RoleId", SqlDbType.TinyInt, 0, ParameterDirection.Input, role);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETBYROLE);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReader<Employee>, ref EmployeeList);
            return EmployeeList;
        }

        public override Employee GetEmployeeById(int Id)
        {
            if (Id <= 0)
                return null;

            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETBYID);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReader<Employee>, ref EmployeeList);
            if (EmployeeList.Count > 0)
                return EmployeeList[0];
            else
                return null;
        }
        public override Employee GetByUserName(string username)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@UserName", SqlDbType.NVarChar, 50, ParameterDirection.Input, username);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETBYUSERNAME);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReader<Employee>, ref EmployeeList);
            if (EmployeeList.Count > 0)
                return EmployeeList[0];
            else
                return null;
        }
        public override List<Employee> GetByIsActive(byte isactive)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, isactive);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETBYACTIVE);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReader<Employee>, ref EmployeeList);
            return EmployeeList;
        }
        public void TGenerateEmployeeFromReader<T>(SqlDataReader returndata, ref List<Employee> EmployeeList)
        {
            while (returndata.Read())
            {
                Employee employee = new Employee((int)returndata["Id"], (string)returndata["Name"], (string)returndata["SignName"],
                    (byte)returndata["RoleId"], (string)returndata["UserName"], (string)returndata["Password"], (byte)returndata["Status"], (string)returndata["Notes"]);

                EmployeeList.Add(employee);
            }
        }
        public override List<Employee> GetEmployeeByReport(int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_Employee_GETBYReport);
            List<Employee> EmployeeList = new List<Employee>();
            TExecuteReaderCmd<Employee>(sqlCmd, TGenerateEmployeeFromReadercommittee<Employee>, ref EmployeeList);
            return EmployeeList;
        }
        public void TGenerateEmployeeFromReadercommittee<T>(SqlDataReader returndata, ref List<Employee> EmployeeList)
        {
            while (returndata.Read())
            {
                Employee employee = new Employee((int)returndata["Id"], (string)returndata["Name"], (string)returndata["SignName"],
                    (byte)returndata["RoleId"], (string)returndata["UserName"], (string)returndata["Password"], (byte)returndata["Status"], (string)returndata["Notes"]);
                if (returndata["ViewDate"] != DBNull.Value)
                    employee.ViewDate = (DateTime)returndata["ViewDate"];
                if (returndata["IsNew"] != DBNull.Value)
                    employee.IsNew = (bool)returndata["IsNew"];
                EmployeeList.Add(employee);
            }
        }
        //* ReportFiles **///////////////////
        private const string SP_ReportFiles_CREATE = "ReportFiles_Create";
        private const string SP_ReportFiles_GETBYID = "ReportFiles_GetById";
        private const string SP_ReportFiles_GETReportFilesBYREPORT = "ReportFiles_GetByReportId";
        private const string SP_ReportFiles_DELETE = "ReportFiles_Delete";

        public override int CreateNewReportFiles(ReportFiles newReportFiles)
        {
            if (newReportFiles == null)
                throw (new ArgumentNullException("newReportFiles"));

            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.TinyInt, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, newReportFiles.ReportId);
            AddParamToSQLCmd(sqlCmd, "@UploadDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportFiles.UploadDate);
            AddParamToSQLCmd(sqlCmd, "@Uploader", SqlDbType.SmallInt, 0, ParameterDirection.Input, newReportFiles.Uploader);
            AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 50, ParameterDirection.Input, newReportFiles.Title);
            AddParamToSQLCmd(sqlCmd, "@Type", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportFiles.Type);
            AddParamToSQLCmd(sqlCmd, "@FileBinary", SqlDbType.VarBinary, 0, ParameterDirection.Input, newReportFiles.FileBinary);
            AddParamToSQLCmd(sqlCmd, "@ContentType", SqlDbType.NVarChar, 50, ParameterDirection.Input, newReportFiles.ContentType);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportFiles_CREATE);
            ExecuteScalarCmd(sqlCmd);

            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }
        //public override bool UpdateReportFiles(ReportFiles newReportFiles)
        //{
        //    if (newReportFiles == null)
        //        throw (new ArgumentNullException("newReportFiles"));
        //    if (newReportFiles.Id <= DefaultValues.GetReportFilesIdMin())
        //        throw (new ArgumentOutOfRangeException("newReportFiles.Id"));

        //    SqlCommand sqlCmd = new SqlCommand();
        //    AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
        //    AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, newReportFiles.ReportId);
        //    AddParamToSQLCmd(sqlCmd, "@UploadDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportFiles.UploadDate);
        //    AddParamToSQLCmd(sqlCmd, "@Uploader", SqlDbType.SmallInt, 0, ParameterDirection.Input, newReportFiles.Uploader);
        //    AddParamToSQLCmd(sqlCmd, "@Title", SqlDbType.NVarChar, 50, ParameterDirection.Input, newReportFiles.Title);
        //    AddParamToSQLCmd(sqlCmd, "@Type", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportFiles.Type);
        //    AddParamToSQLCmd(sqlCmd, "@FileBinary", SqlDbType.VarBinary, 0, ParameterDirection.Input, newReportFiles.FileBinary);
        //    AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, newReportFiles.Id);
        //    SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportFiles_UPDATE);
        //    ExecuteScalarCmd(sqlCmd);

        //    int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
        //    return (returnValue == 0 ? true : false);
        //}
        public override bool DeleteReportFiles(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            SetCommandType(cmd, CommandType.StoredProcedure, SP_ReportFiles_DELETE);

            AddParamToSQLCmd(cmd, "@ID", SqlDbType.Int, 0, ParameterDirection.Input, ID);
            AddParamToSQLCmd(cmd, "@Return", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            ExecuteScalarCmd(cmd);
            int Returnvalue = ((int)cmd.Parameters["@Return"].Value);
            // if return value = 0 retrun tue
            return (Returnvalue == 0 ? true : false);
        }

        public override List<ReportFiles> GetReportFilesByReport(int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportFiles_GETReportFilesBYREPORT);
            List<ReportFiles> ReportFilesList = new List<ReportFiles>();
            TExecuteReaderCmd<ReportFiles>(sqlCmd, TGenerateReportFilesFromReader<ReportFiles>, ref ReportFilesList);
            return ReportFilesList;
        }

        public override ReportFiles GetReportFilesById(int Id)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, Id);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportFiles_GETBYID);
            List<ReportFiles> ReportFilesList = new List<ReportFiles>();
            TExecuteReaderCmd<ReportFiles>(sqlCmd, TGenerateReportFilesFromReader<ReportFiles>, ref ReportFilesList);
            if (ReportFilesList.Count > 0)
                return ReportFilesList[0];
            else
                return null;
        }

        public void TGenerateReportFilesFromReader<T>(SqlDataReader returndata, ref List<ReportFiles> ReportFilesList)
        {
            while (returndata.Read())
            {
                ReportFiles ReportFiles = new ReportFiles((int)returndata["Id"],
                    (int)returndata["ReportId"], (byte)returndata["Type"], (int)returndata["Uploader"],
                    (DateTime)returndata["UploadDate"], (string)returndata["Title"]);
                if (returndata["FileBinary"] != DBNull.Value)
                    ReportFiles.FileBinary = (byte[])returndata["FileBinary"];
                if (returndata["ContentType"] != DBNull.Value)
                    ReportFiles.ContentType = (string)returndata["ContentType"];
                ReportFilesList.Add(ReportFiles);
            }
        }

        //* ReportData ////////////////////
        private const string SP_ReportData_CREATE = "ReportData_Create";
        private const string SP_ReportData_UPDATE = "ReportData_Update";
        private const string SP_ReportData_GETALL = "ReportData_GetAll";
        private const string SP_ReportData_GETBYID = "ReportData_GetById";
        private const string SP_ReportData_GETBYCommittee = "ReportData_GetByCommittee";
        private const string SP_ReportData_GETBYCommitteeisNEW = "ReportData_GetByCommitteeIsNew";
        private const string SP_ReportData_GETBYMAKER = "ReportData_GetByMaker";
        private const string SP_ReportData_GETBYMakerISNEW = "ReportData_GetByMakerIsNew";
        private const string SP_ReportData_GETBYSTATUS = "ReportData_GetByStatus";
        private const string SP_ReportData_GETBYSTATUSIsNew = "ReportData_GetByStatusIsNew";
        private const string SP_ReportData_SETCOMMITTEVIEWED = "ReportData_SetCommitteeViewed";
        private const string SP_ReportData_CEOSIGN = "ReportData_CeoSign";
        private const string SP_ReportData_MANAGERSIGN = "ReportData_ManagerSign";
        private const string SP_ReportData_SETReportVIEWED = "ReportData_SetReportViewed";
        private const string SP_ReportData_SETCOMMITTE = "ReportData_SetCommittee";
        private const string SP_ReportData_DeleteCOMMITTE = "ReportData_DeleteCommittee";
        private const string SP_ReportData_GETBYSTATUSMAKER = "ReportData_GetByStatusAndMaker";

        public override int CreateNewReportData(ReportData newReportData)
        {
            if (newReportData == null)
                throw (new ArgumentNullException("newReportData"));
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@MakeDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportData.MakeDate);
            AddParamToSQLCmd(sqlCmd, "@Maker", SqlDbType.Int, 0, ParameterDirection.Input, newReportData.Maker);
            AddParamToSQLCmd(sqlCmd, "@Type", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportData.Type);
            AddParamToSQLCmd(sqlCmd, "@IssueNo", SqlDbType.NVarChar, 50, ParameterDirection.Input, newReportData.IssueNo);
            AddParamToSQLCmd(sqlCmd, "@IssueDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportData.IssueDate);
            AddParamToSQLCmd(sqlCmd, "@Priority", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportData.Priority);
            AddParamToSQLCmd(sqlCmd, "@Subject", SqlDbType.NVarChar, 100, ParameterDirection.Input, newReportData.Subject);
            AddParamToSQLCmd(sqlCmd, "@BasedOn", SqlDbType.NVarChar, 100, ParameterDirection.Input, newReportData.BasedOn);
            AddParamToSQLCmd(sqlCmd, "@Facts", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Facts);
            AddParamToSQLCmd(sqlCmd, "@Procedures", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Procedures);
            AddParamToSQLCmd(sqlCmd, "@Results", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Results);
            AddParamToSQLCmd(sqlCmd, "@Recommend", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Recommend);
            AddParamToSQLCmd(sqlCmd, "@OutsideNotes", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.OutsideNotes);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return (Convert.ToInt32(sqlCmd.Parameters["@ReturnValue"].Value));
        }
        public override bool UpdateReportData(ReportData newReportData)
        {
            if (newReportData == null)
                throw (new ArgumentNullException("newReportData"));
            if (newReportData.Id <= 0)
                throw (new ArgumentOutOfRangeException("newReportData.Id"));

            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@LastEditDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportData.LastEditDate);
            AddParamToSQLCmd(sqlCmd, "@LastEditor", SqlDbType.Int, 0, ParameterDirection.Input, newReportData.LastEditor);
            AddParamToSQLCmd(sqlCmd, "@Type", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportData.Type);
            AddParamToSQLCmd(sqlCmd, "@IssueNo", SqlDbType.NVarChar, 50, ParameterDirection.Input, newReportData.IssueNo);
            AddParamToSQLCmd(sqlCmd, "@IssueDate", SqlDbType.DateTime, 0, ParameterDirection.Input, newReportData.IssueDate);
            AddParamToSQLCmd(sqlCmd, "@Priority", SqlDbType.TinyInt, 0, ParameterDirection.Input, newReportData.Priority);
            AddParamToSQLCmd(sqlCmd, "@Subject", SqlDbType.NVarChar, 100, ParameterDirection.Input, newReportData.Subject);
            AddParamToSQLCmd(sqlCmd, "@BasedOn", SqlDbType.NVarChar, 100, ParameterDirection.Input, newReportData.BasedOn);
            AddParamToSQLCmd(sqlCmd, "@Facts", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Facts);
            AddParamToSQLCmd(sqlCmd, "@Procedures", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Procedures);
            AddParamToSQLCmd(sqlCmd, "@Results", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Results);
            AddParamToSQLCmd(sqlCmd, "@Recommend", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.Recommend);
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, newReportData.Id);
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.Int, 0, ParameterDirection.Input, newReportData.Status);
            AddParamToSQLCmd(sqlCmd, "@IsNew", SqlDbType.Bit, 0, ParameterDirection.Input, newReportData.IsNew);
            AddParamToSQLCmd(sqlCmd, "@OutsideNotes", SqlDbType.NVarChar, 250, ParameterDirection.Input, newReportData.OutsideNotes);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_UPDATE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override List<ReportData> GetAllReportDatas()
        {
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETALL);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportByCommittee(int empid)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@EmpId", SqlDbType.Int, 0, ParameterDirection.Input, empid);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYCommittee);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportByCommitteeIsNew(int empid)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@EmpId", SqlDbType.Int, 0, ParameterDirection.Input, empid);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYCommitteeisNEW);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportDataByMaker(int maker)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Maker", SqlDbType.Int, 0, ParameterDirection.Input, maker);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYMAKER);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;

        }
        public override ReportData GetReportDataById(int id)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, id);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYID);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            if (ReportDataList.Count > 0)
                return ReportDataList[0];
            else
                return null;
        }
        public override List<ReportData> GetReportByMakerIsNew(int maker)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Maker", SqlDbType.Int, 0, ParameterDirection.Input, maker);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYMakerISNEW);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportByStatus(byte status)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, status);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYSTATUS);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportByStatusMaker(byte status,int maker)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, status);
            AddParamToSQLCmd(sqlCmd, "@Maker", SqlDbType.Int, 0, ParameterDirection.Input, maker);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYSTATUSMAKER);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override List<ReportData> GetReportByStatusIsNew(byte status)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, status);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_GETBYSTATUSIsNew);
            List<ReportData> ReportDataList = new List<ReportData>();
            TExecuteReaderCmd<ReportData>(sqlCmd, TGenerateReportDataFromReader<ReportData>, ref ReportDataList);
            return ReportDataList;
        }
        public override bool SetReportCeoSign(int ceosign, string ceonotes, DateTime ceosigndate, bool isnew, int id, byte status)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CeoSign", SqlDbType.Int, 0, ParameterDirection.Input, ceosign);
            AddParamToSQLCmd(sqlCmd, "@CeoNotes", SqlDbType.NVarChar, 100, ParameterDirection.Input, ceonotes);
            AddParamToSQLCmd(sqlCmd, "@CeoSignDate", SqlDbType.DateTime, 0, ParameterDirection.Input, ceosigndate);

            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, id);
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, status);
            AddParamToSQLCmd(sqlCmd, "@IsNew", SqlDbType.Bit, 0, ParameterDirection.Input, isnew);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_CEOSIGN);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override bool SetReportManagerSign(int managersign, string managernotes, DateTime managersigndate, bool isnew, int id, byte status)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ManagerSign", SqlDbType.Int, 0, ParameterDirection.Input, managersign);
            AddParamToSQLCmd(sqlCmd, "@ManagerNotes", SqlDbType.NVarChar, 100, ParameterDirection.Input, managernotes);
            AddParamToSQLCmd(sqlCmd, "@ManagerSignDate", SqlDbType.DateTime, 0, ParameterDirection.Input, managersigndate);
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, id);
            AddParamToSQLCmd(sqlCmd, "@Status", SqlDbType.TinyInt, 0, ParameterDirection.Input, status);
            AddParamToSQLCmd(sqlCmd, "@IsNew", SqlDbType.Bit, 0, ParameterDirection.Input, isnew);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_MANAGERSIGN);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override bool SetReportCommitteeView(int empid, int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@EmpId", SqlDbType.Int, 0, ParameterDirection.Input, empid);
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_SETCOMMITTEVIEWED);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override bool SetReportCommittee(int empid, int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@EmpId", SqlDbType.Int, 0, ParameterDirection.Input, empid);
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_SETCOMMITTE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override bool DeleteReportCommittee(int empid, int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@EmpId", SqlDbType.Int, 0, ParameterDirection.Input, empid);
            AddParamToSQLCmd(sqlCmd, "@ReportId", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_DeleteCOMMITTE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public override bool SetReportView(int report)
        {
            SqlCommand sqlCmd = new SqlCommand();
            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Id", SqlDbType.Int, 0, ParameterDirection.Input, report);
            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_ReportData_SETReportVIEWED);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }
        public void TGenerateReportDataFromReader<T>(SqlDataReader returndata, ref List<ReportData> ReportDataList)
        {
            while (returndata.Read())
            {

                ReportData reportData = new ReportData((int)returndata["Id"], (int)returndata["Maker"], (DateTime)returndata["MakeDate"],
                    (byte)returndata["Type"], (string)returndata["IssueNo"], (DateTime)returndata["IssueDate"], (byte)returndata["Priority"],
                    (string)returndata["Subject"], (string)returndata["BasedOn"], (string)returndata["Facts"], (string)returndata["Procedures"]
                    , (string)returndata["Results"], (string)returndata["Recommend"], (byte)returndata["Status"], (bool)returndata["IsNew"]);
                if (returndata["LastEditDate"] != DBNull.Value)
                    reportData.LastEditDate = (DateTime)returndata["LastEditDate"];
                if (returndata["LastEditor"] != DBNull.Value)
                    reportData.LastEditor = (int)returndata["LastEditor"];
                if (returndata["ManagerNotes"] != DBNull.Value)
                    reportData.ManagerNotes = (string)returndata["ManagerNotes"];
                if (returndata["ManagerSignDate"] != DBNull.Value)
                    reportData.ManagerSignDate = (DateTime)returndata["ManagerSignDate"];
                if (returndata["ManagerSign"] != DBNull.Value)
                    reportData.ManagerSign = (int)returndata["ManagerSign"];
                if (returndata["CeoNotes"] != DBNull.Value)
                    reportData.CeoNotes = (string)returndata["CeoNotes"];
                if (returndata["CeoSignDate"] != DBNull.Value)
                    reportData.CeoSignDate = (DateTime)returndata["CeoSignDate"];
                if (returndata["CeoSign"] != DBNull.Value)
                    reportData.CeoSign = (int)returndata["CeoSign"];
                if (returndata["ViewDate"] != DBNull.Value)
                    reportData.ViewDate = (DateTime)returndata["ViewDate"];
                if (returndata["OutsideNotes"] != DBNull.Value)
                    reportData.OutsideNotes = (string)returndata["OutsideNotes"];
                ReportDataList.Add(reportData);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*****************************  SQL HELPER METHODS *****************************/
        private void AddParamToSQLCmd(SqlCommand sqlCmd,
                                      string paramId,
                                      SqlDbType sqlType,
                                      int paramSize,
                                      ParameterDirection paramDirection,
                                      object paramvalue)
        {

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));
            if (paramId == string.Empty)
                throw (new ArgumentOutOfRangeException("paramId"));

            SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName = paramId;
            if (sqlType != SqlDbType.Xml)
                newSqlParam.SqlDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            if (paramSize > 0)
                newSqlParam.Size = paramSize;

            if (paramvalue != null)
                newSqlParam.Value = paramvalue;

            sqlCmd.Parameters.Add(newSqlParam);
        }
        /// <summary>
        /// For Decimal 
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="paramId"></param>
        /// <param name="sqlType"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="paramDirection"></param>
        /// <param name="paramvalue"></param>
        private void AddParamToSQLCmd(SqlCommand sqlCmd,
                                     string paramId,
                                     SqlDbType sqlType,
                                     byte precision, byte scale,
                                     ParameterDirection paramDirection,
                                     object paramvalue)
        {

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));
            if (paramId == string.Empty)
                throw (new ArgumentOutOfRangeException("paramId"));

            SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName = paramId;
            newSqlParam.SqlDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            newSqlParam.Precision = precision;
            newSqlParam.Scale = scale;

            if (paramvalue != null)
                newSqlParam.Value = paramvalue;

            sqlCmd.Parameters.Add(newSqlParam);
        }
        private void ExecuteScalarCmd(SqlCommand sqlCmd)
        {
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCmd.Connection = cn;
                cn.Open();
                sqlCmd.ExecuteScalar();
                cn.Close();
            }
        }

        private void SetCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText)
        {
            sqlCmd.CommandType = cmdType;
            sqlCmd.CommandText = cmdText;
        }

        private void TExecuteReaderCmd<T>(SqlCommand sqlCmd, TGenerateListFromReader<T> gcfr, ref List<T> List)
        {
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd == null)
                throw (new ArgumentNullException("sqlCmd"));

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCmd.Connection = cn;
                cn.Open();
                gcfr(sqlCmd.ExecuteReader(), ref List);
                cn.Close();

            }
        }

    }
}
