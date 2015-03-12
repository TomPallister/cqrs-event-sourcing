using FirstOneTo.Commands;

namespace FirstOneTo.CommandValidation
{
    public interface IValidator
    {
        ValidationResult ValidateCreateUser(CreateUser createUser);
        ValidationResult ValidateCreateExample(CreateExample createExample);
        ValidationResult ValidateChangeExample(ChangeExample changeExample);
    }
}