using SignalR.Hubs;
using StructureMap;

namespace PlayR.Infrastructure
{
    public class StructureMapHubActivator : IHubActivator
    {
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)ObjectFactory.GetInstance(descriptor.Type);
        }
    }
}