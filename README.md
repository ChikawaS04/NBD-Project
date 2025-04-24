# 🌿 Natural By Design (NBD3)

An internal web application developed for the company **Natural By Design** as part of a community-sponsored project. Built using **ASP.NET Core**, **Razor Pages**, and **SQLite**, this system allows staff to manage users, client accounts, events, and internal project workflows with a secure login system and user roles.


###IMPORTANT: USE LOGINS PROVIDED HERE TO ACCESS THE WEBSITE
## 🚀 Live Demo
Hosted on Render: (https://nbd-project.onrender.com)

## Logins 
- Admin: admin@outlook.com, Pa55w@rd
- Supervisor: supervisor@outlook.com, Pa55w@rd
- Staff: staff@outlook.com, Pa55w@rd


## 🚀 Project Overview

The system is a fully functional internal tool that enables Natural By Design to:

- Authenticate and authorize staff members
- Manage client and user data with validation
- Oversee internal projects and associated events
- Assign permissions based on user roles
- Simulate a real-world internal business application

---

## ⚙️ Technologies Used

- **ASP.NET Core 7.0**
- **Razor Pages (MVVM)**
- **Entity Framework Core**
- **SQLite** (local development DB)
- **Bootstrap 5**
- **C#**

---

## 💡 Features

### 🔐 User & Role Management
- Sign Up, Log In, and Log Out functionality
- Role-based access:
  - **Admin**: full access to users, clients, and projects
  - **Staff**: limited access based on project role
- Session and cookie handling

### 👤 Client & Account Management
- Create, view, update, and delete client accounts
- Contact information with validation

### 📅 Project & Event Management
- Track internal company projects
- Assign projects to users and clients
- Schedule and view events related to specific projects

### 📊 Internal Admin Tools
- Filter and search tables
- Clean and organized Razor Pages
- Pagination for large datasets

### ⚠️ Validation & UX
- Input validation across all forms
- Confirmation modals for delete/edit actions
- Friendly error messages and success alerts
