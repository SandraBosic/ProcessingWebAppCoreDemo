using Contracts.DataAttributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Filters.RecurringBatches
{
    public class BatchFilter
    {
        [FromDateCannotBeGreatedThanToDate]
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [DisplayName("Filter")]
        public string TextSearch { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? OpenBatch { get; set; }
    }
}
