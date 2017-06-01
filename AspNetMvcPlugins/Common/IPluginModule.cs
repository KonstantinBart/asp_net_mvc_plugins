using Autofac.Core;

namespace Domain.Common
{
    public interface IPluginModule : IModule
    {
        string Name { get; }
    }
}
