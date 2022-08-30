using FluentValidation;
using FreeCourse.WebUI.Models.Catalog;

namespace FreeCourse.WebUI.Validators
{
    public class UpdateCourseInputValidator:AbstractValidator<UpdateCourseInput>
    {
        public UpdateCourseInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kurs adı boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre boş olamaz.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş olamaz.").ScalePrecision(2, 6).WithMessage("Yanlış bir para formatı girdiniz.");
        }
    }
}
