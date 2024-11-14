using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.catgory;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IBaseRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Guid categoryId = Guid.NewGuid();
            DateTime createdAt = DateTime.UtcNow;

            var category = new Category
            {
                Id = categoryId,
                CrearedOn = createdAt,
                Description = request.Description,
                Name = request.Name,
            };
            ErrorOr<Category> result = await _categoryRepository.CreateEntityAsync(category);

            if (result.IsError)
                return result.Errors;

            if (result.Value == null)
                return CategoryErrors.NoCategoriesFound;

            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
