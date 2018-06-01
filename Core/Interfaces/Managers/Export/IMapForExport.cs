using System.Collections.Generic;

namespace Core.Interfaces.Managers.Export
{
    public interface IMapForExport
    {
        string[][] Map<T>(IEnumerable<T> data);
    }
}
