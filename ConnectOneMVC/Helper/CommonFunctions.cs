using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Profile.Models;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.Expressions;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ConnectOneMVC.Helper
{
    public class CommonFunctions
    {
        public static void Programming_Mode(Common_Lib.Common BASE)
        {
            BASE.Allow_Membership = true;
            BASE._open_PAD_No_Main = "208";
            BASE._open_User_ID = BASE._open_PAD_No_Main;
            BASE._open_Cen_ID = 00207;
            BASE._open_Cen_ID_Main = 00207;
            BASE._open_Ins_ID = "00001";
            BASE._open_Cen_ID_Child = "'00207'";
            BASE._open_Cen_Rec_ID = "ccc5768a-fdf0-4063-a816-654ce78896c0";
            BASE._open_Cen_Name = "Test Centre 2";
            BASE._open_Year_Acc_Type = "GENERAL";

            BASE._open_Year_ID = 1112;
            BASE._open_Year_Name = "2011 - 2012";
            BASE._open_Year_Sdt = DateTime.ParseExact("4/1/2011", "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            BASE._open_Year_Edt = DateTime.ParseExact("3/31/2012", "M/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            BASE._open_Ins_Name = "BK";
            BASE._open_User_Type = "Auditor";

            BASE._Sync_Last_DateTime = DateTime.Now;
            BASE.Get_Configure_Setting();
        }

        public static GridViewExportFormat GetExportFormat(string ExportFormat)
        {
            //var result = GridViewExportFormat.None;
            var result = GridViewExportFormat.None;
            Enum.TryParse(ExportFormat, out result);
            return result;
        }

        public static string ConvertToModel(DataTable d1)
        {
            string str = "";

            foreach (DataColumn column in d1.Columns)
            {
                str += "public " + ((column).DataType).Name + " " + (column).ColumnName + "{ get; set; } \r\n";
            }
            return str;
        }
        public static bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public static DataTable GetReferenceData(Common_Lib.Common BASE, string iItemProfile, string ItemID, string iTxnM_ID, string PartyID, Common_Lib.Common.Navigation_Mode Tag, string Profile_Rec_ID = "")//Tr_M_ID removed from parameter as Tr_M_ID=iTxnM_ID on investigation
        {
            // Tag As Object added Bug #5093 fixed
            // Warning!!! Optional parameters not supported
            DataSet JointDS = new DataSet();
            DataTable d1 = null;
            Common_Lib.RealTimeService.Param_Get_JV_Asset_Listing AssetParam = new Common_Lib.RealTimeService.Param_Get_JV_Asset_Listing();
            switch (iItemProfile)
            {
                case "OTHER ASSETS":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "ADVANCES":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.ADVANCES;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.PartyID = PartyID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "GOLD":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    AssetParam.Item_ID = ItemID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "SILVER":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    AssetParam.Item_ID = ItemID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "LAND & BUILDING":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID
                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "LIVESTOCK":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "OTHER DEPOSITS":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_DEPOSITS;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.PartyID = PartyID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "OTHER LIABILITIES":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_LIABILITIES;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.PartyID = PartyID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "VEHICLES":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "OPENING":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_OPENING_BALANCES;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                case "WIP":
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.WIP;
                    AssetParam.Item_ID = ItemID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
                default:
                    AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                    AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                    AssetParam.TR_M_ID = iTxnM_ID;
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING;
                    //if ((((Tag) == Common_Lib.Common.Navigation_Mode._Edit)
                    //            || ((Tag) == Common_Lib.Common.Navigation_Mode._Delete)))
                    //{
                    //    AssetParam.TR_M_ID = Tr_M_ID;
                    //} on investigation found that Tr_M_ID==iTxnM_ID

                    if (!(Profile_Rec_ID == null))
                    {
                        AssetParam.Asset_RecID = Profile_Rec_ID;
                    }

                    d1 = BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam);
                    break;
            }
            return d1;
        }

        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? System.DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private static List<T> ConvertToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static decimal ConvertAsDecimal(string Val)
        {
            if (Val.Length == 0)
                return 0;
            else
                return Convert.ToDecimal(Val);
        }
        public static string EscapeText(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                str = str.Replace("\\", "\\\\");
                str = str.Replace("\"", "\\\"");
                return str;
            }

        }

        public static bool AreDatesEqual(DateTime d1, DateTime d2)
        {
            if (d1.Date != d2.Date) return false;
            if (d1.Hour != d2.Hour) return false;
            if (d1.Minute != d2.Minute) return false;
            if (d1.Second != d2.Second) return false;

            return true;
        }

        public static string GetVouchingCategoryForCallingScreen(string CallingScreen)
        {
            string VouchingCategory = "CASHBOOK";
            switch (CallingScreen.ToUpper())
            {
                case "PROFILE_BANKACCOUNTS":
                    VouchingCategory = "BANK ACCOUNT";
                    break;
                case "PROFILE_CASH":
                    VouchingCategory = "CASH";
                    break;
                case "PROFILE_FD":
                    VouchingCategory = "FD";
                    break;
                case "PROFILE_OPENINGBALANCES":
                    VouchingCategory = "OPENING";
                    break;
                case "PROFILE_LANDANDBUILDING":
                    VouchingCategory = "LAND & BUILDING";
                    break;
                case "PROFILE_GOLDSILVER":
                    VouchingCategory = "GOLD SILVER";
                    break;
                case "PROFILE_VEHICLES":
                    VouchingCategory = "VEHICLES";
                    break;
                case "PROFILE_ASSETS":
                    VouchingCategory = "OTHER ASSETS";
                    break;
                case "PROFILE_LIVESTOCK":
                    VouchingCategory = "LIVESTOCK";
                    break;
                case "PROFILE_WIP":
                    VouchingCategory = "WIP";
                    break;
                case "PROFILE_DEPOSIT":
                    VouchingCategory = "OTHER DEPOSITS";
                    break;
                case "PROFILE_ADVANCES":
                    VouchingCategory = "ADVANCES";
                    break;
                case "PROFILE_LIABILITIES":
                    VouchingCategory = "OTHER LIABILITIES";
                    break;
                case "PROFILE_TELEPHONE":
                    VouchingCategory = "TELEPHONE";
                    break;
                case "PROFILE_MEMBERSHIP":
                    VouchingCategory = "WING MEMBER";
                    break;
                case "MAGAZINE_MEMBERSHIP":
                    VouchingCategory = "MAGAZINE MEMBER";
                    break;
                case "FACILITY_ADDRESSBOOK":
                    VouchingCategory = "ADDRESS BOOK";
                    break;
                case "FACILITY_SERVICEREPORT":
                    VouchingCategory = "SERVICE REPORT";
                    break;
            }
            return VouchingCategory;
        }
        public static ClientScreen GetClientScreenFromTRCode(string iTR_CODE)
        {
            iTR_CODE = iTR_CODE??"";

            switch (iTR_CODE)
            {
                case "1":
                    return ClientScreen.Accounts_Voucher_CashBank;
                case "2":
                    return ClientScreen.Accounts_Voucher_BankToBank;
                case "3":
                    return ClientScreen.Accounts_Voucher_Payment;
                case "4":
                    return ClientScreen.Accounts_Voucher_Receipt;
                case "5":
                    return ClientScreen.Accounts_Voucher_Donation;
                case "6":
                    return ClientScreen.Accounts_Voucher_Donation;
                case "7":
                    return ClientScreen.Accounts_Voucher_Gift;
                case "8":
                    return ClientScreen.Accounts_Voucher_Internal_Transfer;
                case "9":
                    return ClientScreen.Accounts_Voucher_CollectionBox;
                case "10":
                    return ClientScreen.Accounts_Voucher_FD;
                case "11":
                    return ClientScreen.Accounts_Voucher_SaleOfAsset;
                case "12":
                    return ClientScreen.Accounts_Voucher_Membership;
                case "13":
                    return ClientScreen.Accounts_Voucher_Membership_Renewal;
                case "14":
                    return ClientScreen.Accounts_Voucher_Journal;
                case "15":
                    return ClientScreen.Accounts_Voucher_AssetTransfer;
                case "16":
                    return ClientScreen.Accounts_Voucher_Membership_Conversion;
                case "17":
                    return ClientScreen.Accounts_Voucher_WIP_Finalization;
                default:
                    return ClientScreen.Accounts_CashBook;
            }
        }
        public static string TransformFileName(string FileName, byte[] FileByteArray,bool GuidAsFileName = false)
        {
            string filetype = "";
            string mimeType = "";
            string FileNameWithoutExtension = "";
            var FileNameSplit = FileName.Split('.');
            mimeType = MimeMapping.GetMimeMapping(FileName);
            if (FileNameSplit.Length < 2)
            {
                filetype = GetFileType(FileByteArray);
            }
            else if (FileNameSplit[FileNameSplit.Length - 1].ToLower() == "csv")
            {
                filetype = "csv";
            }
            else if (mimeType == "application/octet-stream")
            {
                filetype = GetFileType(FileByteArray);
            }
            else
            {
                filetype = FileNameSplit[FileNameSplit.Length - 1];
            }
            if (FileNameSplit.Length == 1)
            {
                FileNameWithoutExtension = FileNameSplit[0];
            }
            else
            {
                for (int i = 0; i < FileNameSplit.Length - 1; i++)
                {
                    if (i == 0)
                    {
                        FileNameWithoutExtension = FileNameSplit[0];
                    }
                    else
                    {
                        FileNameWithoutExtension = FileNameWithoutExtension + "." + FileNameSplit[i];
                    }
                }
            }
            FileNameWithoutExtension = Regex.Replace(GuidAsFileName==true? Guid.NewGuid().ToString() : FileNameWithoutExtension, "[^0-9A-Za-z]+", "_");
            var FinalFileName = "";
            if (filetype.Length > 0)
            {
                FinalFileName = FileNameWithoutExtension + "." + filetype;
            }
            else
            {
                FinalFileName = FileNameWithoutExtension;
            }
            return FinalFileName;
        }
        public static string GetFileType(byte[] FileField)
        {
            string filetype = "";
            byte[] first16Bytes = new byte[16];
            Array.Copy(FileField, 0, first16Bytes, 0, 16);
            string data_as_hex = BitConverter.ToString(first16Bytes);
            string MagicNumber = data_as_hex.Substring(0, 11);
            if (MagicNumber.StartsWith("42-4D"))
            {
                filetype = "bmp";
                return filetype;
            }
            else if (MagicNumber.StartsWith("FF-FB") || MagicNumber.StartsWith("49-44-33"))
            {
                filetype = "mp3";
                return filetype;
            }
            switch (MagicNumber)
            {
                case "25-50-44-46":
                    filetype = "pdf";
                    break;
                case "FF-D8-FF-DB":
                case "FF-D8-FF-EE":
                case "FF-D8-FF-E0":
                case "FF-D8-FF-E1":
                    filetype = "jpg";
                    break;
                case "89-50-4E-47":
                    filetype = "png";
                    break;
                case "47-49-46-38":
                    filetype = "gif";
                    break;
                case "7B-5C-72-74":
                    filetype = "rtf";
                    break;
                case "50-4B-03-04":
                    filetype = "docx";
                    break;
                case "D0-CF-11-E0":
                    filetype = "doc";
                    break;
                case "49-49-2A-00":
                case "4D-4D-00-2A":
                    filetype = "tiff";
                    break;
                case "38-42-50-53":
                    filetype = "psd";
                    break;
                case "52-49-46-46":
                    filetype = "webp";
                    break;

            }
            if (filetype.Length == 0)
            {
                string MagicNumber_4byteOffset = data_as_hex.Substring(12, 11);
                switch (MagicNumber_4byteOffset)
                {
                    case "66-74-79-70":
                        filetype = "mp4";
                        break;
                }
            }
            return filetype;
        }
        //public static string GetAttachment_DiskFileName(string AttachmentID, string FileName)
        //{
        //    var CustomFileName = "";
        //    var filename = FileName.Split('.');
        //    for (var i = 0; i < filename.Length; i++)
        //    {
        //        if (i == 0)
        //        {
        //            CustomFileName = AttachmentID;
        //        }
        //        if ((i == (filename.Length - 1)) && i != 0)
        //        {
        //            CustomFileName = CustomFileName + "." + filename[i];
        //        }
        //    }
        //    return CustomFileName;      
        //}

        public static string GetAttachment_DiskFileName(string AttachmentID, string FileName, bool GuidAsFileName = false)
        {
            var CustomFileName = "";
            var filename = FileName.Split('.');
            for (var i = 0; i < filename.Length; i++)
            {
                if (i == 0)
                {
                    if (GuidAsFileName == true)
                    {
                        CustomFileName = filename[0];
                    }
                    else
                    {
                        CustomFileName = AttachmentID;
                    }
                }
                if ((i == (filename.Length - 1)) && i != 0)
                {
                    CustomFileName = CustomFileName + "." + filename[i];
                }
            }
            return CustomFileName;
        }  
    }
    class EncryptDecrypt
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        public static string getRandomHexString(int saltSize = 16)
        {
            byte[] randomBytes = new byte[saltSize / 2]; // Since 1 byte = 2 hex chars
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            StringBuilder sb = new StringBuilder(saltSize);
            foreach (byte b in randomBytes)
            {
                sb.Append(b.ToString("x2")); // Convert to 2-character hex representation
            }
            return sb.ToString();
        }
        public static string EncryptUsingPublicCert(string text, string certPath, RSAEncryptionPadding padding)
        {
            byte[] keyByteArr = encoding.GetBytes(text);
            string rootedPath = certPath;
            if (Path.IsPathRooted(certPath)==false)
            {
                rootedPath = System.Web.HttpContext.Current.Server.MapPath(certPath);
            }
            using (X509Certificate2 publicKeyCertificate = GetPublicKeyCertificate(rootedPath))
            {
                using (RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)publicKeyCertificate.PublicKey.Key)
                {
                    byte[] encryptedByte = rsa.Encrypt(keyByteArr, padding);
                    return Convert.ToBase64String(encryptedByte);
                }
            }
        }
        public static string DecryptUsingPrivateKey(string base64EncodedEncryptedData, string privateKeyPath)
        {
            byte[] encryptedDataBytes = Convert.FromBase64String(base64EncodedEncryptedData);
            AsymmetricKeyParameter asymmetricKeyParameter = GetPrivateKeyParameter(privateKeyPath);
            using (var rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)asymmetricKeyParameter))
            {
                byte[] decryptedBytes = rsa.Decrypt(encryptedDataBytes, RSAEncryptionPadding.OaepSHA1);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        public static X509Certificate2 GetPublicKeyCertificate(string certificateFilePath)
        {
            // Load the certificate from the file
            X509Certificate2 publicKeyCertificate = new X509Certificate2(certificateFilePath);
            return publicKeyCertificate;
        }
        public static string EncryptInAesGCM(string plaintext, string key, string IV)
        {
            IBlockCipher cipher = new AesEngine();
            int macSize = 8 * cipher.GetBlockSize(); //tag length is 16
            byte[] keybytes = encoding.GetBytes(key);
            byte[] plaintextbytes= encoding.GetBytes(plaintext);
            byte[] nonce = new byte[12]; //nonce is iv
            Array.Copy(keybytes, nonce, 12);
            KeyParameter keyParam = new KeyParameter(keybytes);

            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce);
            GcmBlockCipher cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(true, keyParamAead);

            int outputSize = cipherMode.GetOutputSize(plaintextbytes.Length);
            byte[] cipherTextData = new byte[outputSize];
            int result = cipherMode.ProcessBytes(plaintextbytes, 0, plaintextbytes.Length, cipherTextData, 0);
            cipherMode.DoFinal(cipherTextData, result);

            return Convert.ToBase64String(cipherTextData);
        }
        public static string DecryptInAesGCM(string base64enctext, string key, string IV)
        {
            IBlockCipher cipher = new AesEngine();
            int macSize = 8 * cipher.GetBlockSize();
            byte[] keybytes = encoding.GetBytes(key);
            byte[] enctextbytes = Convert.FromBase64String(base64enctext);
            byte[] nonce = new byte[12];
            Array.Copy(keybytes, nonce, 12);
            KeyParameter keyParam = new KeyParameter(keybytes);

            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce);
            GcmBlockCipher cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(false, keyParamAead);

            int outputSize = cipherMode.GetOutputSize(enctextbytes.Length);
            byte[] plainTextData = new byte[outputSize];
            int result = cipherMode.ProcessBytes(enctextbytes, 0, enctextbytes.Length, plainTextData, 0);
            cipherMode.DoFinal(plainTextData, result);

            return encoding.GetString(plainTextData);
        }
        public static string DigitalSignatureSHA256_RSA(string plaintext, string privateKeyPath)
        {
            string privateKeyText = System.IO.File.ReadAllText(privateKeyPath);
            AsymmetricKeyParameter asymmetricKeyParameter= GetPrivateKeyParameter(privateKeyPath);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                if (asymmetricKeyParameter is RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters)
                {
                    RSAParameters rsaParameters = DotNetUtilities.ToRSAParameters(rsaPrivateCrtKeyParameters);
                    rsa.ImportParameters(rsaParameters);

                    byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                    byte[] signature = rsa.SignData(bytes, CryptoConfig.MapNameToOID("SHA256"));
                    return Convert.ToBase64String(signature);
                }
                else { return ""; }
            }
        }
        public static bool VerifyDigitalSignature(string enctext, string digitalSignature,string certPath)
        {
            string rootedPath = certPath;
            if (Path.IsPathRooted(certPath) == false)
            {
                rootedPath = System.Web.HttpContext.Current.Server.MapPath(certPath);
            }
            using (X509Certificate2 certificate = GetPublicKeyCertificate(rootedPath))
            {
                using (RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(enctext);
                    byte[] signatureBytes = Convert.FromBase64String(digitalSignature);
                    return  rsa.VerifyData(bytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
        }
        public static AsymmetricKeyParameter GetPrivateKeyParameter(string privateKeyPath)
        {
            string privateKeyText = System.IO.File.ReadAllText(privateKeyPath);
            AsymmetricKeyParameter asymmetricKeyParameter;
            using (TextReader privateKeyTextReader = new StringReader(privateKeyText))
            {
                PemReader pemReader = new PemReader(privateKeyTextReader, new PasswordFinder());
                // Check if ReadObject returns null
                object obj = pemReader.ReadObject();
                if (obj is AsymmetricKeyParameter)
                {
                    asymmetricKeyParameter = (AsymmetricKeyParameter)obj;
                }
                else
                {
                    asymmetricKeyParameter = (RsaPrivateCrtKeyParameters)obj;
                }
            }
            return asymmetricKeyParameter;
        }
        private class PasswordFinder : IPasswordFinder
        {
            public char[] GetPassword() => null;
        }
        public static string EncryptUsingSalt(string salt, string plaintext,string IV,string passphrase, CipherMode mode, PaddingMode pad)
        {
            byte[] iv = HexStringToByteArray(IV); // Convert Hex IV
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = GenerateAesKey(salt, passphrase); // Derive key           
                aes.IV = iv;
                aes.Mode = mode;
                aes.Padding = pad;
                // Create an encryptor to perform the stream transform.
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plaintext);
                            }
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public static string DecryptUsingSalt(string salt, string payload, string IV, string passphrase, CipherMode mode, PaddingMode pad)
        {
            string plainText = null;
            byte[] decodedCipherText = Convert.FromBase64String(payload);
            byte[] key = GenerateAesKey(salt, passphrase);
            byte[] iv = HexStringToByteArray(IV);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = mode;
                aesAlg.Padding = pad;

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(decodedCipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plainText = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return plainText;
        }
        public static byte[] GenerateAesKey(string salt, string passphrase)
        {
            using (var keyDerivation = new Rfc2898DeriveBytes(passphrase, HexStringToByteArray(salt), 10000, HashAlgorithmName.SHA1))
            {
                return keyDerivation.GetBytes(16); // AES 128-bit key
            }
        }
        public static string sha256(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = encoding.GetBytes(inputString);
                byte[] hash = sha256.ComputeHash(bytes);
                return ByteArrayToHexString(hash).ToLower();
            }
        }
        public static string sha512(string inputString)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = encoding.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                return ByteArrayToHexString(hash).ToLower();
            }
        }
        public static string ByteArrayToHexString(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string Base64ToString(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            return encoding.GetString(data);
        }
        public static string EncryptForPNB(string plainText)
        {
            System.Diagnostics.Debug.WriteLine("Inside EncryptUsingPNB");
            System.Diagnostics.Debug.WriteLine($"PlainText: {plainText}");

            string keyString = "bf91a235b5b64858bdb2d87d0f238d8d";
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
            //byte[] ivBytes = Encoding.UTF8.GetBytes("1234567890123456"); 
            byte[] ivBytes = keyBytes.Take(16).ToArray();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            //System.Diagnostics.Debug.WriteLine($"plainBytes Legth, plainBytes: {plainBytes.Length}, {BitConverter.ToString(plainBytes)}");
            //System.Diagnostics.Debug.WriteLine($"Key bytes Legth, Key bytes: {keyBytes.Length}, {BitConverter.ToString(keyBytes)}");
            //System.Diagnostics.Debug.WriteLine($"IV bytes Length, bytes : {ivBytes.Length}, {BitConverter.ToString(ivBytes)}");

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes);
            cipher.Init(true, parameters);

            byte[] cipherBytes = new byte[cipher.GetOutputSize(plainBytes.Length)];
            int len = cipher.ProcessBytes(plainBytes, 0, plainBytes.Length, cipherBytes, 0);

            //System.Diagnostics.Debug.WriteLine("Processed length: " + len);

            cipher.DoFinal(cipherBytes, len);

            string encryptedBase64 = Convert.ToBase64String(cipherBytes);
            System.Diagnostics.Debug.WriteLine($"Final encryptedBase64 return from EncryptUsingPNB: {encryptedBase64}");

            return encryptedBase64;
        }

        public static string DecryptUsingPNB(string base64CipherText)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine($"base64CipherText: {base64CipherText}");

                string keyString = "bf91a235b5b64858bdb2d87d0f238d8d";
                byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
                //byte[] ivBytes = Encoding.UTF8.GetBytes("1234567890123456"); 
                byte[] ivBytes = keyBytes.Take(16).ToArray();
                byte[] cipherBytes = Convert.FromBase64String(base64CipherText);

                //System.Diagnostics.Debug.WriteLine($"Cipher bytes Length, cipher: {cipherBytes.Length}, {BitConverter.ToString(cipherBytes)}");
                //System.Diagnostics.Debug.WriteLine($"Key bytes Length, Key bytes: {keyBytes.Length}, {BitConverter.ToString(keyBytes)}");
                //System.Diagnostics.Debug.WriteLine($"IV bytes Length, IV bytes : {ivBytes.Length}, {BitConverter.ToString(ivBytes)}");

                GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
                AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes);
                cipher.Init(false, parameters);

                byte[] plainBytes = new byte[cipher.GetOutputSize(cipherBytes.Length)];
                int len = cipher.ProcessBytes(cipherBytes, 0, cipherBytes.Length, plainBytes, 0);
                len += cipher.DoFinal(plainBytes, len);
                //cipher.DoFinal(plainBytes, len);

                //System.Diagnostics.Debug.WriteLine("DoFinal succeeded.");

                string decrypted = Encoding.UTF8.GetString(plainBytes).TrimEnd('\0');
                System.Diagnostics.Debug.WriteLine($"FinalDecryption: {decrypted}");

                string decryptedText;

                try
                {
                    decryptedText = Encoding.UTF8.GetString(plainBytes, 0, len);
                }
                catch (DecoderFallbackException ex)
                {
                    System.Diagnostics.Debug.WriteLine("UTF8 decoding failed: " + ex.Message);
                    decryptedText = Encoding.GetEncoding("ISO-8859-1").GetString(plainBytes).TrimEnd('\0');
                    decryptedText = "[ISO-Fallback] " + decryptedText;
                }

                System.Diagnostics.Debug.WriteLine($"Final Encrypted Bytes: {decryptedText}");

                return decryptedText;
            }
            catch (InvalidCipherTextException ex)
            {
                throw new Exception("Decryption failed: MAC check in GCM failed", ex);
            }
            catch (FormatException ex)
            {
                throw new Exception("Decryption failed: invalid Base64 input", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Decryption failed: " + ex.Message, ex);
            }
        }
    }
    public static class DataTableExtensions
    {
        public static string ToJson(this DataTable table)
        {
            var array = new JArray();

            foreach (DataRow row in table.Rows)
            {
                var obj = new JObject();
                foreach (DataColumn col in table.Columns)
                {
                    obj[col.ColumnName] = JToken.FromObject(row[col]);
                }
                array.Add(obj);
            }

            return JsonConvert.SerializeObject(array);
        }
        public static void CleanColumnName(this DataTable table)
        {
            char[] charsToRemove = new char[] { '.', ':', '[', ']' };
            foreach (DataColumn column in table.Columns)
            {
                string originalName = column.ColumnName;
                string newName = originalName;                
                foreach (char charToRemove in charsToRemove)
                {
                    newName = newName.Replace(charToRemove.ToString(), string.Empty);
                }
                if (originalName != newName)
                {
                    column.ColumnName = newName;
                }
            }
        }
    }
    public class FormatIndianNumber : ICustomFunctionOperator
    {
        public string Name => "FormatIndianNumber";
        public object Evaluate(params object[] operands)
        {
            if (operands.Length == 0) return null;
            if (operands[0] == null || string.IsNullOrWhiteSpace(operands[0].ToString())) return null;
            decimal number = Convert.ToDecimal(operands[0]);
            bool includeSymbol = operands.Length > 1 && Convert.ToBoolean(operands[1]);
            string formatted = string.Format(new CultureInfo("en-IN"), "{0:N2}", number);
            return includeSymbol ? $"₹ {formatted}" : formatted;
        }
        public FunctionOperatorType OperatorType => FunctionOperatorType.Custom;

        public Type ResultType(params Type[] operands) => typeof(string);
    }
    public class FormatDateString : ICustomFunctionOperator
    {
        public string Name => "FormatDateString";
        public object Evaluate(params object[] operands)
        {
            if (operands.Length < 2) return null;
            if (operands[0] == null || string.IsNullOrWhiteSpace(operands[0].ToString())) return null;
            string inputDate = operands[0].ToString();
            string outputFormat = operands[1].ToString();
            try
            {
                DateTime parsedDate = Convert.ToDateTime(inputDate);
                return parsedDate.ToString(outputFormat);
            }
            catch
            {
                return null; // or return inputDate if you prefer fallback
            }
        }
        public FunctionOperatorType OperatorType => FunctionOperatorType.Custom;
        public Type ResultType(params Type[] operands) => typeof(string);
    }
}