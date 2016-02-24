# ServiceSystem
Telerik Academy ASP.NET MVC 2016 Project

ServiceSystem is intended to aid service workshops in managing their orders.
The system provides functionality for users in specific role to add and then process the order.
## Non-registered users may access public area 

[Home.png]

view pricelist via controller PricesController.cs - cached for 24 hours

[Prices.png]

and check the status of their order via controller OrderStatusController.cs

[OrderStatus.png]

## Register users with role Engineer have access to Orders management. They can 
-create order (controller CreateOrderController). Information about the customer, device and warranty details must be provided

[Create.png]

at the end the result view may be printed as slip for the customer.
-view list of orders with status pending - accepted but not processed yet by menu Pending orders  - controller ListOrdersController.cs

[Pending.png]
- by clicking on the order number, the engineer may view details about the order

[Assign.png]

- if the engineer desires, he may assign the order to himself. Then new screen opens, where he may fill details abour repair and alter current information, if it not correct

[Edit.png]

- sortable and filterable information about all orders

[AllOrders.png]

- when status is set to Ready, then the customer could get away his device. In this case status is set to Delivered, and order can not be edited any more.

## Administrators have addition menu to edit information about all orders, assign registered users to role Engineer and modify categories and prices.

[Admin.png]