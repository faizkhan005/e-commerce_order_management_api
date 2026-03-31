using E_CommerceOrderManagementAPI.Contracts.Requests;
using FluentValidation;

namespace E_CommerceOrderManagementAPI.Contracts.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.CustomerID).NotEmpty().WithMessage("CustomerID is required.");
        RuleFor(x => x.OrderedItems).NotEmpty().WithMessage("At least one order item is required.");
        RuleForEach(x => x.OrderedItems).SetValidator(new AddOrderItemRequestValidator());
    }
}
