Feature: OrderServiceTests 
	As a client 
	I want to be able to order a product
	So that I can get a tourist sent it my way
	
	Background: 
		Given the endpoint https://localhost:7120/api/v1/orders is available

	@add-order
	Scenario: The client creates an order
		When a POST request is sent to the endpoint with the following data
			| OrderCode | OriginCountry |  OrderDestination |  RequestDate     			| DeadlineDate 	  			| CurrentProcess  	| UserId 		|	 ClientId | OrderStatusId
			| QIASD     | USA 			| Peru  	        | 2019-01-01 00:00:00.000 	| 2019-01-01 00:00:00.000 	| 1     			| 1     	 	| 1 		  | 1
		Then the response status code should be 200
		
	@get-orders
	Scenario: The client gets all the orders
		When a GET request for orders is sent to the endpoint
		Then the response status code should be 200

	@get-orders-by-status-id
	Scenario: The client gets an order by status id
		When a Get request is sent to the endpoint with status id 1
		Then the response status code should be 200
		
	@update-order
	Scenario: The client updates an order
		When a PUT request is sent to the endpoint with the following data
			| value | path |  op |  from     d
			| QIASD     | Canada 							| 1       | 1 		| 1
		Then the response status code should be 200
