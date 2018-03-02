using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models.Dashboard
{
    public class DashboardModel
    {

        // Receipts
        public double ReceiptsTotal { get; set; }

        public double ReceiptsUsed { get; set; }

        public double ReceiptPercentage { get; set; }


        // Salary
        public double SalaryTotal { get; set; }

        public double SalaryUsed { get; set; }

        public double SalaryPercentage { get; set; }



        // Capital
        public double capitalTotal { get; set; }

        public double capitalUsed { get; set; }

        public double capitalPercentage { get; set; }


        // ROE
        public double RoeTotal { get; set; }

        public double RoeUsed { get; set; }

        public double RoePercentage { get; set; }


        public double Transfered { get; set; }

        public double Received { get; set; }



        public int Post { get; set; }
        public int Filled { get; set; }
        public int DiffPost { get; set; }





        public int TotalExpensePer { get; set; }
        public int MgmtCostPer { get; set; }
        public int MCHCGrantPer { get; set; }
        public int BRHFGrantPer { get; set; }






    }
}