ALTER TABLE customers
ADD Cusame VARCHAR(255);

EXEC sp_rename 'customers.CusName', 'cus_name', 'COLUMN';



select * from orders
delete from customers where customer_id = 4