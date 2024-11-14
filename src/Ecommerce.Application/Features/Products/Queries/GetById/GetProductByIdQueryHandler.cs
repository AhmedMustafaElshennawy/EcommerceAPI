using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.product;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = ErrorOr.Error;

namespace Ecommerce.Application.Features.Products.Queries.GetById
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<Product>>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public GetProductByIdQueryHandler(IBaseRepository<Product> productRepository) => _productRepository = productRepository;
        public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetEntityByIdAsync(request.ProductId);

            if (product is null)
                return Error.NotFound(description: "No Product With this Id");

            return product;
        }
    }
}