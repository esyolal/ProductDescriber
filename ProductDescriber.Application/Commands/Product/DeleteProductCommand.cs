using MediatR;
using ProductDescriber.Base;

namespace ProductDescriber.Application.Commands.Product;

public class DeleteProductCommand : IRequest<ApiResponse<object>>
{
    public Guid Id { get; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
