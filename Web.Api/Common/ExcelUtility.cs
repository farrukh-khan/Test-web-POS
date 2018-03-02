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
using System.Collections.Generic;
using System.Collections;
using SmartXLS;
using System.Reflection;
/// <summary>
/// Summary description for ExcelUtility
/// </summary>
/// 
namespace Web.Api.Common
{

    public class ExcelUtility
    {

        int StartRow, StartCol, EndRow, EndCol;

        string StartRange, eRange, hdrRange, colRange, ftrRange, bodyRange;
        public static WorkBook m_WorkBook;
        public static RangeStyle m_RangeStyle;

        public static string createXSL(DataSet ds, string title)
        {
            m_WorkBook = new WorkBook();
            m_WorkBook.Sheet = 0;

            try
            {
                string filepath = HttpContext.Current.Server.MapPath("~/Report/") + title + "_report_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

                //HEADER
                int i = 0;
                m_WorkBook.setText(0, 0, title);
                m_WorkBook.setSelection("A1:U1");
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_RangeStyle.MergeCells = true;
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 300;
                m_WorkBook.setRangeStyle(m_RangeStyle);
                m_WorkBook.setText(1, 0, Convert.ToDateTime(DateTime.Now).ToString("dddd MMMM d,yyyy"));
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_WorkBook.setSelection("A2:U2");
                m_RangeStyle.MergeCells = true;
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 200;
                m_WorkBook.setRangeStyle(m_RangeStyle);



                int row = 3;

                {



                    for (int j = 1; i < ds.Tables[0].Columns.Count; i++)
                    {

                        m_WorkBook.setText(2, i, ds.Tables[0].Columns[i].ColumnName);
                        m_WorkBook.setColWidth(i, 100 * 50);
                        //i++;
                    }

                    m_WorkBook.setSelection("A3:" + Common.Utility.Number2String(i, true) + "3");
                    m_RangeStyle = m_WorkBook.getRangeStyle();
                    m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
                    m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
                    m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                    m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
                    m_RangeStyle.FontBold = true;
                    m_RangeStyle.FontSize = 200;
                    m_RangeStyle.Pattern = RangeStyle.PatternSolid;
                    m_RangeStyle.PatternFG = 0xeeeeee;
                    m_RangeStyle.Locked = true;
                    m_WorkBook.setRangeStyle(m_RangeStyle);
                    m_RangeStyle = m_WorkBook.getRangeStyle();


                    i = 0;


                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                        {
                            //excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                            m_WorkBook.setText(row, k, ds.Tables[0].Rows[j].ItemArray[k].ToString());
                        }
                        row++;
                    }



                }


                row = row + 3;
                m_WorkBook.setText(row, 0, "Summary");
                m_RangeStyle = m_WorkBook.getRangeStyle();
                //m_WorkBook.setSelection("j" + (row + 1) + ":N" + (row + 1));
                m_WorkBook.setSelection("A" + (row + 1) + ":" + Common.Utility.Number2String(ds.Tables[1].Columns.Count, true) + (row + 1));
                m_RangeStyle.MergeCells = true;
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                HeaderFormat("A" + (row + 1) + ":" + Common.Utility.Number2String(ds.Tables[1].Columns.Count, true) + (row + 1));


                row = row + 1;
                int summeryCunter = 0;
                for (int j = 1; i < ds.Tables[1].Columns.Count; i++)
                {
                    m_WorkBook.setText(row, summeryCunter, ds.Tables[1].Columns[i].ColumnName);
                    summeryCunter++;
                }




                FooterFormat("A" + (row + 1) + ":" + Common.Utility.Number2String(ds.Tables[1].Columns.Count, true) + (row + 1));
                row = row + 1;

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {
                    for (int k = 0; k < ds.Tables[1].Columns.Count; k++)
                    {

                        m_WorkBook.setText(row, k, ds.Tables[1].Rows[j].ItemArray[k].ToString());
                    }
                    row++;
                }



                m_WorkBook.setSelection("A4");
                m_WorkBook.write(filepath);


                return filepath;
            }
            catch (Exception ex)
            {

                throw ex; ;

            }

        }

        public static string createXSL(DataTable ds, string title)
        {
            m_WorkBook = new WorkBook();
            m_WorkBook.Sheet = 0;

            try
            {
                string filepath = HttpContext.Current.Server.MapPath("~/Report/") + title + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

                //HEADER
                int i = 0;
                m_WorkBook.setText(0, 0, title);
                m_WorkBook.setSelection("A1:U1");
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_RangeStyle.MergeCells = true;
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 300;
                m_WorkBook.setRangeStyle(m_RangeStyle);
                m_WorkBook.setText(1, 0, Convert.ToDateTime(DateTime.Now).ToString("dddd MMMM d,yyyy"));
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_WorkBook.setSelection("A2:U2");
                m_RangeStyle.MergeCells = true;
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 200;
                m_WorkBook.setRangeStyle(m_RangeStyle);



                int row = 3;

                {



                    for (int j = 1; i < ds.Columns.Count; i++)
                    {

                        m_WorkBook.setText(2, i, ds.Columns[i].ColumnName);
                        m_WorkBook.setColWidth(i, 100 * 50);
                        //i++;
                    }

                    m_WorkBook.setSelection("A3:" + Common.Utility.Number2String(i, true) + "3");
                    m_RangeStyle = m_WorkBook.getRangeStyle();
                    m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
                    m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
                    m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
                    m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
                    m_RangeStyle.FontBold = true;
                    m_RangeStyle.FontSize = 200;
                    m_RangeStyle.Pattern = RangeStyle.PatternSolid;
                    m_RangeStyle.PatternFG = 0xeeeeee;
                    m_RangeStyle.Locked = true;
                    m_WorkBook.setRangeStyle(m_RangeStyle);
                    m_RangeStyle = m_WorkBook.getRangeStyle();


                    i = 0;


                    for (int j = 0; j < ds.Rows.Count; j++)
                    {
                        for (int k = 0; k < ds.Columns.Count; k++)
                        {
                            //excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                            m_WorkBook.setText(row, k, ds.Rows[j].ItemArray[k].ToString());
                        }
                        row++;
                    }



                }


              


                m_WorkBook.setSelection("A4");
                m_WorkBook.write(filepath);


                return filepath;
            }
            catch (Exception ex)
            {

                throw ex; ;

            }

        }




        //int StartRow, StartCol, EndRow, EndCol;

        //string StartRange, eRange, hdrRange, colRange, ftrRange, bodyRange;
        //public static WorkBook m_WorkBook;
        //public static RangeStyle m_RangeStyle;
        //public static string createXSL(ArrayList objList, ArrayList Keys, ArrayList Headres, string filepath)
        //{
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;


        //    try
        //    {
        //        //HEADER
        //        int i = 0;
        //        foreach (string str in Headres)
        //        {

        //            m_WorkBook.setText(0, i, str.Split('|')[0]);
        //            m_WorkBook.setColWidth(i, 100 * Convert.ToInt32(str.Split('|')[1]));
        //            i++;
        //        }
        //        // Convert. (i+65)
        //        m_WorkBook.setSelection("A1:" + Utility.Number2String(i, true) + "1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //        m_RangeStyle.PatternFG = 0xeeeeee;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);

        //        //CONTENTS
        //        i = 0;
        //        int row = 1;
        //        string data = "";
        //        foreach (Hashtable objHash in objList)
        //        {

        //            i = 0;
        //            foreach (string str in Keys)
        //            {

        //                if (objHash.ContainsKey(str.Split('|')[0]))
        //                {
        //                    data = objHash[str.Split('|')[0]].ToString();
        //                    //    if (Utility.IsNumeric(data))
        //                    //    {
        //                    //        m_WorkBook.setNumber(row, i, Utility.makeValue(data));

        //                    //    }
        //                    //    else
        //                    //    {
        //                    m_WorkBook.setText(row, i, data);
        //                    // }


        //                    //double.TryParse(data, out num);
        //                    //if (num != double.NaN) 
        //                    //{
        //                    //    m_WorkBook.setSelection(Utility.Number2String(i, true)); 


        //                    //}

        //                }
        //                else
        //                {
        //                    m_WorkBook.setText(row, i, "");
        //                }
        //                i++;
        //            }
        //            row++;


        //        }
        //        //Hashtable objHash = new Hashtable();
        //        // objHash = (Hashtable)objList[0];

        //        // foreach (string strKey in objHash)
        //        // { 

        //        // if(Utility.IsNumeric(objHash[strKey])
        //        // {


        //        // }


        //        // }



        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;

        //    }

        //}
        //public static string createXSL(List<Objects.Fills> objList, ArrayList Headres, string filepath)
        //{
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;


        //    try
        //    {
        //        //HEADER
        //        int i = 0;
        //        foreach (string str in Headres)
        //        {

        //            m_WorkBook.setText(0, i, str.Split('|')[0]);
        //            m_WorkBook.setColWidth(i, 100 * Convert.ToInt32(str.Split('|')[1]));
        //            i++;
        //        }
        //        // Convert. (i+65)
        //        m_WorkBook.setSelection("A1:" + Utility.Number2String(i, true) + "1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //        m_RangeStyle.PatternFG = 0xeeeeee;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);

        //        //CONTENTS
        //        i = 0;
        //        int row = 1;
        //        string data = "";
        //        foreach (Objects.Fills objFills in objList)
        //        {

        //            if (objFills.count != 0)
        //            {
        //                m_WorkBook.setNumber(row, 0, objFills.count);
        //                m_WorkBook.setText(row, 1, objFills.FillsDate);
        //                m_WorkBook.setNumber(row, 2, objFills.fills_quantity);
        //                m_WorkBook.setText(row, 3, objFills.order_symbol);
        //                m_WorkBook.setNumber(row, 4, objFills.fills_price);
        //                m_WorkBook.setText(row, 5, objFills.fills_contra);
        //                m_WorkBook.setText(row, 6, objFills.fills_exchange);
        //                m_WorkBook.setText(row, 7, objFills.fills_liquidity_indicator);
        //                m_WorkBook.setText(row, 8, objFills.order_executed_exchange);
        //                m_WorkBook.setText(row, 9, objFills.fills_exec_broker);
        //                m_WorkBook.setNumber(row, 10, objFills.fills_order_type);
        //                m_WorkBook.setText(row, 11, objFills.fills_exec_inst);
        //                m_WorkBook.setText(row, 12, objFills.fills_customer_firm);
        //                //  m_WorkBook.setText(row, 13, objFills.tier);
        //                m_WorkBook.setNumber(row, 13, objFills.rates);
        //                //m_WorkBook.setNumber(row, 15, objFills.dbcost);
        //                row++;
        //            }
        //        }


        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;

        //    }

        //}
        //public static string createXSL(List<Objects.SSReport> objList, ArrayList Headres, string filepath)
        //{
        //    string date = Convert.ToDateTime(filepath.Split('_')[2].Substring(0, 4) + "/" + filepath.Split('_')[2].Substring(4, 2) + "/" + filepath.Split('_')[2].Substring(6, 2)).ToString("MMMM dd, yyyy").ToUpper();
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;
        //    m_WorkBook.setText(0, 4, "SHORT SELL REPORT FOR " + date);
        //    m_WorkBook.setSelection("E1:E1");
        //    m_RangeStyle = m_WorkBook.getRangeStyle();
        //    m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //    m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //    m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //    m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //    m_RangeStyle.FontBold = true;
        //    m_RangeStyle.FontSize = 400;
        //    m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //    m_WorkBook.setRangeStyle(m_RangeStyle);
        //    try
        //    {
        //        //HEADER
        //        int i = 0;
        //        foreach (string str in Headres)
        //        {

        //            m_WorkBook.setText(1, i, str.Split('|')[0]);
        //            m_WorkBook.setColWidth(i, 100 * Convert.ToInt32(str.Split('|')[1]));
        //            i++;
        //        }
        //        // Convert. (i+65)
        //        m_WorkBook.setSelection("A2:" + Utility.Number2String(i, true) + "2");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //        m_RangeStyle.PatternFG = 0xeeeeee;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);

        //        //CONTENTS
        //        i = 0;
        //        int row = 2;
        //        string data = "";
        //        foreach (Objects.SSReport objSSReport in objList)
        //        {

        //            if (objSSReport.Price != null)
        //            {
        //                m_WorkBook.setText(row, 0, objSSReport.BoothId);
        //                m_WorkBook.setText(row, 1, objSSReport.Date);
        //                m_WorkBook.setText(row, 2, objSSReport.ReceiveTime);
        //                m_WorkBook.setText(row, 3, objSSReport.OrderID);
        //                m_WorkBook.setText(row, 4, objSSReport.SubOrderID);
        //                m_WorkBook.setText(row, 5, objSSReport.Client);
        //                m_WorkBook.setText(row, 6, objSSReport.Symbol);
        //                m_WorkBook.setText(row, 7, Utility.ToComma(objSSReport.Shares.ToString()));
        //                m_WorkBook.setNumber(row, 8, Utility.ToDouble(objSSReport.Price));
        //                m_WorkBook.setText(row, 9, objSSReport.UserId);
        //                m_WorkBook.setText(row, 10, objSSReport.LocateID);
        //                // m_WorkBook.setText(row, 11, objSSReport.ContactName);
        //                m_WorkBook.setText(row, 11, objSSReport.order_message);
        //                m_WorkBook.setText(row, 12, Utility.ToComma(objSSReport.ExecutedQuantity.ToString()));
        //                m_WorkBook.setText(row, 13, objSSReport.ExecutionPrice);
        //                m_WorkBook.setText(row, 14, objSSReport.ExecutionTime);
        //                m_WorkBook.setText(row, 15, objSSReport.ETB);
        //                row++;
        //            }
        //        }


        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;

        //    }

        //}
        //public static string createXSL(List<Objects.ExecReport> objList, ArrayList Headres, string filepath)
        //{
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;


        //    try
        //    {
        //        //HEADER
        //        int i = 0;
        //        foreach (string str in Headres)
        //        {

        //            m_WorkBook.setText(0, i, str.Split('|')[0]);
        //            m_WorkBook.setColWidth(i, 100 * Convert.ToInt32(str.Split('|')[1]));
        //            i++;
        //        }
        //        // Convert. (i+65)
        //        m_WorkBook.setSelection("A1:" + Utility.Number2String(i, true) + "1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //        m_RangeStyle.PatternFG = 0xeeeeee;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);

        //        //CONTENTS
        //        i = 0;
        //        int row = 1;
        //        string data = "";
        //        foreach (Objects.ExecReport objExecReport in objList)
        //        {

        //            if (objExecReport.Price != null)
        //            {
        //                m_WorkBook.setText(row, 0, objExecReport.BoothId);
        //                m_WorkBook.setText(row, 1, objExecReport.OrderID);
        //                m_WorkBook.setText(row, 2, objExecReport.Destination);
        //                m_WorkBook.setText(row, 3, objExecReport.Client);
        //                m_WorkBook.setText(row, 4, objExecReport.TransType);
        //                m_WorkBook.setText(row, 5, objExecReport.TransQty);
        //                m_WorkBook.setText(row, 6, objExecReport.Symbol);
        //                m_WorkBook.setNumber(row, 7, Utility.ToDouble(objExecReport.Price));
        //                m_WorkBook.setText(row, 8, objExecReport.TradeDate);
        //                m_WorkBook.setText(row, 9, objExecReport.TradeTime);
        //                m_WorkBook.setText(row, 10, objExecReport.EG_U);
        //                m_WorkBook.setText(row, 11, objExecReport.EB_N);
        //                m_WorkBook.setText(row, 12, objExecReport.G_U);
        //                m_WorkBook.setText(row, 13, objExecReport.B_N);
        //                m_WorkBook.setText(row, 14, objExecReport.LiquidityIndicator);
        //                m_WorkBook.setText(row, 15, objExecReport.AwayMarketCode);
        //                m_WorkBook.setText(row, 16, objExecReport.SecurityType);
        //                m_WorkBook.setText(row, 17, objExecReport.ExpiryYearMonth);
        //                m_WorkBook.setText(row, 18, objExecReport.ExpiryDay);
        //                m_WorkBook.setText(row, 19, objExecReport.Put_Call);
        //                m_WorkBook.setText(row, 20, objExecReport.Strike);
        //                m_WorkBook.setText(row, 21, objExecReport.Covered_Uncovered);
        //                m_WorkBook.setText(row, 22, objExecReport.Customer_Firm);
        //                row++;
        //            }
        //        }


        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;

        //    }

        //}
        //public static string createXSL(List<Objects.RangerCap> objList, string filepath)
        //{
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;


        //    try
        //    {
        //        //HEADER
        //        int i = 0;

        //        m_WorkBook.setText(0, 0, "portfolio");
        //        m_WorkBook.setText(0, 1, "Trans");
        //        m_WorkBook.setText(0, 2, "");
        //        m_WorkBook.setText(0, 3, "type");
        //        m_WorkBook.setText(0, 4, "Symbol");
        //        m_WorkBook.setText(0, 5, "TD");
        //        m_WorkBook.setText(0, 6, "SD");
        //        m_WorkBook.setText(0, 7, "");
        //        m_WorkBook.setText(0, 8, "Quantity");
        //        m_WorkBook.setText(0, 17, "Price");
        //        m_WorkBook.setText(0, 22, "SEC FEE");
        //        m_WorkBook.setText(0, 23, "Commis");
        //        m_WorkBook.setText(0, 24, "Broker");
        //        m_WorkBook.setText(0, 29, "Location");
        //        m_WorkBook.setText(1, 23, "Per share");

        //        //  CONTENTS
        //        i = 0;
        //        int row = 2;
        //        string data = "";
        //        foreach (Objects.RangerCap objFills in objList)
        //        {


        //            m_WorkBook.setText(row, 0, "bear");
        //            m_WorkBook.setText(row, 1, objFills.order_buy_sell);// 'side
        //            m_WorkBook.setText(row, 2, "");
        //            m_WorkBook.setText(row, 3, "csus");
        //            m_WorkBook.setText(row, 4, objFills.order_symbol);// 'symbol
        //            m_WorkBook.setText(row, 5, objFills.order_close_date);// 'TD
        //            m_WorkBook.setText(row, 6, objFills.settlementdate);// 'SD
        //            m_WorkBook.setText(row, 7, "");
        //            m_WorkBook.setText(row, 8, objFills.order_fills);// 'Quantity
        //            m_WorkBook.setText(row, 11, "caus");
        //            m_WorkBook.setText(row, 12, "$cash");
        //            m_WorkBook.setText(row, 17, "@" + objFills.order_avg.ToString());// 'Price
        //            m_WorkBook.setText(row, 21, "1");
        //            if (objFills.order_buy_sell == "ss")
        //            { m_WorkBook.setText(row, 22, "y"); }// 'Sec Fee 
        //            else
        //            { m_WorkBook.setText(row, 22, ""); }// 'Sec Fee


        //            m_WorkBook.setText(row, 23, "@" + objFills.pershare);// 'pershare
        //            m_WorkBook.setText(row, 24, "kcci");
        //            m_WorkBook.setText(row, 25, "n");
        //            m_WorkBook.setText(row, 28, "n");
        //            m_WorkBook.setText(row, 29, objFills.location);// 'location
        //            m_WorkBook.setText(row, 41, "1");
        //            m_WorkBook.setText(row, 44, "n");
        //            m_WorkBook.setText(row, 45, "y");

        //            row++;

        //        }


        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;

        //    }

        //}
        //public static string createXSL(List<Objects.ETF> objList, List<Objects.ETF> objListS, ArrayList Headres, string filepath, string date)
        //{
        //    m_WorkBook = new WorkBook();
        //    m_WorkBook.Sheet = 0;


        //    try
        //    {
        //        //HEADER
        //        int i = 0;
        //        m_WorkBook.setText(0, 0, "WallachBeth Trades");
        //        m_WorkBook.setSelection("A1:U1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 300;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(1, 0, Convert.ToDateTime(date).ToString("dddd MMMM d,yyyy"));
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("A2:U2");
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        foreach (string str in Headres)
        //        {

        //            m_WorkBook.setText(2, i, str.Split('|')[0]);
        //            m_WorkBook.setColWidth(i, 100 * Convert.ToInt32(str.Split('|')[1]));
        //            i++;
        //        }
        //        // Convert. (i+65)
        //        m_WorkBook.setSelection("A3:" + Utility.Number2String(i, true) + "3");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_RangeStyle.Pattern = RangeStyle.PatternSolid;
        //        m_RangeStyle.PatternFG = 0xeeeeee;
        //        m_RangeStyle.Locked = true;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_RangeStyle = m_WorkBook.getRangeStyle();


        //        //CONTENTS
        //        i = 0;
        //        int row = 3;
        //        string data = "";

        //        foreach (Objects.ETF objETF in objList)
        //        {

        //            m_WorkBook.setText(row, 0, objETF.OrderName);
        //            m_WorkBook.setText(row, 1, objETF.Date);
        //            m_WorkBook.setText(row, 2, objETF.Branch);
        //            m_WorkBook.setText(row, 3, objETF.Time);
        //            m_WorkBook.setText(row, 4, objETF.Instr);
        //            // m_WorkBook.setNumber(row, 5, objETF.Quan);
        //            m_WorkBook.setText(row, 5, objETF.Symbol);
        //            //m_WorkBook.setText(row, 7, objETF.Price);
        //            // m_WorkBook.setText(row, 8, objETF.TIF);
        //            m_WorkBook.setText(row, 6, objETF.Acct);
        //            //m_WorkBook.setNumber(row, 10, objETF.LMT);
        //            m_WorkBook.setNumber(row, 7, objETF.Commish1);
        //            m_WorkBook.setNumber(row, 8, objETF.DVP);
        //            m_WorkBook.setNumber(row, 9, Math.Abs(objETF.MUMD));
        //            m_WorkBook.setNumber(row, 10, objETF.ExQuan);
        //            m_WorkBook.setNumber(row, 11, objETF.ExPrice);
        //            m_WorkBook.setNumber(row, 12, objETF.CSA);
        //            m_WorkBook.setFormula(row, 13, "h" + (row + 1) + "+i" + (row + 1) + "+j" + (row + 1));
        //            // m_WorkBook.setNumber(row, 17, objETF.Commish);
        //            m_WorkBook.setNumber(row, 14, objETF.Exec);
        //            m_WorkBook.setText(row, 15, objETF.Firm);
        //            m_WorkBook.setText(row, 16, objETF.CXLQuan);

        //            if (objETF.Multiplier == "1")
        //            {
        //                // string sformula = "if(E" + row + "=\"S\" or E " + row + "\"" + "=SS" + "\" or E" + row + "\"" + "=Sell" + "\",k" + row + "*0.0000192,0)";
        //                m_WorkBook.setFormula(row, 17, "IF( OR(E" + (row + 1) + "=\"S\" , E" + (row + 1) + "=\"SS\" , E" + (row + 1) + "=\"Sell\"),k" + (row + 1) + "*0.0000192,0)");
        //            }
        //            else
        //            {
        //                m_WorkBook.setNumber(row, 17, 0);
        //            }
        //            m_WorkBook.setFormula(row, 18, "R" + (row + 1) + "-M" + (row + 1) + "-O" + (row + 1) + "");
        //            m_WorkBook.setText(row, 19, objETF.Filled);
        //            row++;

        //        }






        //        if (row > 3)
        //        {
        //            toPrice2d("H4:H" + (row + 1));
        //            m_WorkBook.setFormula(row, 7, "sum(H4:H" + row + ")");
        //            toPrice2d("I4:I" + (row + 1));
        //            m_WorkBook.setFormula(row, 8, "sum(I4:I" + row + ")");
        //            toPrice2d("J4:J" + (row + 1));
        //            m_WorkBook.setSelection("J4:J" + (row + 1));
        //            m_WorkBook.setFormula(row, 9, "sum(J4:J" + row + ")");
        //            toComma("K4:K" + (row + 1));
        //            m_WorkBook.setFormula(row, 10, "sum(K4:K" + row + ")");
        //            toPrice4d("L4:L" + (row + 1));
        //            m_WorkBook.setSelection("L4:L" + (row + 1));
        //            toPrice4d("M4:M" + (row + 1));
        //            toPrice2d("N4:N" + (row + 1));
        //            m_WorkBook.setFormula(row, 13, "sum(N4:N" + row + ")");
        //            toPrice4d("O4:O" + (row + 1));
        //            m_WorkBook.setFormula(row, 14, "sum(O4:O" + row + ")");
        //            toPrice2d("R4:R" + (row + 1));
        //            m_WorkBook.setFormula(row, 17, "sum(R4:R" + row + ")");
        //            toPrice2d("S4:S" + (row + 1));
        //            m_WorkBook.setFormula(row, 18, "sum(S4:S" + row + ")");
        //            FooterFormat("A" + (row + 1) + ":s" + (row + 1));
        //        }


        //        row = row + 3;
        //        m_WorkBook.setText(row, 11, "Month To Date Totals");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("j" + (row + 1) + ":N" + (row + 1));
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        HeaderFormat("J" + (row + 1) + ":N" + (row + 1));
        //        row = row + 1;
        //        m_WorkBook.setText(row, 9, "Contracts");
        //        m_WorkBook.setText(row, 10, "Commish");
        //        m_WorkBook.setText(row, 11, "DVP");
        //        m_WorkBook.setText(row, 12, "MU/MD");
        //        m_WorkBook.setText(row, 13, "Total");
        //        FooterFormat("J" + (row + 1) + ":N" + (row + 1));
        //        row = row + 1;

        //        Objects.ETF objETF1 = objListS[0];

        //        m_WorkBook.setNumber(row, 9, objETF1.ExQuan);
        //        toComma("J" + (row + 1));
        //        m_WorkBook.setNumber(row, 10, objETF1.Commish);
        //        toPrice2d("K" + (row + 1));
        //        m_WorkBook.setNumber(row, 11, objETF1.DVP);
        //        toPrice2d("L" + (row + 1));
        //        m_WorkBook.setNumber(row, 12, objETF1.MUMD);
        //        toPrice2d("M" + (row + 1));
        //        m_WorkBook.setFormula(row, 13, "sum(K" + (row + 1) + ":M" + (row + 1) + ")");
        //        toPrice2d("N" + (row + 1));


        //        m_WorkBook.setSelection("A4");
        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;

        //    }

        //}
        //public static string createXSLCSA(ArrayList result, string filepath)
        //{
        //    m_WorkBook = new WorkBook();



        //    try
        //    {
        //        Hashtable heading = new Hashtable();
        //        heading = (Hashtable)result[0];


        //        m_WorkBook.NumSheets = 2;
        //        m_WorkBook.Sheet = 0;
        //        m_WorkBook.setSheetName(0, "CSA Detail ( " + heading["month"] + "" + heading["year"] + " )");
        //        m_WorkBook.setText(0, 0, heading["bd_name"].ToString());
        //        m_WorkBook.setSelection("A1:E1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 300;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(1, 0, heading["client"].ToString());
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("A2:E2");
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(2, 0, "CSA Detail ( " + heading["month"] + "" + heading["year"] + " )");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("A3:E4");
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(4, 0, "CSA Detail");
        //        FooterFormat("A5");






        //        m_WorkBook.setText(6, 0, "Date");
        //        m_WorkBook.setColWidth(0, 3500);
        //        //m_WorkBook.setText(6, 1, "Nick");
        //        //m_WorkBook.setColWidth(1, 5000);
        //        m_WorkBook.setText(6, 1, "Side");
        //        m_WorkBook.setColWidth(1, 1500);
        //        m_WorkBook.setText(6, 2, "Shares");
        //        m_WorkBook.setColWidth(2, 4000);
        //        m_WorkBook.setText(6, 3, "Symbol");
        //        m_WorkBook.setColWidth(3, 2000);
        //        m_WorkBook.setText(6, 4, "Price");
        //        m_WorkBook.setColWidth(4, 3000);
        //        m_WorkBook.setText(6, 5, "Rate");
        //        m_WorkBook.setColWidth(5, 3000);
        //        m_WorkBook.setText(6, 6, "Gross Comm.");
        //        m_WorkBook.setColWidth(6, 4000);
        //        m_WorkBook.setText(6, 7, "Net CSA");
        //        m_WorkBook.setColWidth(7, 4000);
        //        //m_WorkBook.setText(6, 9, "Net Comm.");
        //        //m_WorkBook.setColWidth(9, 4000);
        //        HeaderFormat("A7:H7");



        //        int row = 7;


        //        foreach (Objects.Details obj in (List<Objects.Details>)result[5])
        //        {
        //            m_WorkBook.setText(row, 0, obj.order_close_date);
        //            // m_WorkBook.setText(row, 1, obj.order_account_nick);
        //            m_WorkBook.setText(row, 1, obj.order_buy_sell);
        //            m_WorkBook.setNumber(row, 2, obj.order_fills);
        //            m_WorkBook.setText(row, 3, obj.order_symbol);
        //            m_WorkBook.setNumber(row, 4, obj.order_avg);
        //            m_WorkBook.setNumber(row, 5, obj.rate);
        //            m_WorkBook.setNumber(row, 6, obj.gross_comm);
        //            m_WorkBook.setNumber(row, 7, obj.csa);
        //            //m_WorkBook.setNumber(row, 9, obj.net_comm);
        //            row++;
        //        }

        //        toComma("C8:C" + (row + 1));
        //        toPrice4d("E8:E" + (row + 1));
        //        toPrice2d("F8:F" + row);
        //        toPrice2d("G8:G" + (row + 1));
        //        toPrice2d("H8:H" + (row + 1));
        //        //toPrice2d("J8:J" + (row+1));
        //        // var summaryTotal = result[4];

        //        m_WorkBook.setText(row, 0, "Total");
        //        Hashtable summaryTotal = new Hashtable();
        //        summaryTotal = (Hashtable)result[6];
        //        m_WorkBook.setNumber(row, 2, Convert.ToDouble(summaryTotal["totalShares"]));
        //        m_WorkBook.setNumber(row, 6, Convert.ToDouble(summaryTotal["totalGross"]));
        //        m_WorkBook.setNumber(row, 7, Convert.ToDouble(summaryTotal["totalCSA"]));
        //        row++;
        //        FooterFormat("A" + row + ":H" + row);





        //        m_WorkBook.Sheet = 1;
        //        m_WorkBook.setSheetName(1, "CSA Summary");
        //        //HEADER
        //        int i = 0;

        //        m_WorkBook.setText(0, 0, heading["bd_name"].ToString());
        //        m_WorkBook.setSelection("A1:F1");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 300;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(1, 0, heading["client"].ToString());
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("A2:F2");
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(2, 0, "CSA Summary ( " + heading["month"] + "" + heading["year"] + " )");
        //        m_RangeStyle = m_WorkBook.getRangeStyle();
        //        m_WorkBook.setSelection("A3:F4");
        //        m_RangeStyle.MergeCells = true;
        //        m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
        //        m_RangeStyle.FontBold = true;
        //        m_RangeStyle.FontSize = 200;
        //        m_WorkBook.setRangeStyle(m_RangeStyle);
        //        m_WorkBook.setText(4, 0, "Acct. Recap");
        //        FooterFormat("A5");

        //        m_WorkBook.setText(6, 0, "Month");
        //        m_WorkBook.setColWidth(0, 6000);
        //        m_WorkBook.setText(6, 1, "Total CSA");
        //        m_WorkBook.setColWidth(1, 4000);
        //        HeaderFormat("A7:B7");
        //        m_WorkBook.setText(6, 3, "Vendor Paid");
        //        m_WorkBook.setColWidth(3, 8000);
        //        m_WorkBook.setText(6, 4, "Date Paid");
        //        m_WorkBook.setColWidth(4, 4000);
        //        m_WorkBook.setText(6, 5, "Amount Paid");
        //        m_WorkBook.setColWidth(5, 4000);
        //        HeaderFormat("D7:F7");

        //        //  CONTENTS
        //        i = 0;
        //        row = 7;
        //        string data = "";

        //        Hashtable recap = new Hashtable();
        //        recap = (Hashtable)result[7];

        //        //m_WorkBook.setText(row, 0, recap["BalanceForwardMonth"].ToString());
        //        //m_WorkBook.setNumber(row, 1, Convert.ToDouble(recap["BalanceForward"]));
        //        //row++;

        //        foreach (Objects.Summary objSummary in (List<Objects.Summary>)result[3])
        //        {
        //            m_WorkBook.setText(row, 0, objSummary.mon);
        //            m_WorkBook.setNumber(row, 1, objSummary.amount);
        //            row++;
        //        }

        //        m_WorkBook.setText(row, 0, "Total");
        //        m_WorkBook.setNumber(row, 1, Convert.ToDouble(result[4]));
        //        row++;
        //        toPrice2d("B8:B" + (row + 10));
        //        FooterFormat("A" + row + ":B" + row);
        //        row = row + 3;
        //        m_WorkBook.setText(row, 0, "Recap");
        //        m_WorkBook.setText(row, 1, "Amount");
        //        row++;
        //        HeaderFormat("A" + row + ":B" + row);

        //        m_WorkBook.setText(row, 0, recap["BalanceLabel"].ToString());
        //        m_WorkBook.setNumber(row, 1, Convert.ToDouble(recap["openingbalance"]));
        //        row++;
        //        m_WorkBook.setText(row, 0, "YTD Net Comm.");
        //        m_WorkBook.setNumber(row, 1, Convert.ToDouble(recap["NetComm"]));
        //        row++;
        //        m_WorkBook.setText(row, 0, "YTD Paid Amt.");
        //        m_WorkBook.setNumber(row, 1, Convert.ToDouble(recap["NetAmount"]));
        //        row++;
        //        m_WorkBook.setText(row, 0, "Total");
        //        m_WorkBook.setNumber(row, 1, Convert.ToDouble(recap["Balance"]));
        //        row++;
        //        FooterFormat("A" + row + ":B" + row);

        //        row = 7;
        //        foreach (Objects.VendorPaid obj in (List<Objects.VendorPaid>)result[1])
        //        {
        //            m_WorkBook.setText(row, 3, obj.vendor_name);
        //            m_WorkBook.setText(row, 4, obj.sdi_requested_payment_date);
        //            m_WorkBook.setNumber(row, 5, obj.sdi_amount);
        //            row++;
        //        }
        //        toPrice2d("F7:F" + (row + 1));
        //        m_WorkBook.setText(row, 3, "Total");
        //        m_WorkBook.setNumber(row, 5, Convert.ToDouble(result[2]));
        //        row++;
        //        FooterFormat("D" + row + ":F" + row);





        //        m_WorkBook.Sheet = 1;
        //        m_WorkBook.setSelection("A6");
        //        m_WorkBook.Sheet = 0;
        //        m_WorkBook.setSelection("A6");

        //        m_WorkBook.write(HttpContext.Current.Server.MapPath(filepath));

        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;

        //    }

        //}




        public static bool HeaderFormat(string range)
        {

            m_WorkBook.setSelection(range);
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
            m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
            m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
            m_RangeStyle.FontBold = true;
            m_RangeStyle.FontSize = 200;
            m_RangeStyle.Pattern = RangeStyle.PatternSolid;
            m_RangeStyle.PatternFG = 0xeeeeee;
            m_WorkBook.setRangeStyle(m_RangeStyle);



            return true;
        }

        public static bool FooterFormat(string range)
        {

            m_WorkBook.setSelection(range);
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
            m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
            m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentCenter;
            m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentBottom;
            m_RangeStyle.FontBold = true;
            m_RangeStyle.FontSize = 200;
            m_WorkBook.setRangeStyle(m_RangeStyle);



            return true;
        }


        public static bool toPrice2d(string range)
        {
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.CustomFormat = "$* #,##0.00_);[Red]($* #,##0.00)";
            m_WorkBook.setSelection(range);
            m_WorkBook.setRangeStyle(m_RangeStyle);
            return true;
        }

        public static bool toPrice4d(string range)
        {
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.CustomFormat = "$* #,##0.0000_);[Red]($* #,##0.0000)";
            m_WorkBook.setSelection(range);
            m_WorkBook.setRangeStyle(m_RangeStyle);
            return true;
        }

        public static bool toComma(string range)
        {
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.CustomFormat = "#,###";
            m_WorkBook.setSelection(range);
            m_WorkBook.setRangeStyle(m_RangeStyle);
            return true;
        }

    }
}