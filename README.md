
### Technologies Used

- **Database**: PostgreSQL (version 16)
- **Web App**: .NET Core 8

### Prerequisites

Make sure you have the following installed on your system:

- [.NET Core SDK (version 8)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL (version 16)](https://www.postgresql.org/download/)

## Database Setup (PostgreSQL 16)

1. Install PostgreSQL 16 (if not already installed) from the official website: [PostgreSQL Downloads](https://www.postgresql.org/download/)

2. Restore the database from the provided SQL backup file.

### Restoring from Backup
The database backup is located at `\emp_payment\database\db_emp_payment.sql`. Follow the steps below to restore it:

1. Open your PostgreSQL command line or use a tool like pgAdmin.

2. Create a new database (if it doesn't already exist):

```sql
CREATE DATABASE db_emp_payment;
```
3. Restore the SQL backup using the psql command:

```bash
psql -U your-username -d db_emp_payment -f emp_payment/database/db_emp_payment.sql
```
Make sure to replace `your-username` with your PostgreSQL username.

4. Verify that the database has been restored successfully.

## Web App Setup (.NET Core 8)

1. Install .NET 8 (if not already installed) from the official website: [.NET 8 Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

2. Navigate to the app directory:
```bash
cd ...\emp_payment\app
```

3. Restore the .NET Core dependencies:
```bash
dotnet restore
```
4. Update the `appsettings.json` with your PostgreSQL connection string:

```json
"EmpPayment": "Host=localhost; Database=infokes_explorer; Port=5432; Username=postgres; Password={yourpassword}"
```

5. build the backend API:

```bash
dotnet build
```

6. Run the backend API:

```bash
dotnet run
```
The API will be running on `http://localhost:5220`
