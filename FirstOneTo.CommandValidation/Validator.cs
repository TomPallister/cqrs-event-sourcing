using System.Collections.Generic;
using FirstOneTo.Commands;

namespace FirstOneTo.CommandValidation
{
    public class Validator : IValidator
    {
        public ValidationResult ValidateCreateExample(CreateExample createExample)
        {
            var errorMessages = new List<string>();
            if (createExample.AggregateId <= 0)
            {
                errorMessages.Add("No AggregateId was provided");
            }
            if (string.IsNullOrEmpty(createExample.Description))
            {
                errorMessages.Add("No Description was provided");
            }
            if (errorMessages.Count > 0)
            {
                return new ValidationResult(false, errorMessages);
            }
            return new ValidationResult(true, errorMessages);
        }


        public ValidationResult ValidateChangeExample(ChangeExample changeExample)
        {
            var errorMessages = new List<string>();
            if (changeExample.AggregateId <= 0)
            {
                errorMessages.Add("No AggregateId was provided");
            }
            if (string.IsNullOrEmpty(changeExample.Description))
            {
                errorMessages.Add("No Description was provided");
            }
            if (errorMessages.Count > 0)
            {
                return new ValidationResult(false, errorMessages);
            }
            return new ValidationResult(true, errorMessages);
        }

        public ValidationResult ValidateCreateUser(CreateUser createUser)
        {
            var errorMessages = new List<string>();
            if (createUser.AggregateId <= 0)
            {
                errorMessages.Add("No AggregateId was provided");
            }
            if (string.IsNullOrEmpty(createUser.UserName))
            {
                errorMessages.Add("No UserName was provided");
            }
            if (string.IsNullOrEmpty(createUser.Email))
            {
                errorMessages.Add("No UserName was Email");
            }
            if (string.IsNullOrEmpty(createUser.Password))
            {
                errorMessages.Add("No UserName was Password");
            }
            if (errorMessages.Count > 0)
            {
                return new ValidationResult(false, errorMessages);
            }
            return new ValidationResult(true, errorMessages);
        }
    }
}