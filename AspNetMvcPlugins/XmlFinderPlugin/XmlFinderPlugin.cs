using Autofac;
using Domain.Common;

namespace XmlFinderPlugin
{
	public class XmlFinderPlugin : PluginModule
	{
		public override string ToString()
		{
			return ".xml";
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<Finder>().As<IFinder>();
		}
	}
}
