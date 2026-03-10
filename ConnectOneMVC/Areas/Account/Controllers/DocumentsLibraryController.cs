using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using Common_Lib;
using Common_Lib.RealTimeService;


using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

using System.Drawing;

using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Controllers
{

    public class DocumentsLibraryController : BaseController
    {
        public List<Return_GetDocumentList> DocLib_Data
        {
            get
            {
                return (List<Return_GetDocumentList>)GetBaseSession("DocLib_Data_DocLibInfo");
            }
            set
            {
                SetBaseSession("DocLib_Data_DocLibInfo", value);
            }
        }

        public ActionResult Frm_Doc_Library()
        {
            Frm_Doc_Library_Models model = new Frm_Doc_Library_Models();
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Account_Document_Library, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            var documents = BASE._Docs_DBOps.GetDocuments();
            if ((documents == null))
            {
                return View();
            }
            else
            {
                model.GridView1 = ConvertToList(documents);
                DocLib_Data = model.GridView1;

                return View(model);
            }
        }
        public ActionResult Frm_Doc_Library_Grid(string command)
        {
            if (DocLib_Data == null || command == "REFRESH")
            {
                DataTable Documents_Library_Data = BASE._Docs_DBOps.GetDocuments();
                //DataView dview = new DataView(D1);
                //var Documents_Library_Data = DatatableToModel.DataTabletoLookUp_GetDocumentList_INFO(dview.ToTable());
                DocLib_Data = ConvertToList(Documents_Library_Data);
            }

            List<Return_GetDocumentList> _Documents_Library_Data = DocLib_Data;

            return View(_Documents_Library_Data);
        }

        public ActionResult DocLibCustomDataAction(string key)
        {
            var Final_Data = DocLib_Data;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.ID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.ID + "![" + it.Title + "![" + it.Category + "![" + it.FileName + "![" + it.FileLocationPath;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public List<Return_GetDocumentList> ConvertToList(DataTable Dt)
        {
            var data = new List<Return_GetDocumentList>();
            if (Dt.Rows.Count == 0)
            {
                return data;
            }
            else
            {
                string fileLocationPath = ConfigurationManager.AppSettings["FileStorePhysicalPath"].ToString();
                foreach (DataRow row in Dt.Rows)
                {

                    var newrow = new Return_GetDocumentList();
                    newrow.ID = row.Field<string>("ID");
                    newrow.Title = row.Field<string>("Title");
                    newrow.FileName = row.Field<string>("FileName");
                    newrow.Category = row.Field<string>("Category");
                    newrow.FileLocationPath = fileLocationPath + row.Field<string>("FileName");

                    data.Add(newrow);
                }
                return data;
            }
        }
        public ActionResult View_Document(string Doc_Filename, string Title)
        {
            
            try
            {

                string Doc_FilePath = Path.Combine(ConfigurationManager.AppSettings["FileStorePhysicalPath"], Doc_Filename);
                FileInfo file = new FileInfo(Doc_Filename);



                Response.AppendHeader("Content-Disposition", "inline; filename=" + Doc_Filename);

                Response.ContentType = ReturnExtension(file.Extension.ToLower());


                return Json(new
                {
                    Message = "Document Found...!",
                    result = true,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new
                {
                    Message = "Document Not Found...!",
                    result = false,
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".docx":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }

        }
        public void SessionClear()
        {
            ClearBaseSession("_DocLibInfo");
        }

    }
}
