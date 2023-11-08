# School Secretary Application
## App is only in Polish
## Description
The School Secretary Application is a simple platform for storing student data and allowing selection based on various criteria, such as first name, last name, and class. Please note that this application is available only in the Polish language. The application is built using .NET technology and utilizes a SQL Express database to store data.

## Features
- Adding new students (first name, last name, class).
- Storing student data in a database.
- Selecting students based on first name, last name, or class.
- Options for selection criteria: equal, starts with, contains.

## Requirements
- .NET Framework runtime environment.
- SQL Express database (or any compatible database).

## Running the Application
1. Download the application source code from this repository or clone the repository to your local machine.
2. Open the project in a development environment such as Visual Studio.
3. Create new Database and table e.g.:
```SQL{
CREATE TABLE [dbo].[Tables] (
    [Imie]     NVARCHAR (50) NULL,
    [Nazwisko] NVARCHAR (50) NULL,
    [Klasa]    NVARCHAR (50) NULL
);
```
4. Configure the SQL Express database connection and change 87 line in sekretariat.cs connetionString
5. Run the application.
6. 
## Usage Instructions

1. Start the application.
2. Log in using the username "a" and password "a" and get verified by captcha.
3. After a successful login, you will be redirected to the application interface.
4. Go to "Dodaj ucznia" TabControl
5. Add new students by entering their first name, last name, and class, then click the "Add Student" button.
6. To perform student selection Back to last TabControl and choose a criterion (first name, last name, or class) and selection type (equal, starts with, contains), and enter the appropriate phrase.
7. Click the "Search" button to display the selection results.


## Author

This project was created by livcia Contact: xOliwkaa69@gmail.com.

## License

This project is provided under the [no licence xd] license.

Thank you for using the School Secretary Application! If you have any questions or suggestions, please feel free to contact us.
