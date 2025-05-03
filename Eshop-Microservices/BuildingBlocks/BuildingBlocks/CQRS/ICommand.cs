using MediatR;

namespace BuildingBlocks.CQRS
{
    // Commands are used to request a change in the system state.
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
