
<br/>
<div align="center">

<img src="https://picsum.photos/400" alt="Logo" width="80" height="80">
</a>
<h3 align="center">Inventroy Service</h3>
<p align="center">
An Inventroy Service project which defines best pratices of using microservices approach.


  


</p>
</div>

## About The Project

Inventory service is capable of CRUD operations on entity, only based on Inventory data for ordering.

It uses Azure Function to reduce the inventory quantity whenever any new order gets placed.

Here's why:

- Swagger provides better documentations
- Azure function is always in warm phase.
- Immediately it updates the count in Inventory table.
### Built With

Download the project from Github and map it locally. Azure SQL credentials provided seperately over mail.

- [ASP.NET CORE WEBAPI](www.microsoft.com)
- [Azure](www.portal.azure.com)
- [Microservicess](www.portal.azure.com)
## Getting Started

Follow the swagger documentation and test all the endpoints of API and see the impact in db.
- https://orderapi20241006105322.azurewebsites.net/swagger/index.html
- Make sure, while placing a new orders. Those ItemId which represent the Inventory Id is present in Inventory db.
- Once place the order, please check inventory table that order reduces the inventroy quantity by this order quantity.
- Azure SQL Server name:- "**tcp:nsrdatabase.database.windows.net,1433**"
- Database name:- "**nsr_project**"
- User Id and Password provided over mail.
### Prerequisites

This is an example of how to list things you need to use the software and how to install them.

- Microsoft Visual Studio
- Azure subscription
- .Net core 6 or above
## Contact

Your Name - [Sunil Kumar) - sunilkgupta86@gmail.com

Project Link: [https://github.com/sunilkgupta/InventoryService]
