CREATE DATABASE DB_EMP_PAYMENT;

CREATE TABLE EMPLOYEES (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    FIRST_NAME VARCHAR(50) NOT NULL,
    LAST_NAME VARCHAR(50) NOT NULL,
    EMAIL VARCHAR(100) UNIQUE NOT NULL,
    PHONE VARCHAR(15),
    POSITION VARCHAR(50),
    DEPARTMENT VARCHAR(50),
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

CREATE TABLE PAYROLLS (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    EMPLOYEE_ID UUID REFERENCES EMPLOYEES(ID) ON DELETE SET NULL,
    PERIOD DATE NOT NULL, -- periode penggajian (misalnya bulan atau minggu)
    BASIC_SALARY NUMERIC(12, 2) NOT NULL,
    BONUS NUMERIC(12, 2) DEFAULT 0,
    DEDUCTIONS NUMERIC(12, 2) DEFAULT 0,
    TOTAL_SALARY NUMERIC(12, 2) NOT NULL,
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

CREATE TABLE ATTENDANCE (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    EMPLOYEE_ID UUID REFERENCES EMPLOYEES(ID) ON DELETE SET NULL,
    ATTENDANCE_DATE DATE NOT NULL,
    STATUS VARCHAR(20) NOT NULL, -- hadir, sakit, cuti, dll.
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

CREATE TABLE SALARY_COMPONENTS (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    COMPONENT_NAME VARCHAR(50) NOT NULL, -- contoh: "Tunjangan Transportasi", "Potongan BPJS", dll.
    COMPONENT_TYPE VARCHAR(20) CHECK (COMPONENT_TYPE IN ('addition', 'deduction')), -- tambahan atau potongan
    AMOUNT NUMERIC(12, 2) NOT NULL,
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

CREATE TABLE PAYROLL_DETAILS (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    PAYROLL_ID UUID REFERENCES PAYROLLS(ID) ON DELETE CASCADE,
    COMPONENT_ID UUID REFERENCES SALARY_COMPONENTS(ID) ON DELETE SET NULL,
    AMOUNT NUMERIC(12, 2) NOT NULL,
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

CREATE TABLE USERS (
    ID UUID PRIMARY KEY DEFAULT GEN_RANDOM_UUID(),
    USERNAME VARCHAR(50) UNIQUE NOT NULL,
    PASSWORD_HASH VARCHAR(255) NOT NULL, -- disarankan untuk menyimpan hash password, bukan password langsung
    FULL_NAME VARCHAR(100) NOT NULL,
    EMAIL VARCHAR(100) UNIQUE NOT NULL,
    ROLE VARCHAR(50), -- contoh: admin, staff, manager, dll.
    CREATED_BY UUID,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UPDATE_BY UUID,
    UPDATE_DATE TIMESTAMP,
    DELETED_BY UUID,
    DELETED_DATE TIMESTAMP
);

INSERT INTO EMPLOYEES (
    FIRST_NAME,
    LAST_NAME,
    EMAIL,
    PHONE,
    POSITION,
    DEPARTMENT
) VALUES (
    'John',
    'Doe',
    'john.doe@example.com',
    '1234567890',
    'Manager',
    'Sales'
),
(
    'Jane',
    'Smith',
    'jane.smith@example.com',
    '0987654321',
    'Engineer',
    'IT'
),
(
    'Michael',
    'Johnson',
    'michael.j@example.com',
    '1234561111',
    'Technician',
    'Maintenance'
),
(
    'Emily',
    'Davis',
    'emily.davis@example.com',
    '1231231234',
    'HR',
    'HR'
),
(
    'Daniel',
    'Brown',
    'daniel.b@example.com',
    '5554443333',
    'Accountant',
    'Finance'
),
(
    'Laura',
    'Wilson',
    'laura.w@example.com',
    '3334445555',
    'Engineer',
    'IT'
),
(
    'Robert',
    'Moore',
    'robert.moore@example.com',
    '6667778888',
    'Manager',
    'Sales'
),
(
    'Jessica',
    'Taylor',
    'jessica.t@example.com',
    '8889990000',
    'Analyst',
    'Marketing'
),
(
    'Thomas',
    'Anderson',
    'thomas.a@example.com',
    '1112223333',
    'Technician',
    'Maintenance'
),
(
    'Sophia',
    'Martin',
    'sophia.martin@example.com',
    '4445556666',
    'Manager',
    'Finance'
);

CREATE VIEW VW_EMPLOYEE_PAYROLL AS
    SELECT
        P.ID           AS "payroll_id",
        E.ID           AS "employee_id",
        E.FIRST_NAME,
        E.LAST_NAME,
        E."position",
        E.DEPARTMENT,
        P."period",
        P.BASIC_SALARY,
        P.BONUS,
        P.DEDUCTIONS,
        P.TOTAL_SALARY,
        P.CREATED_DATE
    FROM
        PAYROLLS  P
        LEFT JOIN EMPLOYEES E
        ON E.ID = P.EMPLOYEE_ID;