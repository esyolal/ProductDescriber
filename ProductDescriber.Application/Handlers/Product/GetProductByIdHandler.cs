using AutoMapper;
using MediatR;
using ProductDescriber.Application.Queries.Product;
using ProductDescriber.Base;
using ProductDescriber.Data.UnitOfWork;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Handlers.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (entity == null)
            return ApiResponse<ProductResponse>.FailResponse("Ürün bulunamadı.", 404);

        var response = _mapper.Map<ProductResponse>(entity);
        return ApiResponse<ProductResponse>.SuccessResponse(response, "Ürün getirildi.");
    }
}
