using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ErrorOr<IEnumerable<Product>>>
    {
        private readonly IBaseRepository<Product> _ProductsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductsQueryHandler(
            IBaseRepository<Product> ProductsRepository,
            IUnitOfWork unitOfWork)
        {
            _ProductsRepository = ProductsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<IEnumerable<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _ProductsRepository.Entities().ToList();
            //var products = await _ProductsRepository.GetAllAsync();

            if (products == null || !products.Any())
                return ProductErrors.NoProductsFound;

            return products;
        }
    }
}
