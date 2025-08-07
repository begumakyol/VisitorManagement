<h1 align="center">Visitor Management System</h1>
<p align="center"><sub><i>by Begüm Akyol</i></sub></p>


---

<img width="1916" height="1070" alt="Image" src="https://github.com/user-attachments/assets/0315c57c-7b28-4d2e-9ebf-ce272262d3c8" />

<h3 align="center">The Login Page</h3>
<h6 align="center">The login page serves as a secure entry point, allowing users to access the system via Active Directory authentication.</h6>

### Overview

This repository hosts a Visitor Management System project, developed to digitize and manage internal visitor entry and exit processes. The system provides a user-friendly interface for recording, updating, and tracking visitor information while prioritizing security and data integrity.

---

### Technical Details

* **Architecture:** The project adheres to a layered architecture based on the ASP.NET Core 8 MVC framework. This design promotes clean code, maintainability, and scalability by separating concerns across different layers.

* **Technology Stack:**
    * **Backend:** Developed with ASP.NET Core 8 MVC and C#.
    * **Frontend:** Implemented using standard web technologies: HTML5, CSS3, JavaScript, Bootstrap 5, Chart.js, and jQuery.
    * **Database:** SQL Server managed with Entity Framework Core 8.
    * **Security:** Features Cookie-based authentication, LDAP integration, and AES (256-bit) encryption.

* **Project Functions:**
    * **Secure Login System:** Users authenticate via LDAP (Active Directory).
    * **Visitor Management:**
        * **Visitor Addition:** Create new visitor records with essential details.
        * **Data Security:** Sensitive data like names and ID numbers are encrypted using the AES-256 algorithm.
        * **Visitor Listing:** View all visitor records in a table, with filters for date ranges. Visitors currently on-site are highlighted.
        * **Exit Process:** A one-click process to record a visitor's exit time.
        * **Update:** Dynamically update visitor records through a modal window.
    * **Dashboard and Statistics:** The homepage displays real-time statistics, including weekly and daily visitor counts, and a pie chart of visitor distribution, all updated via AJAX.
  
* **Security Measures:**
    * **Data Encryption:** Critical visitor information is stored in the database using a StringCipher service to prevent unauthorized access.
    * **Salt and IV:** Each encryption operation uses a randomly generated salt and IV to enhance security against brute-force attacks.
    * **Authentication:** LDAP integration ensures that only authorized corporate accounts can access the system.

* **Project Structure:** The repository follows a logical directory structure, which includes:
    * `Business`: Contains the core business logic and rules.
    * `Controllers`: Houses the MVC controllers responsible for handling user interactions.
    * `Migrations`: Manages database schema changes using Entity Framework Core.
    * `Models`: Defines data models (Entities), database context (DbContext), and front-end data models (ViewModels).
    * `Properties`: Contains project-specific configuration and property files.
    * `Utilities`: Includes helper classes for encryption, LDAP, and other common functions.
    * `Views`: Stores the user interface (UI) view files (.cshtml) , which define what the user sees.
<br>

### Screenshots

<img width="1899" height="1067" alt="Image" src="https://github.com/user-attachments/assets/5d3ce257-d66f-48f5-bca2-39968a42a717" />
<h3 align="center">The Home Page</h3>
<h6 align="center">The home page displays visual charts that provide a quick overview of visitor statistics.</h6>
<br>

---

<img width="1895" height="1068" alt="Image" src="https://github.com/user-attachments/assets/b287b92f-7038-4c47-8747-92d76f321700" />
<h3 align="center">The Add Visitor Page</h3>
<h6 align="center">The add visitor page enables users to create a new visitor entry with required details.</h6>
<br>

---

<img width="1893" height="1061" alt="Image" src="https://github.com/user-attachments/assets/98275c8a-ff94-44f3-a635-47ddad2ddb5c" />
<h3 align="center">The Visitor List Page</h3>
<h6 align="center">The Visitor List Page allows users to view the list of visitors and filter it based on a selected time range.</h6>
