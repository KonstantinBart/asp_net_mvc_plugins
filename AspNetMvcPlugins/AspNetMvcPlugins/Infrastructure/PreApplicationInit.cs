using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(AspNetMvcPlugins.Infrastructure.PreApplicationInit), "InitializePlugins")]
namespace AspNetMvcPlugins.Infrastructure
{
	public class PreApplicationInit
	{
		public static void InitializePlugins()
		{
			
		}
	}
}