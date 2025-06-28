using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Queries.Product;

public class GetProductByIdQuery : IRequest<ApiResponse<ProductResponse>>
{
    public Guid Id { get; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}
