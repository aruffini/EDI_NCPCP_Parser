## Solution Structure
```
- EDI_NCPDP_Ingestion
  - scr/
    - EDI_NCPDP_Ingestion/    <- Main Project
      - App.config            <- Connection String source
      - Config.cs             <- Serial Key source
      - Contextdb.cs          <- Database Model
      - Program.cs            <- Main application
      - ReadNCPDP.cs          <- Reads and Parses the file
      - SaveNCPDP.cs          <- Saves the parsed file to the database
  - tests/
    - NCPDP_Test/             <- Test Project
      - NCPDPTest.cs          <- Main Test application
      - MSTestSettings.cs     
  - samples/                  <- Testing Files
    - ClaimBilling
    - ClaimBillings
  - docs/
```

## NuGet Packages and Dependencies

- EdiFabric
- EdiFabric.Templates.NCPDP
- EntityFramework
- Microsoft.EntityFrameworkCore
- Microsoft.Extentions.Configuration
- Moq
- MSTest
- localdb
- .NET8 SDK

## Set Up
Copy the contents of samples to a location of your choice and update Config.cs and NCPDPTest.cs with the folder location prior to executing the main application.

### Database Migration Creation
Run the following in PowerShell or the Package Manager Console in Visual Studio
1. Add-Migration InitialCreate
2. Update-Database
  
