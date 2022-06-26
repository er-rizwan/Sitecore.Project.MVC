using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Project.MVC.Web.Extensions
{
	public static class ItemExtensions
	{
		public static string URL(this Item item,ItemUrlBuilderOptions options=null) {
			if (item == null)
			{
				throw new NullReferenceException();
			}
			if(options!=null)
			{
				return LinkManager.GetItemUrl(item,options);
			}

			return LinkManager.GetItemUrl(item);
		}
	}
} 