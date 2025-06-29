﻿using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(bool IsSuccess, string Message, string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(command => command.Cart).NotNull().WithMessage("Cart cannot be null.");
            RuleFor(command => command.Cart.UserName).NotEmpty().WithMessage("User name is required.");
            RuleFor(command => command.Cart.Items).NotEmpty().WithMessage("Cart must contain at least one item.");
        }
    }

    public class StoreBasketCommandHandler
        (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) :
        ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult>
            Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);

            await repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(true, "Basket stored successfully.", command.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            // Communicate with Discount.Grpc and calculate lastest prices of products into sc
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync
                    (new GetDiscountRequest { ProductName = item.ProductName },
                    cancellationToken: cancellationToken);
                if (coupon != null)
                    item.Price -= coupon.Amount;
            }
        }
    }
}
