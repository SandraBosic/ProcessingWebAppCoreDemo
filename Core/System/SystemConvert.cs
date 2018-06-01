using Core.Interfaces.System;
using System;

namespace Core.System
{
    public sealed class SystemConvert : IConvert
    {
        decimal IConvert.ToDecimal(string value)
        {
            return Convert.ToDecimal(value);
        }
    }
}
