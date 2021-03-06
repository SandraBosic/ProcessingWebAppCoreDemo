﻿using System.Collections.Generic;

namespace Contracts.DataEntities.RecurringPayments
{
    public class RecurringBatchPaginationList
    {
        public List<Batch> Batches { get; set; }
        public int TotalCount { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public bool IsPreviousPage { get; set; }

        public bool IsNextPage { get; set; }
    }
}
