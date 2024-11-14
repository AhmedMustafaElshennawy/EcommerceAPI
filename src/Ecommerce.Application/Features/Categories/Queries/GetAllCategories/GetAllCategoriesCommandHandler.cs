using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ErrorOr<IEnumerable<Category>>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IBaseRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = _categoryRepository.Entities().ToList();

            if (result == null || !result.Any())
                return CategoryErrors.NoCategoriesFound;

            return result;
        }
    }
}