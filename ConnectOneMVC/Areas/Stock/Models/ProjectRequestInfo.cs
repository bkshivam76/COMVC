using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ConnectOneMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;//Mantis bug 0000462 fixed

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class Model_Project_Info : AllRights { }
    [Serializable]
    public class Model_Project_NEVD
    {
        public DateTime? Project_Actual_Start_Date { get; set; }
        public int? Project_Estimator_ID { get; set; }
        [Display(Name = "Project Engineer")]
        
        public int? Project_Engineer_ID { get; set; }
        [Display(Name = "Assignee's Sub-Department")]        
        public int[] Project_Assignee_Sub_Dept_ID { get; set; }//Mantis bug 0000314 fixed//Mantis bug 0000323 fixed
        [Display(Name = "Assignee's Main Department")]
        [Required(ErrorMessage = "Main Department Can not be blank...!")]
        public int[] Project_Assignee_Main_Dept_ID { get; set; }//Mantis bug 0000314 fixed
        [Display(Name = " Project Summary")]        
    //    [RegularExpression("^[a-zA-Z0-9, -.]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]//Mantis bug 0000321 fixed //MANTIS #1359
        public string Project_Summary { get; set; }
        public DateTime? Project_Estimation_Date { get; set; }
        public DateTime? Project_Req_Finish_Date { get; set; }
        public DateTime? Project_Req_Start_Date { get; set; }
        public int? Project_Requestor_Sub_Dept_ID { get; set; }
        public int? Project_Requestor_Main_Dept_ID { get; set; }
        public DateTime? Project_Sanction_Date { get; set; }
        [Index(IsUnique = true)]//Mantis bug 0000462 fixed
        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string Project_Sanction_No { get; set; }
        [Display(Name = "Project Type")]
        [Required(ErrorMessage = "Project Type Can not be blank...!")]
        public string Project_Type_ID { get; set; }
        [Display(Name = "Complex Name")]
        [Required(ErrorMessage = "Complex Name Can not be blank...!")]
        public string Project_Complex_ID { get; set; }
        [Display(Name = "Project Request Date")]
        [Required(ErrorMessage = "Project Request date Can not be blank...!")]
        public DateTime? Project_Request_Date { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name Cannot be blank...!")]
      //  [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //MANTIS #1359
        public string Project_Name { get; set; }
        public DateTime? Project_Actual_Finish_Date { get; set; }
        public int ProjectID { get; set; }
        public bool IsDisabled { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
       // [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")] //mantis #1359
        public string Project_Your_Remarks { get; set; }
        public int Project_OpenUserID { get; set; }
        public string PR_PostSuccessFunction { get; set; }
        public string PR_PopUpID { get; set; }
        public string PR_StatusButton { get; set; }

        public List<Common_Lib.DbOperations.Projects.Return_GetRegister> GridData { get; set; }

        public string Proj_Assignee_Main_Dept_ID { get; set; }//Mantis bug 0000314 fixed

        public string Proj_Assignee_Sub_Dept_ID { get; set; }//Mantis bug 0000314 fixed
        public string Project_Complex_Name { get; set; }//Mantis bug 0000453 fixed

    }
    [Serializable]
    public class PRUpDateStatus
    {
        public int PR_ID { get; set; }
        public string PR_StatusType { get; set; }
        [Required(ErrorMessage = "Remarks Cannot be blank...!")]
        public string PR_Status_Remark { get; set; }
        [Required(ErrorMessage = "Completion Date Cannot Be Blank...!")]
        public DateTime PR_Completion_Date { get; set; }
        public string PR_PopupHeader { get; set; }
    }
    [Serializable]
    public class Model_EstimationDetails
    {
        public int SR_NO { get; set; }
        public string Est_Project_Name { get; set; }
        public string Est_Complex_Name { get; set; }
        public string Est_Sanction_No { get; set; }
        //[RegularExpression("^[a-zA-Z0-9, .-]+$", ErrorMessage = "Only Alphanumeric.,- Are Allowed")]//Mantis bug 0000327 fixed //mantis #1395

        [Required(ErrorMessage = "Description Of Work Cannot Be Blank...!")]
        public string Est_Description { get; set; }//Mantis bug 0000327 fixed

        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public double? Estimated_Qty { get; set; }
        [Required(ErrorMessage = "Rate Cannot Be Blank...!")]
        public double? Est_Rate { get; set; }
        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string Est_Unit { get; set; }
        [Required(ErrorMessage = "Amount Cannot Be Blank...!")]
        public double? Est_Amount { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public int ID { get; set; }    
        public string Est_UnitName { get; set; }
    }
    [Serializable]
    public class ProjectRegister_Period
    {
        public string Pr_BE_View_Period { get; set; }
        public DateTime Pr_Fromdate { get; set; }
        public DateTime Pr_Todate { get; set; }
        public string Pr_PeriodSelection { get; set; }
        public DateTime Pr_Opendate { get; set; }
        public DateTime Pr_Closedate { get; set; }
    }
  
}