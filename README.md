#Building a custom ERP using ASP.NET Webform (.aspx) and WCF Services.
-----
To build that custom ERP **(Enterprise Resource Planning)** web portal, you need to first understand the basic concept of ERP.
Like, Master Data (Vendor Master Data, Item Master Data, Warehouse Master Data) and then the basic Purchase Cycle in ERP.

###Here below is a basic flow of purchase to inventory.
1. Purchase Request
    (Here a company raised the demand of an Item or it could be more numbers of items, internally inside the department which finally goes to HR to approve it.)
2. Purchase Order (PO)
    (It is a final list of items, quantity of them **send to the respective Vendors** to buy those items within specified time limit.)
3. Goods Received Purchase Order (GRPO)
    (It is document produce internally by the company to asses how much quantity has received with respect to how much were ordered. It's price, tax, discount all are counted in that document. 
    Untill all the items are received the GRPO remains open. If all are received the it becomes closed.)
4. Inventory
    (After items received it goes to the selected warehouses. That decision is made internally by company and here the implementation of Warehouse Master Data comes in.)
5. Account Purchase Invoice (A/P invoice)
    (After Vendor sends the invoice for to payment of those received items, company pays to vendor and records all those details including per unit price, Tax, discount, Vendor Code, Item Code...etc in that
    A/P Invoice which is store in that ERP Database.)
------------------------

Here i implemented this flow in ASP.NET Webform portal and it's database. Database files also given in SQL folder. The Portal also used legacy WCF REST API service to exchange the DB transaction of PO, GRPO, A/P invoice.  
