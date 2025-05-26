select
    c.FirstName,
    c.LastName,
    o.Id as OrderId,
    i.Price,
    oi.Quantity,
    oi.Quantity * i.Price as OrderTotal,
    i.Name,
    i.Description
from Customer c
    inner join "Order" o
    on o.CustomerId = c.Id
    inner join OrderItem oi
    on oi.OrderId = o.Id
    inner join Item i
    on i.Id = oi.ItemId
order by
    o.Id
    
    