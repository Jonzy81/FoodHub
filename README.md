# FoodHub API - CRUD Endpoints

This API provides CRUD operations for managing users, bookings, tables, and menu items in a restaurant system. It is built using ASP.NET Core and follows RESTful API principles.

## Table of Contents

- [ER-Diagram](#er-diagram)
- [User Endpoints](#user-endpoints)
- [Booking Endpoints](#booking-endpoints)
- [Table Endpoints](#table-endpoints)
- [Menu Endpoints](#menu-endpoints)

---

## ER-Diagram


## User Endpoints

- **GET /api/User**  
  Retrieves all users.

- **GET /api/User/{id}**  
  Retrieves a specific user by their ID.

- **POST /api/User**  
  Creates a new user.  
  **Parameters**: `firstName`, `lastName`, `email`, `userPhoneNumber`.

- **PUT /api/User/{id}**  
  Updates an existing user by ID.  
  **Parameters**: `firstName`, `lastName`, `email`, `userPhoneNumber`.

- **DELETE /api/User/{id}**  
  Deletes a user by ID.

---

## Booking Endpoints

- **GET /api/Booking**  
  Retrieves all bookings.

- **GET /api/Booking/{id}**  
  Retrieves a specific booking by ID.

- **POST /api/Booking**  
  Creates a new booking.  
  **Parameters**: `bookingDate`, `bookingTime`, `numberOfSeats`, `userId`, `tableId`.

- **PUT /api/Booking/{id}**  
  Updates an existing booking by ID.  
  **Parameters**: `bookingDate`, `bookingTime`, `numberOfSeats`, `userId`, `tableId`.

- **DELETE /api/Booking/{id}**  
  Deletes a booking by ID.

---

## Table Endpoints

- **GET /api/Table**  
  Retrieves all tables.

- **GET /api/Table/{id}**  
  Retrieves a specific table by ID.

- **POST /api/Table**  
  Adds a new table.  
  **Parameters**: `tableSeats`, `tableNumber`, `isAvailable`.

- **PUT /api/Table/{id}**  
  Updates an existing table by ID.  
  **Parameters**: `tableSeats`, `tableNumber`, `isAvailable`.

- **DELETE /api/Table/{id}**  
  Deletes a table by ID.

- **GET /api/Table/available?date={date}&time={time}**  
  Checks for available tables on a specific date and time.

---

## Menu Endpoints

- **GET /api/Menu**  
  Retrieves all menu items.

- **GET /api/Menu/{id}**  
  Retrieves a specific menu item by ID.

- **POST /api/Menu**  
  Adds a new menu item.  
  **Parameters**: `menuType`, `menuName`, `description`, `price`, `isAvailable`.

- **PUT /api/Menu/{id}**  
  Updates an existing menu item by ID.  
  **Parameters**: `menuType`, `menuName`, `description`, `price`, `isAvailable`.

- **DELETE /api/Menu/{id}**  
  Deletes a menu item by ID.

---

