using Core.Interfaces.System;
using System;

namespace Core.System
{
    public sealed class SystemGuid : IGuid
    {
        public Guid New
        {
            get { return Guid.NewGuid(); }
        }

        public Guid Empty
        {
            get { return Guid.Empty; }
        }

        public bool Parse(string input, out Guid result)
        {
            return Guid.TryParse(input, out result);
        }
    }
}
