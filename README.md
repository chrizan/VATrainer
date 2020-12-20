# VATrainer
The VATrainer Xamarin android/iOS application helps students to train their value added tax knowledge. 

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* **Database configuration:**   
  The content is shipped on an SQLite database as html formatted text. The application uses Entity Framework Core to access the data.   
  The purpose of the EF_SQLite_Dummy project is to provide a .NET Core runtime for the Entity Framework Core. Further more, the SQLite database (model first)     
  is generated into the EF_SQLite_Dummy project and added as link to the Asssets folder on Android, the Resources folder on iOS respectively.    
  On Visual Studio use the Package Manager Console to migrate and update the database:   
  Uncomment iOS/Android specific part in VATrainer.DataLayer.VATrainerContext.OnConfiguring(...)   
  `PM>dotnet ef migrations --project VATrainer --startup-project EF_SQLite_Dummy add MyMigration`    
  `PM>dotnet ef database --project VATrainer --startup-project EF_SQLite_Dummy update`

* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact
