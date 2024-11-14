# Ecommerce

## Overview

This API is built with ASP.NET Core following Clean Architecture principles. It serves as the backend for an e-commerce platform, handling product management, order processing, shopping carts, and user reviews.

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

## Authentication & Authorization

- The API uses JWT (JSON Web Tokens) for authentication.
- Role-based authorization is implemented to restrict access to certain endpoints.
