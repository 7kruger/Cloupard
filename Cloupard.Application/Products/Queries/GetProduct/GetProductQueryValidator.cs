using FluentValidation;

namespace Cloupard.Application.Products.Queries.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}