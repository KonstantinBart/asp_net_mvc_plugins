using Autofac;

namespace Domain.Common
{
    public abstract class PluginModule : Module, IPluginModule
    {
        public virtual string Name
        {
            get { return GetType().Name; }
        }

        public virtual string ControllerName
        {
			get { return GetType().Name.Substring(0, 3); }
        }
    }
}
