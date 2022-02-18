using HealthAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAPP.BLL
{


    public class Employee
    {

        int _Id;
        string _Name;
        string _SignName;
        byte _RoleId;
        String _UserName;
        string _Password;
        byte _Status;
        string _Notes;

        DateTime _ViewDate;
        bool _IsNew;

        public string RoleName
        {
            get
            {
                if (_RoleId == 1)
                    return "موظف";
                else if (_RoleId == 2)
                    return "مدير";
                else if (_RoleId == 3)
                    return "رئيس تنفيذي";
                else
                    return "";
            }
        }
        public string StatusName
        {
            get
            {
                if (_Status == 1)
                    return "نشط";
                else if (_Status == 2)
                    return "غير نشط";
                else
                    return "";
            }
        }
        public string Notes { get => _Notes; set => _Notes = value; }
        public byte Status { get => _Status; set => _Status = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string UserName { get => _UserName; set => _UserName = value; }
        public byte RoleId { get => _RoleId; set => _RoleId = value; }
        public string SignName { get => _SignName; set => _SignName = value; }
        public string Name { get => _Name; set => _Name = value; }
        public int Id { get => _Id; set => _Id = value; }
        public bool IsNew { get => _IsNew; set => _IsNew = value; }
        public DateTime ViewDate { get => _ViewDate; set => _ViewDate = value; }

        public Employee()
        {
            _Notes = string.Empty;
        }
        public Employee(string name, string signname, byte roleid, string username,
            string password, byte status, string notes)
            : this(0, name, signname, roleid, username, password, status, notes)
        {

        }
        public Employee(int id, string name, string signname, byte roleid, string username,
            string password, byte status, string notes)
        {
            _Id = id;
            _Name = name;
            _SignName = signname;
            _RoleId = roleid;
            _UserName = username;
            _Password = password;
            _Status = status;
            _Notes = notes;
        }

        //* Methods ////////////////
        public bool Save()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (Id <= 0)
            {
                int TempId = DALLayer.CreateNewEmployee(this);
                if (TempId > 0)
                {
                    Id = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DALLayer.UpdateEmployee(this));
        }
        public static bool CreateNew(Employee emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            Employee chkemp = Employee.GetByUserName(emp.UserName);
            if (chkemp != null)
                return false;
            int TempId = DALLayer.CreateNewEmployee(emp);
            if (TempId > 0)
                return true;
            else
                return false;
        }
        public static bool Update(Employee emp)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            if (emp != null)
            {
                Employee chkemp = Employee.GetByUserName(emp.UserName);
                if (chkemp != null && chkemp.Id != emp.Id)
                    return false;
                return (DALLayer.UpdateEmployee(emp));
            }
            else return false;
        }
        public static List<Employee> GetAll()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetAllEmployees());

        }
        public static Employee GetById(int EmployeeId)
        {
            if (EmployeeId <= 0)
                return (null);

            DataAccess DALLAYER = DataAccessHelper.GetDataAccess();
            return (DALLAYER.GetEmployeeById(EmployeeId));
        }
        public static List<Employee> GetByRoleId(byte role)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetByRoleId(role));
        }
        public static List<Employee> GetByActive()
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetByIsActive(1));
        }
        public static Employee GetByUserName(string username)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetByUserName(username));
        }
        public static List<Employee> GetByReport(int report)
        {
            DataAccess DALLayer = DataAccessHelper.GetDataAccess();
            return (DALLayer.GetEmployeeByReport(report));
        }
    }
}