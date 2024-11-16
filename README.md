## E-Commerce API
  This API is built with ASP.NET Core following Clean Architecture principles. It serves as the backend for an e-commerce platform, handling product management,      order processing, shopping carts, and user reviews.

## Features
- Product Management: CRUD operations for products, with details like name, description, price, discount, and image URL.
- Categories: Organize products into various categories.
- Shopping Cart: Allows users to manage items in their cart.
- Orders: Supports order placement and tracking, including shipping and total calculations.
- Reviews: Enables users to leave ratings and comments for products.
- User Authentication: Uses ASP.NET Identity for secure user management.
  
## Database Schema

The database consists of the following tables:
![Alt text](https://github.com/AhmedMustafaElshennawy/EcommerceAPI/blob/master/tables.png)

## Tables
### 1. **Products**

Fields : Id, Name, Description, PictureUrl, Price, Discount, CreatedOn, CategoryId
Stores product details and links to categories.
### 2. **Categories**

Fields: Id, Name, Description, CreatedOn
Represents product categories with names and descriptions.
### 3. **Orders**

Fields: Id, OrderTotalAmount, OrderTotalDiscount, OrderDate, Street, City, PostalCode, Country, ApplicationUserId
Manages user orders with address and total amount details.
### 4. **OrderItems**

Fields: OrderItemId, OrderId, ProductId, Quantity, Price
Represents individual items in an order.
### 5. **Reviews**

Fields: ReviewId, ProductId, ApplicationUserId, Rating, Comment, ReviewDate
Allows users to submit ratings and comments for products.
## Relationships
- Products are associated with Categories (many-to-one).
- Orders have multiple OrderItems (one-to-many).
- Products can have multiple Reviews (one-to-many).
- Orders and Reviews link to ApplicationUserId, associating them with specific users.

## Authentication & Authorization

- The API uses JWT (JSON Web Tokens) for authentication.
- Role-based authorization is implemented to restrict access to certain endpoints.
