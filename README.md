
---

```markdown
# IRIS - Ingredient Inventory System (HM-IMS)

**Description:**  
IRIS is a Windows Forms-based Ingredient Inventory System designed for the College of Hospitality Education.  
It manages ingredient requests, inventory, approvals, and audits with **role-based access** for:

- Office Staff  
- Assistant Dean  
- Dean  
- Quality Assurance Officers  

The system uses **EF Core** for database management and implements **secure password hashing** for user authentication.

---

## **Project Structure**

```

IRIS.Domain        → Entities, Enums, Value Objects
IRIS.Application   → Services, DTOs, Business Logic
IRIS.Infrastructure → DbContext, Repositories, EF Core Migrations, SeedData
IRIS.Presentation  → Windows Forms UI

````

---

## **Getting Started (For Team Members)**

### 1️⃣ Clone the repository

```bash
git clone https://github.com/username/IRIS-IngredientInventorySystem.git
cd IRIS-IngredientInventorySystem
````

---

### 2️⃣ Open the solution

* Open `IRIS.sln` in **Visual Studio 2022** (or later).
* Restore NuGet packages when prompted.

---

### 3️⃣ Configure the database

* Connection string is in **IRIS.Presentation → App.config**:

```xml
<connectionStrings>
  <add name="IrisConnection"
       connectionString="Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;" />
</connectionStrings>
```

* Adjust the connection string if your LocalDB instance is different.

---

### 4️⃣ Apply EF Core migrations

Open **Package Manager Console** and run:

```powershell
Update-Database
```

> This creates the database and tables.

---

### 5️⃣ Seed the database

* The system **automatically seeds default users** on first run:

| Username      | Password | Role           |
| ------------- | -------- | -------------- |
| officestaff   | password | Office Staff   |
| assistantdean | password | Assistant Dean |
| dean          | password | Dean           |
| qa            | password | QA Officer     |

---

### 6️⃣ Run the application

* Start the **IRIS.Presentation** project in Visual Studio (`F5`).
* The **LoginForm** will appear.
* Use one of the seeded users to log in.

---

## **Team Collaboration Guidelines**

1. **Create feature branches**:

```bash
git checkout -b feature/your-feature-name
```

2. **Commit changes frequently** with clear messages.
3. **Push branches to GitHub**:

```bash
git push origin feature/your-feature-name
```

4. **Create Pull Requests** for merging features into `main`.
5. **Migrations**: Only push migration files, **not the database**. Each member runs `Update-Database` locally.
6. **Dependencies**: Restore NuGet packages if missing.

---

### **Notes**

* Passwords are **hashed with a salt**, so the same password will produce different hashes for each user.
* Do **not** commit `.mdf` or `.ldf` files. Use migrations instead.
* Keep your NuGet packages up-to-date for EF Core and Microsoft Extensions.

---

### **Support**

For questions or issues, contact the project lead or create a GitHub issue in this repository.

```

---

This version is **professional, clear, and actionable** for your team.  

If you want, I can also create a **ready-to-use `.gitignore` for Visual Studio + EF Core + LocalDB**, so your team doesn’t accidentally push database files or build folders. This is very important for collaboration.  

Do you want me to do that next?
```
