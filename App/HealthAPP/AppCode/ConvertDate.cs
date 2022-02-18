//***************************************************************************************
//* Class: Greg2Hijri                                                                   *
//* Manage all Hijri and Gregory Date/DateTime conversion and manipulatiom.             *                                                                               *
//* Mohamed Tarek | www.twitter.com/moqane                                              *
//***************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;


public static class ConvertDate
{

    public static CultureInfo arCul = new CultureInfo("ar-SA");
    public static CultureInfo enCul = new CultureInfo("en-US");

    public static bool Is_Hijri(string VB_DateTime)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = arCul;

        try
        {
            DateTime tempDate = DateTime.Parse(VB_DateTime);
        }
        catch
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
            return false;
        }

        System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
        return true;

    }

    public static bool Is_Gregory(string VB_DateTime)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = enCul;

        try
        {
            DateTime tempDate = DateTime.Parse(VB_DateTime);
        }
        catch
        {
            return false;
        }

        return true;

    }

    public static DateTime Hijri2Greg(string hijri_datetime)
    {

        string mode = null;
        DateTime tempDate = (DateTime)SqlDateTime.MinValue;
        if (hijri_datetime.Contains("/") & !hijri_datetime.Contains(":"))
        {
            mode = "Date";
        }
        else if (hijri_datetime.Contains("/") & hijri_datetime.Contains(":"))
        {
            mode = "DateTime";
        }

        switch (mode)
        {

            case "Date":
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = arCul;
                    tempDate = DateTime.Parse(hijri_datetime);
                    System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
                    return tempDate;
                    //.ToString("MM/dd/yyyy", enCul.DateTimeFormat);
                }
                catch
                {
                }

                break;
            case "DateTime":
                string[] var = hijri_datetime.Split(' ');
                string mydate = var[0];
                string Mytime_without_APM = var[1];
                string APM = var[2];

                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = arCul;
                    tempDate = DateTime.Parse(mydate);
                    System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
                    return tempDate;
                    //.ToString("MM/dd/yyyy", enCul.DateTimeFormat) + " " + Mytime_without_APM + " " + APM;
                }
                catch
                {
                }

                break;
        }

        return tempDate;

    }

    public static string Greg2Hijri(string greg_datetime)
    {

        string mode = null;

        if (greg_datetime.Contains("/") & !greg_datetime.Contains(":"))
        {
            mode = "Date";
        }
        else if (greg_datetime.Contains("/") & greg_datetime.Contains(":"))
        {
            mode = "DateTime";
        }

        switch (mode)
        {

            case "Date":
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
                    DateTime tempDate = DateTime.Parse(greg_datetime);
                    // DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    return tempDate.ToString("dd/MM/yyyy", arCul.DateTimeFormat);
                }
                catch
                {
                }

                break;
            case "DateTime":
                string[] var = greg_datetime.Split(' ');
                string mydate = var[0];
                string Mytime_without_APM = var[1];
                string APM = var[2];

                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = enCul;
                    DateTime tempDate = DateTime.Parse(mydate);
                    // DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    return Convert_2Arabic_APM_In_Hijri_Date(tempDate.ToString("dd/MM/yyyy", arCul.DateTimeFormat) + " " + Mytime_without_APM + " " + APM);
                }
                catch
                {
                }

                break;
        }

        return null;
    }




    public static string Convert_2Arabic_APM_In_Hijri_Date(string Hijri_DateTime)
    {
        string[] dt = Hijri_DateTime.Split(' ');
        string APM = null;
        switch (dt[2])
        {
            case "AM":
                APM = "ص";
                break;
            case "PM":
                APM = "م";
                break;
        }
        return APM + " " + dt[1] + " " + dt[0];
    }

}