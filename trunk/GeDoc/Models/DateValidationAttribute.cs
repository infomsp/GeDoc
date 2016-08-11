namespace GeDoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public enum ValidationType
    {
        RangeValidation,
        Compare
    }

    public class DateValidationAttribute: ValidationAttribute
    {
        public DateValidationAttribute()
        { }

        private ValidationType _validationType;
        private DateTime? _fromDate;
        private DateTime _toDate;
        private string _defaultErrorMessage;
        private string _propertyNameToCompare;

        public DateValidationAttribute(ValidationType validationType, string message, string compareWith = "", string fromDate = "")
        {
            _validationType = validationType;
            switch (validationType)
            {
                case ValidationType.Compare:
                    {
                        _propertyNameToCompare = compareWith;
                        _defaultErrorMessage = message;
                        break;
                    }
                case ValidationType.RangeValidation:
                    {
                        _fromDate = new DateTime(2000, 1, 1);
                        _toDate = DateTime.Today;
                        _defaultErrorMessage = message;
                        break;
                    }
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            switch (_validationType)
            {
                case ValidationType.Compare:
                {
                    var baseProperyInfo = validationContext.ObjectType.GetProperty(_propertyNameToCompare);
                    var startDate = (DateTime)baseProperyInfo.GetValue(validationContext.ObjectInstance, null);
     
                    if(value!=null)
                    {
                        DateTime thisDate = (DateTime)value;
                        Type classType = typeof(catFacturas);//pModel.GetType();
                        PropertyInfo methodInfo = classType.GetProperty(_propertyNameToCompare);
                        DisplayAttribute displayAttr = (DisplayAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(DisplayAttribute));
                        if (thisDate < startDate)
                        {
                            string message = string.Format(_defaultErrorMessage, validationContext.DisplayName, displayAttr.Name);
                            return new ValidationResult(message);
                        }
                    }
                    break;
                }
                case ValidationType.RangeValidation:
                    {
                        //Range Validation Logic Here
                        break;
                    }
          
            }
            return null;
        }
    }
}
