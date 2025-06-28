using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductDescriber.Application.Commands.Product;
using ProductDescriber.Schema.Requests;
using ProductDescriber.Base;
using ProductDescriber.Schema.Responses;
using ProductDescriber.Application.Queries.Product;

namespace ProductDescriber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ApiResponse<ProductResponse>> CreateProduct([FromBody] ProductCreateRequest request)
    {
        var result = await _mediator.Send(new CreateProductCommand(request));
        return result;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ProductResponse>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<ProductResponse>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<ProductResponse>> Update(Guid id, [FromBody] ProductUpdateRequest request)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, request));
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<object>> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return result;
    }

}
