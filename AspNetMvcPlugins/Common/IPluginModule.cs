using Autofac.Core;

namespace Common
{
    public interface IPluginModule : IModule
    {
        string Name { get; }
    }
}
