using System;

namespace Core.Interfaces.System
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
