Delete from employees
Delete from licenses
Delete from certifications

Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('Doc', 'Gonzo', '000-00-0000', 'Medical', '1/1/2016', 'Annual', '1', 'Family Medicine', 'docgonzo@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('Mouse', 'Benavente', '111-11-1111', 'Medical', '1/1/2015', 'Annual', '1', 'Registered Nurse', 'mouse@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('Y', 'Knott', '222-22-2222', 'Medical', '1/1/2016', 'Annual', '1', 'Family Medicine', 'yknott@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('G', 'Whiz', '333-33-3333', 'Administrative', '1/1/2015', 'Annual', '3', 'Scheduling', 'gwhiz@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('Doan', 'No', '444-44-4444', 'Administrative', '1/1/2016', 'Hourly', '5', 'Reception', 'doanno@outlook.com', 'password');
Insert into employees (employee_name_first, employee_name_last, employee_ssn, employee_type, employee_date_hire, employee_salary_type, employee_data_access_level, employee_specialty, employee_email, employee_password) VALUES ('Up', 'Innarms', '111-11-1111', 'Administrative', '1/1/2015', 'Monthly', '4', 'Clinic Manager', 'upinnarms@outlook.com', 'password');

Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('MD', 'WA0000', '1/1/2015', '1/1/2020', 'Washington');
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('RN', 'WA0111', '1/1/2015', '1/1/2020', 'Washington');
Insert into licenses (license_type, license_number, license_issue, license_expiration, license_issue_state) VALUES ('FP', 'WA0222', '1/1/2015', '1/1/2020', 'Washington');

Insert into certifications (employee_id, license_id) VALUES ('64', '10'); /**** FILL in the appropriate numbers AFTER populating the first two tables, then run only these three ****/
Insert into certifications (employee_id, license_id) VALUES ('65', '11');
Insert into certifications (employee_id, license_id) VALUES ('66', '12');

Select * from employees
Select * from licenses
Select * from certifications