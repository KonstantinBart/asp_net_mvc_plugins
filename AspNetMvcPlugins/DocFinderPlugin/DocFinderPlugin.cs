using Autofac;
using Domain.Common;

namespace MvcPlugin
{
    public class DocFinderPlugin : PluginModule
    {
        public override string ToString()
        {
            return ".doc";
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Finder>().As<IFinder>().IfNotRegistered(typeof(DocFinderPlugin));
        }
    }
}
