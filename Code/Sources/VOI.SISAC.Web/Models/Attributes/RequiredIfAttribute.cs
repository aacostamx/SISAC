//-----------------------------------------------------------------------------
// <copyright file="RequiredIfAttribute.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//--------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Class RequiredIfAttribute
    /// </summary>
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="desiredvalue">The desiredvalue.</param>
        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
            innerAttribute = new RequiredAttribute();
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>Validation Result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var dependentValue = context.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(context.ObjectInstance, null);

            if (Convert.ToString(DesiredValue.ToString()) == "")
            {
                if (dependentValue != null)
                {
                    if (!innerAttribute.IsValid(value))
                    {
                        return new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName });
                    }
                }
            }
            else
            {
                if (dependentValue.ToString() == DesiredValue.ToString())
                {
                    if (!innerAttribute.IsValid(value))
                    {
                        return new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        private String PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the desired value.
        /// </summary>
        /// <value>
        /// The desired value.
        /// </value>
        private Object DesiredValue { get; set; }

        /// <summary>
        /// The innerattribute
        /// </summary>
        private readonly RequiredAttribute innerAttribute;

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "requiredif",
            };
            rule.ValidationParameters["dependentproperty"] = (context as ViewContext).ViewData.TemplateInfo.GetFullHtmlFieldId(PropertyName);
            rule.ValidationParameters["desiredvalue"] = DesiredValue is bool ? DesiredValue.ToString().ToLower() : DesiredValue;

            yield return rule;
        }
    }
}