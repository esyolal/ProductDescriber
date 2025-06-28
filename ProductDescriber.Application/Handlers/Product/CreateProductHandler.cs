using AutoMapper;
using MediatR;
using ProductDescriber.Base;
using ProductDescriber.Data.UnitOfWork;
using ProductDescriber.Schema.Responses;
using ProductDescriber.Application.Commands.Product;
using ProductDescriber.Application.Services.LLM;

namespace ProductDescriber.Application.Handlers.Product;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductDescriptionGenerator _llm;

    public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IProductDescriptionGenerator llm)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _llm = llm;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Map DTO -> Entity
            var entity = _mapper.Map<Data.Entities.Product>(request.Request);

            // 2. Description boşsa LLM'den üret
            if (string.IsNullOrWhiteSpace(entity.Description))
            {
                var generatedDescription = await _llm.GenerateDescriptionAsync(entity.Title, entity.Features);
                entity.Description = generatedDescription;
            }

            // 3. Veritabanına kaydet
            await _unitOfWork.ProductRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // 4. Map Entity -> DTO
            var response = _mapper.Map<ProductResponse>(entity);

            // 5. ApiResponse<ProductResponse> ile geri dön
            return ApiResponse<ProductResponse>.SuccessResponse(response, "Ürün başarıyla eklendi.");
        }
        catch (Exception ex)
        {
            return ApiResponse<ProductResponse>.FailResponse("Hata oluştu: " + ex.Message, 500);
        }
    }
}
