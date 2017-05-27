using Autofac;

namespace Common
{
    public abstract class PluginModule : Module, IPluginModule
    {
        public virtual string Name
        {
            get { return GetType().Name; }
        }

    }
}
