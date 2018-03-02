using DataAccess.BLL;
using Service.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Web.Model;

namespace Web.Api.Common
{
    public class CommonUtility
    {


        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }


        public static string GetXMLFromObject(object o)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, o);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }


        public static List<MonthModel> GetMonthList()
        {
            var list = new List<MonthModel>();

            for (int i = 1; i <= 12; i++)
            {
                list.Add(new MonthModel
                {
                    MonthNumber = i,
                    MonthName = GetMonthName(i)
                });
            }

            return list;

        }


        public List<MonthModel> GetYearList()
        {
            var list = new List<MonthModel>();

            for (int i = 2010; i <= 2020; i++)
            {
                list.Add(new MonthModel
                {
                    MonthNumber = i,
                    MonthName = i.ToString()
                });
            }

            return list;

        }


        public static List<MonthModelWithPeriod> GetMonthListPeriodFormat()
        {
            var list = new List<MonthModelWithPeriod>();

            for (int i = 1; i <= 12; i++)
            {
                list.Add(new MonthModelWithPeriod
                {
                    MonthNumber = i.ToString("00"),
                    MonthName = GetMonthName(i)
                });
            }

            return list;

        }


        public static IEnumerable<Tuple<string, int, string>> MonthsBetween(
         DateTime startDate,
         DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return Tuple.Create(
                    (iterator.Month.ToString("00")),
                    iterator.Year,
                    (CommonUtility.GetShortMonthName(iterator.Month).ToString()));
                iterator = iterator.AddMonths(1);



            }
        }


        public static int GetMonthNumber(string m)
        {



            int res = 0;
            switch (m.ToUpper().Trim())
            {
                case "JANUARY":
                    res = 1;
                    break;
                case "FEBRUARY":
                    res = 2;
                    break;
                case "MARCH":
                    res = 3;
                    break;
                case "APRIL":
                    res = 4;
                    break;
                case "MAY":
                    res = 5;
                    break;
                case "JUNE":
                    res = 6;
                    break;
                case "JULY":
                    res = 7;
                    break;
                case "AUGUST":
                    res = 8;
                    break;
                case "SEPTEMBER":
                    res = 9;
                    break;
                case "OCTOBER":
                    res = 10;
                    break;
                case "NOVEMBER":
                    res = 11;
                    break;
                case "DECEMBER":
                    res = 12;
                    break;

            }
            return res;
        }


        public static string GetMonthName(int m)
        {

            string res = "";
            switch (m)
            {
                case 1:
                    res = "January";
                    break;
                case 2:
                    res = "February";
                    break;
                case 3:
                    res = "March";
                    break;
                case 4:
                    res = "April";
                    break;
                case 5:
                    res = "May";
                    break;
                case 6:
                    res = "June";
                    break;
                case 7:
                    res = "July";
                    break;
                case 8:
                    res = "August";
                    break;
                case 9:
                    res = "September";
                    break;
                case 10:
                    res = "October";
                    break;
                case 11:
                    res = "November";
                    break;
                case 12:
                    res = "December";
                    break;

            }
            return res;
        }


        public static string GetShortMonthName(int m)
        {

            string res = "";
            switch (m)
            {
                case 1:
                    res = "Jan";
                    break;
                case 2:
                    res = "Feb";
                    break;
                case 3:
                    res = "Mar";
                    break;
                case 4:
                    res = "Apr";
                    break;
                case 5:
                    res = "May";
                    break;
                case 6:
                    res = "Jun";
                    break;
                case 7:
                    res = "Jul";
                    break;
                case 8:
                    res = "Aug";
                    break;
                case 9:
                    res = "Sep";
                    break;
                case 10:
                    res = "Oct";
                    break;
                case 11:
                    res = "Nov";
                    break;
                case 12:
                    res = "Dec";
                    break;

            }
            return res;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public static string GetDayName(int day)
        {

            string retVal = string.Empty;

            switch (day)
            {
                case 0:
                    retVal = DayOfWeek.Sunday.ToString();
                    break;
                case 1:
                    retVal = DayOfWeek.Monday.ToString();
                    break;
                case 2:
                    retVal = DayOfWeek.Tuesday.ToString();
                    break;
                case 3:
                    retVal = DayOfWeek.Wednesday.ToString();
                    break;
                case 4:
                    retVal = DayOfWeek.Thursday.ToString();
                    break;
                case 5:
                    retVal = DayOfWeek.Friday.ToString();
                    break;
                case 6:
                    retVal = DayOfWeek.Saturday.ToString();
                    break;
            }


            return retVal;
        }

       
        public static void LoginFailedLog(IUserLoginService userLoginService, string ip, string clientId, long? fkUser = null)
        {

            UserLogin tbluserLogin = new UserLogin
            {
                UserId = fkUser,
                IPAddress = ip,
                Successful = false,
                AppId = clientId,
                DateUtc = DateTime.UtcNow,
                IsLoggedIn = false
            };

            userLoginService.InsertUserLogin(tbluserLogin);
        }


        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}