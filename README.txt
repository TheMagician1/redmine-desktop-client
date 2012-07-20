System Requirements

* The application was tested to run on .NET Framework 2.0 and Mono 2.4 and Mono 2.2

Installation

* Unzip the archive
* Run RedmineClient.exe
* Go to Settings and fill in your Redmine address and credentials

Configuring Redmine to work properly with the Client

* Go to Administration -> Settings
* On General tab add 999999 to Objects per page options so that the value is for example "25, 50, 100, 999999"
* On General tab set Feed content limit to some reasonable number so that you see all your issues in the Redmine Client. If you for example have 100 issues in your project, you have to set it to a value bigger than 100 obviously.