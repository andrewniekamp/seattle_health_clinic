Select * from employees;
Select * from licenses;
Select * from certifications;
Select * from patients;
select * from certifications;

Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Doc', 'Gonzo', '000-00-0000', 'Medical', '1/1/2016', 'Annual', 'docgonzo@outlook.com', 'password');  
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Mouse', 'Benavente', '111-11-1111', 'Medical', '1/1/2015', 'Annual', 'mouse@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Dr.', 'Aharchi', '222-22-2222', 'Management', '1/1/2015', 'Annual', 'aharchi@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Y', 'Knott', '333-33-3333', 'Administrative', '1/1/2014', 'Hourly', 'yknott@outlook.com', 'password');  
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Fur', 'Shour', '444-44-4444', 'Administrative', '1/1/2013', 'Hourly', 'furshour@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('To', 'Do', '555-55-5555', 'Administrative', '1/1/2012', 'Hourly', 'todo@outlook.com', 'password');  
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Dr.', 'West', '666-66-6666', 'Medical', '1/1/2016', 'Annual', 'docwest@outlook.com', 'password');  
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Dr', 'Benavente', '777-77-7777', 'Medical', '1/1/2015', 'Annual', 'docbenavente@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Dr.', 'Sid', '888-88-8888', 'Management', '1/1/2015', 'Monthly', 'docsid@outlook.com', 'password');   


Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('MD', 'WA0000', '1/1/2015', '1/1/2020', 'Washington');  
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('RN', 'WA0111', '1/1/2015', '1/1/2020', 'Washington');  
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('FP', 'WA0222', '1/1/2015', '1/1/2020', 'Washington');  

Insert into certifications (employee_id, license_id) VALUES ('1', '1'); /**** FILL in the appropriate numbers AFTER populating the first two tables, then run only these three ****/  
Insert into certifications (employee_id, license_id) VALUES ('1', '3');  
Insert into certifications (employee_id, license_id) VALUES ('2', '2');  

Insert into physicians (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Doc', 'Gonzo', '000-00-0000', 'Medical', '1/1/2016', 'Annual', 'docgonzo@outlook.com', 'password');  
Insert into physicians (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Mouse', 'Benavente', '111-11-1111', 'Medical', '1/1/2015', 'Annual', 'mouse@outlook.com', 'password');  

Insert into payrolls (pay_period, salary_type, salary_amount, employee_id) VALUES ('Biweekly', 'Annual', '$200,000', '1');  
Insert into payrolls (pay_period, salary_type, salary_amount, employee_id) VALUES ('Weekly', 'Hourly', '$150,000', '2'); 

Delete from employees;
Delete from licenses;
Delete from certifications;
Delete from patients;
Delete from certifications;
