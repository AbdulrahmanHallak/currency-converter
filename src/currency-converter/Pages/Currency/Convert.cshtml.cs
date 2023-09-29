using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Pages.Currency
{
    public class ConvertModel : PageModel
    {
        private readonly IValidator<CurrencyConverterModel> _validator;

        [BindProperty]
        public CurrencyConverterModel Input { get; set; } = default!;

        public SelectListItem[] CurrencyCodes { get; } =
        {
            new SelectListItem{Text="GBP", Value = "GBP"},
            new SelectListItem{Text="USD", Value = "USD"},
            new SelectListItem{Text="CAD", Value = "CAD"},
            new SelectListItem{Text="EUR", Value = "EUR"},
        };

        public ConvertModel(IValidator<CurrencyConverterModel> validator)
        {
            _validator = validator;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var result = _validator.Validate(Input);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return Page();
            }

            return RedirectToPage("Success");
        }


    }
    public class CurrencyConverterModel
    {
        public string CurrencyFrom { get; set; } = default!;
        public string CurrencyTo { get; set; } = default!;
        public decimal Quantity { get; set; } = default!;
    }
}
