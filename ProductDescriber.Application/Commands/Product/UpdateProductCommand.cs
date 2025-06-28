using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Schema.Requests;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Commands.Product;

public class UpdateProductCommand : IRequest<ApiResponse<ProductResponse>>
{
    public Guid Id { get; }
    public ProductUpdateRequest Request { get; }

    public UpdateProductCommand(Guid id, ProductUpdateRequest request)
    {
        Id = id;
        Request = request;
    }
}
