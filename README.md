# Visitor Management System

---

### Overview

This repository hosts a **Visitor Management System** project, developed based on the principles of **Onion Architecture**. The system is built using Microsoft's popular web application development framework, **ASP.NET MVC**.

---

### Technical Details

* **Architecture:** The project adheres to **Onion Architecture** principles, featuring a layered structure. This design promotes independence and loose coupling between business logic, data access, and presentation layers, resulting in a more maintainable and testable codebase.

* **Technology Stack:**
    * **Backend:** Developed with ASP.NET MVC, utilizing the C# programming language.
    * **Frontend:** Implemented using standard web technologies: HTML and CSS.

* **Languages Used:**
    * **C#:** Accounts for 57.5% of the codebase, primarily used for business logic, controllers, and model definitions.
    * **HTML:** Makes up 35.2% of the code, used for defining the structure and content of user interface view files.
    * **CSS:** Constitutes 7.3% of the code, applied for styling and visual presentation of the user interface.

* **Project Structure:** The repository follows the typical directory structure of a standard ASP.NET MVC project, which includes:
    * `Business`: Contains the business logic layer components.
    * `Controllers`: Houses the MVC controllers responsible for handling user input and interactions.
    * `Migrations`: Used for managing database schema changes, typically with an ORM like Entity Framework Code First.
    * `Models`: Defines data models and potentially View Models that represent the data structure.
    * `Properties`: Contains project-specific configuration and property files.
    * `Utilities`: Includes utility classes or common helper functions.
    * `Views`: Stores the user interface (UI) view files, which define what the user sees.
