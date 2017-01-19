WITH first_purchase AS 
(
	SELECT 
		CustomerId, 
		MIN(DateCreated) AS DateCreated
	FROM 
		Sales
	GROUP BY 
		CustomerId
),
distinct_product AS (
	SELECT DISTINCT
		Sales.ProductId,
		Sales.CustomerId 
	FROM 
		Sales
	INNER JOIN first_purchase AS fp 
		ON Sales.CustomerId = fp.CustomerId 
			AND Sales.DateCreated = fp.DateCreated
)

SELECT
	ProductId, 
	COUNT(*) AS Quantity
FROM 
	distinct_product 
GROUP BY 
	ProductId