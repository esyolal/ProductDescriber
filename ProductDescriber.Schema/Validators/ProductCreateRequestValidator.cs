using FluentValidation;
using ProductDescriber.Schema.Requests;

namespace ProductDescriber.Schema.Validators;

public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
{
    public ProductCreateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(x => x.Features)
            .NotEmpty().WithMessage("Özellikler alanı boş olamaz.")
            .MaximumLength(1000).WithMessage("Özellikler en fazla 1000 karakter olabilir.");
    }
}
