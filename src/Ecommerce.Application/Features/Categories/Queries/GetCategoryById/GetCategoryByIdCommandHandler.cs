using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, ErrorOr<Category>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        public GetCategoryByIdCommandHandler(IBaseRepository<Category> categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<ErrorOr<Category>> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            ErrorOr<Category> result = await _categoryRepository.GetEntityByIdAsync(request.categoryId);

            if (result.IsError)
                return result.Errors;

            if (result.Value == null)
                return CategoryErrors.NotFoundWithThisId;

            return result;
        }
    }
}
