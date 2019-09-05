using System;
using System.Text.RegularExpressions;

namespace Dynata.SamplifyAPIClient
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    internal static class Validator
    {
        private const string errRequiredFieldEmpty = "Required field is empty.";
        private const string errInvalidFieldValue = "Invalid field value.";
        private const string emailRegexPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        private const string urlRegexPattern = @"^((https?):\/\/)?(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";

        private static readonly Regex emailRegex = new Regex(emailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        private static readonly Regex urlRegex = new Regex(urlRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        // Validates a non-null IValidator object through IValidator.IsValid() 
        // Null instance validates.
        internal static void Validate(IValidator instance)
        {
            if (instance != null)
            {
                instance.IsValid();
            }
        }

        // Validates a non-null IValidator array through IValidator.IsValid() for each array item.
        // Null/empty array validates, but validation fails if an array item inside, is null.
        internal static void ValidateAll(IValidator[] instances)
        {
            if (instances != null)
            {
                foreach (var item in instances)
                {
                    if (item == null)
                    {
                        throw new ValidationException(errInvalidFieldValue);
                    }
                    item.IsValid();
                }
            }
        }

        // Validation fails if the `instances` array or any of its item is null
        internal static void IsNotNull(params object[] instances)
        {
            if (instances == null || instances.Length == 0)
            {
                throw new ValidationException(errRequiredFieldEmpty);
            }
            foreach (var item in instances)
            {
                if (item == null)
                {
                    throw new ValidationException(errRequiredFieldEmpty);
                }
            }
        }

        internal static void IsNonZero<T>(T value) where T : IComparable<T>
        {
            if (value.CompareTo(default(T)) <= 0)
            {
                throw new ValidationException(errRequiredFieldEmpty);
            }
        }

        // Validation fails if the string array is null or empty.
        // Or any of its item is null, empty or whitespace only.
        internal static void IsNonEmptyString(params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ValidationException(errRequiredFieldEmpty);
            }
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ValidationException(errRequiredFieldEmpty);
                }
            }
        }

        // Validates on null or empty array.
        // Validation fails if any of the array's item is null or not an email.
        internal static void IsEmail(string[] emails)
        {
            if (emails != null)
            {
                foreach (var e in emails)
                {
                    if (e == null || !emailRegex.Match(e).Success)
                    {
                        throw new ValidationException(errInvalidFieldValue);
                    }
                }
            }
        }

        // Validates on null or empty array.
        // Validation fails if any of the array's item is null
        internal static void IsDeviceType(string[] devices)
        {
            if (devices != null)
            {
                foreach (var device in devices)
                {
                    if (device == null ||
                        (device != DeviceTypeConstants.DeviceTypeDesktop &&
                         device != DeviceTypeConstants.DeviceTypeMobile &&
                         device != DeviceTypeConstants.DeviceTypeTablet))
                    {
                        throw new ValidationException(errInvalidFieldValue);
                    }
                }
            }
        }

        internal static void IsExclusionTypeOrNull(string exType)
        {
            if (exType != null &&
                exType != ExclusionTypeConstants.ExclusionTypeProject &&
                exType != ExclusionTypeConstants.ExclusionTypeTag)
            {
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsUrlOrNull(string url)
        {
            if (url != null && !urlRegex.Match(url).Success)
            {
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsCountryCodeOrNull(string countryCode)
        {
            if (countryCode != null)
            {
                foreach (var cc in ISOCodes.CountryCodes)
                {
                    if (countryCode == cc)
                    {
                        return;
                    }
                }
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsLanguageCodeOrNull(string languageCode)
        {
            if (languageCode != null)
            {
                foreach (var lc in ISOCodes.LanguageCodes)
                {
                    if (languageCode == lc)
                    {
                        return;
                    }
                }
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsActionOrNull(string action)
        {
            if (action != null &&
                action != ActionConstants.ActionClosed &&
                action != ActionConstants.ActionLaunched &&
                action != ActionConstants.ActionPaused)
            {
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsDeliveryTypeOrNull(string deliveryType)
        {
            if (deliveryType != null &&
                deliveryType != DeliveryTypeConstants.Slow &&
                deliveryType != DeliveryTypeConstants.Balanced &&
                deliveryType != DeliveryTypeConstants.Fast)
            {
                throw new ValidationException(errInvalidFieldValue);
            }
        }

        internal static void IsOperatorType(string operatorType)
        {
            if (operatorType != null &&
                operatorType != OperatorTypeConstants.Include &&
                operatorType != OperatorTypeConstants.Exclude)
            {
                throw new ValidationException(errInvalidFieldValue);
            }
        }
    }

    internal interface IValidator
    {
        void IsValid();
    }
}