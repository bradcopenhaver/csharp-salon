# Hair Salon Manager

#### This web app tracks stylists and their clients for a hair salon. {December 2016}

#### By **Brad Copenhaver**

## Description
This program uses a local database to keep track of different stylists and each of their clients for a hair salon. Each client is assigned to an individual stylist. The user can create new stylists or clients and see a list of each stylist's clients. Information for stylists and clients can be changed after they are created, and individuals of either type can be deleted from the system.

### Specifications
_This program will..._

1. Save a record of info for an individual stylist.
 * Input: Stylist: Grace
 * Output: Stylist: {1, Grace}
2. Display a list of saved stylists.
 * Input: All stylists
 * Output: Stylist: {1, Grace}, {2, Harmony}, ...
3. Save a record of info for an individual client that is assigned to a stylist.
 * Input: Grace's Client: Cathy
 * Output: Client: {1, Cathy, 1}
4. Display a list of any stylist's clients.
 * Input: Harmony's clients
 * Output: Client: {1, Cathy, 1}, {2, Cindy, 1}, ...
5. Edit saved info for any individual.
 * Input: Client Cathy -> Kathy
 * Output: Client: {1, Cathy, 1} -> {1, Kathy, 1}
6. Delete any individual client.
 * Input: Remove Client Kathy
 * Output: Client: {1, Kathy, 1} -> {}
7. Delete any individual stylist and all of that stylist's clients.
 * Input: Remove Stylist Grace
 * Output: Stylist: {1, Grace), Client: {1, Kathy, 1} -> {}, {}

## Setup/Installation Requirements

1. Clone this GitHub repository.
2. From the command prompt, run '>SqlLocalDb.exe c MSSQLLocalDB -s' to create an instance of LocalDB.
3. Run the command '>sqlcmd -S "(localdb)\\MSSQLLocalDB"' and run the following SQL commands to create the local database and tables:

>CREATE DATABASE hair_salon
GO
USE [hair_salon]
GO
CREATE TABLE stylists(
	id INT IDENTITY(1,1),
	name VARCHAR(255)
)
GO
CREATE TABLE clients(
	id INT IDENTITY(1,1),
	name VARCHAR(255),
	stylist_id INT
)
GO

4. Navigate to the repository in terminal and run the command >dnu restore
5. In the same location, create a local server by running the command >dnx kestrel
6. Open a web browser and navigate to localhost:5004 to view the app.

## Known Bugs



## Possible future version features

Add more properties to Stylist and Client. 
Reassign a client to a different stylist.

## Support and contact details

If you have questions or comments, contact the author at bradcopenhaver@gmail.com

## Technologies Used

* C#
* SQL
* Nancy framework
* Razor view engine
* html/css
* Bootstrap

### License

This project is licensed under the MIT license.

Copyright (c) 2016 **Brad Copenhaver**
