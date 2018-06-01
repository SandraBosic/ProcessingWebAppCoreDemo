using System;

namespace Core.Interfaces.VirtualNumberApiAccess
{
    public interface IVirtualNumberApiAccess
    {
        string GetCardForVirtualNumber(Guid virtualNumber);
        string Get6Plus4CardForVirtualNumber(Guid virtualNumber);
    }
}
