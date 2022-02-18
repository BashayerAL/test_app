using HealthAPP.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summary description for UberAll
/// </summary>
namespace HealthAPP
{
    public static class UberAll
    {
      
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            System.ComponentModel.PropertyDescriptorCollection props =
                System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static void ReportExport(Stream stream2, string exportType, bool inline)
        {
            //using (ReportDocument rpt = new ReportDocument())
            //{
            //    Stream stream2 = new MemoryStream();
            //    rpt.Load(path);
            //    rpt.SetDataSource(data);
            //    for (int i = 0; i < paramList.Count; i++)
            //    {
            //        rpt.SetParameterValue(paramList[i].Name, paramList[i].Value);
            //    }
            //    for (int i = 0; i < subreports; i++)
            //    {
            //        rpt.Subreports[i].SetDataSource(datasubreports[i]);
            //    }
            //    //  Session["report"] = rpt;

            //    if (exportType == "xls")
            //        stream2 = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
            //    else if (exportType == "pdf")
            //        stream2 = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            ////else if (fileType == "rtf")
            ////    report.ExportToRtf(stream);
            ////else if (fileType == "csv")
            ////    report.ExportToCsv(stream);


            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/" + exportType;
            System.Web.HttpContext.Current.Response.AddHeader("Accept-Header", stream2.Length.ToString());
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", (inline ? "Inline" : "Attachment") + "; filename=" + "exportReport_" + DateTime.Now + "." + exportType);
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", stream2.Length.ToString());
            //Response.ContentEncoding = System.Text.Encoding.Default;
            // stream = stream2 as MemoryStream;
            System.Web.HttpContext.Current.Response.BinaryWrite(ReadFully(stream2));
            System.Web.HttpContext.Current.Response.End();
            //stream2.Dispose();
            //rpt.Close();
            //rpt.Clone();
            //rpt.Dispose();
            //rpt.Dispose();

            //  }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }

    public class paramrpt
    {
        string _name;
        object _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
        public paramrpt(string name, object value)
        {
            _name = name;
            _value = value;
        }
    }
}