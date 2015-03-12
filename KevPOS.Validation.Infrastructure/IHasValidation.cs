namespace KevPOS.Validation.Infrastructure
{
    public interface IHasValidation
    {
        ValidatorResult Validate();
    }
}