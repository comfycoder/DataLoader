# Data Loader Function App

A sample Azure Function App that demonstrates how to process and efficiently insert or update data in an Azure SQL Database from an uploaded CSV file. It contains two set of data types (Person and Company) that it can process.

The process works as follows:

1. A Blob trigger fires and does the following when it detects the upload file blob:
    - Converts the CSV records into smaller batch collection of an entity class object
    - Adds the records and a batch identifier to a message object
    - Adds the message onto an Azure storage message queue

    Note: This part of the process runs and completes within a few seconds.

2. A Queue trigger does the following when it detects a new message on the queue:
    - Retrieves the message from the queue.
    - Iterates through all of the record entities in the message
    - Evaluates whether the record already exists in the SQL database
    - Adds each entity (as an update or insert) to the Entity collection in the Entity Framework database context
    - Saves the entire batch in a single SQL database transaction
    - When an exception occurs, the entire batch transaction gets  rolled back and the message is added to a “poison” message queue in the same Azure Storage Account.

3. Another Queue trigger does the following when it detects a new message on the poison queue:
    - Retrieves the message from the queue.
    - Add the message to a custom filed message object
    - Write the message to a “ManualInterventionRequired” table in the same Azure Storage Account.

Other folders contain the following:
- Small mock data starter files
- Sample test blob files, some in various size
- SQL scripts to create the sample database tables
- A PowerShell script orchestrates uploading a CSV file from on-premise (or your machine) to an Azure Storage Account blob container.

The Company data sample consists of 20,000 records (each record is 1KB in size).

The process is able to load all 20,000 records in 3-4 minutes.
