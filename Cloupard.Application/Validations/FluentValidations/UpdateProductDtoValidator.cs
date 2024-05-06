using Cloupard.Domain.Dto.Products;
using FluentValidation;

namespace Cloupard.Application.Validations.FluentValidations;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id field must be filled in");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title field must be filled in");
    }
}