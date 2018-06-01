using System;

namespace Contracts.Exceptions
{
    public class BatchReportNotFoundForProcessedBatch : Exception
    {
        private static string Message = "Batch was processed but no report was found";
        public BatchReportNotFoundForProcessedBatch() : base(Message)
        {}
    }
}
