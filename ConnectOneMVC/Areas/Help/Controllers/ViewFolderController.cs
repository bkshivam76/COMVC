using ConnectOneMVC.Areas.Help.Models;
using DevExpress.Office.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Help.Controllers
{
    public class ViewFolderController : Controller
    {
        // GET: Help/ViewFolder
        public ActionResult ViewFiles()
        {
            DirectoryInfo viewFolder = null;
            List<ViewFile> model= new List<ViewFile>();
            FileInfo[] files = null;
            string Path = ConfigurationManager.AppSettings["ViewFolderPath"];
            viewFolder = new DirectoryInfo(Path);
            files = viewFolder.GetFiles();
            files = files.OrderBy(f => f.Name).ToArray();
            for (int i = 0; i < files.Count(); i++)
            {
                ViewFile data = new ViewFile();
                data.Name = files[i].Name;
                data.Extension = files[i].Extension;
                data.Size = Math.Round((double)files[i].Length / (1024 * 1024),2);
                data.CreationDate = files[i].CreationTime.ToString();
                data.ModifyDate = files[i].LastAccessTime.ToString();
                model.Add(data);
            }     
            return View(model);
        }
    }
}