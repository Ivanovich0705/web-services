Feature: UsersServiceTests
    As a client
    I want to be able to add a user to the system
    So that I can create an account

    Background:
        Given the endpoint https://localhost:7120/api/v1/users is available

    @add-user
    Scenario: Add a user to the system
        When a POST request is sent to the endpoint with the following data
          | fullname | username | password | email           | country | dateOfBirth         | idNumber | description | photoUrl                   | points | rating | phone     | 
          | John Doe | johndoe  | password | email@gmail.com | USA     | 2002-11-13T04:18:09 | 03       | description | https://myurl.com/img2.png | 2      | 2      | 986124529 |
        Then the response status code should be 200

    @get-users
    Scenario: Get all users
        When a Get request is sent to the endpoint
        Then the response status code should be 200

    @get-users-by-user-id
    Scenario: Get a user by user id
        When a Get request is sent to the endpoint with user id 1
        Then the response status code should be 200

    @update-user
    Scenario: Update a user
        When a Put request is sent to the endpoint with id 1 and the following data
          | fullname | username | password | email           | country | dateOfBirth         | idNumber | description | photoUrl | points | rating | phone |
          | John Doe | johndoe  | password | email@gmail.com | PE      | 2002-11-13T04:18:09 | 12345678 | updated user | https://myurl.com/img2.png | 2      | 2      | 986124529 |

    @delete-user
    Scenario: Delete a user
        When a Delete request is sent to the endpoint with id 1
        Then the response status code should be 200