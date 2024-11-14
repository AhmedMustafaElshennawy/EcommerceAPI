using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Contracts.Products
{
    public record GetProductByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CategoryId { get; set; }

        public GetProductByIdResponse(
            Guid id,
            string name,
            string description,
            string pictureUrl,
            decimal price,
            decimal discount,
            DateTime createdOn,
            Guid categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            Price = price;
            Discount = discount;
            CreatedOn = createdOn;
            CategoryId = categoryId;
        }
    }
}
