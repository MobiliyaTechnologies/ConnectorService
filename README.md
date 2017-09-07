# Energy Management Connector Utility

EM Connector Utility is a console application, which does either of below things
* Fetch Sensor Data from Wireless Tag Server, inserts these data into Local SQL database.
* Fetch Sensor Data from Wireless Tag Server, do PUT request to UFL Connector REST Server. 

## Getting Started

### Prerequisites

* Visual Studio 2017 with Other Project types(Visual Studio installer type) installed.
* Wireless Tag credentials (ClientID,ClientSecret,ClientCode),WirelessTag Api Server Base Address,WirelessTag Api Auth sub-url (Eg: /oauth2/access_token.aspx).You will get it from [here](https://my.wirelesstag.net/eth/oauth2_apps.html)
* OSI UFL Connector Server Address (eg : https://<Domain Address>:<Port Number>/connectordata/<Data Source Name>/)
* User mapping PI Interface Identity credentials (Username, Password).
* Local SQL Database connection string(Pi Server Database string).


### Installing

* Build EMConnectorSetup project in a Release mode.
* Double click .msi file at ProjectPath/Release/
* Follow the instructions of the setup wizard.
* Navigate to the path where EMConnectorSetup is Installed (C:/ProgramFiles(x86)/Mobiliya Technologies/EMConnectorSetup)
* Make changes to WirelessTagConnector.exe.config file if required.
* Double click WirelessTagConnector.exe and run the setup.


## Deployment

* Download this project.
* Configure App.config file of WirelessTagConnector project.
	* PiConnectionString : Local database connection string
	* ClientId : Wireless Tag Client Id
	* ClientSecret : Wireless Tag Client Secret
	* ClientCode : Wireless Tag Code (Generates after following the given steps [here](https://my.wirelesstag.net/eth/oauth2_apps.html) ) 
	* BaseAddress : WirelessTag Api Server Address (Eg: https://my.wirelesstag.net)
	* AuthApiSubAddress : WirelessTag Api Auth sub-url
	* UFLApiUsername : User mapping PI Interface Identity Username used for  UFL Connector Server Authentication
	* UFLApiPassword : User mapping PI Interface Identity password used for  UFL Connector Server Authentication
	* UFLApiAddress : UFL Connector REST Server address
	* IsPosterService : Sets to true(If you want to send data to UFL Connector Server) or false (If you want to push sensor data to local SQL database)
* Build EMConnectorSetup project.
* Browse to EMConnectorSetup Project Path/Release/
* Find EMConnectorSetup.msi and run it.

## Built With

* [Visual Studio 2017](https://www.visualstudio.com/downloads/) - The IDE
