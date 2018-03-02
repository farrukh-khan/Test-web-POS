using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SmartXLS;
using System.Runtime.InteropServices;

namespace Web.Api.Common
{
    class ImportToXl
    {
        public static WorkBook m_WorkBook;
        int rowInd = 0;
        string symb = string.Empty;
        DataTable theData = new DataTable();
        int newEntry = 1;
        string thePath = string.Empty;
        Dictionary<int, string> basColNames = new Dictionary<int, string>();
        Dictionary<int, string> kidColNames = new Dictionary<int, string>();


        public void CreateXls(DataTable MKTDataTable1, string path)
        {
            try
            {
                
                thePath = path;
                m_WorkBook = new WorkBook();
                m_WorkBook.Sheet = 0;
                SetColumnsWidth(1);
                //DataTable MKTDataTable1 = mktdataset.Tables[0];
                theData = MKTDataTable1;
                SetHeadings(0, rowInd, 0, "ETF Basket:");
                rowInd++;
                SetHeaders(MKTDataTable1, 0, "bas");
                rowInd++;
                InsertDataInXls(MKTDataTable1, "bas");
                rowInd++;
                rowInd++;
                //int numTables = mktdataset.Tables.Count;
                //for (int i = 1; i < numTables; i++)
                {
                  //  DataTable MKTDataTable3 = mktdataset.Tables[0];
                    int sum = TotalBasketShare(MKTDataTable1);
                    SetHeadings(0, rowInd, 0, "Constituent Records :");
                    rowInd++;
                    SetHeaders(MKTDataTable1, 0, "kids");
                    rowInd++;
                    m_WorkBook.setSheetName(0, "TradeDetails");
                    InsertDataInXls(MKTDataTable1, sum, "kids");
                    m_WorkBook.write(path);
                }

                Marshal.FinalReleaseComObject(m_WorkBook);
            }
            catch { 
            }
        }




        public void CreateXls(DataSet mktdataset, string path, string symbol)
        {
            try
            {
                symb = symbol;
                thePath = path;
                if (symb == "fromBlotter")
                {
                    m_WorkBook = new WorkBook();
                    m_WorkBook.Sheet = 0;
                    SetColumnsWidth(1);
                    DataTable MKTDataTable1 = mktdataset.Tables[0];
                    theData = MKTDataTable1;
                    SetHeadings(0, rowInd, 0, "ETF Basket:");
                    rowInd++;
                    SetHeaders(MKTDataTable1, 0, "bas");
                    rowInd++;
                    InsertDataInXls(MKTDataTable1, "bas");
                    rowInd++;
                    rowInd++;
                    int numTables = mktdataset.Tables.Count;
                    for (int i = 1; i < numTables; i++)
                    {
                        DataTable MKTDataTable3 = mktdataset.Tables[i];
                        int sum = TotalBasketShare(MKTDataTable3);
                        SetHeadings(0, rowInd, 0, "Constituent Records :");
                        rowInd++;
                        SetHeaders(MKTDataTable3, i, "kids");
                        rowInd++;
                        m_WorkBook.setSheetName(0, "TradeDetails");
                        InsertDataInXls(MKTDataTable3, sum, "kids");
                        m_WorkBook.write(path);
                    }
                    //MessageBox.Show("Basket Download Complete", "Downloading Basket", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else if (symb == "LoadSingleBasket")
                {
                    m_WorkBook = new WorkBook();
                    m_WorkBook.Sheet = 0;
                    SetColumnsWidth(1);
                    DataTable MKTDataTable1 = mktdataset.Tables[0];

                    SetHeadings(0, rowInd, 0, "ETF Basket:");
                    rowInd++;
                    SetHeaders(MKTDataTable1, 0, "");
                    rowInd++;
                    InsertDataInXls(MKTDataTable1, "");
                    rowInd++;
                    rowInd++;
                    DataTable MKTDataTable3 = mktdataset.Tables[1];
                    int sum = TotalBasketShare(MKTDataTable3);
                    SetHeadings(0, rowInd, 0, "Constituent Records :");
                    rowInd++;
                    SetHeaders(MKTDataTable3, 1, "kids");
                    rowInd++;
                    m_WorkBook.setSheetName(0, symbol);
                    InsertDataInXls(MKTDataTable3, sum, "");
                    m_WorkBook.write(path);
                    //MessageBox.Show("Basket Download Complete", "Downloading Basket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    m_WorkBook = new WorkBook();
                    m_WorkBook.Sheet = 0;
                    SetColumnsWidth(1);
                    DataTable MKTDataTable1 = mktdataset.Tables[0];

                    SetHeadings(0, rowInd, 0, "ETF Basket:");
                    rowInd++;
                    SetHeaders(MKTDataTable1, 0, "");
                    rowInd++;
                    InsertDataInXls(MKTDataTable1, "");
                    rowInd++;
                    rowInd++;
                    DataTable MKTDataTable2 = mktdataset.Tables[0];
                    SetHeadings(0, rowInd, 0, "Header Record :");
                    rowInd++;
                    SetHeaders(MKTDataTable2, 0, "");
                    rowInd++;
                    InsertDataInXls(MKTDataTable2, "");
                    rowInd++;
                    rowInd++;
                    DataTable MKTDataTable3 = mktdataset.Tables[1];
                    int sum = TotalBasketShare(MKTDataTable3);
                    SetHeadings(0, rowInd, 0, "Constituent Records :");
                    rowInd++;
                    SetHeaders(MKTDataTable3, 1, "");
                    rowInd++;
                    m_WorkBook.setSheetName(0, symbol);
                    InsertDataInXls(MKTDataTable3, sum, "");
                    m_WorkBook.write(path);
                }
                Marshal.FinalReleaseComObject(m_WorkBook);
            }
            catch { }
        }
        //these are column values and hyperlinks
        public void SetHeaders(DataTable dt, int newcol, string bas)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                HeaderFormat(rowInd, j); // This Fucntion Sets The Inner Header Format

                if (symb == "fromBlotter" || symb == "LoadSingleBasket")
                {
                    string value = dt.Columns[j].ColumnName.ToString();
                    if (j != 0)
                    {
                        if (bas == "bas") basColNames.Add(j, value);
                        if (bas == "kids" && !kidColNames.Keys.Contains(j)) kidColNames.Add(j, value);
                    }
                    else
                    {
                        newEntry++;
                    }
                    m_WorkBook.setText(0, rowInd, j, value);
                }

                if (newcol > 0 && symb != "fromBlotter")
                {
                    HeaderFormat(rowInd, dt.Columns.Count);
                    m_WorkBook.setText(0, rowInd, dt.Columns.Count, "Constituent Weighting");
                }
            }
        }

        public void SetHeadings(int sheet, int row, int col, string heading)
        {
            if (symb == "fromBlotter")
            {
                RangeStyle m_RangeStyle;
                if (heading == "ETF Basket:")
                {
                    m_WorkBook.setSelection(rowInd, 0, rowInd, 26);

                }
                else
                {
                    m_WorkBook.setSelection(rowInd, 0, rowInd, 24);

                }
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
                m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 200;
                m_RangeStyle.FontColor = 0x2C612D;
                m_RangeStyle.Pattern = RangeStyle.PatternSolid;
                m_RangeStyle.PatternFG = 0xC6EFCE;
                m_WorkBook.setRangeStyle(m_RangeStyle);
                m_WorkBook.setText(sheet, row, col, heading);
            }

            else
            {
                RangeStyle m_RangeStyle;
                m_WorkBook.setSelection(rowInd, 0, rowInd, 30);
                m_RangeStyle = m_WorkBook.getRangeStyle();
                m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
                m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
                m_RangeStyle.FontBold = true;
                m_RangeStyle.FontSize = 200;
                m_RangeStyle.FontColor = 0x2C612D;
                m_RangeStyle.Pattern = RangeStyle.PatternSolid;
                m_RangeStyle.PatternFG = 0xC6EFCE;
                m_WorkBook.setRangeStyle(m_RangeStyle);
                m_WorkBook.setText(sheet, row, col, heading);
            }
        }

        //for constituents!!!
        public void InsertDataInXls(DataTable MKTDataTable, int TotalBasShares, string bas)
        {

            int StartInd = rowInd;
            int result;
            double dol;
            int rowCount = 0;
            foreach (DataRow dr in MKTDataTable.Rows)
            {
                for (int i = 0; i < MKTDataTable.Columns.Count; i++)
                {
                    if (i == 0 && rowCount == 0)
                    {
                        //insert hyperlinks to summary data
                        if (theData.TableName == "AllBaskets")
                        {
                            string value = MKTDataTable.TableName.ToString();
                            m_WorkBook.addHyperlink(newEntry, 0, newEntry, 0, ("A" + (rowInd - 1).ToString()), HyperLink.kRange, "View This Basket");
                            m_WorkBook.addHyperlink(rowInd - 1, 0, rowInd - 1, 0, ("A" + (newEntry + 1).ToString()), HyperLink.kRange, "To Summary");

                            m_WorkBook.setText(0, rowInd - 1, 0, value);
                            //newEntry++;
                        }

                    }
                    if (bas == "")
                    {


                        if (int.TryParse(dr[i].ToString(), out result))
                        {

                            if (result >= 0)
                            {
                                ConvertFormat(rowInd, i, "int+", bas);
                            }
                            else if (result < 0)
                            {
                                ConvertFormat(rowInd, i, "int-", bas);
                            }
                            if (MKTDataTable.Columns[i].ColumnName == "CUSIP" || MKTDataTable.Columns[i].ColumnName == "SEDOL" || MKTDataTable.Columns[i].ColumnName == "ColumnOrderID" || MKTDataTable.Columns[i].ColumnName == "OrderID")
                            {
                                m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                            }
                            else
                            {
                                m_WorkBook.setNumber(0, rowInd, i, Convert.ToInt32(dr[i]));

                            }
                        }
                        else
                        {
                            if (double.TryParse(dr[i].ToString(), out dol))
                            {
                                if (dol <= 1 && dol >= 0)
                                {
                                    ConvertFormat(rowInd, i, "percent", bas);
                                }
                                else
                                {
                                    if (dol >= 0)
                                    {
                                        ConvertFormat(rowInd, i, "double+", bas);
                                    }
                                    else
                                    {
                                        ConvertFormat(rowInd, i, "double-", bas);
                                    }
                                }

                                if (MKTDataTable.Columns[i].ColumnName == "CUSIP" || MKTDataTable.Columns[i].ColumnName == "SEDOL")
                                {
                                    m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                                }
                                else
                                {
                                    m_WorkBook.setNumber(0, rowInd, i, Convert.ToDouble(dr[i]));
                                }
                            }
                            else
                            {
                                m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                            }
                        }
                    }
                    if (bas == "kids")
                    {

                        if (int.TryParse(dr[i].ToString(), out result))
                        {
                            if (!kidColNames[i].Contains("ID"))
                            {
                                if (result >= 0 && kidColNames[i] != "ADV_AMT" && kidColNames[i] != "% of Basket" && kidColNames[i] != "% Done")
                                {
                                    if (kidColNames[i] != "Notional" && kidColNames[i] != "PL_launch" && kidColNames[i] != "PL_YC")
                                    {
                                        ConvertFormat(rowInd, i, "int+", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$+", symb);

                                }
                                else if (result < 0 && kidColNames[i] != "ADV_AMT" && kidColNames[i] != "% of Basket" && kidColNames[i] != "% Done")
                                {
                                    if (kidColNames[i] != "Notional" && kidColNames[i] != "PL_launch" && kidColNames[i] != "PL_YC")
                                    {
                                        ConvertFormat(rowInd, i, "int-", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$-", symb);


                                }
                                else if (kidColNames[i] == "ADV_AMT" || kidColNames[i] == "% of Basket" || kidColNames[i] == "% Done")
                                {
                                    ConvertFormat(rowInd, i, "percent", symb);


                                }
                                m_WorkBook.setNumber(0, rowInd, i, Convert.ToInt32(dr[i]));
                            }
                        }
                        else if (double.TryParse(dr[i].ToString(), out dol))
                        {




                            if (!kidColNames[i].Contains("ID"))
                            {

                                if (dol >= 0 && kidColNames[i] != "ADV_AMT" && kidColNames[i] != "% of Basket" && kidColNames[i] != "% Done")
                                {
                                    if (kidColNames[i] != "Notional" && kidColNames[i] != "PL_launch" && kidColNames[i] != "PL_YC")
                                    {
                                        ConvertFormat(rowInd, i, "double+", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$+", symb);

                                }
                                if (dol < 0 && kidColNames[i] != "ADV_AMT" && kidColNames[i] != "% of Basket" && kidColNames[i] != "% Done")
                                {
                                    if (kidColNames[i] != "Notional" && kidColNames[i] != "PL_launch" && kidColNames[i] != "PL_YC")
                                    {
                                        ConvertFormat(rowInd, i, "double-", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$-", symb);


                                }
                                else if (kidColNames[i] == "ADV_AMT" || kidColNames[i] == "% of Basket" || kidColNames[i] == "% Done")
                                {
                                    ConvertFormat(rowInd, i, "percent", symb);


                                }
                                m_WorkBook.setNumber(0, rowInd, i, Convert.ToDouble(dr[i]));
                            }
                        }

                        else
                        {
                            m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                        }

                    }

                    int row = rowInd + 1;
                    if (symb != "fromBlotter")
                    {
                        m_WorkBook.setFormula(rowInd, MKTDataTable.Columns.Count, "(K" + row + "/" + TotalBasShares + ")*100");
                    }
                    toPrice4d(rowInd, MKTDataTable.Columns.Count);

                }
                rowInd++;
                rowCount++;
            }

        }
        public void InsertDataInXls(DataTable MKTDataTable, string symb)
        {
            int result;
            double dol;
            foreach (DataRow dr in MKTDataTable.Rows)
            {
                for (int i = 0; i < MKTDataTable.Columns.Count; i++)
                {
                    if (symb == "")
                    {
                        if (int.TryParse(dr[i].ToString(), out result))
                        {
                            if (result >= 0)
                            {
                                ConvertFormat(rowInd, i, "int+", symb);

                            }
                            else
                            {
                                ConvertFormat(rowInd, i, "int-", symb);


                            }
                            m_WorkBook.setNumber(0, rowInd, i, Convert.ToInt32(dr[i]));
                        }
                        else
                        {
                            if (double.TryParse(dr[i].ToString(), out dol))
                            {
                                if (dol <= 1 && dol >= 0)
                                {
                                    ConvertFormat(rowInd, i, "percent", symb);
                                }
                                else
                                {
                                    if (dol >= 0)
                                    {
                                        ConvertFormat(rowInd, i, "double+", symb);
                                    }
                                    else
                                    {
                                        ConvertFormat(rowInd, i, "double-", symb);
                                    }
                                }
                                m_WorkBook.setNumber(0, rowInd, i, Convert.ToDouble(dr[i]));
                            }
                            else
                            {
                                m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (symb == "bas")
                        {
                            if (int.TryParse(dr[i].ToString(), out result))
                            {
                                if (result >= 0 && basColNames[i] != "% Done" && basColNames[i] != "% ADV")
                                {
                                    if (basColNames[i] != "PL (launch)" && basColNames[i] != "PL (yc)" && basColNames[i] != "Notional" && basColNames[i] != "Fills Amt" && basColNames[i] != "Leaves Amt")
                                    {
                                        ConvertFormat(rowInd, i, "int+", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$+", symb);

                                }
                                else if (result < 0 && basColNames[i] != "% Done" && basColNames[i] != "% ADV")
                                {
                                    if (basColNames[i] != "PL (launch)" && basColNames[i] != "PL (yc))" && basColNames[i] != "Notional" && basColNames[i] != "Fills Amt" && basColNames[i] != "Leaves Amt")
                                    {
                                        ConvertFormat(rowInd, i, "int-", symb);
                                    }
                                    else
                                        ConvertFormat(rowInd, i, "$-", symb);


                                }
                                else if (basColNames[i] == "% Done" || basColNames[i] == "% ADV")
                                {
                                    ConvertFormat(rowInd, i, "percent", symb);


                                }
                                m_WorkBook.setNumber(0, rowInd, i, Convert.ToInt32(dr[i]));
                            }
                            else
                            {
                                if (double.TryParse(dr[i].ToString(), out dol))
                                {

                                    if (dol >= 0 && basColNames[i] != "% Done" && basColNames[i] != "% ADV")
                                    {
                                        if (basColNames[i] != "PL (launch)" && basColNames[i] != "PL (yc)" && basColNames[i] != "Notional" && basColNames[i] != "Fills Amt" && basColNames[i] != "Leaves Amt")
                                        {
                                            ConvertFormat(rowInd, i, "double+", symb);
                                        }
                                        else
                                            ConvertFormat(rowInd, i, "$+", symb);

                                    }
                                    else if (dol < 0 && basColNames[i] != "% Done" && basColNames[i] != "% ADV")
                                    {
                                        if (basColNames[i] != "PL (launch)" && basColNames[i] != "PL (yc)" && basColNames[i] != "Notional" && basColNames[i] != "Fills Amt" && basColNames[i] != "Leaves Amt")
                                        {
                                            ConvertFormat(rowInd, i, "double-", symb);
                                        }
                                        else
                                            ConvertFormat(rowInd, i, "$-", symb);


                                    }
                                    else if (basColNames[i] == "% Done" || basColNames[i] == "% ADV")
                                    {
                                        ConvertFormat(rowInd, i, "percent", symb);


                                    }
                                    m_WorkBook.setNumber(0, rowInd, i, Convert.ToDouble(dr[i]));
                                }
                                else
                                {
                                    m_WorkBook.setText(0, rowInd, i, dr[i].ToString());
                                }
                            }


                        }
                        else if (symb == "kids")
                        { }





                    }
                }
                rowInd++;
            }
        }

        public void HeaderFormat(int row, int col)
        {
            RangeStyle m_RangeStyle;
            m_WorkBook.setSelection(row, 0, row, col);
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.TopBorder = RangeStyle.BorderMedium;
            m_RangeStyle.LeftBorder = RangeStyle.BorderMedium;
            m_RangeStyle.RightBorder = RangeStyle.BorderMedium;
            m_RangeStyle.BottomBorder = RangeStyle.BorderMedium;
            m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
            m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;
            m_RangeStyle.FontBold = true;
            m_RangeStyle.FontSize = 200;
            m_RangeStyle.FontColor = 0x2C612D;
            m_RangeStyle.Pattern = RangeStyle.PatternSolid;
            m_WorkBook.setRangeStyle(m_RangeStyle);
        }
        //added a paramb for differentiating between ints and doubles       
        public void ConvertFormat(int row, int col, string type, string symb)
        {
            RangeStyle m_RangeStyle;
            m_WorkBook.setSelection(row, col, row, col);
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
            m_RangeStyle.VerticalAlignment = RangeStyle.VerticalAlignmentCenter;

            if (type == "int+")
            {
                m_RangeStyle.CustomFormat = "#,##0;#,##0";
            }
            else if (type == "int-")
            {
                m_RangeStyle.CustomFormat = "-#,##0;-#,##0";
            }
            else if (type == "percent")
            {
                m_RangeStyle.CustomFormat = "0.0000%";
            }
            else if (type == "double+")
            {
                m_RangeStyle.CustomFormat = "#,##0.00;#,##0.00";
            }
            else if (type == "double-")
            {
                m_RangeStyle.CustomFormat = "-#,##0.00;-#,##0.00";
            }
            else if (type == "$+")
            {
                m_RangeStyle.CustomFormat = "$#,##0.00;$#,##0.00";
            }
            else if (type == "$-")
            {
                m_RangeStyle.CustomFormat = "-$#,##0.00;-$#,##0.00";
            }


            else
            {
                m_RangeStyle.CustomFormat = "#,##0.00;#,##0.00";
            }




            m_WorkBook.setRangeStyle(m_RangeStyle);
        }
        public void SetColumnsWidth(int row)
        {
            if (symb != "fromBlotter")
            {
                for (int k = 0; k <= 30; k++)
                {
                    RangeStyle m_RangeStyle;
                    m_WorkBook.setSelection(row, k, row, k);
                    if (k > 0)
                    {
                        m_WorkBook.setColWidth(k, 5 * 1000);
                    }
                    else
                    {
                        m_WorkBook.setColWidth(k, 10 * 1000);
                    }
                    m_RangeStyle = m_WorkBook.getRangeStyle();
                    m_WorkBook.setRangeStyle(m_RangeStyle);
                }
            }
            else
            {
                for (int k = 0; k <= 39; k++)
                {
                    RangeStyle m_RangeStyle;
                    m_WorkBook.setSelection(row, k, row, k);
                    if (k > 0)
                    {
                        m_WorkBook.setColWidth(k, 5 * 1000);
                    }
                    else
                    {
                        m_WorkBook.setColWidth(k, 10 * 1000);
                    }
                    m_RangeStyle = m_WorkBook.getRangeStyle();
                    m_WorkBook.setRangeStyle(m_RangeStyle);
                }

            }
        }
        public int TotalBasketShare(DataTable DT)
        {
            int sum = 0;
            try
            {
                DataRow[] rows = DT.Select("BasketType = 'calculation'");
                foreach (DataRow dr in rows)
                {
                    sum += Convert.ToInt32(dr["SharesInBasket"]);
                }
            }
            catch (Exception ex)
            { }
            return sum;
        }
        public bool toPrice4d(int row, int col)
        {
            RangeStyle m_RangeStyle;
            m_RangeStyle = m_WorkBook.getRangeStyle();
            m_RangeStyle.CustomFormat = "#,##0.0000_);[Red](#,##0.0000)";
            m_RangeStyle.HorizontalAlignment = RangeStyle.HorizontalAlignmentLeft;
            m_WorkBook.setSelection(row, col, row, col);
            m_WorkBook.setRangeStyle(m_RangeStyle);
            return true;
        }



    }
}