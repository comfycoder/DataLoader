-- Create external DB AD group for SQL developers
CREATE USER [R-XXXXXXXX-Developers] FROM EXTERNAL PROVIDER;

-- Create external DB user for app managed service identity (MSI)
CREATE USER [AS-XX-DataLoader] FROM EXTERNAL PROVIDER;

-- Add app managed service identity (MSI) to database reader role
EXEC sp_addrolemember N'db_datareader', N'AS-XX-DataLoader'  
GO

-- Add app managed service identity (MSI) to database writer role
EXEC sp_addrolemember N'db_datawriter', N'AS-XX-DataLoader'  
GO  

