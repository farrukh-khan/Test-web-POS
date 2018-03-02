using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Web.Mail;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
/// <summary>
/// Summary description for Utility
/// </summary>
/// 
namespace Web.Api.Common
{
    public class Utility
    {
        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static String Number2String(int number, bool isCaps)
        {

            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));

            return c.ToString();

        }
        public static double ToDouble(string data)
        {
            double num = 0;
            data = data.Replace("$", "");
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0; }
            return Math.Round(num, 4);
        }
        public static double ToDouble(object data)
        {
            double num = 0;
            double.TryParse(data.ToString(), out num);
            if (num == double.NaN) { num = 0.0; }
            return Convert.ToDouble(String.Format("{0:0.0000}", num));
        }
        public static int ToInt(string data)
        {
            int num = 0;
            int.TryParse(data, out num);
            return num;
        }
        public static double makeValue(string value)
        {
            double number = 0;

            try
            {
                number = Convert.ToDouble(value.Replace("$", "").Replace(",", "").Replace(" ", "").Replace(".", ""));
            }
            catch (Exception ex)
            {

            }

            return number;

        }
        public static bool IsNumeric(string value)
        {
            value = value.Replace("$", "").Replace(",", "").Replace(" ", "").Replace(".", "");

            //bool variable to hold the return value

            bool match;

            //regula expression to match numeric values

            string pattern = "(^[0-9]*$)";

            //generate new Regulsr Exoression eith the pattern and a couple RegExOptions

            Regex regEx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            //tereny expresson to see if we have a match or not

            match = regEx.Match(value).Success ? true : false;

            //return the match value (true or false)

            return match;

        }
        public static string ToPrice(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0; }
            num = Math.Round(num, 5);
            return "$ " + String.Format("{0:#,0.0000}", num);
        }
        public static string ToPrice2(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0; }
            //num = Math.Round(num, 5);
            return "$ " + String.Format("{0:#,0.00}", num);
        }
        public static string ToPrice3(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.000; }
            //num = Math.Round(num, 5);
            return "$ " + String.Format("{0:#,0.000}", num);
        }
        public static string ToPrice4(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0000; }
            num = Math.Round(num, 4);
            return "$ " + String.Format("{0:#,0.0000}", num);
        }
        public static string ToPrice5(object data)
        {
            double num = 0.0;
            double.TryParse(data.ToString(), out num);
            if (num == double.NaN) { num = 0.0000000; }
            if (num < 0.01) { return "$ " + String.Format("{0:#,0.0000000}", num); }
            else { return "$ " + String.Format("{0:#,0.0000}", num); }
        }
        public static string ToDouble1(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0; }
            num = Math.Round(num, 5);
            return String.Format("{0:#,0.00}", num);
        }
        public static string ToDouble2d(string data)
        {
            double num = 0.0;
            double.TryParse(data, out num);
            if (num == double.NaN) { num = 0.0; }
            return String.Format("{0:#,0.00}", num);
        }
        public static string ToComma(string data)
        {
            int num = 0;
            int.TryParse(data, out num);
            if (num == double.NaN) { num = 0; }


            return String.Format("{0:#,0}", num);
        }
        public static string ToCommaLong(string data)
        {
            long num = 0;
            long.TryParse(data, out num);
            if (num == double.NaN) { num = 0; }


            return String.Format("{0:#,0}", num);
        }
        public static string CStr(Object o)
        {
            string str = "";
            try
            {
                str = o.ToString();
            }
            catch (Exception ex)
            {
                str = "";
            }
            return str;
        }
    

      

        public static string SendMail(string Subject, string Body)
        {

            try
            {

                MailMessage mail = new MailMessage();


                mail.From = ConfigurationManager.AppSettings["From"].ToString();
                mail.To = ConfigurationManager.AppSettings["To"].ToString();
                mail.Cc = ConfigurationManager.AppSettings["CC"].ToString();
                mail.Bcc = ConfigurationManager.AppSettings["BCC"].ToString();
                mail.Subject = Subject;
                mail.BodyFormat = MailFormat.Html;
                mail.Body = Body;
                mail.Priority = MailPriority.High;
                SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTP"].ToString();
                SmtpMail.Send(mail);


                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
        public static string createCSV(ArrayList objList, ArrayList Keys, ArrayList Headres)
        {


            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int i = 0;
                //sb.Append("\"Date:\",");
                //sb.Append("\"" + DateTime.Now.ToShortDateString() + "\",");
                //sb.Append("\n");
                foreach (Hashtable objHash in objList)
                {


                    if (i == 0)
                    {
                        foreach (string str in Headres)
                        {


                            sb.Append("\"" + str + "\",");

                        }
                        sb.Append("\n");
                    }
                    foreach (string str in Keys)
                    {

                        if (objHash.ContainsKey(str))
                        {
                            if (str != "RRComm")
                            {
                                sb.Append("\"" + objHash[str].ToString() + "\",");
                            }
                            else
                            {

                                sb.Append("\"" + RemoveTable(objHash[str].ToString()) + "\",");
                            }
                            i++;
                        }
                    }

                    sb.Append("\n");

                }

                // HttpContext.Current.Session["CSV"] = sb;
                string s = HttpContext.Current.Session["CSVFile"].ToString();
                if (HttpContext.Current.Session["CSVFile"] != null)
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//" + HttpContext.Current.Session["CSVFile"].ToString()));
                    fp.Write(sb);
                    fp.Close();

                }
                else
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//test.csv"));
                    fp.Write(sb);

                    fp.Close();
                }


                return "Success";

            }
            catch (Exception ex)
            {

                return ex.Message;

            }

        }
        public static string RemoveTable(string tbl)
        {

            return tbl.Replace("<table><tr><td>", "").Replace("</table>", "").Replace("<td>", "").Replace("</td></tr>", ",").Replace("</td><td align='right'>", " : ").Replace("<tr>", "").Replace("<table>", "");

        }
        public static string createCSV(ArrayList objList, ArrayList Keys)
        {


            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int i = 0;
                //sb.Append("\"Date:\",");
                //sb.Append("\"" + DateTime.Now.ToShortDateString() + "\",");
                //sb.Append("\n");

                foreach (Hashtable objHash in objList)
                {


                    if (i == 0)
                    {
                        foreach (string str in Keys)
                        {


                            sb.Append("\"" + str + "\",");

                        }
                        sb.Append("\n");
                    }
                    foreach (string str in Keys)
                    {

                        if (objHash.ContainsKey(str))
                        {
                            sb.Append("\"" + objHash[str].ToString().Replace("$", "") + "\",");
                            i++;
                        }
                    }
                    sb.Append("\n");

                }

                // HttpContext.Current.Session["CSV"] = sb;
                string s = HttpContext.Current.Session["CSVFile"].ToString();
                if (HttpContext.Current.Session["CSVFile"] != null)
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//" + HttpContext.Current.Session["CSVFile"].ToString()));
                    fp.Write(sb);
                    fp.Close();

                }
                else
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//test.csv"));
                    fp.Write(sb);

                    fp.Close();
                }


                return "Success";

            }
            catch (Exception ex)
            {

                return ex.Message;

            }

        }
        public static string createPipe(ArrayList objList, ArrayList Keys)
        {


            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                int i = 0;
                //sb.Append("\"Date:\",");
                //sb.Append("\"" + DateTime.Now.ToShortDateString() + "\",");
                //sb.Append("\n");

                foreach (Hashtable objHash in objList)
                {


                    if (i == 0)
                    {
                        foreach (string str in Keys)
                        {


                            sb.Append(str + "|");

                        }
                        sb.Append(Environment.NewLine);
                    }
                    foreach (string str in Keys)
                    {

                        if (objHash.ContainsKey(str))
                        {
                            sb.Append(objHash[str].ToString() + "|");
                            i++;
                        }
                    }
                    sb.Append(Environment.NewLine);

                }

                // HttpContext.Current.Session["CSV"] = sb;
                string s = HttpContext.Current.Session["PipeFile"].ToString();
                if (HttpContext.Current.Session["PipeFile"] != null)
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("Pipe//" + HttpContext.Current.Session["PipeFile"].ToString()));
                    fp.Write(sb);
                    fp.Close();

                }
                else
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//test.csv"));
                    fp.Write(sb);

                    fp.Close();
                }


                return "Success";

            }
            catch (Exception ex)
            {

                return ex.Message;

            }

        }
        public static string createPipe(ArrayList objList, ArrayList Keys, ArrayList Headres, string client)
        {


            try
            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (client.ToUpper() == "KCCI_LORDABBETT")
                {
                    StreamReader re = File.OpenText(HttpContext.Current.Server.MapPath("Pipe//KCCI_LordAbbett_template.txt"));
                    string input = null;
                    while ((input = re.ReadLine()) != null)
                    {
                        sb.Append(input);
                        sb.Append(Environment.NewLine);
                    }
                    re.Close();


                }
                int i = 0;
                foreach (Hashtable objHash in objList)
                {


                    if (i == 0 && client.ToUpper() != "KCCI_LORDABBETT")
                    {
                        foreach (string str in Headres)
                        {

                            sb.Append(str + "|");

                        }
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {

                    }

                    foreach (string str in Keys)
                    {

                        if (objHash.ContainsKey(str))
                        {

                            sb.Append(objHash[str].ToString().Replace("$ ", "") + "|");

                            i++;
                        }
                    }
                    sb.Append(Environment.NewLine);




                }

                // HttpContext.Current.Session["CSV"] = sb;
                string s = HttpContext.Current.Session["PipeFile"].ToString();
                if (HttpContext.Current.Session["PipeFile"] != null)
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("Pipe//" + HttpContext.Current.Session["PipeFile"].ToString()));
                    fp.Write(sb);
                    fp.Close();

                }
                else
                {
                    StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//test.csv"));
                    fp.Write(sb);

                    fp.Close();
                }


                return "Success";

            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
        public static string createCSV(ArrayList objList, ArrayList Keys, ArrayList Headres, Hashtable Report)
        {


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int i = 0;
            string[] strReport = Report["Rank"].ToString().Split(',');
            foreach (string key in strReport)
            {

                sb.Append("\"" + key + "\",");
                sb.Append("\"" + Report[key].ToString() + "\",");
                sb.Append("\n");
            }

            sb.Append("\n");
            sb.Append("\n");

            foreach (Hashtable objHash in objList)
            {


                if (i == 0)
                {
                    foreach (string str in Headres)
                    {


                        sb.Append("\"" + str + "\",");

                    }
                    sb.Append("\n");
                }
                foreach (string str in Keys)
                {

                    if (objHash.ContainsKey(str))
                    {
                        sb.Append("\"" + objHash[str].ToString() + "\",");
                        i++;
                    }
                }
                sb.Append("\n");

            }

            // HttpContext.Current.Session["CSV"] = sb;
            string s = HttpContext.Current.Session["CSVFile"].ToString();
            if (HttpContext.Current.Session["CSVFile"] != null)
            {
                StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//" + HttpContext.Current.Session["CSVFile"].ToString()));
                fp.Write(sb);
                fp.Close();

            }
            else
            {
                StreamWriter fp = new StreamWriter(HttpContext.Current.Server.MapPath("CSV//test.csv"));
                fp.Write(sb);
                fp.Close();
            }


            return "Success";



        }
        public static string getUser()
        {
            string user = "";

            if (HttpContext.Current.Session["User"] != null)
            {

                user = HttpContext.Current.Session["User"].ToString();
            }
            return user;

        }
      
        public static Hashtable DataViewToArray(DataView lst, string DataTextField, string DataValueField)
        {

            Hashtable objHash = new Hashtable();
            string strDataTextField = "";
            string strDataValueField = "";
            for (int i = 0; i < lst.Count; i++)
            {

                strDataTextField += lst[i][DataTextField] + ",";
                strDataValueField += lst[i][DataValueField] + ",";

            }
            objHash.Add("DataTextField", strDataTextField);
            objHash.Add("DataValueField", strDataValueField);
            return objHash;

        }
        public static string appendExchange(string fills_exchange)
        {


            try
            {

                switch (fills_exchange.ToUpper())
                {

                    case "N":
                    case "NYSE":
                        return fills_exchange + "--NYSE";
                    case "AMEX":
                    case "A":
                        return fills_exchange + "--ASE";
                    case "MW":
                        return fills_exchange + "--CSE";
                    case "P":
                        return fills_exchange + "--ARCA";
                    case "CBOE":
                    case "W":
                        return fills_exchange + "--CBOE";
                    case "PHLX":
                    case "X":
                        return fills_exchange + "--Phlx";
                    case "Y":
                    case "I":
                        return fills_exchange + "--ISE";
                    case "O":
                        return fills_exchange + "--OTC";
                    case "":
                        return fills_exchange + "--Ex Clearing";
                    case "NITE":
                    case "VNDM":
                    case "DOM":
                        return fills_exchange + "--Prime Brokerage";
                    case "Q":
                        return fills_exchange + "--NASDAQ";
                    case "TO":
                        return fills_exchange + "--Toronto";
                    case "B":
                    case "BOX":
                        return fills_exchange + "--Boston";
                    case "6":
                    case "M":
                        return fills_exchange + "--Montreal";
                    default:
                        return fills_exchange + "--Other Exch";








                }


                return fills_exchange;

            }
            catch (Exception ex)
            {

                return fills_exchange;

            }


        }
     



        public static DateTime GetFirstDayOfMonth(int iMonth, int iYear)
        {

            // set return value to the last day of the month

            // for any date passed in to the method



            // create a datetime variable set to the passed in date

            DateTime dtFrom = new DateTime(iYear, iMonth, 1);



            // remove all of the days in the month

            // except the first day and set the

            // variable to hold that date

            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));



            // return the first day of the month

            return dtFrom;

        }

        public static DateTime GetLastDayOfMonth(int iMonth, int iYear)
        {



            // set return value to the last day of the month

            // for any date passed in to the method



            // create a datetime variable set to the passed in date

            DateTime dtTo = new DateTime(iYear, iMonth, 1);



            // overshoot the date by a month

            dtTo = dtTo.AddMonths(1);



            // remove all of the days in the next month

            // to get bumped down to the last day of the

            // previous month

            dtTo = dtTo.AddDays(-(dtTo.Day));



            // return the last day of the month

            return dtTo;



        }






   

    }
}

















