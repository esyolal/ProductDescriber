using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Queries.Product;

public class GetAllProductsQuery : IRequest<ApiResponse<List<ProductResponse>>>
{
}
