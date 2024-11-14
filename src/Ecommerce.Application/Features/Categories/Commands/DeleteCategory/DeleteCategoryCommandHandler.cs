using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ErrorOr<Unit>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IBaseRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetEntityByIdAsync(request.CategoryId);

            if (category == null)
                return CategoryErrors.NoCategoriesFound;

            await _categoryRepository.DeleteEntityAsync(category);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}