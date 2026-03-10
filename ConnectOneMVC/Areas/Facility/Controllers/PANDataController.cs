using Common_Lib;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class PANDataController : BaseController
    {
        // GET: Facility/Letters
        #region Global Variables

        #endregion
        public ActionResult Frm_PAN_Info(string PAN)
        {
            ViewBag.MaxDate = BASE._open_Year_Edt;
            return View();
        }
        public ActionResult ExcelData(string PAN, string DeductionDate)
        {
            DateTime _DeductionDate = new DateTime(Convert.ToInt32(DeductionDate.Split('-')[2]), Convert.ToInt32(DeductionDate.Split('-')[1]), Convert.ToInt32(DeductionDate.Split('-')[0]));

            DataSet PAN_ds = BASE._Audit_DBOps.GetPANDatabase(PAN, _DeductionDate);
            bool result = false; string PAN_4th_Letter = "";            
            
            string Message = "", Name = "", Date = "", Specified = "", Link = "", Terms = "", PAN_Status = "", LinkingEffectiveFrom = "", SpecifiedEffectiveFrom = "",  isSpecifiedPersonClauseApplicable = "Yes";

            string Rate_Contractor = "", Rate_Transporter_upto_10_Value = "", Rate_Transporter_greater_10_Value = "", Rate_Rent_Value = "", Rate_Rent_Machinery_Value = "", Rate_Property_Purchase_Value = "", Rate_Professional_Fees_Value = "", Rate_Technical_Fees_Value = "", Rate_Interest_Value = "", Rate_Commission_Value = "";

            string paymentType_Contractor = "", paymentType_Transporter_upto_10_Value = "", paymentType_Transporter_greater_10_Value = "", paymentType_Rent_Value = "", paymentType_Rent_Machinery_Value = "", paymentType_Property_Purchase_Value = "", paymentType_Professional_Fees_Value = "", paymentType_Technical_Fees_Value = "", paymentType_Interest_Value = "", paymentType_Commission_Value = "";

            string paymentAmount_Contractor = "", paymentAmount_Transporter_upto_10_Value = "", paymentAmount_Transporter_greater_10_Value = "", paymentAmount_Rent_Value = "", paymentAmount_Rent_Machinery_Value = "", paymentAmount_Property_Purchase_Value = "", paymentAmount_Professional_Fees_Value = "", paymentAmount_Technical_Fees_Value = "", paymentAmount_Interest_Value = "", paymentAmount_Commission_Value = "";

            bool Is_SpecifiedEffective = true;
            bool Is_LinkingEffective = true;
                
            if (PAN_ds.Tables.Count > 0)
            {
                if (PAN_ds.Tables["DataBase_xls"].Rows.Count > 0)
                {
                    Name = PAN_ds.Tables["DataBase_xls"].Rows[0]["Name"].ToString();
                    DateTime AllottmentDate;
                    if (DateTime.TryParse(PAN_ds.Tables["DataBase_xls"].Rows[0]["PAN_Allotment_Date"].ToString(), out AllottmentDate))
                    { Date = AllottmentDate.ToString("dd-MMM-yyyy"); }
                    Specified = PAN_ds.Tables["DataBase_xls"].Rows[0]["Specified_Person"].ToString();
                    Link = PAN_ds.Tables["DataBase_xls"].Rows[0]["PAN_Aadhar_Link_Status"].ToString().ToUpper();
                    result = true;
                }
                else
                {
                    result = false; Message = "Invalid PAN / PAN not Found in our Database!! Please contact HQ! <br><br> PAN Invalid है या PAN हमारे Database में नहीं है!! कृपया HQ संपर्क करें !";
                }
            }
            if (!result) return Json(new { result = result, message = Message, Name = Name, Date = Date, Link = Link, Specified = Specified }, JsonRequestBehavior.AllowGet);

            // Details: As per Task 138647 the terms & conditions are hidden on the view & there is no data in the Databse Table also. 
            foreach(DataRow CRow in PAN_ds.Tables["TermsNConditions_xls"].Rows)
            { Terms = Terms + CRow["Sl"] + ". " + CRow["Particulars"] + "<br>"; }
            PAN_4th_Letter = PAN.Substring(3, 1).ToUpper();

            foreach (DataRow cRow in PAN_ds.Tables["PersonStatus_xls"].Rows) 
            {
                if (cRow["4thLetter"].ToString() == PAN_4th_Letter)
                {
                    PAN_Status = cRow["Status"].ToString();
                }
            }

            foreach (DataRow cRow in PAN_ds.Tables["LastDate_xls"].Rows)
            {
                if (cRow["Description"].ToString() == "Pan Aadhar Link")
                {
                    LinkingEffectiveFrom = Convert.ToDateTime(cRow["EffectiveFrom"].ToString()).ToString("dd-MMM-yyyy");
                    if (Convert.ToDateTime(LinkingEffectiveFrom) > _DeductionDate) { Is_LinkingEffective = false; Link = "Not Applicable"; }
                }
                if (cRow["Description"].ToString() == "Specified Person")
                {
                    SpecifiedEffectiveFrom = Convert.ToDateTime(cRow["EffectiveFrom"].ToString()).ToString("dd-MMM-yyyy");
                    if (Convert.ToDateTime(SpecifiedEffectiveFrom) > _DeductionDate) { Is_SpecifiedEffective = false; ; Link = "Not Applicable"; }
                }
            }

            String RateTabName = "IndRateChart_xls", RateColumnName = "Normal";
            if (PAN_4th_Letter != "P" && PAN_4th_Letter != "H") RateTabName= "OthRateChart_xls";
            if(Is_SpecifiedEffective == true && Is_LinkingEffective == true)
            {
                if (Link == "INOPERATIVE" || Link == "-") { RateColumnName = "Aadhar Notlink"; }
                else if (string.Equals(Specified, "Yes", StringComparison.OrdinalIgnoreCase) && BASE._open_Year_ID < 2526) { RateColumnName = "Specified Yes"; }
            }
            else if (Is_SpecifiedEffective == true && Is_LinkingEffective == false)
            {
                if (string.Equals(Specified, "Yes", StringComparison.OrdinalIgnoreCase) && BASE._open_Year_ID < 2526) RateColumnName = "Specified Yes";
            }
            else if (Is_SpecifiedEffective == false && Is_LinkingEffective == false)
            {
                
            }

            if(BASE._open_Year_ID >= 2526)
            {
                isSpecifiedPersonClauseApplicable = "No";
            }

            string paymentAmountColumnName = "payment_amount";
            string paymentTypeColumnName = "payment_type";

            DataTable RatesTable = PAN_ds.Tables[RateTabName];
            foreach (DataRow cRow in RatesTable.Rows)
            {
                if (cRow["Nature"].ToString() == "Contractor")
                {
                    Rate_Contractor = cRow[RateColumnName].ToString();
                    paymentAmount_Contractor = cRow[paymentAmountColumnName].ToString();
                    paymentType_Contractor = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Transporter upto 10 vehicles")
                {
                    Rate_Transporter_upto_10_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Transporter_upto_10_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Transporter_upto_10_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Transporter more than 10 vehicles")
                {
                    Rate_Transporter_greater_10_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Transporter_greater_10_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Transporter_greater_10_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Rent")
                {
                    Rate_Rent_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Rent_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Rent_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Rent Machinery")
                {
                    Rate_Rent_Machinery_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Rent_Machinery_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Rent_Machinery_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Property Purchase")
                {
                    Rate_Property_Purchase_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Property_Purchase_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Property_Purchase_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Professional Fees")
                {
                    Rate_Professional_Fees_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Professional_Fees_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Professional_Fees_Value = cRow[paymentTypeColumnName].ToString();
                }
                if (cRow["Nature"].ToString() == "Technical Fees")
                {
                    Rate_Technical_Fees_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Technical_Fees_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Technical_Fees_Value = cRow[paymentTypeColumnName].ToString();
                }
                // Details: As per task 138647, the interest item is removed from table.
                //if (cRow["Nature"].ToString() == "Interest")
                //{
                //    Rate_Interest_Value = cRow[RateColumnName].ToString();
                //    paymentAmount_Interest_Value = cRow[paymentAmountColumnName].ToString();
                //    paymentType_Interest_Value = cRow[paymentTypeColumnName].ToString();
                //}
                if (cRow["Nature"].ToString() == "Commission")
                {
                    Rate_Commission_Value = cRow[RateColumnName].ToString();
                    paymentAmount_Commission_Value = cRow[paymentAmountColumnName].ToString();
                    paymentType_Commission_Value = cRow[paymentTypeColumnName].ToString();
                }
            }

            return Json(new { result = result, message=Message, Name = Name, Date = Date, Link = Link, Specified = Specified, Terms = Terms, PAN_Status = PAN_Status, SpecifiedEffectiveFrom = SpecifiedEffectiveFrom, LinkingEffectiveFrom = LinkingEffectiveFrom, Rate_Contractor = Rate_Contractor, Rate_Transporter_upto_10 = Rate_Transporter_upto_10_Value, Rate_Transporter_greater_10 = Rate_Transporter_greater_10_Value, Rate_Rent = Rate_Rent_Value, Rate_Rent_Machinery = Rate_Rent_Machinery_Value, Rate_Property_Purchase = Rate_Property_Purchase_Value, Rate_Professional_Fees = Rate_Professional_Fees_Value, Rate_Technical_Fees = Rate_Technical_Fees_Value, Rate_Interest = Rate_Interest_Value, Rate_Commission = Rate_Commission_Value, isSpecifiedPersonClauseApplicable = isSpecifiedPersonClauseApplicable, paymentType_Contractor = paymentType_Contractor, paymentType_Transporter_upto_10_Value = paymentType_Transporter_upto_10_Value, paymentType_Transporter_greater_10_Value = paymentType_Transporter_greater_10_Value, paymentType_Rent_Value = paymentType_Rent_Value, paymentType_Rent_Machinery_Value = paymentType_Rent_Machinery_Value, paymentType_Property_Purchase_Value = paymentType_Property_Purchase_Value, paymentType_Professional_Fees_Value = paymentType_Professional_Fees_Value, paymentType_Technical_Fees_Value = paymentType_Technical_Fees_Value, paymentType_Interest_Value = paymentType_Interest_Value, paymentType_Commission_Value = paymentType_Commission_Value, paymentAmount_Contractor = paymentAmount_Contractor, paymentAmount_Transporter_upto_10_Value = paymentAmount_Transporter_upto_10_Value, paymentAmount_Transporter_greater_10_Value = paymentAmount_Transporter_greater_10_Value, paymentAmount_Rent_Value = paymentAmount_Rent_Value, paymentAmount_Rent_Machinery_Value = paymentAmount_Rent_Machinery_Value, paymentAmount_Property_Purchase_Value = paymentAmount_Property_Purchase_Value, paymentAmount_Professional_Fees_Value = paymentAmount_Professional_Fees_Value, paymentAmount_Technical_Fees_Value = paymentAmount_Technical_Fees_Value, paymentAmount_Interest_Value = paymentAmount_Interest_Value, paymentAmount_Commission_Value = paymentAmount_Commission_Value }, JsonRequestBehavior.AllowGet);
        }
    }
}