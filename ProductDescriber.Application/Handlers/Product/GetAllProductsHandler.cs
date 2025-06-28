using AutoMapper;
using MediatR;
using ProductDescriber.Application.Queries.Product;
using ProductDescriber.Base;
using ProductDescriber.Data.UnitOfWork;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Handlers.Product;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<List<ProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            var mapped = _mapper.Map<List<ProductResponse>>(products);

            return ApiResponse<List<ProductResponse>>.SuccessResponse(mapped, "Tüm ürünler getirildi.");
        }
        catch (Exception ex)
        {
            return ApiResponse<List<ProductResponse>>.FailResponse("Hata: " + ex.Message, 500);
        }
    }
}
