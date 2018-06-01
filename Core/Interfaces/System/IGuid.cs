using System;

namespace Core.Interfaces.System
{
    public interface IGuid
    {
        Guid New { get; }

        Guid Empty { get; }

        bool Parse(string input, out Guid result);
    }
}
