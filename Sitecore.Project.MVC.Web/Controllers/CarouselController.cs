using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Project.MVC.Web.Models;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Project.MVC.Web.Extensions;

namespace Sitecore.Project.MVC.Web.Controllers
{
    public class CarouselController : Controller
    {
      public ActionResult Index()
        {
            Sitecore.Context.Site.HomeItem();

            var model = new CarouselModel();
            List<Slide> slides = new List<Slide>();

            var dataSource = RenderingContext.Current?.Rendering.Item;
            MultilistField slidesField = dataSource.Fields["Slides"];

            //Rendering Parameters
            var slideCountPara = RenderingContext.Current?.Rendering.Parameters["SlideCount"];
            int.TryParse(slideCountPara, out int result);
            int slideCount = result == 0 ? 2 : result;

            if (slidesField?.Count>0)
			{
                var slideItems = slidesField.GetItems();
				foreach (var slideItem in slideItems.Take(slideCount))
				{
                    // var titleField = slideItem.Fields["Title"];
                    var title = new MvcHtmlString(FieldRenderer.Render(slideItem, "Title"));
                    var subTitle = new MvcHtmlString(FieldRenderer.Render(slideItem, "Sub_Title"));
                    var image = new MvcHtmlString(FieldRenderer.Render(slideItem, "Image"));
                    var CallToAction = new MvcHtmlString(FieldRenderer.Render(slideItem, "Call_To_Action", "class=btn animated fadeInUp"));

                    slides.Add(new Slide
                    {
                        Title=title,
                        SubTitle=subTitle,
                        Image=image,
                        CallToAction=CallToAction
                    });
                }
                model.Slides = slides;

			}
             
            return View(model);
        }
    }
}