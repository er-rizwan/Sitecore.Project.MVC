﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Project.MVC.Web.Models
{
	public class CarouselModel
	{
		public List<Slide> Slides { get; set; }
	}
	public class Slide
	{
		public MvcHtmlString Title { get; set; }
		public MvcHtmlString SubTitle { get; set; }
		public MvcHtmlString Image { get; set; }
		public MvcHtmlString CallToAction { get; set; }
	}
}