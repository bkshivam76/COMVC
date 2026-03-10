using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel;
using ConnectOneMVC.Areas.Help.Models;
using System.Configuration;
using ConnectOneMVC.Areas.Statements.Models;
using ConnectOneMVC.Areas.Magazine.Models;
using ConnectOneMVC.Areas.Stock.Models;
using System.Data.Entity;
using ConnectOneMVC.Areas.Membership.Models;
using ConnectOneMVC.Areas.Reports.Models;
using ConnectOneMVC.Areas.Options.Models;

namespace ConnectOneMVC.Helper
{
    public class DatatableToModel
    {
        public static List<stringData> DataTableTo_ListString(DataTable dt,string columnName="")
        {
            List<stringData> stringList = new List<stringData>();
            if (dt == null) 
            {
                return stringList;
            }
            for (int i = 0; i < dt.Rows.Count;i++) 
            {
                stringData row = new stringData();
                row.Info = dt.Rows[i][columnName].ToString();
                stringList.Add(row);
            }
            return stringList;
        }
        public static List<ChartVisibilityGridData> DataTableTo_ChartVisiblityDetailList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ChartVisibilityGridData
            {
                Rec_ID = row.Field<int>("REC_ID"),
                Centre = row.Field<string>("CENTRE"),
                FromYear = row.Field<int?>("SCVI_FN_YEAR_FROM"),
                ToYear = row.Field<int?>("SCVI_FN_YEAR_TO"),
                Institute = row.Field<string>("INSTITUTE"),
                AccType = row.Field<string>("ACCTYPE"),
                UserType = row.Field<string>("SCVI_USER_TYPE"),
                Cen_ID = row.Field<int?>("SCVI_CEN_ID"),
                Ins_ID = row.Field<string>("SCVI_INSTT_ID"),
                AccType_ID = row.Field<string>("SCVI_ACC_TYPE_ID"),
                Cen_UID = row.Field<string>("CEN_UID"),
                VisibleInMenu= row.Field<string>("SCVI_MENU_IN_WHICH_VISIBLE")
            }).ToList();
            else return null;
        }
        public static List<FyDetail> DataTableTo_FinancialYearList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new FyDetail
            {
                cod_year_id = row.Field<int>("cod_year_id"),
                cod_year_name = row.Field<string>("cod_year_name"),
                cod_year_sdt = row.Field<DateTime>("cod_year_sdt"),
                cod_year_edt = row.Field<DateTime>("cod_year_edt")      
            }).ToList();
            else return null;
        }
        public static List<CentreDetail> DataTableTo_CentresList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new CentreDetail
            {
                cen_id = row.Field<int>("cen_id"),
                cen_uid = row.Field<string>("cen_uid"),
                cen_name = row.Field<string>("cen_name"),
                ins_name = row.Field<string>("ins_name"),
                ins_short = row.Field<string>("ins_short")
            }).ToList();
            else return null;
        }
        public static List<PartyList> DataTableTo_PartyList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new PartyList
            {
                NAME = row.Field<string>("NAME"),
                Org = row.Field<string>("C_ORG_NAME"),
                Remarks = row.Field<string>("Remarks"),
                Mobile = row.Field<string>("ContactNo"),
                Email = row.Field<string>("Email"),
                City = row.Field<string>("C_CITY"),
                EditOn = row.Field<DateTime?>("REC_EDIT_ON"),
                ID= row.Field<string>("ID")

            }).ToList();
            else return null;
        }
        public static List<miscInfo> DataTableTo_Name_ID(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new miscInfo
            {
                NAME = row.Field<string>("NAME"),
                ID = row.Field<string>("ID"),
            }).ToList();
            else return null;
        }
        public static List<AccommodationList> DataTableTo_AccommodationPlaces(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AccommodationList
            {
                Building = row.Field<string>("Building"),
                Room = row.Field<string>("ROOM"),
                REC_ID = row.Field<string>("REC_ID"),
                Allowed = row.Field<Int32>("Allowed"),
                Available = row.Field<Int32>("Available"),
                Allotted = row.Field<Int32>("AlreadyAllotted"),
                CurrentAllottment = row.Field<Int32>("CurrentAllottment"),
            }).ToList();
            else return null;
        }
      public static List<LiveFormsList> DataTableTo_LiveFromsList(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new LiveFormsList
            {
                INSTANCE_ID = row.Field<Int32>("INSTANCE_ID"),
                FORM_NAME = row.Field<string>("FORM_NAME"),
                EVENTNAME = row.Field<string>("EVENTNAME"),
                CSN_SRNO = row.Field<Int32>("CSN_SRNO"),
                CSN_CHART_ID = row.Field<Int32>("CSN_CHART_ID"),
                FROMDATE = row.Field<DateTime?>("FROMDATE"),
                TODATE = row.Field<DateTime?>("TODATE"),
                CSN_SERVICE_REPORT_ID = row.Field<string>("CSN_SERVICE_REPORT_ID")
            }).ToList();
            else return null;
        }
       public static List<AccommodationDetailedList> DataTableTo_AccommodationDetailsList(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AccommodationDetailedList
            {
                CNAME = row.Field<string>("C_NAME"),
                GENDER = row.Field<string>("GENDER"),
                //CITY = row.Field<string>("CITY"),
                //CenterName = row.Field<string>("CEN_NAME"),
                //Arr_Date = row.Field<DateTime?>("Arr_Date"),
                Dep_Date = row.Field<DateTime?>("Dep_Date"),                
                Dep_Time = row.Field<DateTime?>("Dep_Time"),                
                ROOMID = row.Field<String>("ROOM_ID"),
            }).ToList();
            else return null;
        }
        public static List<ServicePlaceNamesList> DataTableTo_ServicePlaces(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ServicePlaceNamesList
            {
                BUILDING_NAME = row.Field<string>("SP_SERVICE_PLACE_NAME"),
                REC_ID = row.Field<string>("REC_ID")
                
            }).ToList();
            else return null;
        }
        public static List<Service_Report_Event> DataTableTo_Service_Report_Event(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Service_Report_Event
            {
                Name = row.Field<string>("Name"),
                ID = row.Field<string>("ID"),
                ProjID = row.Field<string>("Proj_ID"),
                Type = row.Field<string>("Type"),
                FromDate = row.Field<DateTime>("FromDate"),
                ToDate = row.Field<DateTime>("ToDate"),
                Venue = row.Field<string>("Venue"),
                Cen_Name = row.Field<string>("Cen_Name"),
                Cen_ID = row.Field<int?>("SR_CEN_ID"),

            }).ToList();
            else return null;
        }
        public static List<scheduleInfo> DataTableTo_scheduleList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new scheduleInfo
            {
                NAME = row.Field<string>("NAME"),
                ID = row.Field<int>("Rec_ID"),
                SCHEDULE_TYPE = row.Field<string>("SCHEDULE_TYPE"),
                FROM_TIME_1 = row.Field<string>("FROM_TIME_1"),
            }).ToList();
            else return null;
        }
        public static List<Service_Report_Institute> DataTableTo_Service_Report_Institutes(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Service_Report_Institute
            {
                Ins_Name = row.Field<string>("INS_NAME"),
                Ins_ID = row.Field<string>("INS_ID"),
                Ins_Short = row.Field<string>("INS_SHORT")
            }).ToList();
            else return null;
        }
        public static List<PreviousChartInstance> DataTabletoPreviousChartInstance(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new PreviousChartInstance
            {
                ChartName = row.Field<string>("CI_CHARTNAME"),
                SrNO = row.Field<int>("REC_ID").ToString(),
            }).ToList();
            else return null;
        }
        public static List<ProjectNameList_GSM> DataTabletoProjectNameList_GSM(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProjectNameList_GSM
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Facility.Models.WingsList> DataTabletoGSR_GetWingsList(DataTable dt) 
        {
            List<ConnectOneMVC.Areas.Facility.Models.WingsList> newList = new List<ConnectOneMVC.Areas.Facility.Models.WingsList>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ConnectOneMVC.Areas.Facility.Models.WingsList newRow = new ConnectOneMVC.Areas.Facility.Models.WingsList();
                    newRow.ID = dt.Columns.Contains("ID") ? row.Field<string>("ID") : null;
                    newRow.Name = dt.Columns.Contains("Name") ? row.Field<string>("Name") : null;
                    newRow.REC_ID = dt.Columns.Contains("REC_ID") ? row.Field<string>("REC_ID") : null;             
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<ConnectOneMVC.Areas.Facility.Models.CentreList> DataTabletoGSR_CentreList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Facility.Models.CentreList
            {
                //Cen_id = row.Field<int>("Cen_id"),
                cen_name = row.Field<string>("cen_name"),
                //cen_ins_id = row.Field<string>("cen_ins_id"),
                //cen_uid = row.Field<string>("cen_uid"),
                Cen_bk_pad_no= row.Field<string>("cen_bk_pad_no")
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Facility.Models.InsList> DataTabletoGSR_InsList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Facility.Models.InsList
            {
                Cen_id = row.Field<int>("Cen_id"),
                ins_name = row.Field<string>("ins_name"),
                ins_id = row.Field<string>("ins_id")              
            }).ToList();
            else return null;
        }

        public static List<ProjectNameList> DataTabletoProjectNameList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProjectNameList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<TreePlantList> DataTabletoTreeNameList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new TreePlantList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<ProgramTypeList> DataTabletoProgramTypeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProgramTypeList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<ProgramOccasionList> DataTabletoProgramOccasionList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProgramOccasionList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<ProgramOrganiserNames> DataTabletoProgramOrganisersList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProgramOrganiserNames
            {
                //CEN_ID = row.Field<Int32>("CEN_ID"),
                CEN_NAME = row.Field<string>("CEN_NAME"),
            }).ToList();
            else return null;
        }
        public static List<WIP_FinalizedAssetList> DataTableto_GetFinalizedAssetList(DataTable dt) 
        {
            List<WIP_FinalizedAssetList> newList = new List<WIP_FinalizedAssetList>();
            if (dt != null) 
            {
                foreach (DataRow row in dt.Rows)
                {
                    WIP_FinalizedAssetList newRow = new WIP_FinalizedAssetList();
                    newRow.Finalized_Asset = dt.Columns.Contains("Finalized_Asset") ? row.Field<string>("Finalized_Asset") : null;
                    newRow.LED_TYPE = dt.Columns.Contains("LED_TYPE") ? row.Field<string>("LED_TYPE") : null;
                    newRow.CON_LED_TYPE = dt.Columns.Contains("CON_LED_TYPE") ? row.Field<string>("CON_LED_TYPE") : null;
                    newRow.ITEM_CON_MIN_VALUE = dt.Columns.Contains("ITEM_CON_MIN_VALUE") ? row.Field<int?>("ITEM_CON_MIN_VALUE") : 0;
                    newRow.ITEM_CON_MAX_VALUE = dt.Columns.Contains("ITEM_CON_MAX_VALUE") ? row.Field<int?>("ITEM_CON_MAX_VALUE") : 0;
                    newRow.Asset_Item_ID = dt.Columns.Contains("Asset_Item_ID") ? row.Field<string>("Asset_Item_ID") : null;
                    newRow.Asset_LED_ID = dt.Columns.Contains("Asset_LED_ID") ? row.Field<string>("Asset_LED_ID") : null;
                    newRow.ITEM_CON_LED_ID = dt.Columns.Contains("ITEM_CON_LED_ID") ? row.Field<string>("ITEM_CON_LED_ID") : null;
                    newList.Add(newRow);
                }
            }
            return newList;
        }

        public static List<Common_Lib.DbOperations.Return_WIP_Ledger> DataTableto_GetWIPLedgerList(DataTable dt) 
        {
            List<Common_Lib.DbOperations.Return_WIP_Ledger> newList = new List<Common_Lib.DbOperations.Return_WIP_Ledger>();
            if (dt !=null)
            {
                foreach (DataRow row in dt.Rows) 
                {
                    Common_Lib.DbOperations.Return_WIP_Ledger newRow = new Common_Lib.DbOperations.Return_WIP_Ledger();
                    newRow.WIP_LED_ID = dt.Columns.Contains("WIP_LED_ID") ? row.Field<string>("WIP_LED_ID") : null;
                    newRow.WIP_LEDGER = dt.Columns.Contains("WIP_LEDGER") ? row.Field<string>("WIP_LEDGER") : null;
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<DonationInKind_GridData> DataTableto_GetDonationINKindGridList_DNK(DataTable dt) 
        {
            List<DonationInKind_GridData> newList =new List<DonationInKind_GridData>();
            if (dt !=null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DonationInKind_GridData newRow = new DonationInKind_GridData();
                    newRow.Sr = dt.Columns.Contains("Sr.") ? row.Field<int>("Sr.") : Convert.ToInt32(null);
                    newRow.Item_ID = dt.Columns.Contains("Item_ID") ? row.Field<string>("Item_ID") : null;
                    newRow.Item_Led_ID = dt.Columns.Contains("Item_Led_ID") ? row.Field<string>("Item_Led_ID") : null;
                    newRow.Item_Trans_Type = dt.Columns.Contains("Item_Trans_Type") ? row.Field<string>("Item_Trans_Type") : null;
                    newRow.Item_Party_Req = dt.Columns.Contains("Item_Party_Req") ? row.Field<string>("Item_Party_Req") : null;
                    newRow.Item_Profile = dt.Columns.Contains("Item_Profile") ? row.Field<string>("Item_Profile") : null;
                    newRow.ITEM_VOUCHER_TYPE = dt.Columns.Contains("ITEM_VOUCHER_TYPE") ? row.Field<string>("ITEM_VOUCHER_TYPE") : null;
                    newRow.Item_Name = dt.Columns.Contains("Item_Name") ? row.Field<string>("Item_Name") : null;
                    newRow.Head = dt.Columns.Contains("Head") ? row.Field<string>("Head") : null;
                    newRow.Qty = dt.Columns.Contains("Qty.") ? row.Field<double>("Qty.") : Convert.ToDouble(null);
                    newRow.Unit = dt.Columns.Contains("Unit") ? row.Field<string>("Unit") : null;
                    newRow.Rate = dt.Columns.Contains("Rate") ? row.Field<double>("Rate") : Convert.ToDouble(null);
                    newRow.Amount = dt.Columns.Contains("Amount") ? row.Field<double>("Amount") : Convert.ToDouble(null);
                    newRow.Remarks = dt.Columns.Contains("Remarks") ? row.Field<string>("Remarks") : null;
                    newRow.Pur_ID = dt.Columns.Contains("Pur_ID") ? row.Field<string>("Pur_ID") : null;
                    newRow.LOC_ID = dt.Columns.Contains("LOC_ID") ? row.Field<string>("LOC_ID") : null;
                    newRow.GS_DESC_MISC_ID = dt.Columns.Contains("GS_DESC_MISC_ID") ? row.Field<string>("GS_DESC_MISC_ID") : null;
                    newRow.GS_ITEM_WEIGHT = dt.Columns.Contains("GS_ITEM_WEIGHT") ? row.Field<Decimal>("GS_ITEM_WEIGHT") :Convert.ToDecimal(null);
                    newRow.AI_TYPE = dt.Columns.Contains("AI_TYPE") ? row.Field<string>("AI_TYPE") : null;
                    newRow.AI_MAKE = dt.Columns.Contains("AI_MAKE") ? row.Field<string>("AI_MAKE") : null;
                    newRow.AI_MODEL = dt.Columns.Contains("AI_MODEL") ? row.Field<string>("AI_MODEL") : null;
                    newRow.AI_SERIAL_NO = dt.Columns.Contains("AI_SERIAL_NO") ? row.Field<string>("AI_SERIAL_NO") : null;
                    newRow.AI_PUR_DATE = dt.Columns.Contains("AI_PUR_DATE") ? row.Field<string>("AI_PUR_DATE") : null;
                    newRow.AI_WARRANTY = dt.Columns.Contains("AI_WARRANTY") ? row.Field<double>("AI_WARRANTY") : Convert.ToDouble(null);
                    newRow.AI_IMAGE = dt.Columns.Contains("AI_IMAGE") ? row.Field<Byte[]>("AI_IMAGE") : null;
                    newRow.LS_NAME = dt.Columns.Contains("LS_NAME") ? row.Field<string>("LS_NAME") : null;
                    newRow.LS_BIRTH_YEAR = dt.Columns.Contains("LS_BIRTH_YEAR") ? row.Field<string>("LS_BIRTH_YEAR") : null;
                    newRow.LS_INSURANCE = dt.Columns.Contains("LS_INSURANCE") ? row.Field<string>("LS_INSURANCE") : null;
                    newRow.LS_INSURANCE_ID = dt.Columns.Contains("LS_INSURANCE_ID") ? row.Field<string>("LS_INSURANCE_ID") : null;
                    newRow.LS_INS_POLICY_NO = dt.Columns.Contains("LS_INS_POLICY_NO") ? row.Field<string>("LS_INS_POLICY_NO") : null;
                    newRow.LS_INS_AMT = dt.Columns.Contains("LS_INS_AMT") ? row.Field<double>("LS_INS_AMT") : Convert.ToDouble(null);
                    newRow.LS_INS_DATE = dt.Columns.Contains("LS_INS_DATE") ? row.Field<string>("LS_INS_DATE") : null;
                    newRow.VI_MAKE = dt.Columns.Contains("VI_MAKE") ? row.Field<string>("VI_MAKE") : null;
                    newRow.VI_REG_NO_PATTERN = dt.Columns.Contains("VI_REG_NO_PATTERN") ? row.Field<string>("VI_REG_NO_PATTERN") : null;
                    newRow.VI_REG_NO = dt.Columns.Contains("VI_REG_NO") ? row.Field<string>("VI_REG_NO") : null;
                    newRow.VI_REG_DATE = dt.Columns.Contains("VI_REG_DATE") ? row.Field<string>("VI_REG_DATE") : null;
                    newRow.VI_OWNERSHIP = dt.Columns.Contains("VI_OWNERSHIP") ? row.Field<string>("VI_OWNERSHIP") : null;
                    newRow.VI_OWNERSHIP_AB_ID = dt.Columns.Contains("VI_OWNERSHIP_AB_ID") ? row.Field<string>("VI_OWNERSHIP_AB_ID") : null;
                    newRow.VI_DOC_RC_BOOK = dt.Columns.Contains("VI_DOC_RC_BOOK") ? row.Field<string>("VI_DOC_RC_BOOK") : null;
                    newRow.VI_DOC_AFFIDAVIT = dt.Columns.Contains("VI_DOC_AFFIDAVIT") ? row.Field<string>("VI_DOC_AFFIDAVIT") : null;
                    newRow.VI_DOC_WILL = dt.Columns.Contains("VI_DOC_WILL") ? row.Field<string>("VI_DOC_WILL") : null;
                    newRow.VI_DOC_TRF_LETTER = dt.Columns.Contains("VI_DOC_TRF_LETTER") ? row.Field<string>("VI_DOC_TRF_LETTER") : null;
                    newRow.VI_DOC_FU_LETTER = dt.Columns.Contains("VI_DOC_FU_LETTER") ? row.Field<string>("VI_DOC_FU_LETTER") : null;
                    newRow.VI_DOC_OTHERS = dt.Columns.Contains("VI_DOC_OTHERS") ? row.Field<string>("VI_DOC_OTHERS") : null;
                    newRow.VI_DOC_NAME = dt.Columns.Contains("VI_DOC_NAME") ? row.Field<string>("VI_DOC_NAME") : null;
                    newRow.VI_INSURANCE_ID = dt.Columns.Contains("VI_INSURANCE_ID") ? row.Field<string>("VI_INSURANCE_ID") : null;
                    newRow.VI_INS_POLICY_NO = dt.Columns.Contains("VI_INS_POLICY_NO") ? row.Field<string>("VI_INS_POLICY_NO") : null;
                    newRow.VI_INS_EXPIRY_DATE = dt.Columns.Contains("VI_INS_EXPIRY_DATE") ? row.Field<string>("VI_INS_EXPIRY_DATE") : null;
                    newRow.LB_PRO_TYPE = dt.Columns.Contains("LB_PRO_TYPE") ? row.Field<string>("LB_PRO_TYPE") : null;
                    newRow.LB_PRO_CATEGORY = dt.Columns.Contains("LB_PRO_CATEGORY") ? row.Field<string>("LB_PRO_CATEGORY") : null;
                    newRow.LB_PRO_USE = dt.Columns.Contains("LB_PRO_USE") ? row.Field<string>("LB_PRO_USE") : null;
                    newRow.LB_PRO_NAME = dt.Columns.Contains("LB_PRO_NAME") ? row.Field<string>("LB_PRO_NAME") : null;
                    newRow.LB_PRO_ADDRESS = dt.Columns.Contains("LB_PRO_ADDRESS") ? row.Field<string>("LB_PRO_ADDRESS") : null;
                    newRow.LB_OWNERSHIP = dt.Columns.Contains("LB_OWNERSHIP") ? row.Field<string>("LB_OWNERSHIP") : null;
                    newRow.LB_OWNERSHIP_PARTY_ID = dt.Columns.Contains("LB_OWNERSHIP_PARTY_ID") ? row.Field<string>("LB_OWNERSHIP_PARTY_ID") : null;
                    newRow.LB_SURVEY_NO = dt.Columns.Contains("LB_SURVEY_NO") ? row.Field<string>("LB_SURVEY_NO") : null;
                    newRow.LB_TOT_P_AREA = dt.Columns.Contains("LB_TOT_P_AREA") ? row.Field<double>("LB_TOT_P_AREA") : Convert.ToDouble(null);
                    newRow.LB_CON_AREA = dt.Columns.Contains("LB_CON_AREA") ? row.Field<double>("LB_CON_AREA") : Convert.ToDouble(null);
                    newRow.LB_CON_YEAR = dt.Columns.Contains("LB_CON_YEAR") ? row.Field<string>("LB_CON_YEAR") : null;
                    newRow.LB_RCC_ROOF = dt.Columns.Contains("LB_RCC_ROOF") ? row.Field<string>("LB_RCC_ROOF") : null;
                    newRow.LB_DEPOSIT_AMT = dt.Columns.Contains("LB_DEPOSIT_AMT") ? row.Field<double>("LB_DEPOSIT_AMT") : Convert.ToDouble(null);
                    newRow.LB_PAID_DATE = dt.Columns.Contains("LB_PAID_DATE") ? row.Field<string>("LB_PAID_DATE") : null;
                    newRow.LB_MONTH_RENT = dt.Columns.Contains("LB_MONTH_RENT") ? row.Field<double>("LB_MONTH_RENT") : Convert.ToDouble(null);
                    newRow.LB_MONTH_O_PAYMENTS = dt.Columns.Contains("LB_MONTH_O_PAYMENTS") ? row.Field<double>("LB_MONTH_O_PAYMENTS") : Convert.ToDouble(null);
                    newRow.LB_PERIOD_FROM = dt.Columns.Contains("LB_PERIOD_FROM") ? row.Field<string>("LB_PERIOD_FROM") : null;
                    newRow.LB_PERIOD_TO = dt.Columns.Contains("LB_PERIOD_TO") ? row.Field<string>("LB_PERIOD_TO") : null;
                    newRow.LB_DOC_OTHERS = dt.Columns.Contains("LB_DOC_OTHERS") ? row.Field<string>("LB_DOC_OTHERS") : null;
                    newRow.LB_DOC_NAME = dt.Columns.Contains("LB_DOC_NAME") ? row.Field<string>("LB_DOC_NAME") : null;
                    newRow.LB_OTHER_DETAIL = dt.Columns.Contains("LB_OTHER_DETAIL") ? row.Field<string>("LB_OTHER_DETAIL") : null;
                    newRow.LB_REC_ID = dt.Columns.Contains("LB_REC_ID") ? row.Field<string>("LB_REC_ID") : null;
                    newRow.LB_ADDRESS1 = dt.Columns.Contains("LB_ADDRESS1") ? row.Field<string>("LB_ADDRESS1") : null;
                    newRow.LB_ADDRESS2 = dt.Columns.Contains("LB_ADDRESS2") ? row.Field<string>("LB_ADDRESS2") : null;
                    newRow.LB_ADDRESS3 = dt.Columns.Contains("LB_ADDRESS3") ? row.Field<string>("LB_ADDRESS3") : null;
                    newRow.LB_ADDRESS4 = dt.Columns.Contains("LB_ADDRESS4") ? row.Field<string>("LB_ADDRESS4") : null;
                    newRow.LB_STATE_ID = dt.Columns.Contains("LB_STATE_ID") ? row.Field<string>("LB_STATE_ID") : null;
                    newRow.LB_DISTRICT_ID = dt.Columns.Contains("LB_DISTRICT_ID") ? row.Field<string>("LB_DISTRICT_ID") : null;
                    newRow.LB_CITY_ID = dt.Columns.Contains("LB_CITY_ID") ? row.Field<string>("LB_CITY_ID") : null;
                    newRow.LB_PINCODE = dt.Columns.Contains("LB_PINCODE") ? row.Field<string>("LB_PINCODE") : null;
                    newRow.REF_REC_ID = dt.Columns.Contains("REF_REC_ID") ? row.Field<string>("REF_REC_ID") : null;
                    newRow.REFERENCE = dt.Columns.Contains("REFERENCE") ? row.Field<string>("REFERENCE") : null;
                    newRow.WIP_REF_TYPE = dt.Columns.Contains("WIP_REF_TYPE") ? row.Field<string>("WIP_REF_TYPE") : null;

                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<PartyList1_DNK> DataTableto_GetPartyList_DNK(DataTable dt)
        {
            List<PartyList1_DNK> newList = new List<PartyList1_DNK>();
            if (dt!=null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    PartyList1_DNK newRow = new PartyList1_DNK();
                    newRow.C_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("C_NAME")) ? row.Field<string>("C_NAME") : null;
                    newRow.CI_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CI_NAME")) ? row.Field<string>("CI_NAME") : null;
                    newRow.C_PAN_NO = !string.IsNullOrWhiteSpace(row.Field<string>("C_PAN_NO")) ? row.Field<string>("C_PAN_NO") : null;
                    newRow.C_PASSPORT_NO = !string.IsNullOrWhiteSpace(row.Field<string>("C_PASSPORT_NO")) ? row.Field<string>("C_PASSPORT_NO") : null;
                    newRow.C_R_ADD1 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD1")) ? row.Field<string>("C_R_ADD1") : null;
                    newRow.C_R_ADD2 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD2")) ? row.Field<string>("C_R_ADD2") : null;
                    newRow.C_R_ADD3 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD3")) ? row.Field<string>("C_R_ADD3") : null;
                    newRow.C_R_ADD4 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD4")) ? row.Field<string>("C_R_ADD4") : null;
                    newRow.C_R_PINCODE = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_PINCODE")) ? row.Field<string>("C_R_PINCODE") : null;
                    newRow.ST_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("ST_NAME")) ? row.Field<string>("ST_NAME") : null;
                    newRow.DI_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("DI_NAME")) ? row.Field<string>("DI_NAME") : null;
                    newRow.CO_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CO_NAME")) ? row.Field<string>("CO_NAME") : null;
                    newRow.C_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_ID")) ? row.Field<string>("C_ID") : null;
                    newRow.C_OTHER_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_OTHER_ID")) ? row.Field<string>("C_OTHER_ID") : null;
                    newRow.C_OTHER_ID_LABEL = !string.IsNullOrWhiteSpace(row.Field<string>("C_OTHER_ID_LABEL")) ? row.Field<string>("C_OTHER_ID_LABEL") : null;
                    newRow.C_UID_NO = !string.IsNullOrWhiteSpace(row.Field<string>("C_UID_NO")) ? row.Field<string>("C_UID_NO") : null;
                    newRow.C_CATEGORY = !string.IsNullOrWhiteSpace(row.Field<string>("C_CATEGORY")) ? row.Field<string>("C_CATEGORY") : null;

                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<WIP_Final_Select_Asset_Grid> DataTableto_WIP_Final_Select_Asset_Grid(DataTable dt)
        {
            DateTime? dtnull = null;
            List<WIP_Final_Select_Asset_Grid> newList = new List<WIP_Final_Select_Asset_Grid>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    WIP_Final_Select_Asset_Grid newRow = new WIP_Final_Select_Asset_Grid();
                    newRow.Item = !string.IsNullOrWhiteSpace(row.Field<string>("Item")) ? row.Field<string>("Item") : null;
                    newRow.Make = !string.IsNullOrWhiteSpace(row.Field<string>("Make")) ? row.Field<string>("Make") : null;
                    newRow.Model = !string.IsNullOrWhiteSpace(row.Field<string>("Model")) ? row.Field<string>("Model") : null;
                    newRow.Org_Qty = !string.IsNullOrWhiteSpace(row.Field<decimal>("Org Qty").ToString()) ? row.Field<decimal>("Org Qty") :(decimal) 0;
                    newRow.Curr_Qty = !string.IsNullOrWhiteSpace(row.Field<decimal>("Curr Qty").ToString()) ? row.Field<decimal>("Curr Qty") : (decimal)0;
                    newRow.Org_Value = !string.IsNullOrWhiteSpace(row.Field<decimal>("Org Value").ToString()) ? row.Field<decimal>("Org Value") : (decimal)0;
                    newRow.Curr_Value = !string.IsNullOrWhiteSpace(row.Field<decimal>("Curr Value").ToString()) ? row.Field<decimal>("Curr Value") : (decimal)0;
                    newRow.REC_ID = !string.IsNullOrWhiteSpace(row.Field<string>("REC_ID")) ? row.Field<string>("REC_ID") : (string)null;
                    newRow.REC_EDIT_ON = !string.IsNullOrWhiteSpace(row.Field<DateTime>("REC_EDIT_ON").ToString()) ? row.Field<DateTime>("REC_EDIT_ON") : (DateTime?)dtnull;
                    newRow.AI_TR_ID = !string.IsNullOrWhiteSpace(row.Field<string>("AI_TR_ID")) ? row.Field<string>("AI_TR_ID") : (string)null;
                    newList.Add(newRow);
                }                
            }
            return newList;
        }
        public static List<CategoryList> DataTableto_GetCategoryList(DataTable dt) 
        {
            List<CategoryList> newList = new List<CategoryList>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows) 
                {
                    CategoryList newRow = new CategoryList();
                    newRow.CAT_ID = !string.IsNullOrWhiteSpace(row.Field<string>("CAT_ID").ToString()) ? row.Field<string>("CAT_ID") : null;
                    newRow.CAT_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CAT_NAME").ToString()) ? row.Field<string>("CAT_NAME") : null;
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<CurrencyList> DataTableto_GetCurList(DataTable dt) 
        {
            List<CurrencyList> newList = new List<CurrencyList>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CurrencyList newRow = new CurrencyList();
                    newRow.CUR_ID = !string.IsNullOrWhiteSpace(row.Field<string>("CUR_ID").ToString()) ? row.Field<string>("CUR_ID") : null;
                    newRow.CUR_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CUR_NAME").ToString()) ? row.Field<string>("CUR_NAME") : null;
                    newRow.CUR_CODE = !string.IsNullOrWhiteSpace(row.Field<string>("CUR_CODE").ToString()) ? row.Field<string>("CUR_CODE") : null;
                    newRow.CUR_SYMBOL = !string.IsNullOrWhiteSpace(row.Field<string>("CUR_SYMBOL").ToString()) ? row.Field<string>("CUR_SYMBOL") : null;
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<PurList_Itm_Dtel> DataTableto_PurList_Itm_Dtel(DataTable dt)
        {
            //List < PurList_Itm_Dtel >
            if (dt != null) return dt.AsEnumerable().Select(row => new PurList_Itm_Dtel
            {
                PUR_ID = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME"),
            }).ToList();
            else return null;
        }
        public static List<PurList_JV_Itm> DataTableto_PurList_Itm_Jv(DataTable dt)
        {
            //List < PurList_Itm_Dtel >
            if (dt != null) return dt.AsEnumerable().Select(row => new PurList_JV_Itm
            {
                PUR_ID = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME"),
            }).ToList();
            else return null;
        }
        public static List<RefList_JV_Ref> DataTableto_RefList_Jv_Ref(DataTable dt)
        {
            int sr = 1;
            List<RefList_JV_Ref> newList = new List<RefList_JV_Ref>();
            if (dt != null)
            {
                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row = dt.Rows[i];
                    RefList_JV_Ref newRow = new RefList_JV_Ref();
                    newRow.Type = dt.Columns.Contains("Type") ? row.Field<string>("Type") : null;
                    newRow.Category = dt.Columns.Contains("Category") ? row.Field<string>("Category") : null;
                    newRow.Use = dt.Columns.Contains("Use") ? row.Field<string>("Use") : null;
                    newRow.Item = dt.Columns.Contains("Item") ? row.Field<string>("Item") : null;
                    newRow.OWNER = dt.Columns.Contains("OWNER") ? row.Field<string>("OWNER") : null;
                    newRow.Org_Value = dt.Columns.Contains("Org Value") ? row.Field<decimal>("Org Value") : Convert.ToDecimal(null);
                    newRow.Curr_Value = dt.Columns.Contains("Curr Value") ? row.Field<decimal>("Curr Value") : Convert.ToDecimal(null);
                    newRow.REC_ID = dt.Columns.Contains("REC_ID") ? row.Field<string>("REC_ID") : null;
                    newRow.REC_EDIT_ON = dt.Columns.Contains("REC_EDIT_ON") ? row.Field<DateTime>("REC_EDIT_ON") : Convert.ToDateTime(null);
                    newRow.REF_CREATION_DATE = dt.Columns.Contains("REF_CREATION_DATE") ? row.Field<DateTime?>("REF_CREATION_DATE") : Convert.ToDateTime(null);

                    newRow.Make = dt.Columns.Contains("Make") ? row.Field<string>("Make") : null;
                    newRow.Model = dt.Columns.Contains("Model") ? row.Field<string>("Model") : null;
                    newRow.Org_Qty = dt.Columns.Contains("Org Qty") ? row.Field<decimal>("Org Qty") : Convert.ToDecimal(null);
                    newRow.Curr_Qty = dt.Columns.Contains("Curr Qty") ? row.Field<decimal>("Curr Qty") : Convert.ToDecimal(null);

                    newRow.Head = dt.Columns.Contains("Head") ? row.Field<string>("Head") : null;
                    newRow.Head_Type = dt.Columns.Contains("Head Type") ? row.Field<string>("Head Type") : null;

                    newRow.Ledger = dt.Columns.Contains("Ledger") ? row.Field<string>("Ledger") : null;
                    newRow.Reference = dt.Columns.Contains("Reference") ? row.Field<string>("Reference") : null;
                    newRow.Next_Year_Closing_Value = dt.Columns.Contains("Next Year Closing Value") ? row.Field<decimal>("Next Year Closing Value") : Convert.ToDecimal(null);

                    newRow.Party = dt.Columns.Contains("Party") ? row.Field<string>("Party") : null;
                    newRow.Date = dt.Columns.Contains("Date") ? Convert.ToDateTime(row.Field<String>("Date")) : Convert.ToDateTime(null);
                    newRow.Due = dt.Columns.Contains("Due") ? row.Field<string>("Due") : null;
                    newRow.Reason = dt.Columns.Contains("Reason") ? row.Field<string>("Reason") : null;
                    newRow.Detail = dt.Columns.Contains("Detail") ? row.Field<string>("Detail") : null;

                    newRow.Vehicle = dt.Columns.Contains("Vehicle") ? row.Field<string>("Vehicle") : null;
                    newRow.Reg_No = dt.Columns.Contains("Reg No") ? row.Field<string>("Reg No") : null;

                    newRow.Period = dt.Columns.Contains("Period") ? row.Field<decimal>("Period") : Convert.ToDecimal(null);

                    newRow.Name = dt.Columns.Contains("Name") ? row.Field<string>("Name") : null;
                    newRow.BIRTH_YEAR = dt.Columns.Contains("BIRTH YEAR") ? row.Field<string>("BIRTH YEAR") : null;

                    newRow.Org_Weight = dt.Columns.Contains("Org Weight") ? row.Field<decimal>("Org Weight") : Convert.ToDecimal(null);
                    newRow.Curr_Weight = dt.Columns.Contains("Curr Weight") ? row.Field<decimal>("Curr Weight") : Convert.ToDecimal(null);
                    newRow.DESC = dt.Columns.Contains("DESC") ? row.Field<string>("DESC") : null;
                    newRow.DETAILS = dt.Columns.Contains("DETAILS") ? row.Field<string>("DETAILS") : null;

                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<WIP_Txn_Report_Grid> DataTableto_WIP_Txn_Report_Grid(DataTable dt) 
        {
            List<WIP_Txn_Report_Grid> newList = new List<WIP_Txn_Report_Grid>();
            if (dt != null) 
            {
                foreach (DataRow row in dt.Rows) 
                {
                    WIP_Txn_Report_Grid newRow = new WIP_Txn_Report_Grid();
                    newRow.Voucher = dt.Columns.Contains("Voucher") ? row.Field<string>("Voucher") : null;
                    newRow.Date = dt.Columns.Contains("Date") ? row.Field<DateTime?>("Date") : (DateTime?)null;
                    newRow.ItemName = dt.Columns.Contains("Item Name") ? row.Field<string>("Item Name") : null;
                    newRow.Party = dt.Columns.Contains("Party") ? row.Field<string>("Party") : null;
                    newRow.Debit = dt.Columns.Contains("Debit") ? row.Field<decimal?>("Debit") : (decimal?)null;
                    newRow.Credit = dt.Columns.Contains("Credit") ? row.Field<decimal?>("Credit") : (decimal?)null;
                    newRow.Balance = dt.Columns.Contains("Balance") ? row.Field<string>("Balance") : null;
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<Lookup_Cen_List_AsetTrans> DataTableto_Cen_List_Pending(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Lookup_Cen_List_AsetTrans
            {
                TO_CEN_ID = row.Field<int?>("TO_CEN_ID"),
                TO_ID = row.Field<string>("TO_ID"),
                TO_CEN_NAME = row.Field<string>("TO_CEN_NAME"),
                TO_INCHARGE = row.Field<string>("TO_INCHARGE"),
                TO_PAD_NO = row.Field<string>("TO_PAD_NO"),
                TO_TEL_NO = row.Field<string>("TO_TEL_NO"),
                TO_UID = row.Field<string>("TO_UID"),
                TO_ZONE = row.Field<string>("TO_ZONE"),
            }).ToList();
            else return null;
        }

        public static List<ResponsePerson> DataTabletoLookUp_GetResponsePersonsList_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ResponsePerson
            {
                Name = row.Field<string>("Name"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                ID = row.Field<string>("ID"),
            }).ToList();
            else return null;
        }
        public static List<ProperyDDValues> DataTableto_ProperyData(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ProperyDDValues
            {
                Institute = row.Field<string>("Institute"),
                Type= row.Field<string>("TYPE"),
                PropName = row.Field<string>("PROP_NAME"),
                Category= row.Field<string>("CATEGORY"),
                OwnerShip= row.Field<string>("OWNERSHIP"),
                UseOf= row.Field<string>("USE OF PROPERTY"),
                Property_ID = row.Field<string>("LB_ID")

            }).ToList();
            else return null;
        }
        public static List<ServiceDDValues> DataTableToServiceNameData(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ServiceDDValues
            {
                Institute = row.Field<string>("Institute"),
                CenterNo = row.Field<string>("Centre No."),
                UID = row.Field<string>("UID"),
                No = row.Field<string>("No."),
                ServicePlaceName = row.Field<string>("Service Place Name"),
                PlaceType = row.Field<string>("Place Type"),
                ServicePlace_ID = row.Field<string>("SP_ID"),
            }).ToList();
            else return null;
        }
        public static List<BANK_INFO> DataTabletoBANK_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new BANK_INFO
            {
                REC_ID = row.Field<string>("REC_ID"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_BANK_PAN_NO = row.Field<string>("BI_BANK_PAN_NO")
            }).ToList();
            else return null;
        }

        public static List<OpeningBalancesinfo> DataTabletoOB_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new OpeningBalancesinfo
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
                Head = row.Field<string>("Head"),
                HeadType= row.Field<string>("Head Type")
            }).ToList();
            else return null;
        }

        public static List<GLoopUpMagaList> DataTabletoMag_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new GLoopUpMagaList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
                Language = row.Field<string>("Language"),
                Mag_Short_Name=row.Field<string>("Short Name"),
                Publish_On = row.Field<string>("Publish On")
            }).ToList();
            else return null;
        }
        public static List<PCMember_List> DataTabletoPCMember_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new PCMember_List
            {
                Magazine = row.Field<string>("Magazine"),
                MEM_NAME = row.Field<string>("MEM_NAME"),
                MEM_CEN_NAME = row.Field<string>("MEM_CEN_NAME"),
                MM_CITY = row.Field<string>("MEM_CITY"),
                MEMBER_ID = row.Field<string>("MEMBER ID"),
                MEM_ADDRESS = row.Field<string>("MEM_ADDRESS"),
                MEM_COUNTRY = row.Field<string>("MEM_COUNTRY"),
                MEM_CEN_ID = row.Field<int?>("MEM_CEN_ID").ToString(),
                MEM_CEN_UID = row.Field<string>("MEM_CEN_UID"),
                MEM_CEN_INCHARGE = row.Field<string>("MEM_CEN_INCHARGE"),
                MEM_ID = row.Field<string>("MEM_ID"),
                MAG_ID = row.Field<string>("MAG_ID"),
                MM_MEMBER_TYPE = row.Field<string>("MM_MEMBER_TYPE"),
                CC_MEMBER_TYPE = row.Field<string>("CC_MEMBER_TYPE"),
                MEM_ADD_1 = row.Field<string>("MEM_ADD_1"),
                MEM_ADD_2 = row.Field<string>("MEM_ADD_2"),
                MEM_ADD_3 = row.Field<string>("MEM_ADD_3"),
                MEM_ADD_4 = row.Field<string>("MEM_ADD_4"),
                MEM_ADD_5 = row.Field<string>("MEM_ADD_5"),
                MEM_ADD_6 = row.Field<string>("MEM_ADD_6"),
                MEM_PINCODE = row.Field<string>("MEM_PINCODE"),
                MM_STATUS = row.Field<string>("MM_STATUS"),
                MM_MS_OLD_ID = row.Field<string>("MM_MS_OLD_ID"),
                MM_MS_ID = row.Field<string>("MM_MS_ID"),
                MM_OTHER_DETAIL=row.Field<string>("MM_OTHER_DETAIL"),
                MM_REC_ID= row.Field<string>("MM_REC_ID"),
                MM_MS_START_DATE= row.Field<DateTime?>("MM_MS_START_DATE").ToString(),
                MM_AUTO_RENEWAL = row.Field<string>("MM_AUTO_RENEWAL"),
                MM_CC_APPLICABLE = row.Field<string>("MM_CC_APPLICABLE"),
                MM_CC_SPONSORED = row.Field<int?>("MM_CC_SPONSORED").ToString(),
                MM_CC_MS_ID = row.Field<string>("MM_CC_MS_ID"),
                // CC_MEMBER_TYPE = row.Field<string>("CC_MEMBER_TYPE")

            }).ToList();
            else return null;
        }

        public static List<GLookUp_CC_Member_List> DataTabletoCCMember_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new GLookUp_CC_Member_List
            {
                ID = row.Field<string>("ID"),
                MEM_ID = row.Field<string>("MEM ID"),
                OLD_ID = row.Field<string>("OLD ID"),
                Joint_ID = row.Field<string>("joint ID")
            }).ToList();
            else return null;
        }

        public static List<GLookUp_RefBankList> DataTabletMagBank_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new GLookUp_RefBankList
            {
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_ID = row.Field<string>("BI_ID")

            }).ToList();
            else return null;
        }

        public static List<IssueList> LookUp_GetMagIssues(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new IssueList
            {
                ID = row.Field<string>("ID"),
                Issue = row.Field<DateTime>("Issue").ToString("MM/dd/yyyy"),
            }).ToList();
            else return null;
        }

        public static List<Subscriptiondetails_Model> GLookUp_STypeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscriptiondetails_Model
            {
                GLookUp_STypeList = row.Field<string>("ID"),
                SubscriptionType = row.Field<string>("Type"),
            }).ToList();
            else return null;
        }

        public static List<Subscriptiondetails_Model> LookUp_GetDispatchList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscriptiondetails_Model
            {
                GLookUp_DTypeList = row.Field<string>("ID"),
                DispatchName = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }

        public static List<StoreInfo> LookUp_StoreList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new StoreInfo
            {
                StoreId =Convert.ToInt32(row.Field<string>("StoreId")),
                StoreName = row.Field<string>("StoreName"),
                DeptName = row.Field<string>("DeptName"),
                SubDeptName = row.Field<string>("SubDeptName"),
                DeptInchargeName = row.Field<string>("DeptInchargeName"),
                StoreInchargeName = row.Field<string>("StoreInchargeName"),

            }).ToList();
            else return null;
        }
        public static List<StoreListInfo> LookUp_GetStoreList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new StoreListInfo
            {
                Store_Id = Convert.ToInt32(row.Field<string>("Store_Id")),
                Store_Name = row.Field<string>("Store_Name"),
                Dept_Name = row.Field<string>("Dept_Name"),
                SubDept_Name = row.Field<string>("SubDept_Name"),
                DeptInchargeName = row.Field<string>("DeptInchargeName"),
                StoreInchargeName = row.Field<string>("StoreInchargeName"),

            }).ToList();
            else return null;
        }
        public static List<ItemInfo> LookUp_GetStockItemsList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ItemInfo
            {
                ItemId = Convert.ToInt32(row.Field<string>("ItemId")),
                ItemName = row.Field<string>("ItemName"),
                ItemCategory = row.Field<string>("ItemCategory"),
                ItemType = row.Field<string>("ItemType"),
                ItemCode = row.Field<string>("ItemCode"),
                Unit = row.Field<string>("Unit"),
                UnitID = row.Field<string>("UnitID"),
            }).ToList();
            else return null;
        }
   

  
  
  
 
   
        public static List<LocationInfo> LookUp_GetLocations(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new LocationInfo
            {
                LocId = row.Field<string>("LocId"),
                LocationName = row.Field<string>("LocationName"),
                MatchedName = row.Field<string>("MatchedName"),
                MatchedType = row.Field<string>("MatchedType"),
                MatchedInstt = row.Field<string>("MatchedInstt"),               
            }).ToList();
            else return null;
        }

        public static List<ProjectInfo> LookUp_GetProjects(DataTable dt)
        {
            if (dt.Rows.Count != 0) return dt.AsEnumerable().Select(row => new ProjectInfo
            {
                ProjectId = Convert.ToInt32(row.Field<string>("ProjectId")),
                ProjectName = row.Field<string>("ProjectName"),
                Sanctionno = row.Field<string>("Sanctionno"),
                ComplexName = row.Field<string>("ComplexName")
            }).ToList();
            else return null;
        }

        public static List<Subscriptiondetails_Model> LookUp_GetPurposeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscriptiondetails_Model
            {
                GLookUp_PurList = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME"),
            }).ToList();
            else return null;
        }

        public static List<MagazineList> DataTabletoLang_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new MagazineList
            {
                LangID = row.Field<string>("MISC_ID"),
                LangName = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }

        public static List<MagazineList> DataTabletoPublishOn_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new MagazineList
            {
                PublishID = row.Field<string>("MISC_ID"),
                PublishName = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }

        public static List<TelePhonebillinfo> DataTabletoTelePhoneInfo_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new TelePhonebillinfo
            {
                ID = row.Field<string>("TP_ID"),
                TelePhoneNo = row.Field<string>("TP_NO"),
                Company = row.Field<string>("TP_COMPANY"),
                Category = row.Field<string>("TP_CATEGORY"),
                PlanType = row.Field<string>("TP_TYPE")
            }).ToList();
            else return null;
        }

        public static List<ReferenceType> DataTabletoReference_Type(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ReferenceType
            {
                SNo = row.Field<long>("SNo."),
                WIP_Ledger = row.Field<string>("WIP Ledger"),
                Next_Year_Closing_Value = row.Field<decimal>("Next Year Closing Value"),
                Date_of_Creation = row.Field<DateTime>("Date of Creation"),
                TR_ID = row.Field<string>("TR ID"),
                Entry_Type = row.Field<string>("Entry Type"),
                Add_By = row.Field<string>("Add By"),
                Add_Date = row.Field<DateTime>("Add Date"),
                Edit_By = row.Field<string>("Edit By"),
                Edit_Date = row.Field<DateTime>("Edit Date"),
                Action_Status = row.Field<string>("Action Status"),
                Action_By = row.Field<string>("Action By"),
                Action_Date = row.Field<DateTime>("Action Date")
            }).ToList();
            else return null;
        }

        public static List<Telecom_INFO> DataTabletoTelecom_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Telecom_INFO
            {
                MISC_ID = row.Field<string>("MISC_ID"),
                MISC_NAME = row.Field<string>("MISC_NAME"),
            }).ToList();
            else return null;
        }
        public static List<Item_INFO> DataTabletoPropertyType(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Item_INFO
            {
                NAME = row.Field<string>("NAME"),
                ID = row.Field<string>("ID"),
            }).ToList();
            else return null;
        }
        public static List<VehiclesInfo> DataTabletoVehicles_INFO1(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new VehiclesInfo
            {
                ITEM_NAME = row.Field<string>("ITEM_NAME"),
                VI_ITEM_ID = row.Field<string>("VI_ITEM_ID"),
                MAKE = row.Field<string>("MAKE"),
                Model = row.Field<string>("Model"),
                VI_REG_NO = row.Field<string>("VI_REG_NO"),
                //Date_of_First = row.Field<DateTime>("Date of First"),
                Opening_Value = row.Field<decimal>("Opening Value"),
                Curr_Value = row.Field<decimal>("Curr Value"),
                Ownership = row.Field<string>("Ownership"),
                VI_DOC_RC_BOOK = row.Field<string>("VI_DOC_RC_BOOK"),
                Affidavit = row.Field<string>("Affidavit"),
                Will = row.Field<string>("Will"),
                Transfer_Lettter = row.Field<string>("Transfer Letter"),
                Free_Use_Letter = row.Field<string>("Free Use Letter"),
                Other_Documents = row.Field<string>("Other Documents"),
                //Insurance = row.Field<string>("Insurance"),
                INSURANCE_ID = row.Field<string>("INSURANCE_ID"),
                VI_INS_POLICY_NO = row.Field<string>("VI_INS_POLICY_NO"),
                Expiry_Date = row.Field<DateTime>("Expiry Date"),
                VI_OTHER_DETAIL = row.Field<string>("VI_OTHER_DETAIL"),
                //AL_LOC_AL_ID = row.Field<string>("AL_LOC_AL_ID"),
                YEAR_ID = row.Field<string>("YearID"),
                TR_ID = row.Field<string>("TR_ID"),
                ID = row.Field<string>("ID"),
                Sale_Status = row.Field<string>("Sale Status"),
                Entry_Type = row.Field<string>("Entry Type"),
                Remark_Count = row.Field<int>("RemarkCount"),
                Remark_Status = row.Field<string>("RemarkStatus"),
                Open_Actions = row.Field<int>("OpenActions"),
                Crossed_Time_Limit = row.Field<int>("CrossedTimeLimits"),
                Add_By = row.Field<string>("Add By"),
                Edit_By = row.Field<string>("Edit By"),
                Add_Date = row.Field<DateTime>("Add Date"),
                Edit_Date = row.Field<DateTime>("Edit Date"),
                Action_Status = row.Field<string>("Action Status"),
                Action_By = row.Field<string>("Action By"),
                Entry_Status = row.Field<string>("Entry Status"),

            }).ToList();
            else return null;
        }

        public static List<Item_INFO_Vehicle> DataTabletoItem_INFO_Vehicle(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Item_INFO_Vehicle
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<Item_INFO_GS> DataTabletoGSItem(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Item_INFO_GS
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<ASSET_LOCATION_INFO> DataTabletoGSLocation(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ASSET_LOCATION_INFO
            {
                AL_ID = row.Field<string>("AL_ID"),
                Location_Name = row.Field<string>("Location_Name"),
            }).ToList();
            else return null;
        }
        public static List<Item_INFO> DataTabletoItem_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Item_INFO
            {
                ID = row.Field<string>("ID"),
                NAME = row.Field<string>("NAME"),
            }).ToList();
            else return null;
        }

        public static List<TELEPHONEINFO> DataTabletoTELEPHONEINFO(DataTable dt)
        {

            if (dt != null) return dt.AsEnumerable().Select(row => new TELEPHONEINFO
            {
                TP_TELECOM_MISC_ID = row.Field<string>("TP_TELECOM_MISC_ID"),
                REC_ADD_BY = row.Field<string>("REC_ADD_BY"),
                REC_ADD_ON = row.Field<DateTime?>("REC_ADD_ON"),
                REC_EDIT_BY = row.Field<string>("REC_EDIT_BY"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
                REC_ID = row.Field<string>("REC_ID"),
                REC_STATUS = row.Field<int>("REC_STATUS"),
                REC_STATUS_BY = row.Field<string>("REC_STATUS_BY"),
                REC_STATUS_ON = row.Field<DateTime?>("REC_STATUS_ON"),
                TP_CATEGORY = row.Field<string>("TP_CATEGORY"),
                TP_CEN_ID = row.Field<int>("TP_CEN_ID"),
                TP_CLOSE_DATE = row.Field<DateTime?>("TP_CLOSE_DATE"),
                TP_CLOSE_REMARKS = row.Field<string>("TP_CLOSE_REMARKS"),
                TP_NO = row.Field<string>("TP_NO"),
                TP_OTHER_DETAIL = row.Field<string>("TP_OTHER_DETAIL"),
                TP_TYPE = row.Field<string>("TP_TYPE"),
            }).ToList();
            else return null;
        }
        public static List<LOCATIONINFO> DataTabletoLOCATIONINFO(DataTable dt)
        {

            if (dt != null) return dt.AsEnumerable().Select(row => new LOCATIONINFO
            {
                LocationName=row.Field<string>("AL_LOC_NAME"),
                otherDetails= row.Field<string>("AL_OTHER_DETAIL"),
                AC_or_NonAC = row.Field<string>("AL_AC_OR_NON_AC"),
                Category = row.Field<string>("AL_CATEGORY"),
                roomfloor = row.Field<string>("AL_ROOM_FLOOR"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                max_Capacity = Convert.IsDBNull(row.Field<Int32?>("AL_MAXCAPACITY"))==false? Convert.ToInt32(row.Field<Int32?>("AL_MAXCAPACITY")):0
            }).ToList();
            else return null;
        }
        public static List<AdvancesParam> DataTabletoAdvancesINFO(DataTable dt)
        {

            if (dt != null) return dt.AsEnumerable().Select(row => new AdvancesParam
            {
                ItemID = row.Field<string>("AI_ITEM_ID"),
                PartyID = row.Field<string>("AI_PARTY_ID"),
                RecID = row.Field<string>("REC_ID"),
                AdvanceDate = (row.Field<DateTime?>("AI_ADV_DATE")).ToString(),
                Amount = Convert.ToDecimal(row.Field<Decimal>("AI_ADV_AMT")),
                Purpose = row.Field<string>("AI_PURPOSE"),
                Remarks = row.Field<string>("AI_OTHER_DETAIL"),
            }).ToList();
            else return null;
        }

        public static List<BANK_BRANCH_INFO> DataTabletoBANK_BRANCH_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new BANK_BRANCH_INFO
            {
                BB_BRANCH_NAME = row.Field<string>("BB_BRANCH_NAME"),
                BB_ID = row.Field<string>("BB_ID"),
                BB_IFSC_CODE = row.Field<string>("BB_IFSC_CODE"),
                BB_MICR_CODE = row.Field<string>("BB_MICR_CODE")
            }).ToList();
            else return null;
        }

        public static List<ADDRESS_BOOK> DataTabletoADDRESS_BOOK(DataTable dt)
        {
            if (dt != null)
                return dt.AsEnumerable().Select(row => new ADDRESS_BOOK
                {
                    ID = dt.Columns.Contains("ID") ? row.Field<string>("ID") : null,
                    Name = dt.Columns.Contains("Name") ? row.Field<string>("Name") : null,
                    Organization = dt.Columns.Contains("Organization") ? row.Field<string>("Organization") : null,
                    REC_EDIT_ON = dt.Columns.Contains("REC_EDIT_ON") ? row.Field<DateTime>("REC_EDIT_ON") : Convert.ToDateTime(null),
                    Status = dt.Columns.Contains("Status") ? row.Field<string>("Status") : null
        }).ToList();
            else return null;
        }
        public static List<InsMISC_INFO> DataTabletoInsMISC_INFO(DataTable dt)
        {
            if (dt != null)
                return dt.AsEnumerable().Select(row => new InsMISC_INFO
                {
                    ID = row.Field<string>("ID"),
                    Name = row.Field<string>("Name"),

                }).ToList();
            else return null;
        }
        public static List<ASSET_LOCATION_INFO> DataTabletoASSET_LOCATION_INFO(DataTable dt)
        {
            if (dt != null)
                return dt.AsEnumerable().Select(row => new ASSET_LOCATION_INFO
                {
                    AL_ID = row.Field<string>("AL_ID"),
                    Location_Name = row.Field<string>("Location Name"),
                    Matched_Type = row.Field<string>("Matched Type"),
                    Matched_Name = row.Field<string>("Matched Name"),
                    Matched_Instt = row.Field<string>("Matched Instt."),
                    Final_Amount = row.Field<decimal?>("Final_Amount"),
                    REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON")
                }).ToList();
            else
                return null;
        }
        public static List<MISC_INFO> DataTabletoMISC_INFO(DataTable dt)
        {
            if (dt != null)
                return dt.AsEnumerable().Select(row => new MISC_INFO
                {
                    Name = row.Field<string>("Name"),

                }).ToList();
            else return null;
        }

        public static List<BANK_ACCOUNT_INFO> DataTabletoBANK_ACCOUNT_INFO(DataTable dt)
        {
            if (dt != null)
            {

                return dt.AsEnumerable().Select(row => new BANK_ACCOUNT_INFO
                {
                    BA_BRANCH_ID = row.Field<string>("BA_BRANCH_ID"),
                    BA_ACCOUNT_NO = row.Field<string>("BA_ACCOUNT_NO"),
                    BA_ACCOUNT_NEW = row.Field<string>("BA_ACCOUNT_NEW"),
                    BA_ACCOUNT_TYPE = row.Field<string>("BA_ACCOUNT_TYPE"),
                    BA_CEN_ID = row.Field<int>("BA_CEN_ID"),
                    BA_CLOSE_DATE = row.Field<DateTime?>("BA_CLOSE_DATE"),
                    BA_COD_YEAR_ID = row.Field<int>("BA_COD_YEAR_ID"),
                    BA_CUST_NO = row.Field<string>("BA_CUST_NO"),
                    BA_FERA_ACC = row.Field<string>("BA_FERA_ACC"),
                    BA_EMAIL_ID = row.Field<string>("BA_EMAIL_ID"),
                    BA_OPEN_DATE = row.Field<DateTime?>("BA_OPEN_DATE"),
                    BA_OTHER_DETAIL = row.Field<string>("BA_OTHER_DETAIL"),
                    BA_SIGN_AB_ID_1 = row.Field<string>("BA_SIGN_AB_ID_1"),
                    BA_SIGN_AB_ID_2 = row.Field<string>("BA_SIGN_AB_ID_2"),
                    BA_SIGN_AB_ID_3 = row.Field<string>("BA_SIGN_AB_ID_3"),
                    BA_TAN_NO = row.Field<string>("BA_TAN_NO"),
                    BA_TEL_NOS = row.Field<string>("BA_TEL_NOS"),
                    REC_ADD_BY = row.Field<string>("REC_ADD_BY"),
                    REC_ADD_ON = row.Field<DateTime?>("REC_ADD_ON"),
                    REC_EDIT_BY = row.Field<string>("REC_EDIT_BY"),
                    REC_ID = row.Field<string>("REC_ID"),
                    REC_STATUS = row.Field<int>("REC_STATUS"),
                    REC_STATUS_BY = row.Field<string>("REC_STATUS_BY"),
                    REC_STATUS_ON = row.Field<DateTime?>("REC_STATUS_ON"),
                }).ToList();
            }
            else return null;
        }
        public static List<Organization_INFO> DataTabletoOrganization_INFO(DataTable dt)
        {
            List<Organization_INFO> organizationList = new List<Organization_INFO>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("C_ORG_NAME") != null)
                    {
                        var newdata = new Organization_INFO();
                        newdata.C_ORG_ID = row.Field<string>("C_ORG_NAME");
                        newdata.C_ORG_NAME = row.Field<string>("C_ORG_NAME");
                        organizationList.Add(newdata);
                    }

                }
            }
            return organizationList;
        }
        public static List<Title_INFO> DataTabletoTitle_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Title_INFO
            {
                C_TITLE_ID = row.Field<string>("ID"),
                C_TITLE_NAME = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }
        public static List<Party_Info> DataTabelToPartyList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Party_Info
            {
                Name = row.Field<string>("Name"),
                BUILDING = row.Field<string>("BUILDING"),
                HOUSE_NO = row.Field<string>("HOUSE NO"),
                AREA_STREET = row.Field<string>("AREA/STREET"),
                DISTRICT = row.Field<string>("DISTRICT"),
                MOBILE = row.Field<string>("MOBILE"),
                ID = row.Field<string>("ID"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
            }).ToList();
            else return null;
        }
        public static List<Country_INFO> DataTabletoCountry_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Country_INFO
            {
                R_CO_REC_ID = row.Field<string>("R_CO_REC_ID"),
                R_CO_NAME = row.Field<string>("R_CO_NAME"),
                R_CO_CODE = row.Field<string>("R_CO_CODE"),
            }).ToList();
            else return null;
        }

        public static List<State_INFO> DataTabletoState_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new State_INFO
            {
                R_ST_REC_ID = row.Field<string>("R_ST_REC_ID"),
                R_ST_NAME = row.Field<string>("R_ST_NAME"),
                R_ST_CODE = Convert.ToString(row.Field<int>("R_ST_CODE")),
            }).ToList();
            else return null;
        }
        public static List<District_INFO> DataTabletoDistrict_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new District_INFO
            {
                R_DI_REC_ID = row.Field<string>("R_DI_REC_ID"),
                R_DI_NAME = row.Field<string>("R_DI_NAME"),
                //R_DI_CODE = Convert.ToString(row.Field<int>("R_DI_CODE")),
            }).ToList();
            else return null;
        }
        public static List<CityList> DataTableToCityList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new CityList
            {
                CityName = row.Field<string>("City & Area"),
                DistrictName=row.Field<string>("District"),
                StateName=row.Field<string>("State"),
                PinCode=row.Field<string>("Pincode"),
                CountryName=row.Field<string>("Country")
            }).ToList();
            else return null;

        }
        public static List<City_INFO> DataTabletoCity_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new City_INFO
            {
                R_CI_REC_ID = row.Field<string>("R_CI_REC_ID"),
                R_CI_NAME = row.Field<string>("R_CI_NAME"),
            }).ToList();
            else return null;
        }
        public static List<Institute_Info> DataTabletoLookUp_GetInsList_LWF(DataTable dt) 
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Institute_Info
            {
                INS_ID = row.Field<string>("INS_ID"),
                INS_NAME = row.Field<string>("INS_NAME"),
                INS_SHORT = row.Field<string>("INS_SHORT"),
            }).ToList();
            else return null;
        }

        public static List<Magazine_Membership_Info> DataTable_to_Magazine_Membership_Info(DataTable dt)
        {
            if (dt != null)
            {

                return dt.AsEnumerable().Select(row => new Magazine_Membership_Info
                {
                    tag = row.Field<string>("tag"),
                    membershipID = row.Field<string>("membershipID"),
                    startDate = row.Field<DateTime>("startDate"),
                    memberName = row.Field<string>("memberName"),
                    addressLine1 = row.Field<string>("addressLine1"),
                    addressLine2 = row.Field<string>("addressLine2"),
                    addressLine3 = row.Field<string>("addressLine3"),
                    addressLine4 = row.Field<string>("addressLine4"),
                    pincode = row.Field<string>("pincode"),
                    city = row.Field<string>("city"),
                    state = row.Field<string>("state"),
                    district = row.Field<string>("district"),
                    country = row.Field<string>("country"),
                    telNos = row.Field<string>("telNos"),
                    memberType = row.Field<string>("memberType"),
                    status = row.Field<string>("status"),
                    cOPIES = row.Field<int>("cOPIES"),
                    openingDue = row.Field<decimal>("openingDue"),
                    openingAdvance = row.Field<decimal>("openingAdvance"),
                    openingAccBalance = row.Field<decimal>("openingAccBalance"),
                    currDue = row.Field<decimal>("currDue"),
                    currAdvance = row.Field<decimal>("currAdvance"),
                    period = row.Field<string>("period"),
                    magazine = row.Field<string>("magazine"),
                    membershipOldID = row.Field<string>("membershipOldID"),
                    category = row.Field<string>("category"),
                    mM_MS_NO = row.Field<int>("mM_MS_NO"),
                    id = row.Field<string>("id"),
                    addBy = row.Field<string>("addBy"),
                    addDate = row.Field<DateTime>("addDate"),
                    editBy = row.Field<string>("editBy"),
                    editDate = row.Field<DateTime>("editDate"),
                    actionStatus = row.Field<string>("actionStatus"),
                    actionBy = row.Field<string>("actionBy"),
                    actionDate = row.Field<DateTime>("actionDate"),
                    entryType = row.Field<string>("entryType"),
                    aB_ID = row.Field<string>("aB_ID"),
                    connectedto = row.Field<string>("connectedto"),
                    closedon = row.Field<DateTime>("closedon"),
                    closureRemarks = row.Field<string>("closureRemarks"),
                    mMB_PERIOD_TO = row.Field<DateTime>("mMB_PERIOD_TO"),
                    mMB_CC_DISPATCH = row.Field<string>("mMB_CC_DISPATCH"),
                }).ToList();
            }
            else return null;
        }
        public static List<CB_Grid_Model> DataTabletoCashBook(DataTable dt)
        {
            int sr = 1;
            List<CB_Grid_Model> newList = new List<CB_Grid_Model>();
            if (dt != null)
            {
                int count = dt.Rows.Count;
                for(int i=0;i<count;i++)
                {
                    DataRow row = dt.Rows[i];
                    CB_Grid_Model newRow = new CB_Grid_Model();
                    newRow.iTR_VNO = dt.Columns.Contains("iTR_VNO") ? row.Field<string>("iTR_VNO") : null;
                    newRow.iTR_DATE = dt.Columns.Contains("iTR_DATE") ? row.Field<DateTime?>("iTR_DATE") : null;
                    newRow.iTR_ITEM_ID = dt.Columns.Contains("iTR_ITEM_ID") ? row.Field<string>("iTR_ITEM_ID") : null;
                    newRow.iTR_ITEM = dt.Columns.Contains("iTR_ITEM") ? row.Field<string>("iTR_ITEM") : null;
                    newRow.iLED_ID = dt.Columns.Contains("iLED_ID") ? row.Field<string>("iLED_ID") : null;
                    newRow.iTR_HEAD = dt.Columns.Contains("iTR_HEAD") ? row.Field<string>("iTR_HEAD") : null;
                    newRow.iTR_SUB_ID = dt.Columns.Contains("iTR_SUB_ID") ? row.Field<string>("iTR_SUB_ID") : null;
                    newRow.iTR_AB_ID_1 = dt.Columns.Contains("iTR_AB_ID_1") ? row.Field<string>("iTR_AB_ID_1") : null;
                    newRow.iTR_PARTY_1 = dt.Columns.Contains("iTR_PARTY_1") ? row.Field<string>("iTR_PARTY_1") : null;
                    newRow.iTR_CR_ID = dt.Columns.Contains("iTR_CR_ID") ? row.Field<string>("iTR_CR_ID") : null;
                    newRow.iTR_CR_NAME = dt.Columns.Contains("iTR_CR_NAME") ? row.Field<string>("iTR_CR_NAME") : null;
                    newRow.iTR_DATE_SERIAL = dt.Columns.Contains("iTR_DATE_SERIAL") ? row.Field<int?>("iTR_DATE_SERIAL") : null;
                    newRow.iTR_DATE_SHOW = dt.Columns.Contains("iTR_DATE_SHOW") ? row.Field<string>("iTR_DATE_SHOW") : null;
                    newRow.iTR_ENTRY = dt.Columns.Contains("iTR_ENTRY") ? row.Field<string>("iTR_ENTRY") : null;
                    newRow.iTR_REC_CASH = dt.Columns.Contains("iTR_REC_CASH") ? row.Field<decimal?>("iTR_REC_CASH") : null;
                    newRow.iTR_REC_BANK = dt.Columns.Contains("iTR_REC_BANK") ? row.Field<decimal?>("iTR_REC_BANK") : null;
                    //newRow.REC_BANK01 = dt.Columns.Contains("REC_BANK01") ? row.Field<decimal?>("REC_BANK01") : null;
                    //newRow.REC_BANK02 = dt.Columns.Contains("REC_BANK02") ? row.Field<decimal?>("REC_BANK02") : null;
                    //newRow.REC_BANK03 = dt.Columns.Contains("REC_BANK03") ? row.Field<decimal?>("REC_BANK03") : null;
                    //newRow.REC_BANK04 = dt.Columns.Contains("REC_BANK04") ? row.Field<decimal?>("REC_BANK04") : null;
                    //newRow.REC_BANK05 = dt.Columns.Contains("REC_BANK05") ? row.Field<decimal?>("REC_BANK05") : null;
                    //newRow.REC_BANK06 = dt.Columns.Contains("REC_BANK06") ? row.Field<decimal?>("REC_BANK06") : null;
                    //newRow.REC_BANK07 = dt.Columns.Contains("REC_BANK07") ? row.Field<decimal?>("REC_BANK07") : null;
                    //newRow.REC_BANK08 = dt.Columns.Contains("REC_BANK08") ? row.Field<decimal?>("REC_BANK08") : null;
                    //newRow.REC_BANK09 = dt.Columns.Contains("REC_BANK09") ? row.Field<decimal?>("REC_BANK09") : null;
                    //newRow.REC_BANK10 = dt.Columns.Contains("REC_BANK10") ? row.Field<decimal?>("REC_BANK10") : null;
                    newRow.iTR_REC_JOURNAL = dt.Columns.Contains("iTR_REC_JOURNAL") ? row.Field<decimal?>("iTR_REC_JOURNAL") : null;
                    newRow.iTR_REC_TOTAL = dt.Columns.Contains("iTR_REC_TOTAL") ? row.Field<decimal?>("iTR_REC_TOTAL") : null;
                    newRow.iTR_PAY_CASH = dt.Columns.Contains("iTR_PAY_CASH") ? row.Field<decimal?>("iTR_PAY_CASH") : null;
                    newRow.iTR_PAY_BANK = dt.Columns.Contains("iTR_PAY_BANK") ? row.Field<decimal?>("iTR_PAY_BANK") : null;
                    //newRow.PAY_BANK01 = dt.Columns.Contains("PAY_BANK01") ? row.Field<decimal?>("PAY_BANK01") : null;
                    //newRow.PAY_BANK02 = dt.Columns.Contains("PAY_BANK02") ? row.Field<decimal?>("PAY_BANK02") : null;
                    //newRow.PAY_BANK03 = dt.Columns.Contains("PAY_BANK03") ? row.Field<decimal?>("PAY_BANK03") : null;
                    //newRow.PAY_BANK04 = dt.Columns.Contains("PAY_BANK04") ? row.Field<decimal?>("PAY_BANK04") : null;
                    //newRow.PAY_BANK05 = dt.Columns.Contains("PAY_BANK05") ? row.Field<decimal?>("PAY_BANK05") : null;
                    //newRow.PAY_BANK06 = dt.Columns.Contains("PAY_BANK06") ? row.Field<decimal?>("PAY_BANK06") : null;
                    //newRow.PAY_BANK07 = dt.Columns.Contains("PAY_BANK07") ? row.Field<decimal?>("PAY_BANK07") : null;
                    //newRow.PAY_BANK08 = dt.Columns.Contains("PAY_BANK08") ? row.Field<decimal?>("PAY_BANK08") : null;
                    //newRow.PAY_BANK09 = dt.Columns.Contains("PAY_BANK09") ? row.Field<decimal?>("PAY_BANK09") : null;
                    //newRow.PAY_BANK10 = dt.Columns.Contains("PAY_BANK10") ? row.Field<decimal?>("PAY_BANK10") : null;
                    newRow.iTR_PAY_JOURNAL = dt.Columns.Contains("iTR_PAY_JOURNAL") ? row.Field<decimal?>("iTR_PAY_JOURNAL") : null;
                    newRow.iTR_PAY_TOTAL = dt.Columns.Contains("iTR_PAY_TOTAL") ? row.Field<decimal?>("iTR_PAY_TOTAL") : null;
                    newRow.iTR_NARRATION = dt.Columns.Contains("iTR_NARRATION") ? row.Field<string>("iTR_NARRATION") : null;
                    newRow.iTR_ROW_POS = dt.Columns.Contains("iTR_ROW_POS") ? row.Field<string>("iTR_ROW_POS") : null;
                    newRow.iTR_TYPE = dt.Columns.Contains("iTR_TYPE") ? row.Field<string>("iTR_TYPE") : null;
                    newRow.iTR_CODE = dt.Columns.Contains("iTR_CODE") ? row.Field<int?>("iTR_CODE") : null;
                    newRow.iREC_ID = dt.Columns.Contains("iREC_ID") ? row.Field<string>("iREC_ID") : null;
                    newRow.iTR_M_ID = dt.Columns.Contains("iTR_M_ID") ? row.Field<string>("iTR_M_ID") : null;
                    newRow.iTR_SR_NO = dt.Columns.Contains("iTR_SR_NO") ? row.Field<int?>("iTR_SR_NO") : null;
                    newRow.iTR_SORT = dt.Columns.Contains("iTR_SORT") ? row.Field<string>("iTR_SORT") : null;
                    newRow.iREC_ADD_ON = dt.Columns.Contains("iREC_ADD_ON") ? row.Field<DateTime?>("iREC_ADD_ON") : null;
                    newRow.iTR_TEMP_ID = dt.Columns.Contains("iTR_TEMP_ID") ? row.Field<string>("iTR_TEMP_ID") : null;
                    newRow.iTR_REF_NO = dt.Columns.Contains("iTR_REF_NO") ? row.Field<int?>("iTR_REF_NO") : null;
                    newRow.iACTION_STATUS = dt.Columns.Contains("iACTION_STATUS") ? row.Field<string>("iACTION_STATUS") : null;
                    newRow.iREC_EDIT_ON = dt.Columns.Contains("iREC_EDIT_ON") ? row.Field<DateTime?>("iREC_EDIT_ON") : null;
                    newRow.iREC_STATUS_ON = dt.Columns.Contains("iREC_STATUS_ON") ? row.Field<DateTime?>("iREC_STATUS_ON") : null;
                    newRow.iREC_ADD_BY = dt.Columns.Contains("iREC_ADD_BY") ? row.Field<string>("iREC_ADD_BY") : null;
                    newRow.iREC_EDIT_BY = dt.Columns.Contains("iREC_EDIT_BY") ? row.Field<string>("iREC_EDIT_BY") : null;
                    newRow.iREC_STATUS_BY = dt.Columns.Contains("iREC_STATUS_BY") ? row.Field<string>("iREC_STATUS_BY") : null;
                    newRow.iCross_Ref_ID = dt.Columns.Contains("iCross_Ref_ID") ? row.Field<string>("iCross_Ref_ID") : null;
                    newRow.iRef_no = dt.Columns.Contains("iRef_no") ? row.Field<string>("iRef_no") : null;
                    newRow.Attachment_IDs = dt.Columns.Contains("Attachment_IDs") ? row.Field<string>("Attachment_IDs") : null;
                    newRow.iPurpose = dt.Columns.Contains("iPurpose") ? row.Field<string>("iPurpose") : null;
                    newRow.Advanced_Filter = dt.Columns.Contains("Advanced_Filter") ? row.Field<string>("Advanced_Filter") : null;
                    newRow.iREQ_ATTACH_COUNT = row.Field<Int32?>("REQ_ATTACH_COUNT");
                    newRow.iCOMPLETE_ATTACH_COUNT = row.Field<Int32?>("COMPLETE_ATTACH_COUNT");
                    newRow.iRESPONDED_COUNT = row.Field<Int32?>("RESPONDED_COUNT");
                    newRow.iREJECTED_COUNT = row.Field<Int32?>("REJECTED_COUNT");
                    newRow.iOTHER_ATTACH_CNT = row.Field<Int32?>("OTHER_ATTACH_CNT");
                    newRow.iALL_ATTACH_CNT = row.Field<Int32?>("ALL_ATTACH_CNT");
                    if (row.Field<string>("iREC_ID") == "NOTE-BOOK")
                    {
                        newRow.Grid_PK = (string.IsNullOrEmpty(row.Field<string>("iTR_M_ID")) ? "Null" : row.Field<string>("iTR_M_ID")) + (string.IsNullOrEmpty(row.Field<string>("iREC_ID")) ? "Null" : row.Field<string>("iTR_ITEM_ID"));
                    }
                    else
                    {
                        newRow.Grid_PK = (string.IsNullOrEmpty(row.Field<string>("iTR_M_ID")) ? "Null" : row.Field<string>("iTR_M_ID")) + (string.IsNullOrEmpty(row.Field<string>("iREC_ID")) ? "Null" : row.Field<string>("iREC_ID"));
                    }
                    newRow.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newRow.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newRow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newRow.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newRow.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newRow.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newRow.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newRow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newRow.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newRow.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newRow.SPECIAL_VOUCHER_REFERENCE = row.Field<string>("SPECIAL_VOUCHER_REFERENCE");
                    newRow.BA_ACC_NO = row.Field<string>("BA_ACC_NO");
                    newRow.ITEM_TDS_CODE = row.Field<string>("ITEM_TDS_CODE");
                    newRow.LED_TYPE = row.Field<string>("LED_TYPE");
                    newRow.Sr = sr++;
                    newRow.iIcon = "";
                    newRow.InvNo = row.Field<string>("TR_INV_NO");
                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newRow.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newRow.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newRow.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newRow.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newRow.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newRow.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newRow.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newRow.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newRow.iIcon += "AuditPartial|"; }
                    if((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newRow.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newRow.iIcon += "CorrectedEntry|"; }
                    newList.Add(newRow);
                }
            }
            return newList;
        }
        public static List<CB_AdvanceFilter> DataTableToAdvanceFilter(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new CB_AdvanceFilter
            {              
                REC_ID = dt.Columns.Contains("REC_ID") == true ? row.Field<string>("REC_ID") : null,
                Item = dt.Columns.Contains("Item") == true ? row.Field<string>("Item") : null,
                Party = dt.Columns.Contains("Party") == true ? row.Field<string>("Party") : null,
                Date = dt.Columns.Contains("Date") == true ? row.Field<string>("Date") : null,
                FD_No = dt.Columns.Contains("FD No.") == true ? row.Field<string>("FD No."):null,
                FD_Date = dt.Columns.Contains("FD Date") == true ? row.Field<DateTime>("FD Date").ToString():null,
                FD_Amt = dt.Columns.Contains("FD amt") == true ? Convert.ToDecimal(row.Field<decimal>("FD amt")):(decimal?)null,
                Category = dt.Columns.Contains("Category") == true ? row.Field<string>("Category"):null,
                Type = dt.Columns.Contains("Type") == true ? row.Field<string>("Type"):null,
                Use = dt.Columns.Contains("Use") == true ? row.Field<string>("Use"):null,
                Name = dt.Columns.Contains("Name") == true ? row.Field<string>("Name"):null,
                BIRTHYEAR = dt.Columns.Contains("BIRTH YEAR") == true ? row.Field<string>("BIRTH YEAR"):null,
                Make = dt.Columns.Contains("Make") == true ? row.Field<string>("Make"):null,
                Model = dt.Columns.Contains("Model") == true ? row.Field<string>("Model"):null,
                DESC = dt.Columns.Contains("DESC") == true ? row.Field<string>("DESC"):null,
                Reg_No = dt.Columns.Contains("Reg No") == true ? row.Field<string>("Reg No"):null,
                AccNo = dt.Columns.Contains("AccNo") == true ? row.Field<string>("AccNo"):null,
                 Bank = dt.Columns.Contains("Bank") == true ? row.Field<string>("Bank") : null,
                 Status= dt.Columns.Contains("Status") == true ? row.Field<string>("Status") : null
                
            }).ToList();
            else return null;
        }
        public static List<AF_Advance> DataTabletoAF_Advance(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Advance
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                Party = row.Field<string>("Party"),
                Date = row.Field<string>("Date")
            }).ToList();
            else return null;
        }
        public static List<AF_Fd> DataTabletoAF_Fd(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Fd
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Bank"),
                FD_No = row.Field<string>("FD No."),
                FD_Date = row.Field<DateTime>("FD Date").ToString(),
               FD_Amt = Convert.ToDecimal(row.Field<Decimal>("FD amt"))
            }).ToList();
            else return null;
        }
        public static List<AF_LandAndBuilding> DataTabletoAF_LandAndBuilding(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_LandAndBuilding
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                Category = row.Field<string>("Category"),
                Type = row.Field<string>("Type"),
                Use = row.Field<string>("Use")
            }).ToList();
            else return null;
        }
        public static List<AF_LiveStock> DataTabletoAF_LiveStock(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_LiveStock
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                Name = row.Field<string>("Name"),
                BIRTHYEAR = row.Field<string>("BIRTH YEAR")
            }).ToList();
            else return null;
        }
        public static List<AF_Movabale_Assets> DataTabletoAF_Movabale_Assets(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Movabale_Assets
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                Make = row.Field<string>("Make"),
                Model = row.Field<string>("Model")
            }).ToList();
            else return null;
        }
        public static List<AF_Other_Deposits_Liabilites> DataTabletoAF_Other_Deposits_Liabilites(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Other_Deposits_Liabilites
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                Party = row.Field<string>("Party"),
                Date = row.Field<string>("Date")
            }).ToList();
            else return null;
        }
        public static List<AF_Silver_Gold> DataTabletoAF_Silver_Gold(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Silver_Gold
            {
                REC_ID = row.Field<string>("REC_ID"),
                Item = row.Field<string>("Item"),
                DESC = row.Field<string>("DESC")
            }).ToList();
            else return null;
        }
        public static List<AF_Vechiles> DataTabletoAF_Vechiles(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_Vechiles
            {
                Item = row.Field<string>("Item"),
                REC_ID = row.Field<string>("REC_ID"),
                Make = row.Field<string>("Make"),
                Model = row.Field<string>("Model"),
                Reg_No = row.Field<string>("Reg No")
            }).ToList();
            else return null;
        }
        public static List<AF_BankAccounts> DataTabletoAF_BankAccount(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AF_BankAccounts
            {
                Item = row.Field<string>("Bank"),
                REC_ID = row.Field<string>("REC_ID"),
                AccNo = row.Field<string>("AccNo")              
            }).ToList();
            else return null;
        }
        public static List<Summary> DataTabletoSummary(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Summary
            {
                Title = row.Field<string>("Title"),
                Sr = row.Field<double>("Sr"),
                Description = row.Field<string>("Description"),
                O_BALANCE = row.Field<double>("O_BALANCE"),
                R_BALANCE = row.Field<double>("R_BALANCE"),
                P_BALANCE = row.Field<double>("P_BALANCE"),
                C_BALANCE = row.Field<double>("C_BALANCE")
            }).ToList();
            else return null;
        }

        public static List<Items> DataTabletoItemlist(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Items
            {
                ITEM_ID = row.Field<string>("ITEM_ID"),
                ITEM_NAME = row.Field<string>("ITEM_NAME"),
                LED_NAME = row.Field<string>("LED_NAME"),
                ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ"),
                CON_LED_TYPE = row.Field<string>("CON_LED_TYPE"),
                //HEAD= row.Field<string>("HEAD"),
                //ITEMID= row.Field<string>("ITEMID"),
                //ITEMNAME= row.Field<string>("ITEMNAME"),
                ITEM_CON_LED_ID = row.Field<string>("ITEM_CON_LED_ID"),
                ITEM_CON_MAX_VALUE = row.Field<int>("ITEM_CON_MAX_VALUE"),
                ITEM_CON_MIN_VALUE = row.Field<int>("ITEM_CON_MIN_VALUE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
                ITEM_PROFILE = row.Field<string>("ITEM_PROFILE"),
                ITEM_TDS_CODE = row.Field<string>("ITEM_TDS_CODE"),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE"),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
                LED_TYPE = row.Field<string>("LED_TYPE"),
                //PARTY= row.Field<string>("PARTY"),
                TDS_RATE = row.Field<int>("TDS_RATE")
            }).ToList();
            else return null;
        }
        public static List<VoucherTypeItems> DataTabletoVoucherTypeLookUp_GetItemList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new VoucherTypeItems
            {
                ITEMID = row.Field<string>("ITEM_ID"),
                ITEMNAME = row.Field<string>("ITEM_NAME").ToString(),
                LED_NAME = row.Field<string>("LED_NAME").ToString(),
                ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
            }).ToList();
            else return null;
        }
        public static List<VoucherTypeItems> DataTabletoVoucherWinCashLookUp_GetItemList(DataTable dt)
        {
            List<VoucherTypeItems> voucherTypeItems = new List<VoucherTypeItems>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("ITEM_ID") != null)
                    {
                        var newdata = new VoucherTypeItems();
                        newdata.ITEMID = row.Field<string>("ITEM_ID");
                        newdata.ITEMNAME = row.Field<string>("ITEM_NAME").ToString();
                        newdata.LED_NAME = row.Field<string>("LED_NAME").ToString();
                        newdata.ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE").ToString();
                        newdata.ITEM_LED_ID = row.Field<string>("ITEM_LED_ID").ToString();
                        newdata.ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE").ToString();
                        //newdata.ITEM_PROFILE = row.Field<string>("ITEM_PROFILE").ToString();
                        //ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
                        voucherTypeItems.Add(newdata);
                    }

                }
            }
            return voucherTypeItems;
        }
        public static List<SVR_List> DataTabletoTB_Report_GetSVRList(DataTable dt)
        {
            List<SVR_List> SVRListItems = new List<SVR_List>();
            SVRListItems.Add(new SVR_List() { Spl_Vouch_Ref = "ALL" });
            SVRListItems.Add(new SVR_List() { Spl_Vouch_Ref = "GENERAL" });
            if (dt != null && dt.Rows.Count > 0) // If SVR are enabled for the UID then it will return list of SVR and we will add manually the options of ALL & General prior to other SVR.
            {                
                foreach (DataRow row in dt.Rows)
                {
                        var newdata = new SVR_List();
                        newdata.Spl_Vouch_Ref = row.Field<string>("TASK_NAME");
                        SVRListItems.Add(newdata);
                }
            }

            return SVRListItems;
        }
        public static List<VoucherTypeItems> DataTabletoVoucherMembershipLookUp_GetItemList(DataTable dt)
        {
            List<VoucherTypeItems> voucherTypeItems = new List<VoucherTypeItems>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("ITEM_ID") != null)
                    {
                        var newdata = new VoucherTypeItems();
                        newdata.ITEMID = row.Field<string>("ITEM_ID");
                        newdata.ITEMNAME = row.Field<string>("ITEM_NAME").ToString();
                        newdata.LED_NAME = row.Field<string>("LED_NAME").ToString();
                        newdata.ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE").ToString();
                        newdata.ITEM_LED_ID = row.Field<string>("ITEM_LED_ID").ToString();
                        newdata.ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE").ToString();
                        newdata.ITEM_PROFILE = row.Field<string>("ITEM_PROFILE").ToString();
                        //ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
                        voucherTypeItems.Add(newdata);
                    }

                }
            }
            return voucherTypeItems;
        }
        public static List<SubscriptionList> DataTabletoLookUp_GetSubsList(DataTable dt)
        {
            List<SubscriptionList> subsList = new List<SubscriptionList>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("SI_REC_ID") != null)
                    {
                        var newdata = new SubscriptionList();
                        newdata.SI_REC_ID = row.Field<string>("SI_REC_ID");
                        newdata.SI_NAME = row.Field<string>("SI_NAME").ToString();
                        newdata.SI_CATEGORY = row.Field<string>("SI_CATEGORY").ToString();
                        newdata.SI_START_MONTH = row.Field<int?>("SI_START_MONTH");
                        newdata.SI_TOTAL_MONTH = row.Field<int?>("SI_TOTAL_MONTH");
                        //newdata.ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE").ToString();
                        //ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
                        subsList.Add(newdata);
                    }

                }
            }
            return subsList;
        }
        public static List<MembershipNamesList> DataTabletoGetMembershipNamesList(DataTable dt)
        {
            DateTime? dtnull = null;
            List<MembershipNamesList> subsList = new List<MembershipNamesList>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("C_ID") != null)
                    {
                        var newdata = new MembershipNamesList();
                        newdata.C_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_ID")) ? row.Field<string>("C_ID") : null;
                        newdata.C_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("C_NAME")) ? row.Field<string>("C_NAME") : null;
                        newdata.C_R_ADD1 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD1")) ? row.Field<string>("C_R_ADD1") : null;
                        newdata.C_R_ADD2 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD2")) ? row.Field<string>("C_R_ADD2") : null;
                        newdata.C_R_ADD3 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD3")) ? row.Field<string>("C_R_ADD3") : null;
                        newdata.C_R_ADD4 = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_ADD4")) ? row.Field<string>("C_R_ADD4") : null;
                        newdata.C_R_PINCODE = !string.IsNullOrWhiteSpace(row.Field<string>("C_R_PINCODE")) ? row.Field<string>("C_R_PINCODE") : null;
                        newdata.CI_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CI_NAME")) ? row.Field<string>("CI_NAME") : null; 
                        newdata.ST_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("ST_NAME")) ? row.Field<string>("ST_NAME") : null; 
                        newdata.DI_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("DI_NAME")) ? row.Field<string>("DI_NAME") : null; 
                        newdata.CO_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CO_NAME")) ? row.Field<string>("CO_NAME") : null;
                        newdata.C_EDUCATION = !string.IsNullOrWhiteSpace(row.Field<string>("C_EDUCATION")) ? row.Field<string>("C_EDUCATION") : null;
                        newdata.C_OCCUPATION = !string.IsNullOrWhiteSpace(row.Field<string>("C_OCCUPATION")) ? row.Field<string>("C_OCCUPATION") : null;
                        newdata.C_DOB = !string.IsNullOrWhiteSpace(row.Field<DateTime?>("C_DOB").ToString()) ? row.Field<DateTime?>("C_DOB") : (DateTime?)dtnull;
                        newdata.C_AGE = !string.IsNullOrWhiteSpace(row.Field<int?>("C_AGE").ToString()) ? row.Field<int?>("C_AGE").ToString() : null; 
                        newdata.TEL_NOS = !string.IsNullOrWhiteSpace(row.Field<string>("TEL_NOS").ToString())? row.Field<string>("TEL_NOS"):null;
                        newdata.MOB_NOS = !string.IsNullOrWhiteSpace(row.Field<string>("MOB_NOS").ToString()) ? row.Field<string>("MOB_NOS") : null;
                        newdata.EMAILS = !string.IsNullOrWhiteSpace(row.Field<string>("EMAILS")) ? row.Field<string>("EMAILS") : null;
                        newdata.C_CEN_CATEGORY = !string.IsNullOrWhiteSpace(row.Field<int?>("C_CEN_CATEGORY").ToString()) ? row.Field<int?>("C_CEN_CATEGORY") : null;
                        newdata.C_CLASS_CEN_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_CLASS_CEN_ID")) ? row.Field<string>("C_CLASS_CEN_ID") : null;
                        newdata.CEN_NAME = !string.IsNullOrWhiteSpace(row.Field<string>("CEN_NAME")) ? row.Field<string>("CEN_NAME") : null;
                        newdata.CEN_UID = !string.IsNullOrWhiteSpace(row.Field<string>("CEN_UID")) ? row.Field<string>("CEN_UID") : null;
                        newdata.C_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_ID")) ? row.Field<string>("C_ID") : null;
                        newdata.C_ORG_ID = !string.IsNullOrWhiteSpace(row.Field<string>("C_ORG_ID")) ? row.Field<string>("C_ORG_ID") : null;
                        newdata.C_REC_EDIT_ON = !string.IsNullOrWhiteSpace(row.Field<DateTime>("C_REC_EDIT_ON").ToString()) ? row.Field<DateTime>("C_REC_EDIT_ON") : (DateTime?)dtnull;
                        //newdata.ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE").ToString();
                        //ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
                        subsList.Add(newdata);
                    }

                }
            }
            return subsList;
        }

        public static List<Purpose> DataTabletoPurposelist(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Purpose
            {
                PUR_ID = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME").ToString(),
            }).ToList();
            else return null;
        }
     


        public static List<Party> DataTabletoPartylist(DataTable dt)
        {
            return Extensions.ToList<Party>(dt);
            //List <DataRow> list = dt.AsEnumerable().ToList();
            //if (dt != null) return dt.AsEnumerable().Select(row => new Party
            //{
            //    ID = row.Field<string>("ID"),
            //    Name = row.Field<string>("Name").ToString(),
            //    PAN = row.Field<string>("PAN").ToString(),
            //}).ToList();
            //else return null;
        }
        public static List<PaymentVoucherPartyList> DataTabletoPaymentVoucherPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new PaymentVoucherPartyList
            {
                C_ID = row.Field<string>("C_ID"),
                C_NAME = row.Field<string>("C_NAME"),
                C_CITY = row.Field<string>("C_CITY"),
                C_PAN_NO = row.Field<string>("C_PAN_NO"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
            }).ToList();
            else return null;
        }

        public static List<AdvancesPartyList> DataTabletoAdvancesPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new AdvancesPartyList
            {
                ID = row.Field<string>("ID"),
                NAME = row.Field<string>("NAME"),
                Occupation = row.Field<string>("Occupation"),
                Status = row.Field<string>("Status"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
            }).ToList();
            else return null;
        }


        public static List<RefBank> DataTabletoRefBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new RefBank
            {
                BI_ID = row.Field<string>("BI_ID"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
            }).ToList();
            else return null;
        }

        public static List<VehiclesInfo> DataTabletoVehicles_INFO(DataTable dt)
        {
            List<VehiclesInfo> VehicleList = new List<VehiclesInfo>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new VehiclesInfo();
                    newdata.ITEM_NAME = row.Field<string>("ITEM_NAME");
                    newdata.VI_ITEM_ID = row.Field<string>("VI_ITEM_ID");
                    newdata.MAKE = row.Field<string>("MAKE");
                    newdata.Model = row.Field<string>("Model");
                    newdata.VI_REG_NO = row.Field<string>("VI_REG_NO");
                    newdata.Date_of_First_Registration = row.Field<DateTime?>("Date of First Registration");
                    newdata.Opening_Value = row.Field<decimal>("Opening Value");
                    newdata.Curr_Value = row.Field<decimal>("Curr Value");
                    newdata.Ownership = row.Field<string>("Ownership");
                    newdata.VI_DOC_RC_BOOK = row.Field<string>("VI_DOC_RC_BOOK");
                    newdata.Affidavit = row.Field<string>("Affidavit");
                    newdata.Will = row.Field<string>("Will");
                    newdata.Transfer_Lettter = row.Field<string>("Transfer Letter");
                    newdata.Free_Use_Letter = row.Field<string>("Free Use Letter");
                    newdata.Other_Documents = row.Field<string>("Other Documents");
                    newdata.Insurance_Company = row.Field<string>("Insurance Company");
                    newdata.INSURANCE_ID = row.Field<string>("INSURANCE_ID");
                    newdata.VI_INS_POLICY_NO = row.Field<string>("VI_INS_POLICY_NO");
                    newdata.Expiry_Date = row.Field<DateTime?>("Expiry Date");
                    newdata.VI_OTHER_DETAIL = row.Field<string>("VI_OTHER_DETAIL");
                    newdata.AL_LOC_AL_ID = row.Field<string>("VI_LOC_AL_ID");
                    newdata.AL_LOC_Name = row.Field<string>("AL_LOC_Name");
                    newdata.YEAR_ID = row.Field<int?>("YearID").ToString();
                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.ID = row.Field<string>("ID");
                    newdata.Sale_Status = row.Field<string>("Sale Status");
                    newdata.Entry_Type = row.Field<string>("Entry Type");
                    newdata.Remark_Count = row.Field<int?>("RemarkCount");
                    newdata.Remark_Status = row.Field<string>("RemarkStatus");
                    newdata.Open_Actions = row.Field<int?>("OpenActions");
                    newdata.Crossed_Time_Limit = row.Field<int>("CrossedTimeLimit");
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Add_Date = row.Field<DateTime>("Add Date");
                    newdata.Edit_Date = row.Field<DateTime>("Edit Date");
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime>("Action Date");
                    newdata.Entry_Status = row.Field<string>("Entry Status");
                    //Remarks = row.Field<string>("Remarks"),
                    newdata.REQ_ATTACH_COUNT = row.Field<int?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<int?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<int?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<int?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<int?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<int?>("ALL_ATTACH_CNT");
                    //newdata.VOUCHING_STATUS = row.Field<String>("VOUCHING_STATUS");
                    //newdata.AUDIT_STATUS = row.Field<String>("AUDIT_STATUS");
                    //newdata.VOUCHING_REMARKS = row.Field<String>("VOUCHING_REMARKS");
                    //newdata.AUDITOR_REMARKS = row.Field<String>("AUDITOR_REMARKS");

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Field<int?>("RemarkCount"))) && row.Field<int?>("RemarkCount") > 0)
                        newdata.Remarks = true;
                    else
                        newdata.Remarks = false;

                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    VehicleList.Add(newdata);
                }
            }
            return VehicleList;

        }


        public static List<GoldSilverInfo> DataTabletoGoldSilver_INFO(DataTable dt)
        {
            List<GoldSilverInfo> GoldSilverList = new List<GoldSilverInfo>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new GoldSilverInfo();
                    newdata.GS_ITEM_ID = row.Field<string>("GS_ITEM_ID");
                    newdata.GS_DESC_MISC_ID = row.Field<string>("GS_DESC_MISC_ID");
                    newdata.GS_LOC_AL_ID = row.Field<string>("GS_LOC_AL_ID");
                    newdata.ITEM_NAME = row.Field<string>("ITEM_NAME");
                    newdata.MISC_NAME = row.Field<string>("MISC_NAME");
                    newdata.GS_ITEM_WEIGHT = row.Field<decimal>("GS_ITEM_WEIGHT");
                    newdata.GS_AMT = row.Field<decimal>("GS_AMT").ToString();
                    newdata.AL_LOC_NAME = row.Field<string>("AL_LOC_NAME");
                    newdata.GS_OTHER_DETAIL = row.Field<string>("GS_OTHER_DETAIL");
                    newdata.YearID = row.Field<int?>("YearID").ToString();
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Add_Date = row.Field<DateTime>("Add Date").ToString();
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Edit_Date = row.Field<DateTime>("Edit Date").ToString();
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime>("Action Date").ToString();
                    newdata.ID = row.Field<string>("ID");
                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.RemarkCount = row.Field<int?>("RemarkCount").ToString();
                    newdata.RemarkStatus = row.Field<int?>("RemarkStatus").ToString();
                    newdata.Rate_per_Gram = row.Field<decimal>("Rate per Gram");
                    newdata.CrossedTimeLimit = row.Field<int?>("CrossedTimeLimit").ToString();
                    newdata.Curr_Weight = row.Field<decimal>("Curr Weight");
                    newdata.Curr_Value = row.Field<decimal>("Curr Value");
                    newdata.Entry_Type = row.Field<string>("Entry Type");
                    newdata.Type = row.Field<string>("Type");
                    newdata.Sale_Status = row.Field<string>("Sale Status");
                    newdata.REQ_ATTACH_COUNT = row.Field<Int32?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<Int32?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<Int32?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<Int32?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<Int32?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<Int32?>("ALL_ATTACH_CNT");
                    newdata.OpenActions = row.Field<Int32?>("OpenActions");

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Field<int?>("RemarkCount"))) && row.Field<int?>("RemarkCount") > 0)
                        newdata.Remarks = true;
                    else
                        newdata.Remarks = false;

                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString(); ;
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    GoldSilverList.Add(newdata);
                }

            }
            return GoldSilverList;
        }

        public static List<LiabilitiesInfo> DataTabletoLiabilities_INFO(DataTable dt)
        {
            List<LiabilitiesInfo> LiabilitiesList = new List<LiabilitiesInfo>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new LiabilitiesInfo();
                    newdata.LI_ITEM_ID = row.Field<string>("LI_ITEM_ID");
                    newdata.LI_PARTY_ID = row.Field<string>("LI_PARTY_ID");
                    newdata.ITEM_NAME = row.Field<string>("ITEM_NAME");
                    newdata.PARTY_NAME = row.Field<string>("PARTY_NAME");
                    newdata.LI_DATE = Convert.ToDateTime(row.Field<DateTime?>("LI_DATE")).ToString("dd/MM/yyyy");//Redmine Bug #132724 fixed
                    newdata.LI_PAY_DATE = row.Field<DateTime?>("LI_PAY_DATE").ToString();
                    newdata.LI_OTHER_DETAIL = row.Field<string>("LI_OTHER_DETAIL");
                    newdata.YearID = row.Field<int?>("YearID").ToString();
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Add_Date = row.Field<DateTime>("Add Date").ToString();
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Edit_Date = row.Field<DateTime>("Edit Date").ToString();
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime>("Action Date").ToString();
                    newdata.ID = row.Field<string>("ID");
                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.RemarkStatus = row.Field<int?>("RemarkStatus").ToString();
                    newdata.CrossedTimeLimit = row.Field<int?>("CrossedTimeLimit").ToString();
                    newdata.OpenActions = row.Field<int?>("OpenActions").ToString();
                    newdata.Amount = row.Field<decimal>("Amount");
                    newdata.Paid = row.Field<decimal>("Paid");
                    newdata.Type = row.Field<string>("Type");
                    newdata.Out_Standing = row.Field<decimal>("Out-Standing");
                    newdata.Addition = row.Field<decimal>("Addition");
                    newdata.Adjusted = row.Field<decimal>("Adjusted");
                    newdata.RemarkCount = row.Field<int?>("RemarkCount");
                    newdata.Reason = row.Field<string>("Reason");
                    newdata.REQ_ATTACH_COUNT = row.Field<Int32?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<Int32?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<Int32?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<Int32?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<Int32?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<Int32?>("ALL_ATTACH_CNT");
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Field<int?>("RemarkCount"))) && row.Field<int?>("RemarkCount") > 0)
                        newdata.Remarks = true;
                    else
                        newdata.Remarks = false;

                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    LiabilitiesList.Add(newdata);
                }

            }
            return LiabilitiesList;
        }

        public static List<DepositsInfo> DataTabletoDeposits_INFO(DataTable dt)
        {
            List<DepositsInfo> DepositsList = new List<DepositsInfo>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new DepositsInfo();
                    newdata.DI_PARTY_ID = row.Field<string>("DI_PARTY_ID");
                    newdata.ITEM_NAME = row.Field<string>("ITEM_NAME");
                    newdata.DI_ITEM_ID = row.Field<string>("DI_ITEM_ID");

                    newdata.PARTY_NAME = row.Field<string>("PARTY_NAME");
                    newdata.DI_AGAINST_INSURANCE = row.Field<string>("DI_AGAINST_INSURANCE").ToString();
                    newdata.DI_DEP_DATE = row.Field<DateTime?>("DI_DEP_DATE").ToString();
                    newdata.DI_DEP_PERIOD = row.Field<decimal>("DI_DEP_PERIOD");
                    newdata.DI_INT_RATE = row.Field<decimal>("DI_INT_RATE");
                    newdata.DI_MAT_DATE = row.Field<DateTime?>("DI_MAT_DATE");

                    newdata.DI_OTHER_DETAIL = row.Field<string>("DI_OTHER_DETAIL");
                    newdata.YearID = row.Field<int?>("YearID").ToString();
                    newdata.Type = row.Field<string>("Type");
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Add_Date = row.Field<DateTime>("Add Date").ToString();
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Edit_Date = row.Field<DateTime>("Edit Date").ToString();
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime>("Action Date").ToString();
                    newdata.ID = row.Field<string>("ID");
                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.RemarkStatus = row.Field<int?>("RemarkStatus").ToString();
                    newdata.CrossedTimeLimit = row.Field<int?>("CrossedTimeLimit").ToString();
                    newdata.OpenActions = row.Field<int?>("OpenActions").ToString();
                    newdata.Out_Standing = row.Field<decimal>("Out-Standing");
                    newdata.Refund = row.Field<decimal>("Refund");
                    newdata.Addition = row.Field<decimal>("Addition");
                    newdata.Adjusted = row.Field<decimal>("Adjusted");
                    newdata.RemarkCount = row.Field<int?>("RemarkCount");
                    newdata.Deposit = row.Field<decimal>("Deposit");
                    newdata.Reason = row.Field<string>("Reason");
                    newdata.REQ_ATTACH_COUNT = row.Field<Int32?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<Int32?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<Int32?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<Int32?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<Int32?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<Int32?>("ALL_ATTACH_CNT");
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Field<int?>("RemarkCount"))) && row.Field<int?>("RemarkCount") > 0)
                        newdata.Remarks = true;
                    else
                        newdata.Remarks = false;
                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    DepositsList.Add(newdata);
                }

            }
            return DepositsList;
        }


        public static List<PropertiesInfo> DataTabletoProperty_INFO(DataTable dt)
        {
            List<PropertiesInfo> PropertyList = new List<PropertiesInfo>();
            if (dt != null)
            {             
                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new PropertiesInfo();
                    newdata.Category = row.Field<string>("Category");
                    newdata.LB_PRO_NAME = row.Field<string>("LB_PRO_NAME");
                    newdata.Address = row.Field<string>("Address");
                    newdata.Use_of_Property = row.Field<string>("Use of Property");
                    newdata.Ownership = row.Field<string>("Ownership");
                    newdata.Owner = row.Field<string>("Owner");
                    newdata.LB_SURVEY_NO = row.Field<string>("LB_SURVEY_NO");
                    newdata.LB_TOT_P_AREA = row.Field<decimal>("LB_TOT_P_AREA");
                    newdata.Construction_Year = row.Field<string>("Construction Year");
                    newdata.RCC_Roof = row.Field<string>("RCC Roof");
                    newdata.Deposit_Amount = row.Field<decimal>("Deposit Amount");
                    newdata.Paid_Date = row.Field<DateTime?>("Paid Date");
                    newdata.Monthly_Rent = row.Field<decimal>("Monthly Rent");
                    newdata.Other_Monthly_Payments = row.Field<decimal>("Other Monthly Payments");
                    newdata.Period_From = row.Field<DateTime?>("Period From");
                    newdata.Period_To = row.Field<DateTime?>("Period To");
                    newdata.Other_Detail = row.Field<string>("Other Detail");
                    newdata.Entry_Type = row.Field<string>("Entry Type");
                    newdata.LB_CON_AREA = row.Field<decimal>("LB_CON_AREA");

                    newdata.YearID = row.Field<int?>("YearID").ToString();
                    newdata.Type = row.Field<string>("Type");
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Add_Date = row.Field<DateTime?>("Add Date");
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Edit_Date = row.Field<DateTime?>("Edit Date");
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime?>("Action Date");
                    newdata.ID = row.Field<string>("ID");
                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.RemarkStatus = row.Field<string>("RemarkStatus");
                    newdata.Crossed_Time_Limit = row.Field<int?>("CrossedTimeLimit").ToString();
                    newdata.Open_Actions = row.Field<int?>("OpenActions").ToString();
                    newdata.Opening_Value = row.Field<decimal>("Opening Value");
                    newdata.Curr_Value = row.Field<decimal>("Curr Value");
                    newdata.Sale_Status = row.Field<string>("Sale Status");
                    newdata.RemarkCount = row.Field<int?>("RemarkCount");
                    newdata.REQ_ATTACH_COUNT = row.Field<int?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<int?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<int?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<int?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<int?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<int?>("ALL_ATTACH_CNT");
                    //newdata.Remarks= row.Field<Boolean?>("Remarks");
                    if (Convert.IsDBNull(row["RemarkCount"]) == false && row.Field<int?>("RemarkCount") > 0)
                    {
                        newdata.Remarks = true;
                    }
                    else
                    {
                        newdata.Remarks = false;
                    }
                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();                    
                    newdata.TYPE_CHANGE_LOG = string.IsNullOrWhiteSpace(row["TYPE_CHANGE_LOG"].ToString()) ? "" : row["TYPE_CHANGE_LOG"].ToString();                    
                    newdata.iIcon = "";

                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    PropertyList.Add(newdata);
                }

            }
            return PropertyList;
        }
        public static List<MagazineReciptRegInfo> DataTabletoMagazineReg_INFO(DataTable dt)
        {
            List<MagazineReciptRegInfo> MagazineRegList = new List<MagazineReciptRegInfo>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new MagazineReciptRegInfo();
                    newdata.Receipt_NO = row.Field<string>("Receipt No.");
                    newdata.Voucher_No = row.Field<string>("Voucher. No.");
                    newdata.Date = row.Field<DateTime?>("Date");
                    newdata.Entry = row.Field<string>("Entry");
                    newdata.Magazine = row.Field<string>("Magazine");
                    newdata.TagName = row.Field<string>("Tag");
                    newdata.Member_Name = row.Field<string>("Member Name");
                    newdata.City = row.Field<string>("City");
                    newdata.Membership_ID = row.Field<string>("Membership ID");
                    newdata.Old_ID = row.Field<string>("Old ID");
                    newdata.Category = row.Field<string>("Category");
                    newdata.Subs_Type = row.Field<string>("Subs. Type");
                    newdata.Start_Date = row.Field<DateTime?>("Start Date");
                    newdata.Period = row.Field<string>("Period");
                    newdata.Copies = row.Field<int?>("Copies");
                    newdata.Cash_Amount = row.Field<decimal?>("Cash Amount");
                    newdata.Bank_Amount = row.Field<decimal?>("Bank Amount");
                    // newdata.Entry_Type = row.Field<string>("Entry Type");
                    newdata.Total_Amount = row.Field<decimal?>("Total Amount");
                    newdata.ID = row.Field<string>("ID");
                    newdata.Other_Details = row.Field<string>("Other Details");
                    newdata.Ref_No = row.Field<string>("Ref. No");
                    newdata.Ref_Date = row.Field<string>("Ref. Date");
                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Edit_Date = row.Field<DateTime?>("Edit Date");
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Receipt_ID= row.Field<string>("Receipt ID");
                    newdata.MS_REC_ID= row.Field<string>("MS_REC_ID");
                    newdata.MM_MEMBER_ID = row.Field<string>("MM_MEMBER_ID");
                    newdata.MM_MEMBER_TYPE = row.Field<string>("MM_MEMBER_TYPE");
                    newdata.AddDate= row.Field<DateTime>("Add Date");
                    MagazineRegList.Add(newdata);
                }

            }
            return MagazineRegList;
        }
        public static List<CoreInfoLocation> DataTabletoCoreLocationList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new CoreInfoLocation
            {
                ID = row.Field<string>("ID"),
                Location_Name = Convert.ToString(row["Location Name"]),
                Other_Detail = Convert.ToString(row["Other Detail"]),
                System = Convert.ToString(row["System"]),
                Matched_Type = Convert.ToString(row["Matched Type"]),
                Matched_Name = Convert.ToString(row["Matched Name"]),
                Matched_Instt = Convert.ToString(row["Matched Instt."]),
                YEARID = Convert.ToString(row["YEARID"]),
                ORG_REC = Convert.ToString(row["ORG_REC"]),
                Add_By = Convert.ToString(row["Add By"]),
                Add_Date = Convert.ToDateTime(row["Add Date"]),
                Edit_By = Convert.ToString(row["Edit By"]),
                Edit_Date = Convert.ToDateTime(row["Edit Date"]),
                Action_Status = Convert.ToString(row["Action Status"]),
                Ac_nonAc = Convert.ToString(row["AL_AC_OR_NON_AC"]),
                Category = Convert.ToString(row["AL_CATEGORY"]),
                roomfloor = Convert.ToString(row["AL_ROOM_FLOOR"]),
                Action_By = Convert.ToString(row["Action By"]),
                Capacity = Convert.ToInt32(row["Capacity"]),
                Action_Date = Convert.ToDateTime(row["Action Date"])
            }).ToList();
            else return null;
        }


        public static List<AssetInfo> DataTabletoAsset_INFO(DataTable dt)
        {
            List<AssetInfo> AssetList = new List<AssetInfo>();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var newdata = new AssetInfo();
                    newdata.ASSET_TYPE = row.Field<string>("ASSET_TYPE");
                    newdata.ITEM_NAME = row.Field<string>("ITEM_NAME");
                    newdata.AI_ITEM_ID = row.Field<string>("AI_ITEM_ID");
                    newdata.ITEM_LED_ID = row.Field<string>("ITEM_LED_ID");
                    newdata.Head = row.Field<string>("Head");
                    newdata.Make = row.Field<string>("Make");
                    newdata.Model = row.Field<string>("Model");

                    newdata.AI_SERIAL_NO = row.Field<string>("AI_SERIAL_NO");
                    newdata.AI_PUR_DATE = row.Field<DateTime?>("AI_PUR_DATE");
                    newdata.AI_AMT_FOR_INS = row.Field<decimal?>("AI_AMT_FOR_INS");
                    newdata.AI_ADJ_FOR_INS = row.Field<decimal?>("AI_ADJ_FOR_INS");
                    newdata.AI_CLOSE_FOR_INS = row.Field<decimal?>("AI_CLOSE_FOR_INS");
                    newdata.AL_LOC_NAME = row.Field<string>("AL_LOC_NAME");
                    newdata.AI_OTHER_DETAIL = row.Field<string>("AI_OTHER_DETAIL");
                    newdata.AI_PUR_AMT = row.Field<decimal?>("AI_PUR_AMT");
                    newdata.Curr_Value = row.Field<decimal?>("Curr Value");

                    newdata.sale_quantity = row.Field<decimal?>("sale_quantity");
                    newdata.Warranty = row.Field<decimal?>("Warranty");
                    newdata.Quantity = row.Field<decimal?>("Quantity");
                    newdata.Curr_Qty = row.Field<decimal?>("Curr Qty");
                    newdata.Rate = row.Field<decimal?>("Rate");

                    newdata.AL_LOC_NAME = row.Field<string>("AL_LOC_NAME");
                    newdata.AI_LOC_AL_ID = row.Field<string>("AI_LOC_AL_ID");
                    newdata.AI_OTHER_DETAIL = row.Field<string>("AI_OTHER_DETAIL");

                    newdata.TR_ID = row.Field<string>("TR_ID");
                    newdata.YearID = row.Field<int?>("YearID").ToString();
                    newdata.ID = row.Field<string>("ID");
                    newdata.QR_Code_ID = row.Field<string>("QRCodeID");

                    newdata.Sale_Status = row.Field<string>("Sale Status");
                    newdata.Entry_Type = row.Field<string>("Entry Type");
                    newdata.Remark_Count = row.Field<int?>("RemarkCount");
                    newdata.Remark_Status = row.Field<string>("RemarkStatus");

                    newdata.Open_Actions = row.Field<int?>("OpenActions");
                    newdata.Crossed_Time_Limit = row.Field<int?>("CrossedTimeLimit");

                    newdata.Add_By = row.Field<string>("Add By");
                    newdata.Add_Date = row.Field<DateTime?>("Add Date");
                    newdata.Edit_By = row.Field<string>("Edit By");
                    newdata.Edit_Date = row.Field<DateTime?>("Edit Date");
                    newdata.Action_Status = row.Field<string>("Action Status");
                    newdata.Action_By = row.Field<string>("Action By");
                    newdata.Action_Date = row.Field<DateTime?>("Action Date");
                    newdata.REQ_ATTACH_COUNT = row.Field<Int32?>("REQ_ATTACH_COUNT");
                    newdata.COMPLETE_ATTACH_COUNT = row.Field<Int32?>("COMPLETE_ATTACH_COUNT");
                    newdata.RESPONDED_COUNT = row.Field<Int32?>("RESPONDED_COUNT");
                    newdata.REJECTED_COUNT = row.Field<Int32?>("REJECTED_COUNT");
                    newdata.OTHER_ATTACH_CNT = row.Field<Int32?>("OTHER_ATTACH_CNT");
                    newdata.ALL_ATTACH_CNT = row.Field<Int32?>("ALL_ATTACH_CNT");
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Field<int?>("RemarkCount"))) && row.Field<int?>("RemarkCount") > 0)
                        newdata.Remarks = true;
                    else
                        newdata.Remarks = false;

                    newdata.VOUCHING_PENDING_COUNT = row.Field<Int32?>("VOUCHING_PENDING_COUNT");
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT");
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.VOUCHING_REJECTED_COUNT = row.Field<Int32?>("VOUCHING_REJECTED_COUNT");
                    newdata.VOUCHING_TOTAL_COUNT = row.Field<Int32?>("VOUCHING_TOTAL_COUNT");
                    newdata.AUDIT_PENDING_COUNT = row.Field<Int32?>("AUDIT_PENDING_COUNT");
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_COUNT");
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT");
                    newdata.AUDIT_REJECTED_COUNT = row.Field<Int32?>("AUDIT_REJECTED_COUNT");
                    newdata.AUDIT_TOTAL_COUNT = row.Field<Int32?>("AUDIT_TOTAL_COUNT");
                    newdata.Special_Ref = string.IsNullOrWhiteSpace(row["Special_Ref"].ToString()) ? "" : row["Special_Ref"].ToString();

                    newdata.iIcon = "";
                    
                    if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                    {
                        newdata.iIcon += "GreenShield|";
                    }
                    else if ((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                    {
                        newdata.iIcon += "YellowShield|";
                    }
                    else if (((((row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (row.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((row.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                    {
                        newdata.iIcon += "BlueShield|";
                    }
                    if (((row.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                    {
                        newdata.iIcon += "RedFlag|";
                    }
                    if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                    {
                        newdata.iIcon += "RequiredAttachment|";
                    }
                    else if ((((row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                    {
                        newdata.iIcon += "AdditionalAttachment|";
                    }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAccepted|"; }
                    if ((row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingReject|"; }
                    if ((row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "VouchingAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "VouchingPartial|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAccepted|"; }
                    if ((row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditReject|"; }
                    if ((row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newdata.iIcon += "AuditAcceptWithRemarks|"; }
                    if ((row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newdata.iIcon += "AuditPartial|"; }
                    if ((row.Field<Int32?>("IS_AUTOVOUCHING")) > 0) { newdata.iIcon += "AutoVouching|"; }
                    if ((row.Field<Int32?>("IS_CORRECTED_ENTRY")) > 0) { newdata.iIcon += "CorrectedEntry|"; }
                    AssetList.Add(newdata);
                }
            }
            return AssetList;
        }

        public static List<VoucherTypeItems> DataTabletoVoucherReceiptLookUp_GetItemList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new VoucherTypeItems
            {
                ITEMID = row.Field<string>("ITEM_ID"),
                ITEMNAME = row.Field<string>("ITEM_NAME"),
                LED_NAME = row.Field<string>("LED_NAME"),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
                ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ"),
                ITEM_PROFILE = row.Field<string>("ITEM_PROFILE"),
                ITEM_CON_LED_ID = row.Field<string>("ITEM_CON_LED_ID"),
                ITEM_CON_MIN_VALUE = row.Field<int?>("ITEM_CON_MIN_VALUE"),
                ITEM_CON_MAX_VALUE = row.Field<int?>("ITEM_CON_MAX_VALUE"),
                ITEM_TDS_CODE = row.Field<string>("ITEM_TDS_CODE"),
                ITEM_LINK_REC_ID = row.Field<string>("ITEM_LINK_REC_ID"),
                ITEM_OFFSET_REC_ID = row.Field<string>("ITEM_OFFSET_REC_ID"),
                ITEM_OFFSET_NAME = row.Field<string>("ITEM_OFFSET_NAME"),
                //ITEM_ID = row.Field<string>("ITEM_ID").ToString(),
            }).ToList();
            else return null;
        }

        public static List<PlaceOwners> DataTabletoServicePlacesLookUp_GetOwnerList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new PlaceOwners
            {
                NAME = row.Field<string>("NAME"),
                BUILDING = row.Field<string>("BUILDING"),
                HOUSE_NO = row.Field<string>("HOUSE NO"),
                AREA_STREET = row.Field<string>("AREA/STREET"),
                DISTRICT = row.Field<string>("DISTRICT"),
                MOBILE = row.Field<string>("MOBILE"),
                ID = row.Field<string>("ID"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
                
                
            }).ToList();
            else return null;
        }

        public static List<ResponsiblePerson> DataTabletoServicePlacesLookUp_GetResponsibleList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ResponsiblePerson
            {
                NAME = row.Field<string>("NAME"),
                BUILDING = row.Field<string>("BUILDING"),
                HOUSE_NO = row.Field<string>("HOUSE NO"),
                AREA_STREET = row.Field<string>("AREA/STREET"),
                DISTRICT = row.Field<string>("DISTRICT"),
                MOBILE = row.Field<string>("MOBILE"),
                ID = row.Field<string>("ID"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),


            }).ToList();
            else return null;
        }

        public static List<ConnectOneMVC.Areas.Profile.Models.Subscribers_ww> DataTabletoMembershipLookUp_GetSubsList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscribers_ww
            {
                SI_NAME = row.Field<string>("SI_NAME"),
                SI_CATEGORY = row.Field<string>("SI_CATEGORY"),
                SI_START_MONTH = row.Field<Int32?>("SI_START_MONTH"),
                SI_TOTAL_MONTH = row.Field<Int32?>("SI_TOTAL_MONTH"),
                SI_REC_ID = row.Field<string>("SI_REC_ID"),
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Profile.Models.Subscribers_wow> DataTabletoMembershipLookUp_GetSubsList_Wow(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscribers_wow
            {
                SI_NAME = row.Field<string>("SI_NAME"),
                SI_CATEGORY = row.Field<string>("SI_CATEGORY"),
                SI_START_MONTH = row.Field<Int32?>("SI_START_MONTH"),
                SI_TOTAL_MONTH = row.Field<Int32?>("SI_TOTAL_MONTH"),
                SI_REC_ID = row.Field<string>("SI_REC_ID"),
            }).ToList();
            else return null;
        }

        public static List<ConnectOneMVC.Areas.Profile.Models.WingsList_ww> DataTabletoMembershipLookUp_GetWingList(DataTable dt)
        {
            List<WingsList_ww> WingListItems = new List<WingsList_ww>();
            if (dt != null) //return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Profile.Models.WingsList
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row.Field<string>("WING_REC_ID") != null)
                    {
                        var newdata = new WingsList_ww();
                        newdata.NAME = row.Field<string>("WING_NAME");
                        newdata.WING_SHORT_MS = row.Field<string>("WING_SHORT_MS");
                        newdata.ID = row.Field<string>("WING_REC_ID");
                        WingListItems.Add(newdata);
                    }
                }
            }
            return WingListItems;
        }

        public static List<VoucherTypeItems> DataTabletoVoucherCollectionLookUp_GetItemList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new VoucherTypeItems
            {
                ITEMID = row.Field<string>("ITEM_ID"),
                ITEMNAME = row.Field<string>("ITEM_NAME"),
                LED_NAME = row.Field<string>("LED_NAME"),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
            }).ToList();
            else return null;
        }

        public static List<ReceiptPartyList> DataTabletoVoucherReceiptLookUp_GetPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new ReceiptPartyList
            {
                C_ID = row.Field<string>("C_ID"),
                C_NAME = row.Field<string>("C_NAME").ToString(),
                C_PAN_NO = row.Field<string>("C_PAN_NO"),
                C_CITY = row.Field<string>("C_CITY"),
                REC_Edit_ON = row.Field<DateTime?>("REC_Edit_ON")

            }).ToList();
            else return null;
        }

        public static List<CollectionBoxPartyList> DataTabletoVoucherCollectionBoxLookUp_GetPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new CollectionBoxPartyList
            {
                C_ID = row.Field<string>("C_ID"),
                C_NAME = row.Field<string>("C_NAME").ToString(),
                C_OCCUPATION = row.Field<string>("C_OCCUPATION"),
                REC_Edit_ON = row.Field<DateTime?>("REC_Edit_ON")

            }).ToList();
            else return null;
        }

        public static List<JournalPartyList> DataTabletoJournalVoucherLookUp_GetPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new JournalPartyList
            {
                
                Name = row.Field<string>("Name").ToString(),
                PAN = row.Field<string>("PAN"),
                ID = row.Field<string>("ID"),
                REC_Edit_ON = row.Field<DateTime?>("REC_Edit_ON"),
                C_UID_NO = row.Field<string>("C_UID_NO"),
                C_OTHER_ID = row.Field<string>("C_OTHER_ID"),
                C_OTHER_ID_LABEL = row.Field<string>("C_OTHER_ID_LABEL"),
                C_CATEGORY = row.Field<string>("C_CATEGORY")

            }).ToList();
            else return null;
        }

        public static List<RefBank> DataTabletoReciptRefBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new RefBank
            {
                BI_ID = row.Field<string>("BI_ID"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
            }).ToList();
            else return null;
        }

        public static List<RefBank> DataTabletoCollectionBoxRefBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new RefBank
            {
                BI_ID = row.Field<string>("BI_ID"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
            }).ToList();
            else return null;
        }

        public static List<DonationVocuherPurposeList> DataTabletoDonationVoucherPurposeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new DonationVocuherPurposeList
            {
                PUR_NAME = row.Field<string>("PUR_NAME"),
                PUR_ID = row.Field<string>("PUR_ID"),
            }).ToList();
            else return null;
        }

        public static List<DonationVoucherPartyList> DataTabletoDonationVoucherPartyList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new DonationVoucherPartyList
            {
                C_ID = row.Field<string>("C_ID"),
                C_NAME = row.Field<string>("C_NAME"),
                C_PASSPORT_NO = row.Field<string>("C_PASSPORT_NO"),
                C_PAN_NO = row.Field<string>("C_PAN_NO"),
                CI_NAME = row.Field<string>("CI_NAME"),
                CO_NAME = row.Field<string>("CO_NAME"),
                C_R_ADD1 = row.Field<string>("C_R_ADD1"),
                C_R_ADD2 = row.Field<string>("C_R_ADD2"),
                C_R_ADD3 = row.Field<string>("C_R_ADD3"),
                C_R_ADD4 = row.Field<string>("C_R_ADD4"),
                C_R_PINCODE = row.Field<string>("C_R_PINCODE"),
                ST_NAME = row.Field<string>("ST_NAME"),
                DI_NAME = row.Field<string>("DI_NAME"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            }).ToList();
            else return null;
        }
        public static List<ReceiptPurposeList> DataTabletoVoucherReceiptLookUp_GetPurposeList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new ReceiptPurposeList
            {
                PUR_ID = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME")

            }).ToList();
            else return null;
        }

        public static List<ReferenceBankList> DataTabletoDonationVoucherRefBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new ReferenceBankList
            {
                BI_ID = row.Field<string>("BI_ID"),
                //RefBankList = row.Field<string>("BI_ID"),
                BI_BANK_NAME = row.Field<string>("BI_BANK_NAME"),
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
            }).ToList();
            else return null;
        }

        public static List<DonationVoucherItemList> DataTabletoDonationVoucherItemList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new DonationVoucherItemList
            {
                ITEM_ID = row.Field<string>("ITEM_ID"),
                //RefBankList = row.Field<string>("BI_ID"),
                ITEM_NAME = row.Field<string>("ITEM_NAME"),
                LED_NAME = row.Field<string>("LED_NAME"),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
            }).ToList();
            else return null;
        }

        public static List<ReceiptAdjustmentList> DataTabletoVoucherReceiptGLookUp_Adjustment_List(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new ReceiptAdjustmentList
            {
                REF_ID = row.Field<string>("REF_ID"),
                REF_AMT = row.Field<decimal?>("REF_AMT"),
                REF_OUTSTAND = row.Field<decimal?>("REF_OUTSTAND"),

                REF_DATE = row.Field<DateTime?>("REF_DATE"),
                REF_ADDITION = row.Field<decimal?>("REF_ADDITION"),
                REF_ADJUSTED = row.Field<decimal?>("REF_ADJUSTED"),
                REF_REFUND = row.Field<decimal?>("REF_REFUND"),
                REF_OUTSTAND_NEXT_YEAR = row.Field<decimal?>("REF_OUTSTAND_NEXT_YEAR"),
                REF_ITEM = row.Field<string>("REF_ITEM"),
                REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
                REF_PURPOSE = row.Field<string>("REF_PURPOSE"),
                REF_OTHER_DETAIL = row.Field<string>("REF_OTHER_DETAIL")
            }).ToList();
            else return null;
        }
        public static List<FDType> DataTabletoFdType(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new FDType
            {
                ITEMID = row.Field<string>("ITEMID"),
                FDActivity = row.Field<string>("FD Activity"),
                Sr = row.Field<int>("Sr")
            }).ToList();
            else return null;
        }
        public static List<FDVoucherItems> DataTabletoFDVoucher_GetItemList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new FDVoucherItems
            {
                ITEMID = row.Field<string>("ITEM_ID"),
                ITEMNAME = row.Field<string>("ITEM_NAME").ToString(),
                LED_NAME = row.Field<string>("LED_NAME").ToString(),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE").ToString(),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID").ToString(),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE").ToString(),
                //ITEM_PARTY_REQ = row.Field<string>("ITEM_PARTY_REQ").ToString(),
            }).ToList();
            else return null;
        }
        public static List<FdListItems> DataTabletoFDVoucher_FDList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new FdListItems
            {
                BI_SHORT_NAME = row.Field<string>("BI_SHORT_NAME"),
                BANK_NAME = row.Field<string>("BANK_NAME"),
                BANK_BRANCH = row.Field<string>("BANK_BRANCH"),
                BANK_CUST_NO = row.Field<string>("BANK_CUST_NO"),
                BA_ID = row.Field<string>("BA_ID"),
                FD_ID = row.Field<string>("FD_ID"),
                FD_NO = row.Field<string>("FD_NO"),
                FD_AMOUNT = row.Field<decimal?>("FD_AMOUNT"),
                MATURITY_AMOUNT = row.Field<decimal?>("MATURITY_AMOUNT"),
                MATURITY_DATE = row.Field<DateTime?>("MATURITY_DATE"),
                FD_DATE = row.Field<DateTime?>("FD_DATE"),
                FD_AS_DATE = row.Field<DateTime?>("FD_AS_DATE"),
                FD_STATUS = row.Field<string>("FD_STATUS"),
                ROI = row.Field<decimal?>("ROI"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON")
            }).ToList();
            else return null;
        }


        public static List<AssetTransfer_FR_Ins_List> DataTabletoVoucherAssetTransferLookUp_GetFRCenterList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_FR_Ins_List
            {
                FR_CEN_ID = row.Field<int>("FR_CEN_ID"),
                FR_ID = row.Field<string>("FR_ID"),
                FR_CEN_NAME = row.Field<string>("FR_CEN_NAME"),
                FR_INCHARGE = row.Field<string>("FR_INCHARGE"),
                FR_PAD_NO = row.Field<string>("FR_PAD_NO"),
                FR_TEL_NO = row.Field<string>("FR_TEL_NO"),
                FR_UID = row.Field<string>("FR_UID"),
                FR_ZONE = row.Field<string>("FR_ZONE")               
            }).ToList();
            else return null;
        }

        public static List<AssetTransfer_TO_Ins_List> DataTabletoVoucherAssetTransferLookUp_GetTOCenterList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_TO_Ins_List
            {
                TO_CEN_ID = row.Field<int>("TO_CEN_ID"),
                TO_ID = row.Field<string>("TO_ID"),
                TO_CEN_NAME = row.Field<string>("TO_CEN_NAME"),
                TO_INCHARGE = row.Field<string>("TO_INCHARGE"),
                TO_PAD_NO = row.Field<string>("TO_PAD_NO"),
                TO_TEL_NO = row.Field<string>("TO_TEL_NO"),
                TO_UID = row.Field<string>("TO_UID"),
                TO_ZONE = row.Field<string>("TO_ZONE")                
            }).ToList();
            else return null;
        }

        public static List<AssetTransfer_LocationList> DataTabletoVoucherAssetTransferLookUp_GetLocationList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_LocationList
            {
                LocationName = row.Field<string>("Location Name"),
                AL_ID = row.Field<string>("AL_ID"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                MatchedType = row.Field<string>("Matched Type"),
                MatchedName = row.Field<string>("Matched Name"),
                MatchedInstt = row.Field<string>("Matched Instt."),
                Final_Amount = row.Field<decimal?>("Final_Amount"),
            }).ToList();
            else return null;
        }

        public static List<AssetTransfer_PurList> DataTabletoVoucherAssetTransferLookUp_GetPurList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_PurList
            {
                PUR_ID = row.Field<string>("PUR_ID"),
                PUR_NAME = row.Field<string>("PUR_NAME"),
            }).ToList();
            else return null;
        }

        public static List<AssetTransfer_OwnerList> DataTabletoVoucherAssetTransferLookUp_GetOwnerList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_OwnerList
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
                Organization = row.Field<string>("Organization"),
                Status = row.Field<string>("Status"),
            }).ToList();
            else return null;
        }

        public static List<InternalTransferPurposeList> DataTabletoInternalTransferPurposeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferPurposeList
            {
                PUR_NAME = row.Field<string>("PUR_NAME"),
                PUR_ID = row.Field<string>("PUR_ID"),
            }).ToList();
            else return null;
        }    

        public static List<InternalTransferItemList> DataTabletoInternalTransferItemList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferItemList
            {
                ITEM_ID = row.Field<string>("ITEM_ID"),
                //RefBankList = row.Field<string>("BI_ID"),
                ITEM_NAME = row.Field<string>("ITEM_NAME"),
                LED_NAME = row.Field<string>("LED_NAME"),
                ITEM_TRANS_TYPE = row.Field<string>("ITEM_TRANS_TYPE"),
                ITEM_LED_ID = row.Field<string>("ITEM_LED_ID"),
                ITEM_VOUCHER_TYPE = row.Field<string>("ITEM_VOUCHER_TYPE"),
            }).ToList();
            else return null;
        }

        public static List<InternalTransferTrfBankList> DataTabletoInternalTransferTrfBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferTrfBankList
            {
                TRF_BI_ID = row.Field<string>("TRF_BI_ID"),
                //RefBankList = row.Field<string>("BI_ID"),
                TRF_BI_BANK_NAME = row.Field<string>("TRF_BI_BANK_NAME"),
                TRF_BI_SHORT_NAME = row.Field<string>("TRF_BI_SHORT_NAME"),

            }).ToList();
            else return null;
        }
        public static List<InternalTransferTrfBankList> DataTabletoInternalTransferBankList(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferTrfBankList
            {
                TRF_BI_ID = row.Field<string>("TRF_BI_ID"),
                TRF_BI_BANK_NAME = row.Field<string>("TRF_BI_BANK_NAME"),
                TRF_BI_SHORT_NAME = row.Field<string>("TRF_BI_SHORT_NAME"),              
                BA_FERA_ACC = dt.Columns.Contains("BA_FERA_ACC")?row.Field<string>("BA_FERA_ACC"):null,
                TRF_BB_BRANCH_NAME = dt.Columns.Contains("TRF_BB_BRANCH_NAME") ? row.Field<string>("TRF_BB_BRANCH_NAME") : null,
                TRF_BA_ACCOUNT_NO = dt.Columns.Contains("TRF_BA_ACCOUNT_NO") ? row.Field<string>("TRF_BA_ACCOUNT_NO") : null,
                BA_REC_ID = dt.Columns.Contains("BA_REC_ID") ? row.Field<string>("BA_REC_ID") : null,
                TRF_IFSC_CODE = dt.Columns.Contains("TRF_IFSC_CODE") ? row.Field<string>("TRF_IFSC_CODE") : null,
                TRF_STATUS = dt.Columns.Contains("TRF_STATUS") ? row.Field<string>("TRF_STATUS") : null,
                REC_EDIT_ON = dt.Columns.Contains("REC_EDIT_ON") ? row.Field<DateTime?>("REC_EDIT_ON") :(DateTime?)null               
            }).ToList();
            else return null;
        }


        public static List<InternalTransferFromCenterList> DataTabletoInternalTransferFromCenterList(DataTable dt)
        {
            return Extensions.ToList<InternalTransferFromCenterList>(dt);
            //List<DataRow> list = dt.AsEnumerable().ToList();
            //if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferFromCenterList
            //{
            //    FR_UID = row.Field<string>("FR_UID"),
            //    //RefBankList = row.Field<string>("BI_ID"),
            //    FR_CEN_NAME = row.Field<string>("FR_CEN_NAME"),
            //    FR_PAD_NO = row.Field<string>("FR_PAD_NO"),
            //    FR_INCHARGE = row.Field<string>("FR_INCHARGE"),
            //    FR_ZONE = row.Field<string>("FR_ZONE"),
            //    FR_CEN_ID = row.Field<int>("FR_CEN_ID"),
            //    FR_ID = row.Field<string>("FR_ID"),
            //    FR_TEL_NO = row.Field<string>("FR_TEL_NO"),

            //}).ToList();
            //else return null;
        }


        public static List<AssetTransfer_AssetList_GOLD> DataTabletoVoucherAssetTransferLookUp_GetAssetList_GOLD(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_GOLD>(dt);
            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_GOLD
            //{
            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<decimal>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_MISC_ID = row.Field<string>("REF_MISC_ID"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),
            //    REF_TRANS_TYPE = row.Field<string>("REF_TRANS_TYPE"),
            //    REF_LOC_ID = row.Field<string>("REF_LOC_ID"),
            //    REF_OWNERSHIP = row.Field<string>("REF_OWNERSHIP"),
            //    REF_OWNERSHIP_ID = row.Field<string>("REF_OWNERSHIP_ID"),
            //    REF_USE = row.Field<string>("REF_USE"),
            //    REF_AMT = row.Field<decimal>("REF_AMT"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;
        }

        public static List<AssetTransfer_AssetList_SILVER> DataTabletoVoucherAssetTransferLookUp_GetAssetList_SILVER(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_SILVER>(dt);
            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_SILVER
            //{
            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<decimal>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_MISC_ID = row.Field<string>("REF_MISC_ID"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),
            //    REF_TRANS_TYPE = row.Field<string>("REF_TRANS_TYPE"),
            //    REF_ID = row.Field<string>("REF_ID"),
            //    SALE_QTY = row.Field<decimal>("SALE_QTY"),
            //    REF_LOC_ID = row.Field<string>("REF_LOC_ID"),
            //    REF_AMT = row.Field<decimal>("REF_AMT"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;
        }

        public static List<AssetTransfer_AssetList_VEHICLES> DataTabletoVoucherAssetTransferLookUp_GetAssetList_VEHICLES(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_VEHICLES>(dt);
            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_VEHICLES
            //{
            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<decimal>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_MISC_ID = row.Field<string>("REF_MISC_ID"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),
            //    REF_TRANS_TYPE = row.Field<string>("REF_TRANS_TYPE"),
            //    REF_LOC_ID = row.Field<string>("REF_LOC_ID"),
            //    REF_OWNERSHIP = row.Field<string>("REF_OWNERSHIP"),
            //    REF_OWNERSHIP_ID = row.Field<string>("REF_OWNERSHIP_ID"),
            //    SALE_QTY = row.Field<decimal>("REF_OWNERSHIP_ID"),
            //    REF_USE = row.Field<string>("REF_USE"),
            //    REF_AMT = row.Field<decimal>("REF_AMT"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;
        }

        public static List<AssetTransfer_AssetList_LIVESTOCK> DataTabletoVoucherAssetTransferLookUp_GetAssetList_LIVESTOCK(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_LIVESTOCK>(dt);
            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_LIVESTOCK
            //{
            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<decimal>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_MISC_ID = row.Field<string>("REF_MISC_ID"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),
            //    REF_TRANS_TYPE = row.Field<string>("REF_TRANS_TYPE"),
            //    REF_ID = row.Field<string>("REF_ID"),
            //    SALE_QTY = row.Field<decimal>("SALE_QTY"),
            //    REF_LOC_ID = row.Field<string>("REF_LOC_ID"),
            //    REF_AMT = row.Field<decimal>("REF_AMT"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;
        }

        public static List<AssetTransfer_AssetList_MOVABLE_ASEETS> DataTabletoVoucherAssetTransferLookUp_GetAssetList_MOVABLE_ASEETS(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_MOVABLE_ASEETS>(dt);
        }
        public static List<ItemList_Itm_Dtel> DataTableto_ItemList_Itm_Dtel(DataTable dt) 
        {
            return Extensions.ToList<ItemList_Itm_Dtel>(dt);
        }
        public static List<ItemList_JV_Itm> DataTableto_ItemList_Itm_JV(DataTable dt) 
        {
            return Extensions.ToList<ItemList_JV_Itm>(dt);
        }
        public static List<AssetTransfer_AssetList_LAND_AND_BUILDING> DataTabletoVoucherAssetTransferLookUp_GetAssetList_LAND_AND_BUILDING(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_LAND_AND_BUILDING>(dt);

            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_LAND_AND_BUILDING
            //{
            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<decimal>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_MISC_ID = row.Field<string>("REF_MISC_ID"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),
            //    REF_TRANS_TYPE = row.Field<string>("REF_TRANS_TYPE"),
            //    REF_ID = row.Field<string>("REF_ID"),
            //    SALE_QTY = row.Field<decimal>("SALE_QTY"),
            //    REF_OWNERSHIP = row.Field<string>("REF_OWNERSHIP"),
            //    REF_OWNERSHIP_ID = row.Field<string>("REF_OWNERSHIP_ID"),
            //    REF_USE = row.Field<string>("REF_USE"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;
        }
        public static List<AssetTransfer_AssetList_FD> DataTabletoVoucherAssetTransferLookUp_GetAssetList_FD(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList_FD>(dt);
            //if (dt != null) return dt.AsEnumerable().Select(row => new AssetTransfer_AssetList_FD
            //{



            //    REF_ITEM = row.Field<string>("REF_ITEM"),
            //    REF_ITEM_ID = row.Field<string>("REF_ITEM_ID"),
            //    REF_QTY = row.Field<int>("REF_QTY"),
            //    REF_DESC = row.Field<string>("REF_DESC"),
            //    REF_LED_ID = row.Field<string>("REF_LED_ID"),               
            //    REF_ID = row.Field<string>("REF_ID"),
            //    FD_DATE = row.Field<DateTime>("FD_DATE"),
            //    FD_AS_DATE = row.Field<DateTime>("FD_AS_DATE"),
            //    REF_AMT = row.Field<decimal>("REF_AMT"),
            //    FD_INT_RATE = row.Field<decimal>("FD_INT_RATE"),
            //    FD_INT_PAY_COND = row.Field<string>("FD_INT_PAY_COND"),
            //    FD_MAT_DATE = row.Field<DateTime>("FD_MAT_DATE"),
            //    FD_MAT_AMT = row.Field<decimal>("FD_MAT_AMT"),
            //    BA_CUST_NO = row.Field<string>("BA_CUST_NO"),
            //    REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
            //    REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE"),
            //}).ToList();
            //else return null;

        }
        public static List<InternalTransferToCenterList> DataTabletoInternalTransferToCenterList(DataTable dt)
        {
            //return Extensions.ToList<InternalTransferToCenterList>(dt);

            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferToCenterList
            {
                TO_CEN_ID = row.Field<int>("TO_CEN_ID"),
                TO_ID = row.Field<string>("TO_ID"),
                TO_CEN_NAME = row.Field<string>("TO_CEN_NAME"),
                TO_INCHARGE = row.Field<string>("TO_INCHARGE"),
                TO_PAD_NO = row.Field<string>("TO_PAD_NO"),
                TO_TEL_NO = row.Field<string>("TO_TEL_NO"),
                TO_UID = row.Field<string>("TO_UID"),
                TO_ZONE = row.Field<string>("TO_ZONE"),

            }).ToList();
            else return null;
        }
        public static List<InternalTransferCenterList> DataTabletoInternalTransferCenterList(DataTable dt)
        {
            //return Extensions.ToList<InternalTransferToCenterList>(dt);

            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new InternalTransferCenterList
            {
                TO_CEN_ID = row.Field<int?>("TO_CEN_ID"),
                TO_ID = row.Field<string>("TO_ID"),
                TO_CEN_NAME = row.Field<string>("TO_CEN_NAME"),
                TO_INCHARGE = row.Field<string>("TO_INCHARGE"),
                TO_PAD_NO = row.Field<string>("TO_PAD_NO"),
                TO_TEL_NO = row.Field<string>("TO_TEL_NO"),
                TO_UID = row.Field<string>("TO_UID"),
                TO_ZONE = row.Field<string>("TO_ZONE"),

            }).ToList();
            else return null;
        }
 
     

      

    
    
     
      
        public static List<PaymentVoucher_Frm_Vehicles_Window_AssetLocations_Model> DataTabletoPaymentVoucher_Frm_Vehicles_Window_AssetLocations_Model(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new PaymentVoucher_Frm_Vehicles_Window_AssetLocations_Model
            {
                AL_ID = row.Field<string>("AL_ID"),
                Final_Amount = row.Field<decimal?>("Final_Amount"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                Location_Name = row.Field<string>("Location Name"),
                Matched_Instt = row.Field<string>("Matched Instt."),
                Matched_Name = row.Field<string>("Matched Name"),
                Matched_Type = row.Field<string>("Matched Type"),

            }).ToList();
            else return null;
        }


    
        public static List<Frm_Live_Stock_Window_Look_LocList> DataTable_Frm_Live_Stock_Window_Look_LocListTOModel(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Frm_Live_Stock_Window_Look_LocList
            {
                AL_ID = row.Field<string>("AL_ID"),
                Final_Amount = row.Field<decimal?>("Final_Amount"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                Location_Name = row.Field<string>("Location Name"),
                Matched_Instt = row.Field<string>("Matched Instt."),
                Matched_Name = row.Field<string>("Matched Name"),
                Matched_Type = row.Field<string>("Matched Type"),

            }).ToList();
            else return null;
        }
        public static List<PaymentVoucher_Frm_Window_Select_SelectLocationList> DataTabletoPaymentVoucher_Frm_Asset_Window_Select_SelectLocationList(DataTable dt)
        {
            //return Extensions.ToList<PaymentVoucher_Frm_Window_Select_SelectLocationList>(dt);
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new PaymentVoucher_Frm_Window_Select_SelectLocationList
            {
                AL_ID = row.Field<string>("AL_ID"),
                Final_Amount = row.Field<decimal?>("Final_Amount"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                LocationName = row.Field<string>("Location Name"),
                MatchedInstt = row.Field<string>("Matched Instt."),
                MatchedName = row.Field<string>("Matched Name"),
                MatchedType = row.Field<string>("Matched Type"),

            }).ToList();
            else return null;
        }
        public static List<PaymentVoucher_Frm_Window_Select_SelectLocationList> DataTabletoPaymentVoucher_Frm_Vehicle_Window_SelectLocationList(DataTable dt)
        {
            //return Extensions.ToList<PaymentVoucher_Frm_Window_Select_SelectLocationList>(dt);
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new PaymentVoucher_Frm_Window_Select_SelectLocationList
            {
                AL_ID = row.Field<string>("AL_ID"),
                Final_Amount = row.Field<decimal?>("Final_Amount"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                LocationName = row.Field<string>("Location Name"),
                MatchedInstt = row.Field<string>("Matched Instt."),
                MatchedName = row.Field<string>("Matched Name"),
                MatchedType = row.Field<string>("Matched Type"),

            }).ToList();
            else return null;
        }
        public static List<Center_INFO> DataTabletoCenter_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Center_INFO
            {
                CEN_NAME = row.Field<string>("CEN_NAME"),
                CEN_BK_PAD_NO = row.Field<string>("CEN_BK_PAD_NO"),
                CEN_INCHARGE = row.Field<string>("CEN_INCHARGE"),
                CEN_ZONE_ID = row.Field<string>("CEN_ZONE_ID"),
                CEN_ID = row.Field<int?>("CEN_ID"),


            }).ToList();
            else return null;
        }

        public static List<Payment_Frm_Location_Map_Window_ProList_Model> DataTabletoFrm_Location_Map_Window_ProList(DataTable dt)
        {
            //return Extensions.ToList<InternalTransferToCenterList>(dt);

            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Payment_Frm_Location_Map_Window_ProList_Model
            {
                LB_ID = row.Field<string>("LB_ID"),
                Institute = row.Field<string>("Institute"),
                TYPE = row.Field<string>("TYPE"),
                PROP_NAME = row.Field<string>("PROP_NAME"),
                CATEGORY = row.Field<string>("CATEGORY"),
                LED_ID = row.Field<string>("LED_ID"),
                CURR_VALUE = row.Field<decimal>("CURR_VALUE"),
                OWNERSHIP = row.Field<string>("OWNERSHIP"),
                USE_OF_PROPERTY = row.Field<string>("USE OF PROPERTY"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),

            }).ToList();
            else return null;
        }

        public static List<Payment_Frm_Location_Map_Window_SerList_Model> DataTabletoFrm_Location_Map_Window_SerList(DataTable dt)
        {
            //return Extensions.ToList<InternalTransferToCenterList>(dt);

            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Payment_Frm_Location_Map_Window_SerList_Model
            {
                SP_ID = row.Field<string>("SP_ID"),
                Institute = row.Field<string>("Institute"),
                Centre_No = row.Field<string>("Centre No."),
                UID = row.Field<string>("UID"),
                No = row.Field<string>("No."),
                Service_Place_Name = row.Field<string>("Service Place Name"),
                Place_Type = row.Field<string>("Place Type"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),

            }).ToList();
            else return null;
        }
  
        public static List<ConnectOneMVC.Areas.Magazine.Models.LookUp_GetDispatch_info> DataTabletoDispatchTypeList_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Magazine.Models.LookUp_GetDispatch_info
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
                //Default = row.Field<string>("Default"),
                Action_Status = row.Field<string>("Action Status"),
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Profile.Models.Documents_List_Info> DataTabletoDocuments_List_Info(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Profile.Models.Documents_List_Info
            {
                ID = row.Field<string>("ID"),
                Name = row.Field<string>("Name"),
            }).ToList();
            else return null;
        }

        public static List<AdvanceAdjustment_Grid_Datatable> DataTabletoAdvanceAdjustment(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new AdvanceAdjustment_Grid_Datatable
            {
                Sr = row.Field<Int64>("Sr"),
                GivenDate = row.Field<DateTime?>("Given Date"),
                Item = row.Field<string>("Item"),
                AI_ITEM_ID = row.Field<string>("AI_ITEM_ID"),
                OFFSET_ID = row.Field<string>("OFFSET_ID"),
                Advance = row.Field<decimal?>("Advance"),
                Addition = row.Field<decimal?>("Addition"),
                Adjusted = row.Field<decimal?>("Adjusted"),
                Refund = row.Field<decimal?>("Refund"),
                Out_Standing = row.Field<decimal?>("Out-Standing"),
                Next_Year_Out_Standing = row.Field<decimal>("Next Year Out-Standing"),
                Payment = row.Field<decimal?>("Payment"),
                Purpose = row.Field<string>("Purpose"),
                Other_Details = row.Field<string>("Other Details"),
                AI_ID = row.Field<string>("AI_ID"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
                // REF_CREATION_DATE = row.Field<DateTime>("REF_CREATION_DATE"),

            }).ToList();
            else return null;
        }

        public static List<Frm_Voucher_Win_Gen_Pay_LB_Grid4_Model> DataTabletoPayableAdjustment(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Frm_Voucher_Win_Gen_Pay_LB_Grid4_Model
            {
                Sr = row.Field<Int64>("Sr"),
                Addition = row.Field<decimal?>("Addition"),
                Adjusted = row.Field<decimal?>("Adjusted"),
                Amount = row.Field<decimal?>("Amount"),
                Date = row.Field<DateTime?>("Date"),
                Item = row.Field<string>("Item"),
                LI_ID = row.Field<string>("LI_ID"),
                LI_ITEM_ID = row.Field<string>("LI_ITEM_ID"),
                Next_Year_OutStanding = row.Field<decimal?>("Next Year Out-Standing"),
                OFFSET_ID = row.Field<string>("OFFSET_ID"),
                Other_Details = row.Field<string>("Other Details"),
                OutStanding = row.Field<decimal?>("Out-Standing"),
                Paid = row.Field<decimal?>("Paid"),
                Payment = row.Field<decimal?>("Payment"),
                Purpose = row.Field<string>("Purpose"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON"),
                REF_CREATION_DATE = row.Field<DateTime?>("REF_CREATION_DATE")
            }).ToList();
            else return null;
        }
        public static List<Payment_Window_Grid> DataTabletoPayment_Window_Grid_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Payment_Window_Grid
            {
                Sr = row.Field<int?>("Sr."),
                Mode = row.Field<string>("Mode"),
                Amount = row.Field<decimal?>("Amount"),
                Pmt_Date = row.Field<DateTime>("Pmt_Date"),
                Deposited_Bank = row.Field<string>("Deposited Bank")==null?"": row.Field<string>("Deposited Bank"),
                Deposited_Branch = row.Field<string>("Deposited Branch")==null?"": row.Field<string>("Deposited Branch"),
                Deposited_Bank_ID = row.Field<string>("Dep_Bank_ID")==null?"": row.Field<string>("Dep_Bank_ID"),
                Deposited_Ac_No = row.Field<string>("Deposited A/c. No.")==null?"": row.Field<string>("Deposited A/c. No."),
                Ref_No = row.Field<string>("Ref No.")==null?"": row.Field<string>("Ref No."),
                Ref_Date = row.Field<DateTime?>("Ref Date"),
                Ref_Clearing_Date = row.Field<DateTime?>("Ref Clearing Date"),
                Ref_Branch = row.Field<string>("Ref Branch"),
                RefBank_Name = row.Field<string>("Ref Bank"),
                Ref_Bank_ID = row.Field<string>("Ref_Bank_ID"),
                Pur_ID= row.Field<string>("Pur_ID"),
                Narration = row.Field<string>("Narration"),
                Reference = row.Field<string>("Reference"),
                Tr_M_ID= row.Field<string>("Tr_M_ID"),
                UPDATED= row.Field<Boolean>("UPDATED"),
                REC_GENERATED = row.Field<Boolean>("REC_GENERATED"),
                TR_CODE = row.Field<int?>("TR_CODE"),
                Type = row.Field<string>("Type"),
                REC_ID = row.Field<string>("REC_ID"),
                REC_ADD_ON = row.Field<DateTime?>("REC_ADD_ON"),
                REC_ADD_BY = row.Field<string>("REC_ADD_BY"),
                REC_EDIT_ON = row.Field<DateTime>("REC_EDIT_ON"),
                REC_EDIT_BY = row.Field<string>("REC_EDIT_BY"),
                RECEIPT_NO = row.Field<string>("RECEIPT_NO"),
                RECEIPT_ID = row.Field<string>("RECEIPT_ID"),
            }).ToList();
            else return null;
        }
        public static List<Magazine_Dispatch_Detail_Grid> DataTabletoDispatch_Window_Grid_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Magazine_Dispatch_Detail_Grid
            {
                Sr = row.Field<Int64>("Sr."),
                Date = row.Field<DateTime?>("Date"),
                Copies = row.Field<int>("Copies"),
                Disp_Count = row.Field<int>("Disp. Count"),
                Mode = row.Field<string>("Mode"),
                Mode_ID = row.Field<string>("Mode_ID"),
                Status = row.Field<string>("Status"),
                Remarks = row.Field<string>("Remarks"),
                Tr_M_ID = row.Field<string>("Tr_M_ID"),
                UPDATED = row.Field<int>("UPDATED"),

            }).ToList();
            else return null;
        }

        public static List<Subscription_Window_Grid> DataTabletoSubscription_Window_Grid_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new Subscription_Window_Grid
            {
                Sr = row.Field<int?>("Sr."),
                SubsType = row.Field<string>("Subs. Type"),
                Subs_ID = row.Field<string>("Subs_ID"),
                SubsDate = row.Field<DateTime?>("Subs. Date"),
                FrDate = row.Field<DateTime?>("Fr. Date"),
                ToDate = row.Field<DateTime?>("To Date"),
                Copies = row.Field<int>("Copies"),
                Free = row.Field<int>("Free"),
                SubAmt = row.Field<decimal?>("SubAmt"),
                TotalAmt = row.Field<decimal?>("Total Amt."),
                DispType = row.Field<string>("Disp. Type"),
                Dis_ID = row.Field<string>("Dis_ID"),
                DispAmt = row.Field<decimal?>("Disp.Amt"),
                DisponCC = row.Field<string>("Disp.on CC"),
                Pur_ID = row.Field<string>("Pur_ID"),
                Narration = row.Field<string>("Narration"),
                Reference = row.Field<string>("Reference"),
                Tr_M_ID = row.Field<string>("Tr_M_ID"),
                Type = row.Field<string>("Type"),
                UPDATED = row.Field<bool>("UPDATED"),
                REC_GENERATED = row.Field<bool>("REC_GENERATED"),
                TR_CODE = row.Field<int?>("TR_CODE"),
                REC_ID = row.Field<string>("REC_ID"),
                REC_ADD_ON = row.Field<DateTime?>("REC_ADD_ON").ToString(),
                REC_ADD_BY = row.Field<string>("REC_ADD_BY"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON").ToString(),
                REC_EDIT_BY = row.Field<string>("REC_EDIT_BY"),
                RECEIPT_NO = row.Field<string>("RECEIPT_NO"),
                RECEIPT_ID = row.Field<string>("RECEIPT_ID"),
                IS_LIFE = row.Field<bool>("IS_LIFE"),
                CURR_YEAR_INCOME = row.Field<decimal?>("CURR_YEAR_INCOME"),

            }).ToList();
            else return null;
        }

        public static List<ConnectOneMVC.Areas.Profile.Models.Property_Window_Grid> DataTabletoProperty_Window_Grid_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Profile.Models.Property_Window_Grid
            {
                Sr =row.Field<int?>("Sr."),
                Institution = row.Field<string>("Institution"),
                Ins_ID = row.Field<string>("Ins_ID"),
                Total_Plot_Area = row.Field<double?>("Total Plot Area (Sq.Ft.)"),
                Constructed_Area = row.Field<double?>("Constructed Area (Sq.Ft.)"),
                Construction_Year = row.Field<string>("Construction Year"),
                M_O_U_Date = row.Field<string>("M.O.U. Date"),
                Value = row.Field<double?>("Value"),
                Other_Detail = row.Field<string>("Other Detail"),
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Stock.Models.ItemProperties> GetItemProperties(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Stock.Models.ItemProperties
            {
                SrNo = row.Field<int>("Sr."),
                Property_Name = row.Field<string>("Property Name"),
                Property_Value = row.Field<string>("Property Value"),
                Remarks= row.Field<string>("Remarks"),
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Profile.Models.LookUp_GetInsList_Info> DataTabletoLookUp_GetInsList_INFO(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Profile.Models.LookUp_GetInsList_Info
            {
                ID = row.Field<string>("INS_ID"),
                Name = row.Field<string>("INS_Name"),
                Short_Name = row.Field<string>("INS_Short"),
            }).ToList();
            else return null;
        }

        public static List<Return_GetDocumentList> DataTabletoLookUp_GetDocumentList_INFO(DataTable dt)
        {
            string fileLocationPath = ConfigurationManager.AppSettings["FileStorePhysicalPath"].ToString();
            if (dt != null) return dt.AsEnumerable().Select(row => new Return_GetDocumentList
            {
                ID = row.Field<string>("ID"),
                FileName = row.Field<string>("Filename"),
                Title = row.Field<string>("Title"),
                Category = row.Field<string>("Category"),
                FileLocationPath = fileLocationPath + row.Field<string>("Filename")
            }).ToList();
            else return null;
        }

        public static List<ActionItems_GetTitleList_INFO> DataTabletoActionItems_GetTitleList_INFO(DataTable dt)
        {
            return Extensions.ToList<ActionItems_GetTitleList_INFO>(dt);
        }
        public static List<SaleTransferItemList> DataTabletoSaleTransferItemList(DataTable dt)
        {
            return Extensions.ToList<SaleTransferItemList>(dt);
        }
        public static List<ReferenceBankList> DataTableToSaleAssetRefBankList(DataTable dt)
        {
            return Extensions.ToList<ReferenceBankList>(dt);
        }
        public static List<SaleAssetPartyList> DataTableToSaleAssetPartyList(DataTable dt)
        {            
            if (dt != null) return dt.AsEnumerable().Select(row => new SaleAssetPartyList
            {
                C_ID = row.Field<string>("C_ID"),
                C_NAME = row.Field<string>("C_NAME"),
                C_CITY = row.Field<string>("C_CITY"),
                C_PAN_NO = row.Field<string>("C_PAN_NO"),
                REC_EDIT_ON = row.Field<DateTime?>("REC_EDIT_ON")
            }).ToList();
            else return null;
        }
        public static List<SaleAssetPurposeList> DataTabletoSaleAssetPurposeList(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new SaleAssetPurposeList
            {
                PUR_NAME = row.Field<string>("PUR_NAME"),
                PUR_ID = row.Field<string>("PUR_ID"),
            }).ToList();
            else return null;
        }
        public static List<SaleAsset_AssetList> DataTabletoVoucherSaleAssetLookUp_GetAssetList(DataTable dt)
        {
            return Extensions.ToList<SaleAsset_AssetList>(dt);
        }
        public static List<AssetTransfer_AssetList> DataTabletoVoucherAssetTransferLookUp_GetAssetList(DataTable dt)
        {
            return Extensions.ToList<AssetTransfer_AssetList>(dt);
        }
        public static List<Payment_Grid_Datatable> DataTabletoPaymentGrid(DataTable dt)
        {
            List<DataRow> list = dt.AsEnumerable().ToList();
            if (dt != null) return dt.AsEnumerable().Select(row => new Payment_Grid_Datatable
            {
                Sr = row.Field<int>("Sr."),
                Item_ID = row.Field<string>("Item_ID"),
                Item_Led_ID = row.Field<string>("Item_Led_ID"),
                Item_Trans_Type = row.Field<string>("Item_Trans_Type"),
                Item_Party_Req = row.Field<string>("Item_Party_Req"),
                Item_Profile = row.Field<string>("Item_Profile"),
                ItemName = row.Field<string>("Item Name"),
                Head = row.Field<string>("Head"),
                Qty = row.Field<decimal?>("Qty."),
                Unit = row.Field<string>("Unit"),
                Rate = row.Field<decimal?>("Rate"),
                Amount = row.Field<decimal>("Amount"),
                Remarks = row.Field<string>("Remarks"),
                TDS = row.Field<decimal>("TDS"),
            }).ToList();
            else return null;
        }
        public static List<MagazineList_LookUp> GetMagazineLis_Info(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new MagazineList_LookUp
            {
                Name = row.Field<string>("Name"),
                Short_Name = row.Field<string>("Short Name"),
                Language = row.Field<string>("Language"),
                Publish_On = row.Field<string>("Publish On"),
                Magazine_Regd_No = row.Field<string>("Magazine Regd. No."),
                Postal_Regd_No = row.Field<string>("Postal Regd. No."),
                Membership_Start_No = row.Field<int>("Membership Start No."),
                Foreign_Subscriptions = row.Field<string>("Foreign Subscriptions"),
                ID = row.Field<string>("ID"),
                Add_By = row.Field<string>("Add By"),
                Add_Date = row.Field<DateTime?>("Add Date"),
                Edit_By = row.Field<string>("Edit By"),
                Edit_Date = row.Field<DateTime?>("Edit Date"),
                Action_Status = row.Field<string>("Action Status"),
                Action_By = row.Field<string>("Action By"),
                Action_Date = row.Field<DateTime?>("Action Date")
            }).ToList();
            else return null;
        }
        public static List<ConnectOneMVC.Areas.Account.Models.Magazine> DataTabletoMagazineLookUp(DataTable dt)
        {
            if (dt != null) return dt.AsEnumerable().Select(row => new ConnectOneMVC.Areas.Account.Models.Magazine
            {
                Name = row.Field<string>("Name"),
                ShortName = row.Field<string>("Short Name"),
                Language = row.Field<string>("Language"),
                ID = row.Field<string>("ID"),
                PublishOn = row.Field<string>("Publish On"),
                MagazineRegdNo = row.Field<string>("Magazine Regd. No."),
                PostalRegdNo = row.Field<string>("Postal Regd. No."),
                MembershipStartNo = Int32.Parse(row.Field<string>("Membership Start No.")),
                AddBy = row.Field<string>("Add By"),
                EditBy = row.Field<string>("Edit By"),
                ActionStatus = row.Field<string>("Action Status"),
                ActionBy = row.Field<string>("Action By"),
                AddDate = DateTime.Parse(row.Field<string>("Add Date")),
                ActionDate = DateTime.Parse(row.Field<string>("Action Date")),
                EditDate = DateTime.Parse(row.Field<string>("Edit Date")),
                ForeignSubscriptionsCount = Boolean.Parse(row.Field<string>("Foreign Subscriptions")),
            }).ToList();
            else return null;
        }
    }
    public class ModelToDatatable
    {
        public static DataTable ConvertModelToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            if (data != null)
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

        public static DataTable ConvertPaymentGridModelToDataTable(List<Payment_Grid_Datatable> dt)
        {
            DataTable DT = new DataTable();
            DataRow ROW;
            //DataTable dt = new DataTable();
            //Payment_Grid_Datatable objmkt = new Payment_Grid_Datatable();
            //dt.Columns.Add("Sr.");

            DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT.Columns.Add("Item_ID", Type.GetType("System.String"));
            DT.Columns.Add("Item_Led_ID", Type.GetType("System.String"));
            DT.Columns.Add("Item_Trans_Type", Type.GetType("System.String"));
            DT.Columns.Add("Item_Party_Req", Type.GetType("System.String"));
            DT.Columns.Add("Item_Profile", Type.GetType("System.String"));
            DT.Columns.Add("Item Name", Type.GetType("System.String"));
            DT.Columns.Add("ITEM_VOUCHER_TYPE", Type.GetType("System.String"));
            DT.Columns.Add("Head", Type.GetType("System.String"));
            DT.Columns.Add("Qty.", Type.GetType("System.Double"));
            DT.Columns.Add("Unit", Type.GetType("System.String"));
            DT.Columns.Add("Rate", Type.GetType("System.Double"));
            DT.Columns.Add("TDS", Type.GetType("System.Double"));
            DT.Columns.Add("Amount", Type.GetType("System.Decimal"));
            DT.Columns.Add("Remarks", Type.GetType("System.String"));
            DT.Columns.Add("Pur_ID", Type.GetType("System.String"));
            DT.Columns.Add("LOC_ID", Type.GetType("System.String"));
            DT.Columns.Add("CREATION_PROF_REC_ID", Type.GetType("System.String"));
            DT.Columns.Add("GS_DESC_MISC_ID", Type.GetType("System.String"));
            DT.Columns.Add("GS_ITEM_WEIGHT", Type.GetType("System.String"));
            DT.Columns.Add("AI_TYPE", Type.GetType("System.String"));
            DT.Columns.Add("AI_MAKE", Type.GetType("System.String"));
            DT.Columns.Add("AI_MODEL", Type.GetType("System.String"));
            DT.Columns.Add("AI_SERIAL_NO", Type.GetType("System.String"));
            DT.Columns.Add("AI_PUR_DATE", Type.GetType("System.String"));
            DT.Columns.Add("AI_WARRANTY", Type.GetType("System.Double"));
            DT.Columns.Add("AI_IMAGE", Type.GetType("System.Byte[]"));
            DT.Columns.Add("LS_NAME", Type.GetType("System.String"));
            DT.Columns.Add("LS_BIRTH_YEAR", Type.GetType("System.String"));
            DT.Columns.Add("LS_INSURANCE", Type.GetType("System.String"));
            DT.Columns.Add("LS_INSURANCE_ID", Type.GetType("System.String"));
            DT.Columns.Add("LS_INS_POLICY_NO", Type.GetType("System.String"));
            DT.Columns.Add("LS_INS_AMT", Type.GetType("System.String"));
            DT.Columns.Add("LS_INS_DATE", Type.GetType("System.String"));
            DT.Columns.Add("VI_MAKE", Type.GetType("System.String"));
            DT.Columns.Add("VI_MODEL", Type.GetType("System.String"));
            DT.Columns.Add("VI_REG_NO_PATTERN", Type.GetType("System.String"));
            DT.Columns.Add("VI_REG_NO", Type.GetType("System.String"));
            DT.Columns.Add("VI_REG_DATE", Type.GetType("System.String"));
            DT.Columns.Add("VI_OWNERSHIP", Type.GetType("System.String"));
            DT.Columns.Add("VI_OWNERSHIP_AB_ID", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_RC_BOOK", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_AFFIDAVIT", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_WILL", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_TRF_LETTER", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_FU_LETTER", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_OTHERS", Type.GetType("System.String"));
            DT.Columns.Add("VI_DOC_NAME", Type.GetType("System.String"));
            DT.Columns.Add("VI_INSURANCE_ID", Type.GetType("System.String"));
            DT.Columns.Add("VI_INS_POLICY_NO", Type.GetType("System.String"));
            DT.Columns.Add("VI_INS_EXPIRY_DATE", Type.GetType("System.String"));
            DT.Columns.Add("LB_PRO_TYPE", Type.GetType("System.String"));
            DT.Columns.Add("LB_PRO_CATEGORY", Type.GetType("System.String"));
            DT.Columns.Add("LB_PRO_USE", Type.GetType("System.String"));
            DT.Columns.Add("LB_PRO_NAME", Type.GetType("System.String"));
            DT.Columns.Add("LB_PRO_ADDRESS", Type.GetType("System.String"));
            DT.Columns.Add("LB_OWNERSHIP", Type.GetType("System.String"));
            DT.Columns.Add("LB_OWNERSHIP_PARTY_ID", Type.GetType("System.String"));
            DT.Columns.Add("LB_SURVEY_NO", Type.GetType("System.String"));
            DT.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            DT.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            DT.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            DT.Columns.Add("LB_RCC_ROOF", Type.GetType("System.String"));
            DT.Columns.Add("LB_DEPOSIT_AMT", Type.GetType("System.Double"));
            DT.Columns.Add("LB_PAID_DATE", Type.GetType("System.String"));
            DT.Columns.Add("LB_MONTH_RENT", Type.GetType("System.Double"));
            DT.Columns.Add("LB_MONTH_O_PAYMENTS", Type.GetType("System.Double"));
            DT.Columns.Add("LB_PERIOD_FROM", Type.GetType("System.String"));
            DT.Columns.Add("LB_PERIOD_TO", Type.GetType("System.String"));
            DT.Columns.Add("LB_DOC_OTHERS", Type.GetType("System.String"));
            DT.Columns.Add("LB_DOC_NAME", Type.GetType("System.String"));
            DT.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            DT.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
            DT.Columns.Add("LB_REC_EDIT_ON", Type.GetType("System.DateTime"));
            DT.Columns.Add("LB_ADDRESS1", Type.GetType("System.String"));
            DT.Columns.Add("LB_ADDRESS2", Type.GetType("System.String"));
            DT.Columns.Add("LB_ADDRESS3", Type.GetType("System.String"));
            DT.Columns.Add("LB_ADDRESS4", Type.GetType("System.String"));
            DT.Columns.Add("LB_STATE_ID", Type.GetType("System.String"));
            DT.Columns.Add("LB_DISTRICT_ID", Type.GetType("System.String"));
            DT.Columns.Add("LB_CITY_ID", Type.GetType("System.String"));
            DT.Columns.Add("LB_PINCODE", Type.GetType("System.String"));
            DT.Columns.Add("TP_ID", Type.GetType("System.String"));
            DT.Columns.Add("TP_BILL_NO", Type.GetType("System.String"));
            DT.Columns.Add("TP_BILL_DATE", Type.GetType("System.String"));
            DT.Columns.Add("TP_PERIOD_FROM", Type.GetType("System.String"));
            DT.Columns.Add("TP_PERIOD_TO", Type.GetType("System.String"));
            DT.Columns.Add("REF_REC_ID", Type.GetType("System.String"));
            DT.Columns.Add("REFERENCE", Type.GetType("System.String"));
            DT.Columns.Add("WIP_REF_TYPE", Type.GetType("System.String"));
            DT.Columns.Add("REF_TDS_DED", Type.GetType("System.String"));

            //foreach (PropertyInfo info in typeof(Payment_Grid_Datatable).GetProperties())
            //{
            //    DT.Rows.Add(info.Name);
            //}
            //DT.AcceptChanges();
            if (dt != null)
            {
                var Final_Data = (List<Payment_Grid_Datatable>)dt.ToList();

                //List<DataRow> list = dt.AsEnumerable().ToList();

                foreach (var FieldName in Final_Data)
                {
                    ROW = DT.NewRow();
                    ROW["Sr."] = string.IsNullOrEmpty(FieldName.Sr.ToString()) ? (Int32?)null : FieldName.Sr;
                    ROW["Item_ID"] = string.IsNullOrEmpty(FieldName.Item_ID) ? "" : FieldName.Item_ID;
                    ROW["Item_Led_ID"] = string.IsNullOrEmpty(FieldName.Item_Led_ID) ? "" : FieldName.Item_Led_ID;
                    ROW["Item_Trans_Type"] = FieldName.Item_Trans_Type;
                    ROW["Item_Party_Req"] = FieldName.Item_Party_Req;
                    ROW["Item_Profile"] = string.IsNullOrEmpty(FieldName.Item_Profile) ? "" : FieldName.Item_Profile;
                    ROW["Item Name"] = string.IsNullOrEmpty(FieldName.ItemName) ? "" : FieldName.ItemName;
                    ROW["ITEM_VOUCHER_TYPE"] = string.IsNullOrEmpty(FieldName.ITEM_VOUCHER_TYPE) ? "" : FieldName.ITEM_VOUCHER_TYPE;
                    ROW["Head"] = string.IsNullOrEmpty(FieldName.Head) ? "" : FieldName.Head;
                    ROW["Qty."] = !Convert.IsDBNull(FieldName.Qty) ? 0 : FieldName.Qty;
                    ROW["Unit"] = string.IsNullOrEmpty(FieldName.Unit) ? "" : FieldName.Unit;
                    ROW["Rate"] = Convert.IsDBNull(FieldName.Rate);
                    ROW["TDS"] = string.IsNullOrEmpty(FieldName.TDS.ToString()) ? (decimal?)null : FieldName.TDS;
                    ROW["Amount"] = string.IsNullOrEmpty(FieldName.Amount.ToString()) ? (decimal?)null : FieldName.Amount;
                    ROW["Remarks"] = string.IsNullOrEmpty(FieldName.Remarks) ? "" : FieldName.Remarks;
                    ROW["Pur_ID"] = string.IsNullOrEmpty(FieldName.Pur_ID) ? "" : FieldName.Pur_ID;
                    ROW["LOC_ID"] = string.IsNullOrEmpty(FieldName.LOC_ID) ? "" : FieldName.LOC_ID;
                    ROW["CREATION_PROF_REC_ID"] = string.IsNullOrEmpty(FieldName.CREATION_PROF_REC_ID) ? "" : FieldName.CREATION_PROF_REC_ID;
                    ROW["GS_DESC_MISC_ID"] = string.IsNullOrEmpty(FieldName.GS_DESC_MISC_ID) ? "" : FieldName.GS_DESC_MISC_ID;
                    ROW["GS_ITEM_WEIGHT"] = FieldName.GS_ITEM_WEIGHT;
                    ROW["AI_TYPE"] = string.IsNullOrEmpty(FieldName.AI_TYPE) ? "" : FieldName.AI_TYPE;
                    ROW["AI_MAKE"] = string.IsNullOrEmpty(FieldName.AI_MAKE) ? "" : FieldName.AI_MAKE;
                    ROW["AI_MODEL"] = string.IsNullOrEmpty(FieldName.AI_MODEL) ? "" : FieldName.AI_MODEL;
                    ROW["AI_SERIAL_NO"] = string.IsNullOrEmpty(FieldName.AI_SERIAL_NO) ? "" : FieldName.AI_SERIAL_NO;
                    ROW["AI_PUR_DATE"] = FieldName.AI_PUR_DATE;
                    ROW["AI_WARRANTY"] = Convert.IsDBNull(FieldName.AI_WARRANTY);
                    ROW["AI_IMAGE"] = Convert.IsDBNull(FieldName.AI_IMAGE);
                    ROW["LS_NAME"] = string.IsNullOrEmpty(FieldName.LS_NAME) ? "" : FieldName.LS_NAME;
                    ROW["LS_BIRTH_YEAR"] = string.IsNullOrEmpty(FieldName.LS_BIRTH_YEAR) ? "" : FieldName.LS_BIRTH_YEAR;
                    ROW["LS_INSURANCE"] = string.IsNullOrEmpty(FieldName.LS_INSURANCE) ? "" : FieldName.LS_INSURANCE;
                    ROW["LS_INSURANCE_ID"] = string.IsNullOrEmpty(FieldName.LS_INSURANCE_ID) ? "" : FieldName.LS_INSURANCE_ID;
                    ROW["LS_INS_POLICY_NO"] = string.IsNullOrEmpty(FieldName.LS_INS_POLICY_NO) ? "" : FieldName.LS_INS_POLICY_NO;
                    ROW["LS_INS_AMT"] = FieldName.LS_INS_AMT;
                    ROW["LS_INS_DATE"] = FieldName.LS_INS_DATE;
                    ROW["VI_MAKE"] = string.IsNullOrEmpty(FieldName.VI_MAKE) ? "" : FieldName.VI_MAKE;
                    ROW["VI_MODEL"] = string.IsNullOrEmpty(FieldName.VI_MODEL) ? "" : FieldName.VI_MODEL;
                    ROW["VI_REG_NO_PATTERN"] = string.IsNullOrEmpty(FieldName.VI_REG_NO_PATTERN) ? "" : FieldName.VI_REG_NO_PATTERN;
                    ROW["VI_REG_NO"] = string.IsNullOrEmpty(FieldName.VI_REG_NO) ? "" : FieldName.VI_REG_NO;
                    ROW["VI_REG_DATE"] = FieldName.VI_REG_DATE;
                    ROW["VI_OWNERSHIP"] = string.IsNullOrEmpty(FieldName.VI_OWNERSHIP) ? "" : FieldName.VI_OWNERSHIP;
                    ROW["VI_OWNERSHIP_AB_ID"] = string.IsNullOrEmpty(FieldName.VI_OWNERSHIP_AB_ID) ? "" : FieldName.VI_OWNERSHIP_AB_ID;
                    ROW["VI_DOC_RC_BOOK"] = FieldName.VI_DOC_RC_BOOK ;
                    ROW["VI_DOC_AFFIDAVIT"] = FieldName.VI_DOC_AFFIDAVIT;
                    ROW["VI_DOC_WILL"] = FieldName.VI_DOC_WILL ;
                    ROW["VI_DOC_TRF_LETTER"] = FieldName.VI_DOC_TRF_LETTER;
                    ROW["VI_DOC_FU_LETTER"] = FieldName.VI_DOC_FU_LETTER ;
                    ROW["VI_DOC_OTHERS"] = FieldName.VI_DOC_OTHERS ;
                    ROW["VI_DOC_NAME"] = string.IsNullOrEmpty(FieldName.VI_DOC_NAME) ? "" : FieldName.VI_DOC_NAME;
                    ROW["VI_INSURANCE_ID"] = string.IsNullOrEmpty(FieldName.VI_INSURANCE_ID) ? "" : FieldName.VI_INSURANCE_ID;
                    ROW["VI_INS_POLICY_NO"] = string.IsNullOrEmpty(FieldName.VI_INS_POLICY_NO) ? "" : FieldName.VI_INS_POLICY_NO;
                    ROW["VI_INS_EXPIRY_DATE"] = FieldName.VI_INS_EXPIRY_DATE;
                    ROW["LB_PRO_TYPE"] = string.IsNullOrEmpty(FieldName.LB_PRO_TYPE) ? "" : FieldName.LB_PRO_TYPE;
                    ROW["LB_PRO_CATEGORY"] = string.IsNullOrEmpty(FieldName.LB_PRO_CATEGORY) ? "" : FieldName.LB_PRO_CATEGORY;
                    ROW["LB_PRO_USE"] = string.IsNullOrEmpty(FieldName.LB_PRO_USE) ? "" : FieldName.LB_PRO_USE;
                    ROW["LB_PRO_NAME"] = string.IsNullOrEmpty(FieldName.LB_PRO_NAME) ? "" : FieldName.LB_PRO_NAME;
                    ROW["LB_PRO_ADDRESS"] = string.IsNullOrEmpty(FieldName.LB_PRO_ADDRESS) ? "" : FieldName.LB_PRO_ADDRESS;
                    ROW["LB_OWNERSHIP"] = string.IsNullOrEmpty(FieldName.LB_OWNERSHIP) ? "" : FieldName.LB_OWNERSHIP;
                    ROW["LB_OWNERSHIP_PARTY_ID"] = string.IsNullOrEmpty(FieldName.LB_OWNERSHIP_PARTY_ID) ? "" : FieldName.LB_OWNERSHIP_PARTY_ID;
                    ROW["LB_SURVEY_NO"] = string.IsNullOrEmpty(FieldName.LB_SURVEY_NO) ? "" : FieldName.LB_SURVEY_NO;
                    ROW["LB_TOT_P_AREA"] = string.IsNullOrEmpty(FieldName.LB_TOT_P_AREA.ToString()) ? "" : FieldName.LB_TOT_P_AREA.ToString();
                    ROW["LB_CON_AREA"] = string.IsNullOrEmpty(FieldName.LB_CON_AREA.ToString()) ? "" : FieldName.LB_CON_AREA.ToString();
                    ROW["LB_CON_YEAR"] = string.IsNullOrEmpty(FieldName.LB_CON_YEAR) ? "" : FieldName.LB_CON_YEAR;
                    ROW["LB_RCC_ROOF"] = string.IsNullOrEmpty(FieldName.LB_RCC_ROOF) ? "" : FieldName.LB_RCC_ROOF;
                    ROW["LB_DEPOSIT_AMT"] = string.IsNullOrEmpty(FieldName.LB_DEPOSIT_AMT.ToString()) ? "" : FieldName.LB_DEPOSIT_AMT.ToString();
                    ROW["LB_PAID_DATE"] = FieldName.LB_PAID_DATE;
                    ROW["LB_MONTH_RENT"] = string.IsNullOrEmpty(FieldName.LB_MONTH_RENT.ToString()) ? "" : FieldName.LB_MONTH_RENT.ToString();
                    ROW["LB_MONTH_O_PAYMENTS"] = !Convert.IsDBNull(FieldName.LB_MONTH_O_PAYMENTS) ? 0 : FieldName.LB_MONTH_O_PAYMENTS;
                    ROW["LB_PERIOD_FROM"] = FieldName.LB_PERIOD_FROM;
                    ROW["LB_PERIOD_TO"] = FieldName.LB_PERIOD_TO;

                    ROW["LB_DOC_OTHERS"] = string.IsNullOrEmpty(FieldName.LB_DOC_OTHERS) ? null : (FieldName.LB_DOC_OTHERS == "True" ? "YES" : "NO");
                    //ROW["LB_DOC_OTHERS"] = string.IsNullOrEmpty(FieldName.LB_DOC_OTHERS) ? "" : FieldName.LB_DOC_OTHERS;

                    ROW["LB_DOC_NAME"] = string.IsNullOrEmpty(FieldName.LB_DOC_NAME) ? "" : FieldName.LB_DOC_NAME;
                    ROW["LB_OTHER_DETAIL"] = string.IsNullOrEmpty(FieldName.LB_OTHER_DETAIL) ? "" : (FieldName.LB_OTHER_DETAIL);// FieldName.LB_OTHER_DETAIL;
                    ROW["LB_REC_ID"] = string.IsNullOrEmpty(FieldName.LB_REC_ID) ? "" : FieldName.LB_REC_ID;
                    ROW["LB_REC_EDIT_ON"] = string.IsNullOrEmpty(FieldName.LB_REC_EDIT_ON.ToString()) ? "" : FieldName.LB_REC_EDIT_ON.ToString();
                    //ROW["LB_REC_ID"] = string.IsNullOrEmpty(FieldName.LB_REC_ID) ? "" : FieldName.LB_REC_ID;
                    ROW["LB_ADDRESS1"] = string.IsNullOrEmpty(FieldName.LB_ADDRESS1) ? "" : FieldName.LB_ADDRESS1;
                    ROW["LB_ADDRESS2"] = string.IsNullOrEmpty(FieldName.LB_ADDRESS2) ? "" : FieldName.LB_ADDRESS2;
                    ROW["LB_ADDRESS3"] = string.IsNullOrEmpty(FieldName.LB_ADDRESS3) ? "" : FieldName.LB_ADDRESS3;
                    ROW["LB_ADDRESS4"] = string.IsNullOrEmpty(FieldName.LB_ADDRESS4) ? "" : FieldName.LB_ADDRESS4;
                    ROW["LB_STATE_ID"] = string.IsNullOrEmpty(FieldName.LB_STATE_ID) ? "" : FieldName.LB_STATE_ID;
                    ROW["LB_DISTRICT_ID"] = string.IsNullOrEmpty(FieldName.LB_DISTRICT_ID) ? "" : FieldName.LB_DISTRICT_ID;
                    ROW["LB_CITY_ID"] = string.IsNullOrEmpty(FieldName.LB_CITY_ID) ? "" : FieldName.LB_CITY_ID;
                    ROW["LB_PINCODE"] = string.IsNullOrEmpty(FieldName.LB_PINCODE) ? "" : FieldName.LB_PINCODE;
                    ROW["TP_ID"] = string.IsNullOrEmpty(FieldName.TP_ID) ? "" : FieldName.TP_ID;
                    ROW["TP_BILL_NO"] = string.IsNullOrEmpty(FieldName.TP_BILL_NO) ? "" : FieldName.TP_BILL_NO;
                    ROW["TP_BILL_DATE"] = FieldName.TP_BILL_DATE;
                    ROW["TP_PERIOD_FROM"] = FieldName.TP_PERIOD_FROM;
                    ROW["TP_PERIOD_TO"] = FieldName.TP_PERIOD_TO;
                    ROW["REF_REC_ID"] = string.IsNullOrEmpty(FieldName.REF_REC_ID) ? "" : FieldName.REF_REC_ID;
                    ROW["REFERENCE"] = string.IsNullOrEmpty(FieldName.REFERENCE) ? "" : FieldName.REFERENCE;
                    ROW["WIP_REF_TYPE"] = string.IsNullOrEmpty(FieldName.WIP_REF_TYPE) ? "" : FieldName.WIP_REF_TYPE;
                    ROW["REF_TDS_DED"] = string.IsNullOrEmpty(FieldName.REF_TDS_DED) ? "" : FieldName.REF_TDS_DED;

                    DT.Rows.Add(ROW);
                }
            }
            return DT;
        }

        public static DataTable ConvertPaymentBankGridModelToDataTable(List<PaymentBankDetail_Grid_Datatable> dt)
        {
            DataTable DT = new DataTable();
            DataRow ROW;
            DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT.Columns.Add("Amount", Type.GetType("System.Double"));
            DT.Columns.Add("Mode", Type.GetType("System.String"));
            DT.Columns.Add("No.", Type.GetType("System.String"));
            DT.Columns.Add("Date", Type.GetType("System.DateTime"));
            DT.Columns.Add("Clearing Date", Type.GetType("System.String"));
            DT.Columns.Add("Bank Name", Type.GetType("System.String"));
            DT.Columns.Add("Branch", Type.GetType("System.String"));
            DT.Columns.Add("A/c. No.", Type.GetType("System.String"));
            DT.Columns.Add("ID", Type.GetType("System.String"));
            DT.Columns.Add("MT_BANK_ID", Type.GetType("System.String"));
            DT.Columns.Add("Money Transfer", Type.GetType("System.String"));
            DT.Columns.Add("Ref. A/c. No.", Type.GetType("System.String"));
            DT.Columns.Add("Edit Time", Type.GetType("System.DateTime"));

            if (dt != null)
            {


                var Final_Data = (List<PaymentBankDetail_Grid_Datatable>)dt.ToList();

                foreach (var FieldName in Final_Data)
                {
                    ROW = DT.NewRow();
                    ROW["Sr."] = string.IsNullOrEmpty(FieldName.Sr.ToString()) ? (Int32?)null : FieldName.Sr;
                    ROW["Amount"] = Convert.IsDBNull(FieldName.Amount);
                    ROW["Mode"] = string.IsNullOrEmpty(FieldName.Mode) ? "" : FieldName.Mode;
                    ROW["No."] = string.IsNullOrEmpty(FieldName.Ref_No) ? "" : FieldName.Ref_No;
                    ROW["Date"] = FieldName.Ref_Date;
                    ROW["Clearing Date"] = string.IsNullOrEmpty(FieldName.Ref_CDate.ToString()) ? "" : FieldName.Ref_CDate.ToString();
                    ROW["Bank Name"] = string.IsNullOrEmpty(FieldName.BANK_NAME) ? "" : FieldName.BANK_NAME;
                    ROW["Branch"] = string.IsNullOrEmpty(FieldName.Branch) ? "" : FieldName.Branch;
                    ROW["A/c. No."] = string.IsNullOrEmpty(FieldName.Acc_No) ? "" : FieldName.Acc_No;
                    ROW["ID"] = string.IsNullOrEmpty(FieldName.ID) ? "" : FieldName.ID;
                    ROW["MT_BANK_ID"] = string.IsNullOrEmpty(FieldName.MT_BANK_ID) ? "" : FieldName.MT_BANK_ID;
                    //ROW["Money Transfer"] = string.IsNullOrEmpty(FieldName.Amount.ToString()) ? (decimal?)null : FieldName.Amount;
                    ROW["Ref. A/c. No."] = string.IsNullOrEmpty(FieldName.Ref_Acc_No) ? "" : FieldName.Ref_Acc_No;
                    ROW["Edit Time"] = string.IsNullOrEmpty(FieldName.Edit_Time.ToString()) ? "" : FieldName.Edit_Time.ToString();
                    DT.Rows.Add(ROW);
                }
            }
            return DT;
        }
        public static DataTable ConvertLBEXTENDEDModelToDataTable(DataTable LB_EXTENDED_PROPERTY_TABLE)
        {
            //DataTable LB_EXTENDED_PROPERTY_TABLE = new DataTable();
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            if ((LB_EXTENDED_PROPERTY_TABLE == null))
            {
                //LB_EXTENDED_PROPERTY_TABLE = LB_EXTENDED_PROPERTY_TABLE;
            }
            else
            {
                if ((LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0))
                {
                    //LB_EXTENDED_PROPERTY_TABLE = new DataTable();
                    // With...
                }

                foreach (DataRow XROW in LB_EXTENDED_PROPERTY_TABLE.Rows)
                {
                    DataRow Row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                    Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"].ToString();
                    Row["LB_SR_NO"] = XROW["LB_SR_NO"].ToString();
                    Row["LB_INS_ID"] = XROW["LB_INS_ID"].ToString();
                    Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"].ToString();
                    Row["LB_CON_AREA"] = XROW["LB_CON_AREA"].ToString();
                    Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"].ToString();
                    Row["LB_VALUE"] = XROW["LB_VALUE"].ToString();
                    Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"].ToString();
                    Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                    LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                }

            }
            return LB_EXTENDED_PROPERTY_TABLE;
        }

        public static DataTable ConvertLB_DOCS_ARRAYModelToDataTable(DataTable LB_DOCS_ARRAY)
        {
            //DataTable LB_DOCS_ARRAY = new DataTable();
            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
            if ((LB_DOCS_ARRAY == null))
            {
                //LB_DOCS_ARRAY = model.LB_DOCS_ARRAY;
            }
            else
            {
                if ((LB_DOCS_ARRAY.Rows.Count <= 0))
                {
                    //LB_DOCS_ARRAY = new DataTable();

                    // With...
                }

                foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                {
                    DataRow Row = LB_DOCS_ARRAY.NewRow();
                    Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                    Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                    LB_DOCS_ARRAY.Rows.Add(Row);
                }

            }
            return LB_DOCS_ARRAY;
        }

    }
    [Serializable]
    public class stringData 
    {
        public string Info { get; set; }
    }
}
