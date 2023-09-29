using CurrencyConverter.Pages.Currency;
using FluentValidation;

namespace CurrencyConverter;
public class CurrencyConverterValidator : AbstractValidator<CurrencyConverterModel>
{
    private readonly string[] _allowedCurrencies = new[] { "GBP", "USD", "CAD", "EUR" };

    public CurrencyConverterValidator()
    {
        RuleFor(x => x.CurrencyFrom)
            .NotEmpty()
            .Length(3)
            .Must(x => _allowedCurrencies.Contains(x));

        RuleFor(x => x.CurrencyTo)
            .NotEmpty()
            .Length(3)
            .Must(x => _allowedCurrencies.Contains(x))
            .Must((CurrencyConverterModel model, string currencyTo) => currencyTo != model.CurrencyFrom);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .InclusiveBetween(1, 1000);
    }
}
