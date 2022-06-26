using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Project.MVC.Web.Extensions;
using Sitecore.Project.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Sitecore.Project.MVC.Web.Controllers
{
	public class NavigationController : Controller
	{
		// GET: Navigation
		public ActionResult Index()
		{
			//Get Item by ID
			var aboutItem = Sitecore.Context.Database.GetItem(new Data.ID("{3E5F0C04-CB80-40B4-B6DB-1C5A7C2D517C}"));
			var readItemId = aboutItem.ID;
			var readItemName = aboutItem.Name;

			var aboutItemByPath = Sitecore.Context.Database.GetItem("/sitecore/content/Builderz/Home/About Us");
			var readItemPath = aboutItemByPath.Paths.FullPath;
			var displayName = aboutItemByPath.DisplayName;

			var model = new NavigationViewModel();
			List<Navigation> navigations = new List<Navigation>();
			var homeItem = Sitecore.Context.Site.HomeItem();

			navigations.Add(BuildNavigation(homeItem));

			if (homeItem.HasChildren)
			{
				foreach (Item childItem in homeItem.Children)
				{
					CheckboxField hideInNavigation = childItem.Fields["Hide_In_Navigation"];
					if (!hideInNavigation.Checked)
					{
						navigations.Add(BuildNavigation(childItem));
					}
				}
			}
			model.Navigations = navigations;
			return View(model);
		}

		private Navigation BuildNavigation(Item item)
		{
			return new Navigation
			{
				NavigationTitle = item.Fields["Navigation_Title"]?.Value,
				NavigationLink = item.URL(),
				ActiveClass = PageContext.Current.Item.ID == item.ID ? "Active" : string.Empty
			};
		}

		public ActionResult HeaderNavigationByDS()
		{
			var model = new NavigationViewModel();
			List<Navigation> parentNavigations = new List<Navigation>();

			var dataSource = RenderingContext.Current?.Rendering.Item;

			parentNavigations.Add(BuildNavigation(dataSource));

			if (dataSource != null && dataSource.HasChildren)
			{
				foreach (Item parentItem in dataSource.Children)
				{
					var parentNavigation = BuildNavigationByDS(parentItem);
					var childNavigations = new List<Navigation>();

					if (parentItem.HasChildren)
					{
						foreach (Item childItem in parentItem.Children)
						{
							var childNavigation = BuildNavigationByDS(childItem);
							childNavigations.Add(childNavigation);
						}

						parentNavigation.ChildNavigations = childNavigations;
					}
					parentNavigations.Add(parentNavigation);
				}
			}
			model.Navigations = parentNavigations;

			return View(model);
		}

		private Navigation BuildNavigationByDS(Item item)
		{
			return new Navigation
			{
				NavigationTitle = item.Fields["Navigation_Title"]?.Value,
				NavigationLink = ((LinkField)item.Fields["Navigation_Link"]).GetFriendlyUrl(),
				ActiveClass = PageContext.Current.Item.ID == item.ID ? "Active" : string.Empty
			};
		}
	}
}