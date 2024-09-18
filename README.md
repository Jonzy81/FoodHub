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
![ER-Diagram](https://github.com/Jonzy81/FoodHub/blob/master/image(1).png)

## User Endpoints

- **GET /api/User**  
  Retrieves all users.

- **GET /api/User/{id}**  
  Retrieves a specific user by their ID.

- **POST /api/User**  
  Creates a new user.  
  - **Request Body**:
  ```json
  {
  "userId": 1,
  "firstName": "Thomas",
  "lastName": "Andersson",
  "email": "Tandersson@email.com",
  "userPhoneNumber": "+46782847437437"
}

- **PUT /api/User/{id}**  
  Updates an existing user by ID.  
  ```json
  {
  "userId": 1,
  "firstName": "xxxxx",
  "lastName": "xxxxxxx",
  "email": "xxxxxxxxxx@xxxxxxxx.com",
  "userPhoneNumber": "xxxxxxxxxxxxxxx"
  }
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
  - **Request Body**:
   ```json
   {
    "bookingId": 0,
    "bookingDate": "2024-11-14",
    "bookingTime": "20:15:00",
    "numberOfSeats": 4,
    "userId": 1,
    "tableId": 1
   }

- **PUT /api/Booking/{id}**  
  Updates an existing booking by ID.  
   - **Request Body**:
   ```json
   {
    "bookingId": 0,
    "bookingDate": "2024-11-14",
    "bookingTime": "20:15:00",
    "numberOfSeats": 4,
    "userId": 1,
    "tableId": 1
   }

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
  - **Request Body**:
   ```json
   {
  "tableId": 0,
  "tableSeats": "string",
  "tableNumber": 0,
  "isAwailable": true
}

- **PUT /api/Table/{id}**  
  Updates an existing table by ID.  
   - **Request Body**:
   ```json
   {
  "tableId": 0,
  "tableSeats": "string",
  "tableNumber": 0,
  "isAwailable": true
}

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
    - **Request Body**:
   ```json
   {
  "menuId": 0,
  "menuType": "string",
  "menuName": "string",
  "description": "string",
  "price": 0,
  "isAwailable": true
}

- **PUT /api/Menu/{id}**  
  Updates an existing menu item by ID.  
      - **Request Body**:
   ```json
   {
  "menuId": 0,
  "menuType": "string",
  "menuName": "string",
  "description": "string",
  "price": 0,
  "isAwailable": true
}

- **DELETE /api/Menu/{id}**  
  Deletes a menu item by ID.

---

