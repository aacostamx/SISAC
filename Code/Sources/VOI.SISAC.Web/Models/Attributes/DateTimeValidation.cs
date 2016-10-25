//------------------------------------------------------------------------
// <copyright file="DateTimeValidation.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// Class DateTimeValidation
    /// </summary>
    public class DateTimeValidation : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dt;

                var culture = CultureHelper.GetCurrentCulture();
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(culture);
                DateTimeStyles styles;
                styles = DateTimeStyles.None;

                bool parsed = DateTime.TryParse(value.ToString(), cultureInfo, styles, out dt);

                if (!parsed)
                {
                    return new ValidationResult("Please Enter a Valid Date.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult(validationContext.DisplayName + " is required ");
            }
        }
    }
}