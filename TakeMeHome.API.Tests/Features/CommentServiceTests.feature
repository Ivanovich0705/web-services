Feature: CommentServiceTests
	As a client 
	I want to be able to add a comment to a tourist
	So that I can express my experience with the order
	
	Background: 
		Given the endpoint https://localhost:7120/api/v1/comments is available
		
	@add-comment
	Scenario: The client adds a comment to a tourist
		When a POST request is sent to the endpoint with the following data
			| stars | content               | orderId |
			| 3     | This is a new comment | 3       |
		Then the response status code should be 200
		
	@get-comments
	Scenario: The client gets all comments
		When a Get request is sent to the endpoint
		Then the response status code should be 200
		
	@get-comments-by-user-id
	Scenario: The client gets a comment by id
		When a Get request is sent to the endpoint with user id 1
		Then the response status code should be 200
		
	@update-comment
	Scenario: The client updates a comment
		When a Put request is sent to the endpoint with id 6 and the following data
			| stars | content                     |
			| 3     | This is the updated comment |
		Then the response status code should be 200
		
	@delete-comment
	Scenario: The client deletes a comment
		When a Delete request is sent to the endpoint with id 11
		Then the response status code should be 200