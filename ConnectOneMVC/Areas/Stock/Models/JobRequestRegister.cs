using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class JobRequestRegister_Info : AllRights
    { }
    [Serializable]
    public class JobRequestRegister_Period
    {
        public string JobRR_BE_View_Period { get; set; }
        public DateTime JobRR_Fromdate { get; set; }
        public DateTime JobRR_Todate { get; set; }
        public string JobRR_PeriodSelection { get; set; }
        public DateTime JobRR_Opendate { get; set; }
        public DateTime JobRR_Closedate { get; set; }
    }
    [Serializable]
    public class Model_NEVD_JobRequest
    {
        public int Job_Id { get; set; }
        public int? Job_No { get; set; }
        public string ActionMethod { get; set; }
        [Display(Name = "Job Name")]
        [Required(ErrorMessage = "Job Name Cannot Be Blank...!")]
       // [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis 1359
        public string Job_Name { get; set; }
        [Display(Name = "Job Request Date")]
        [Required(ErrorMessage = "Job Request Date Cannot Be Blank...!")]
        public DateTime Job_RequestDate { get; set; }
        [Required(ErrorMessage = "Job Type Cannot Be Blank...!")]
        public string Job_Type { get; set; }
        public int? Job_ProjectId { get; set; }
        public string Job_ComplexId { get; set; }
        public int? Job_RequestorId { get; set; }
        [Required(ErrorMessage = "Requestor Main Dept Cannot Be Blank...!")]
        public int? Job_RequestorMainDeptId { get; set; }
        public int? Job_RequestorSubDeptId { get; set; }
        public int? Job_AssigneeId { get; set; }
        [Required(ErrorMessage = "Assignee Main Dept Cannot Be Blank...!")]
        public int? Job_AssigneeMainDeptID { get; set; }
        [Required(ErrorMessage = "Assignee Sub Dept Cannot Be Blank....!")]//Mantis bug 0000477 fixed
        public int? Job_AssigneeSubDeptID { get; set; }
        public DateTime? Job_RequestedStartDate { get; set; }
        public DateTime? Job_RequestedFinishDate { get; set; }
        public DateTime? Job_StartDate { get; set; }
        public DateTime? Job_EndDate { get; set; }
        [Required(ErrorMessage = "Job Description Is Required...! ")]
        // [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]//mantis 1359
        public string Job_Description { get; set; }
      //  [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]//mantis 1359
        public string Job_Remarks { get; set; }
        public string Job_Status { get; set; }
        public bool? Job_EstRequired { get; set; }
        public double? Job_BudgetLimit { get; set; }
        public int? Job_CurrUser_PersonnelID { get; set; }
        public int? Job_CurrUser_MainDeptID { get; set; }
        public int? Job_CurrUser_SubDeptID { get; set; }
        public int? Job_AssigneeNameMainDeptID { get; set; }
        public int? Job_RequestorMainDeptInchargeID { get; set; }
        public int? Job_AssigneeMainDeptInchargeID { get; set; }
        public int? Job_AssigneeNameSubDeptID { get; set; }
        public string Job_SatusButtonClick { get; set; }
        public string Job_PostSuccessFunction{get;set;}
        public string Job_PopUPId { get; set; }
        public string Job_CallingScreen { get; set; }        

        public string Job_Assignee_Sub_Dept_ID { get; set; }//Mantis bug 0000374 fixed
        public string Job_Assignee_Sub_Dept_PersonnelID { get; set; }//Mantis bug 00001324 fixed
        public string Job_CurrUserRole { get; set; }//Mantis bug 0000447 fixed

        public DateTime Job_SanctionDate { get; set; }//Mantis bug 0000466 fixed
        public string Job_SanctionNo { get; set; }//Mantis bug 0000466 fixed
    }
    [Serializable]
    public class Model_Job_QuickJob
    {
        [Display(Name = "Job Name")]
        [Required(ErrorMessage = "Job Name Cannot Be Blank...!")]
      //  [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Quick_Job_Name { get; set; }
        [Display(Name = "Job Request Date")]
        [Required(ErrorMessage = "Job Request Date Cannot Be Blank...!")]
        public DateTime Quick_Job_RequestDate { get; set; }
        public int? Quick_Job_No { get; set; }
        [Required(ErrorMessage = "Job Type Cannot Be Blank...!")]
        public string Quick_Job_Type { get; set; }
        public int? Quick_Job_ProjectId { get; set; }
        public string Quick_Job_ComplexId { get; set; }
        [Required(ErrorMessage = "Assignee Main Dept Cannot Be Blank...!")]
        public int? Quick_Job_AssigneeMainDeptID { get; set; }
        public int? Quick_Job_AssigneeSubDeptID { get; set; }
        [Required(ErrorMessage = "Job Description Is Required...! ")]
        //[RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Quick_Job_Description { get; set; }
        public string Quick_Job_Status { get; set; }
        public string Quick_PostSuccessFunction { get; set; }
        public string Quick_popupID { get; set; }
        public string Quick_CallingScreen { get; set; }
        public string Job_QJ_Assignee_Sub_Dept_ID { get; set; }//Mantis bug 0000374 fixed
    }
    [Serializable]
    public class JobManpowerEstimates
    {   
  
        [Display(Name = "Estimated_Unit")]
        [Required(ErrorMessage = "Estimated Unit Cannot be blank...!")]
        public string Job_Estimated_UnitID { get; set; }
        [Display(Name = "Estimated Consumption")]
        [Required(ErrorMessage = "Estimated_Consumption Cannot be blank...!")]
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Consumption Should Be Between 0 and 99999999999999999.99 ...!")]
        public double? Job_Estimated_Consumption { get; set; }
        [Display(Name = "Estimated Rate per Unit")]
        [Required(ErrorMessage = "Estimated_Rate_per_Unit Cannot be blank...!")]
        public double? Job_Estimated_Rate_per_Unit { get; set; }
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Cost Should Be Between 0 and 99999999999999999.99 ...!")]
        public decimal Job_Est_Cost { get; set; }

        [Required(ErrorMessage = "Type of Work Cannot be blank...!")]
        public string Job_ManEstRemarks { get; set; }     
        public string Job_Manpower_skilltype { get; set; }
        [Display(Name = "Manpower Type")]
        [Required(ErrorMessage = "Manpower Type Cannot be blank...!")]
        public string Job_Manpower_skilltypeID { get; set; }
        public string Job_Estimated_Unit{ get; set; }
        public int Job_Manpower_REC_ID { get; set; }
        public string Job_UnitID { get; set; }
        public string Job_Manpower_ActionMethod { get; set; }
        public int Job_Manpower_Sr { get; set; }

    }
    [Serializable]
    public class JobItemUsageEstimation
    {
        public int Job_Item_Sr_No { get; set; }
        [Required(ErrorMessage = "Item Name Cannot be blank...!")]
        public int? Job_ItemID { get; set; }
        public string Job_ItemName { get; set; }
        public string Job_Item_Type { get; set; }
        public string Job_Item_Code { get; set; }
     //   [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Job_ItemUsage_Remarks { get; set; }        
        public double? Job_Tolerance { get; set; }
        [Required(ErrorMessage = "Estimated Amount Cannot be blank...!")]
        public double? Job_Estimated_Amount { get; set; }
        [Required(ErrorMessage = "Estimated Rate Cannot be blank...!")]
        [Range(0.01, 99999999999999999.99, ErrorMessage = "Rate Should Be Between 0 and 99999999999999999.99 ...!")]
        public double? Job_Estimated_Rate { get; set; }
        [Required(ErrorMessage = "Estimated Quantity Cannot be blank...!")]
        [Range(0.01, 999999999.99, ErrorMessage = "Quantity Should Be Between 0 and 999999999.99 ...!")]
        public double? Job_Estimated_Quantity_Usage { get; set; }
        public int Job_Item_REC_ID { get; set; }
        [Required(ErrorMessage = "Unit Cannot be blank...!")]
        public string Job_Unit { get; set; }
        public string Job_UnitID { get; set; }
        public string Job_Item_ActionMethod { get; set; }
    }
    [Serializable]
    public class JobUpDateStatus
    {
        public int JobID { get; set; }
        public string Job_StatusType { get; set; }
        [Required(ErrorMessage = "Remarks Cannot be blank...!")]
        [Display(Name = "Remarks:")]
        public string Job_Status_Remark { get; set; }
        [Required(ErrorMessage = "Completion Date Cannot Be Blank...!")]
        [Display(Name = "Completion Date:")]
        public DateTime Job_Completion_Date { get; set; }
        public string PopupHeader { get; set;}
    }
    [Serializable]
    public class Return_Complex_GetList
    {
        public string Complex_Name { get; set; }
        public int? CrossedTimeLimit { get; set; }
        public int? OpenActions { get; set; }
        public string RemarkStatus { get; set; }
        public int? RemarkCount { get; set; }
        public int? Building_Count { get; set; }
        public DateTime? Action_Date { get; set; }
        public string Action_By { get; set; }
        public string Action_Status { get; set; }
        public DateTime? Edit_date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Add_By { get; set; }
        public string ID { get; set; }
        public string YEAR_ID { get; set; }
        public string Remarks { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime? Max_Edit_on { get; set; }


    }
    [Serializable]
    public class Return_GetList
    {
        public string Project_Name { get; set; }
        public string Sanction_no;
        public DateTime? Sanction_Date { get; set; }
        public string Complex_Name { get; set; }
        public int Project_Id { get; set; }
        public string Complex_Id { get; set; }
    }
    [Serializable]
    public class UpdateProjectStatus
    {
        public int ProjectID { get; set; }
        public string StatusType { get; set; }
        public string Remarks { get; set; }
    }
    [Serializable]
    public class JobMachineUsageDetail
    {
        public int Job_Machine_Sr_No { get; set; }
        [Required(ErrorMessage = "Machine Name Can Not be Empty...!")]
        public int? Job_Machine_NameID { get; set; }
        public string Job_Machine_No { get; set; }
        [Range(1, 999, ErrorMessage = "Machine Count Cannot Be Less Than 1 and Greater Than 1000!")]
        [Required(ErrorMessage = "Machine Count Cannot be Empty...!")]
        public int? Job_Machine_Count { get; set; }
        [Required(ErrorMessage = "Machine Usage Can Not be Empty...!")]
        [Range(0.01, 9999999.99, ErrorMessage = "Machine Usage Hours Should Be Between 0.01 and 9999999.99")]
        public double? Job_Machine_Usage_Hours { get; set; }
     //   [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Job_Machine_Remarks { get; set; }
        public string Job_Machine_Name { get; set; }
        public int Job_Machine_REC_ID { get; set; }
        public string Job_Machine_ActionMethod { get; set; }
        public decimal Job_Machine_Curr_Qty { get; set; }
        public int? jobID { get; set; }

    }
    [Serializable]
    public class JobActualManpowerUsage
    {
        [Required(ErrorMessage = "Person Name Cannot be blank...!")]
        public int? Job_ManpowerID { get; set; }

        public DateTime? Leave_Date { get; set; }

        public DateTime? Joined_Date { get; set; }
        public DateTime? JobRequestDate { get; set; }//Mantis bug 0000541 fixed
        public DateTime? JobStartDate { get; set; }//Mantis bug 0000541 fixed
        public int Job_Manpower_ChargeID { get; set; }//Mantis bug 0000362 fixed
        public string Job_PersonName { get; set; }
        public string Job_ManpowerUnitsID { get; set; }
        public string Job_PersonType { get; set; }
        [Display(Name = "Work Period From")]
        [Required(ErrorMessage = "Work Period From Cannot be blank...!")]
        public DateTime? Job_W_PeriodFrom { get; set; }
        [Display(Name = "Work Period To")]
        [Required(ErrorMessage = "Work Period To Cannot be blank...!")]
        public DateTime? Job_W_PeriodTo { get; set; }
        [Required(ErrorMessage = "Unit of Work Cannot be blank...!")]
        public string Job_ManpowerUnits { get; set; }
        [Display(Name = "Actual Unit Worked")]
        [Range(0.01, 999.99, ErrorMessage = "Actual Unit Worked Should Be Between 0 and 999.99 ...!")]
        [Required(ErrorMessage = "Actual Unit Worked Cannot be blank...!")]
        public double? Job_Units_Worked { get; set; }
        [Required(ErrorMessage = "Rate/Unit Cannot Be Blank ...!")]
        public double Job_RatePerUnit { get; set; }
        [Required(ErrorMessage = "Total Amount Cannot Be Blank ...!")]
        public double? Job_TotalCost { get; set; }
     //   [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Job_ManpowerRemarks { get; set; }
        public int Job_ManpowerUsage_REC_ID { get; set; }
        public string Job_ManpowerUsage_ActionMethod { get; set; }
        public int Job_ManpowerUsage_sr { get; set; }
        public int? jobID { get; set; }
  
    }
}