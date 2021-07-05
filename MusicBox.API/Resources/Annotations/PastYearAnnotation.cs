using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MusicBox.API.Resources.Annotations
{
    public class PastYearAttribute : ValidationAttribute
    {
        public PastYearAttribute()
        {
        }

        const string DefaultErrorMessage = "{0} must be before or equal to the current date";

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null)
            {
                ErrorMessage = DefaultErrorMessage;
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        public override bool IsValid(object value)
        {
            var year = (int)value;
            return year <= DateTime.Now.Year;
        }
    }
}
