using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KevPOS.Validation.Infrastructure
{
    public class ValidatorResult
    {
        #region Constructor

        public ValidatorResult()
        {
            Errors = new List<string>();
        }

        public ValidatorResult(int entityId)
        {
            IsNew = entityId <= 0;
            Errors = new List<string>();
        }

        #endregion

        #region Properties

        public bool IsNew { get; set; }

        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }

        public bool HasErrors
        {
            get { return !IsValid; }
        }

        public dynamic Entity { get; set; }

        public IList<string> Errors { get; set; }

        #endregion

        #region Public Methods

        public void Add(ValidatorResult validate)
        {
            foreach (string e in validate.Errors)
            {
                Errors.Add(e);
            }
        }

        public void AddException(Type type, string methodName, string friendlyMessage, Exception ex)
        {
            //Log4NetLogger.LogEntry(type, methodName, friendlyMessage, LoggerLevel.Error, ex);
            Errors.Add(friendlyMessage);
        }

        public string CollateErrors()
        {
            var sb = new StringBuilder();
            int a = 1;
            foreach (string e in Errors)
            {
                sb.AppendFormat("{0} - {1} ", a, e);
                a++;
            }
            return sb.ToString();
        }

        #endregion

        #region Validation Helpers

        /// <summary>
        ///     Returns true if valid
        ///     todo add a regex or better password validation/errors
        ///     Otherwise returns false and adds an error to the list
        /// </summary>
        public bool ValidatePassword(string value, string title = "Password")
        {
            // TODO?
            // Regex validation?

            return ValidateLength(title, value, 6, 20);
        }

        //public bool ValidateEmail(string emailAddress, string title = "Email address")
        //{
        //    if (!Tools.StringManipulation.IsValidEmailAddress(emailAddress))
        //    {
        //        Errors.Add(string.Format("The {0} entered is invalid.", title));
        //        return false;
        //    }

        //    return ValidateLength(title, emailAddress, 6, 100);
        //}
        /// <summary>
        ///     Returns true if valid
        ///     Otherwise returns false and adds an error to the list
        /// </summary>
        /// <summary>
        ///     Checks if value is not null
        ///     If valid returns true
        ///     Otherwise returns false and adds an error to the errors list.
        /// </summary>
        public bool ValidateNotNull(string title, object value)
        {
            if (value != null) return true;

            Errors.Add(string.Format("{0} is not valid", title));
            return false;
        }

        /// <summary>
        ///     Checks if value greater than 0
        ///     If valid returns true
        ///     Otherwise returns false and adds an error to the errors list.
        /// </summary>
        public bool ValidateGreaterThanZero(string title, int value)
        {
            if (value >= 1) return true;

            Errors.Add(string.Format("{0} is a required field", title));
            return false;
        }

        /// <summary>
        ///     Checks if value greater than 0
        ///     If valid returns true
        ///     Otherwise returns false and adds an error to the errors list.
        /// </summary>
        public bool ValidateGreaterThanZero(string title, decimal value)
        {
            if (value >= 1) return true;

            Errors.Add(string.Format("{0} is a required field", title));
            return false;
        }

        /// <summary>
        ///     Check if the string has a value then check its length
        ///     Returns true if between the min max specified
        ///     Otherwise adds an error to the Errors collection and returns false
        /// </summary>
        public bool ValidateLength(string title, string value, int min, int max,
            string format = "{0} must be between {1} and {2} characters long.")
        {
            // Already got an error, exit.
            if (!ValidateNotEmpty(title, value)) return false;

            if (value.Length >= min && value.Length <= max) return true;
            Errors.Add(string.Format(format, title, min, max));
            return false;
        }

        /// <summary>
        ///     Returns true if string isn't empty
        ///     Otherwise returns false and adds an error to the errors list
        /// </summary>
        public bool ValidateNotEmpty(string title, string value, string message = "{0} is a required field")
        {
            if (!string.IsNullOrEmpty(value)) return true;

            Errors.Add(string.Format(message, title));

            return false;
        }


        /// <summary>
        ///     Returns true if strings match
        ///     Otherwise adds an error to the list and returns false
        /// </summary>
        public bool CheckStringsMatch(string title1, string title2, string a, string b)
        {
            if (a == b) return true;

            Errors.Add(string.Format("{0} and {1} do not match.", title1, title2));
            return false;
        }

        /// <summary>
        ///     Returns true if the telephone number is valid
        ///     Otherwise adds an error to the list and returns false
        /// </summary>
        /// <param name="title1"></param>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        public bool ValidateTelephoneNumber(string title1, string telephoneNumber)
        {
            telephoneNumber = telephoneNumber.Replace("-", string.Empty).Replace(" ", string.Empty);

            // todo use a better regex, this currently allows 9 ('s to be valid etc.
            if (!Regex.IsMatch(telephoneNumber, @"^\+?[0-9()]{9,}$"))
            {
                Errors.Add(string.Format("Please enter a valid {0}.", title1));
                return false;
            }

            return ValidateLength(title1, telephoneNumber, 10, 30);
        }

        #endregion
    }
}