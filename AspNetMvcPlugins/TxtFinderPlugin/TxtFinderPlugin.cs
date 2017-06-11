using Autofac;
using Domain.Common;

namespace TxtFinderPlugin
{
    public class TxtFinderPlugin : PluginModule
    {
        public override string ToString()
        {
            return ".txt";
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Finder>().As<IFinder>().IfNotRegistered(typeof(TxtFinderPlugin)); ;
        }
    }
}
