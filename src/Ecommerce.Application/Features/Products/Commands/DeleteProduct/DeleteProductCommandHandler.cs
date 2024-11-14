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

namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DeleteProductCommandHandler(
            IBaseRepository<Product> productRepository,
            IWebHostEnvironment webHostEnvironment,
            IUnitOfWork unitOfWork) 
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetEntityByIdAsync(request.ProductId);
            if (product == null)
                return ProductErrors.NotFoundWithThisId;

            var picturePath = Path.Combine(_webHostEnvironment.WebRootPath, product.PictureUrl);
            if (File.Exists(picturePath))
                File.Delete(picturePath);

            await _productRepository.DeleteEntityAsync(product);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}