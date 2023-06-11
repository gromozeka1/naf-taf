Feature: Login

Login with valid credentials
Login with invalid credentials

Scenario: Login with valid credentials
	When I click login link
	And I enter '<Username>' login and '<Password>' password
	Then I successfully login

Examples: 
	| Username   | Password   |
	| admin      | password   |
	| failedTest | failedTest |

Scenario: Login with invalid credentials
	When I click login link
	And I enter login details
	| UserName | Password        |
	| admin    | invalidPassword |
	Then I see 'Invalid login attempt' validation error
