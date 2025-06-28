using AutoMapper;
using MediatR;
using ProductDescriber.Application.Commands.Product;
using ProductDescriber.Base;
using ProductDescriber.Data.UnitOfWork;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Handlers.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (existing == null)
            return ApiResponse<ProductResponse>.FailResponse("Ürün bulunamadı.", 404);

        _mapper.Map(request.Request, existing); // UpdateRequest -> Entity (üzerine yazar)

        _unitOfWork.ProductRepository.Update(existing);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<ProductResponse>(existing);
        return ApiResponse<ProductResponse>.SuccessResponse(response, "Ürün başarıyla güncellendi.");
    }
}
