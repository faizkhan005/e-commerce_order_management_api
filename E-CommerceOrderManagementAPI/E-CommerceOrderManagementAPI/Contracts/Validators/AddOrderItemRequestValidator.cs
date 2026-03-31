using E_CommerceOrderManagementAPI.Contracts.Requests;
using FluentValidation;

namespace E_CommerceOrderManagementAPI.Contracts.Validators;

public class AddOrderItemRequestValidator : AbstractValidator<AddOrderItemRequest>
{
    public AddOrderItemRequestValidator()
    {
        RuleFor(x => x.ProductID).NotEmpty().WithMessage("ProductID is required.");
        RuleFor(x => x.Qty).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
