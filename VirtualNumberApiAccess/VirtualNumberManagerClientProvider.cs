
using VirtualNumberManager;

namespace VirtualNumberApiAccess
{
    public sealed  class VirtualNumberManagerClientProvider : IProviderVirtualNumberManagerClient
    {
        VirtualNumberManagerClient IProviderVirtualNumberManagerClient.Get()
        {
            return new VirtualNumberManagerClient();
        }
    }
}
