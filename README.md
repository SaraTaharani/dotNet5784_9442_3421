# Multi‑Layer .NET Application

## Overview
This is a **multi‑layer .NET application** demonstrating layered architecture and clean separation of concerns.

The solution contains several projects:
- **PL** – Presentation layer (user interface)
- **BL** – Business logic layer
- **DAL** – Data access layer with multiple implementations
- **Tests** – Unit tests for BL and DAL functionality

This structure shows a clear understanding of core software architecture patterns in C# and .NET. :contentReference[oaicite:3]{index=3}

---

## Tech Stack
- **Language:** C#  
- **Framework:** .NET (Core / 6 / 7)  
- **Testing:** XUnit / NUnit (if present)  
- **IDE:** Visual Studio / VS Code  
- Typical project type: Console / Class Library / Unit Tests

---

## Layers & Architecture
- **PL (Presentation Layer):** Contains UI code and program entry point  
- **BL (Business Logic Layer):** Implements core logic and business rules  
- **DAL (Data Access Layer):** Multiple data access implementations for flexibility  
- **Tests:** Unit tests verifying BL and DAL behavior

This layered approach demonstrates:
- separation of concerns  
- dependency inversion  
- testable core logic

---

## How to Run
1. Clone the repository:  
   ```bash
   git clone https://github.com/SaraTaharani/dotNet5784_9442_3421.git
2. Open the solution file .sln in Visual Studio or VS Code

3. Restore NuGet packages

4. Build the solution

5. Run the main project (e.g., PL)

6. Run tests from Test Explorer / terminal

## What You’ll Learn

This project demonstrates:

- Managing multi‑project .NET solutions
- Layered architecture (PL / BL / DAL)
- Implementing business logic and data access patterns
- Writing and running unit tests
