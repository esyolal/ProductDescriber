using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Schema.Requests;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Commands.Product;

public class CreateProductCommand : IRequest<ApiResponse<ProductResponse>>
{
    public ProductCreateRequest Request { get; }

    public CreateProductCommand(ProductCreateRequest request)
    {
        Request = request;
    }
}
