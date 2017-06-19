using Autofac;
using Domain.Common;

namespace MvcPlugin
{
    public class TxtFinderModule : PluginModule
    {
        public override string ToString()
        {
            return ".txt";
        }

        protected override void Load(ContainerBuilder builder)
        {
			builder.RegisterType<Finder>().As<IFinder>().IfNotRegistered(typeof(TxtFinderModule)); ;
        }
    }
}
