using Autofac;
using Common;

namespace DocFinderPlugin
{
    public class DocFinderPlugin : PluginModule
    {
        public override string ToString()
        {
            return ".doc searcher";
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Finder>().As<IFinder>();
        }
    }
}
