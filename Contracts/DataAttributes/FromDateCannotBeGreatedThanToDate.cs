using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Contracts.Filters.RecurringBatches;

namespace Contracts.DataAttributes
{
    public class FromDateCannotBeGreatedThanToDate : ValidationAttribute
    {
        private const string DefaultErrorMessage = "From date cannot be greater than to date";
        public FromDateCannotBeGreatedThanToDate() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fromDate = Convert.ToDateTime(value);
            var batchFilter = validationContext.ObjectInstance as BatchFilter;

            if (fromDate > batchFilter.To)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
