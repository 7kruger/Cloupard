using Cloupard.Domain.Dto.Products;
using FluentValidation;

namespace Cloupard.Application.Validations.FluentValidations;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title must be filled in");
    }
}