
select * from employees
select * from licenses
select * from certifications
select * from physicians
Select * from payrolls

Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Doc', 'Gonzo', '000-00-0000', 'Medical', '1/1/2016', 'Annual', 'docgonzo@outlook.com', 'password');  
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Mouse', 'Benavente', '111-11-1111', 'Medical', '1/1/2015', 'Annual', 'mouse@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Dr.', 'Aharchi', '222-22-2222', 'Management', '1/1/2015', 'Annual', 'aharchi@outlook.com', 'password');   


Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('MD', 'WA0000', '1/1/2015', '1/1/2020', 'Washington');  
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('RN', 'WA0111', '1/1/2015', '1/1/2020', 'Washington');  
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('FP', 'WA0222', '1/1/2015', '1/1/2020', 'Washington');  

Insert into certifications (employee_id, license_id) VALUES ('1', '1'); /**** FILL in the appropriate numbers AFTER populating the first two tables, then run only these three ****/  
Insert into certifications (employee_id, license_id) VALUES ('1', '3');  
Insert into certifications (employee_id, license_id) VALUES ('2', '2');  

Insert into physicians (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Doc', 'Gonzo', '000-00-0000', 'Medical', '1/1/2016', 'Annual', 'docgonzo@outlook.com', 'password');  
Insert into physicians (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_email, employee_password) VALUES ('Mouse', 'Benavente', '111-11-1111', 'Medical', '1/1/2015', 'Annual', 'mouse@outlook.com', 'password');  

Insert into payroll (pay_period, salary_type, salary_amount, employee_id) VALUES ('Biweekly', 'Annual', '$200,000', '1');  
Insert into payroll (pay_period, salary_type, salary_amount, employee_id) VALUES ('Weekly', 'Hourly', '$150,000', '2'); 

Delete from employees
Delete from licenses
Delete from certifications
Delete from physicians
Delete from payrolls