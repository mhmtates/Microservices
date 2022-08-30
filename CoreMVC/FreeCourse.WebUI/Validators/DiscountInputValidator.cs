using FluentValidation;
using FreeCourse.WebUI.Models.Discount;


namespace FreeCourse.WebUI.Validators
{
    public class DiscountInputValidator:AbstractValidator<DiscountInput>
    {
        public DiscountInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim kuponu boş olamaz.");
        }
    }
}
