using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Core.Interfaces.Managers.Export
{
    public interface IManageExport
    {
        FileContentResult ConstructExportDocument<T>(List<T> results);
    }
}
