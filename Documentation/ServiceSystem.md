# ServiceSystem
Telerik Academy ASP.NET MVC 2016 Project

ServiceSystem is intended to aid service workshops in managing their orders.
The system provides functionality for users in specific role to add and then process the order.
## Non-registered users may access public area 

![Home](Home.PNG)

view pricelist via controller PricesController.cs - cached for 24 hours

![Prices](Prices.PNG)

and check the status of their order via controller OrderStatusController.cs

![OrderStatus](OrderStatus.PNG)

## Register users with role Engineer have access to Orders management. They can 
-create order (controller CreateOrderController). Information about the customer, device and warranty details must be provided

![Create](Create.PNG)

at the end the result view may be printed as slip for the customer.
-view list of orders with status pending - accepted but not processed yet by menu Pending orders  - controller ListOrdersController.cs

![Pending](Pending.PNG)
- by clicking on the order number, the engineer may view details about the order

![Assign](Assign.PNG)

- if the engineer desires, he may assign the order to himself. Then new screen opens, where he may fill details abour repair and alter current information, if it not correct

![Edit](Edit.PNG)

- sortable and filterable information about all orders

![AllOrders](AllOrders.png)

- when status is set to Ready, then the customer could get away his device. In this case status is set to Delivered, and order can not be edited any more.

## Administrators 
have additional menu to edit information about all orders, assign registered users to role Engineer and modify categories and prices.

![Admin](Admin.PNG)
