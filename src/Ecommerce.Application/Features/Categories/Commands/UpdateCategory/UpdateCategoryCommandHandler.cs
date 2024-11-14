using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ErrorOr<Category>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IBaseRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetEntityByIdAsync(request.CategoryId);

            if (category == null)
                return CategoryErrors.NotFoundWithThisId;

            category.Name = request.Name;
            category.Description = request.Description;

            await _categoryRepository.UpdateEntityAsync(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }
    }
}
