using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using XMLApplication.Models;

namespace XMLApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<ProjectModels> lstProject = new List<ProjectModels>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/XML/ProjectList.xml"));
            DataView dvPrograms;
            dvPrograms = ds.Tables[0].DefaultView;
            dvPrograms.Sort = "Id";
            foreach(DataRowView dr in dvPrograms)
            {

                ProjectModels model = new ProjectModels();
                model.Id = Convert.ToInt32(dr[0]);
                model.ProjectName = Convert.ToString(dr[1]);
                model.Location = Convert.ToString(dr[2]);
                lstProject.Add(model);
            }
            if (lstProject.Count > 0)
            {
                return View(lstProject);
            }


            return View();
        }

        ProjectModels model = new ProjectModels();
        public ActionResult AddEditProject(int ? id)
        {
            int Id = Convert.ToInt32(id);
            if (Id > 0)
            {
                GetDetailsById(Id);
                model.IsEdit = true;
                return View(model);
            }
            else
            {
                model.IsEdit = false;
                return View(model);
            }
        }

        public void GetDetailsById(int Id)
        {

            XDocument oXmlDocument = XDocument.Load(Server.MapPath("~/XML/ProjectList.xml"));
            var items = (from item in oXmlDocument.Descendants("Project")
                         where
                        Convert.ToInt32(item.Element("Id").Value) == Id
                         select new projectItems
                         {
                             Id = Convert.ToInt32(item.Element("Id").Value),
                             ProjectName = item.Element("ProjectName").Value,
                             Location = item.Element("Location").Value,

                         }).SingleOrDefault();
           if (items != null)
            {
                model.Id = items.Id;
                model.ProjectName = items.ProjectName;
                model.Location = items.Location;
            }
        }


        public class projectItems
        {
            public int Id
            {
                get;
                set;
            }
            public string ProjectName
            {
                get;
                set;
            }
            public string Location
            {
                get;
                set;
            }
            public projectItems() { }
        }
    }
}