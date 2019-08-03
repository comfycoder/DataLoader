# Specify the name of your storage account
$SA_NAME = "sa"
Write-Verbose "SA_NAME: $SA_NAME" -Verbose

# Specify the folder location of your local on-premise file
$BLOB_DATA_PATH = "C:\srcMM\DataLoader\LargeBlobData"
Write-Verbose "BLOB_DATA_PATH: $BLOB_DATA_PATH" -Verbose

# Specify the name of the file to upload
$FILE_NAME = "$BLOB_DATA_PATH\Person.csv"
Write-Verbose "FILE_NAME: $FILE_NAME" -Verbose

# Specify the name of the blob container to create
$CONTAINER_NAME = "csv-files"
Write-Verbose "CONTAINER_NAME: $CONTAINER_NAME" -Verbose

# Specify the full URI to your storage account blob container
$SA_CONTAINER_URI = "https://$SA_NAME.blob.core.windows.net/$CONTAINER_NAME"
Write-Verbose "SA_CONTAINER_URI: $SA_CONTAINER_URI" -Verbose

# Tell PowerShell where it can find your Azure CLI command line executable
$env:path += ";C:\Program Files (x86)\AzCopy;"

# Log into Azure (NOTE: Normal az login is not sufficient)
azcopy login 
<#
You should see something like the following:

PS C:\src\DataLoader> azcopy login
To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code E9NPJUAN6 to authenticate.

INFO: Logging in under the "Common" tenant. This will log the account in under its home tenant.
INFO: If you plan to use AzCopy with a B2B account (where the account's home tenant is separate from the tenant of the target storage account), please sign in under the target tenant with --tenant-id
INFO: Login succeeded.
#>

# Create a blob container
azcopy make "$SA_CONTAINER_URI"
<#
You should see something like the following:

PS C:\src\DataLoader> azcopy make "$SA_CONTAINER_URI"
INFO: Make is using OAuth token for authentication.

Successfully created the resource.
#>

# Copy a file to the blob container
azcopy cp "$FILE_NAME" "$SA_CONTAINER_URI"
<#
You should see something like the following:

PS C:\src\DataLoader> azcopy make "$SA_CONTAINER_URI"
INFO: Make is using OAuth token for authentication.

Successfully created the resource.
PS C:\src\DataLoader> azcopy cp "$FILE_NAME" "$SA_CONTAINER_URI"
INFO: Scanning...
INFO: Using OAuth token for authentication.

Job 27bc1845-fcaf-984e-7e2c-ac7ff72ab3f6 has started
Log file is located at: C:\Users\dmm0890/.azcopy/27bc1845-fcaf-984e-7e2c-ac7ff72ab3f6.log

0 Done, 0 Failed, 1 Pending, 0 Skipped, 1 Total,


Job 27bc1845-fcaf-984e-7e2c-ac7ff72ab3f6 summary
Elapsed Time (Minutes): 0.0333
Total Number Of Transfers: 1
Number of Transfers Completed: 1
Number of Transfers Failed: 0
Number of Transfers Skipped: 0
TotalBytesTransferred: 74882
Final Job Status: Completed
#>


# List the files int the blob container
$SA_FILES = azcopy list "$SA_CONTAINER_URI"
$SA_FILES
<#
You should see something like the following:

PS C:\src\DataLoader> # List the files int the blob container
$SA_FILES = azcopy list "$SA_CONTAINER_URI"
$SA_FILES
INFO: List is using OAuth token for authentication.
INFO: Person.csv; Content Size: 73.13 KiB
#>
