using MediatR;
using ErrorOr;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Products.Commands.CreateProduct;
using Ecommerce.Domain.product;
using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Ecommerce.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateProductCommandHandler(
        IBaseRepository<Product> productRepository,
        IUnitOfWork unitOfWork,
        IWebHostEnvironment webHostEnvironment)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var createdAt = DateTime.UtcNow;
        string imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.PictureUrl.FileName)}";
        string imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products");

        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        string imagePath = Path.Combine(imagesFolder, imageFileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
            await request.PictureUrl.CopyToAsync(stream);

        var pictureUrl = Path.Combine("Images", "Products", imageFileName);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            PictureUrl = pictureUrl,
            Price = request.Price,
            Discount = request.Discount,
            CreatedOn = createdAt,
            CategoryId = request.CategoryId
        };

        var result = await _productRepository.CreateEntityAsync(product);
        await _unitOfWork.CompleteAsync();

        return result;
    }
}