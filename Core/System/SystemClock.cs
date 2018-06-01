using Core.Interfaces.System;
using System;

namespace Core.System
{
    public sealed class SystemClock : IClock
    {
        DateTime IClock.Now
        {
            get { return DateTime.Now; }
        }
    }
}
