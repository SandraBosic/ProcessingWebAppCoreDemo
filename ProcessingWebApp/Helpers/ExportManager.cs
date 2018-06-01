using Core.Interfaces.DependencyResolution;
using Core.Interfaces.Managers.Export;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Interfaces.System;
using Microsoft.AspNetCore.Mvc;
using static System.Text.Encoding;

namespace Managers.Export
{
    public class ExportManager : Controller, IManageExport
    {
        private readonly IMapForExport mapper;
        private readonly IClock clock;

        public ExportManager(IMapForExport mapper, IClock clock)
        {
            this.mapper = mapper;
            this.clock = clock;
        }

        private string ContentType => "text/csv; charset=windows-1252";

        private string FileNameExtension => ".csv";

        private byte[] Convert<T>(List<T> data)
        {
            string csv = ConvertToCsvString(data);
            var windows1252 = GetEncoding(1252);
            return windows1252.GetBytes(csv);
        }

        private string ConvertToCsvString<T>(IEnumerable<T> data)
        {
            var comma = "\";=\"";
            var lines = mapper.Map(data).Select(line => "\"" + string.Join(comma, line) + "\"");

            return string.Join("\r\n", lines) + "\r\n";
        }

        public FileContentResult ConstructExportDocument<T>(List<T> results)
        {
            byte[] document = Convert(results);
            var fileName = string.Format(typeof(T).Name + "_{0}{1}", clock.Now.ToString("MMddyyyyHHmmssfff"), FileNameExtension);
            return File(document, ContentType, fileName);
        }
    }
}
