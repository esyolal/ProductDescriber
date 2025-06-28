using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Data.UnitOfWork;
using ProductDescriber.Application.Commands.Product;

namespace ProductDescriber.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            return ApiResponse<object>.FailResponse("Ürün bulunamadı.", 404);
        }

        _unitOfWork.ProductRepository.Remove(entity);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponse<object>.SuccessResponse(null, "Ürün başarıyla silindi.");
    }
}
