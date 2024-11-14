using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,ErrorOr<Product>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdateProductCommandHandler(
            IBaseRepository<Product> productRepository,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        { 
            var product = await _productRepository.GetEntityByIdAsync(request.productId);
            if (product == null)
                return ProductErrors.NotFoundWithThisId;

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Discount = request.Discount;
            product.CategoryId = request.CategoryId;

            if (request.PictureUrl != null)
            {
                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products");
                Directory.CreateDirectory(directoryPath);

                var oldPicturePath = Path.Combine(_webHostEnvironment.WebRootPath, product.PictureUrl);
                if (File.Exists(oldPicturePath))
                    File.Delete(oldPicturePath);

                var newFileName = $"{Guid.NewGuid()}_{request.PictureUrl.FileName}";
                var newFilePath = Path.Combine(directoryPath, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                    await request.PictureUrl.CopyToAsync(stream);

                product.PictureUrl = Path.Combine("images", "products", newFileName);
            }

            await _productRepository.UpdateEntityAsync(product);
            await _unitOfWork.CompleteAsync();
            return product;
        }
    }
}
