using Sitecore.Mvc.Presentation;
using Sitecore.Project.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Project.MVC.Web.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            var model = new AboutViewModel() 
            {
                item = RenderingContext.Current?.Rendering.Item
            };
            
            return View(model);
        }
    }
}