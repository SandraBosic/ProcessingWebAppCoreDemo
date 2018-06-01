using System;
using Core.Interfaces.VirtualNumberApiAccess;
using VirtualNumberManager;

namespace VirtualNumberApiAccess
{
    public sealed class VirtualNumberApiAccess : IVirtualNumberApiAccess
    {
        private readonly IVirtualNumberManager virtualNumberManager;

        public VirtualNumberApiAccess(IProviderVirtualNumberManagerClient providerVirtualNumberManagerClient)
        {
            //this.virtualNumberManager = providerVirtualNumberManagerClient.Get();
        }

        public string GetCardForVirtualNumber(Guid virtualNumber)
        {
            //var card = virtualNumberManager.GetCardNumber(virtualNumber);
            //var cardData = virtualNumberManager.GetCardData(virtualNumber);
            //return card + "-" + cardData.ValidToDate;

            return "";
        }

        public string Get6Plus4CardForVirtualNumber(Guid virtualNumber)
        {
            //var cardData = virtualNumberManager.GetCardData(virtualNumber);
            //if (cardData == null)
            //{
            //    return string.Empty;
            //}
            //return cardData.MaskedCardNumber;

            return "";
        }
    }
}
