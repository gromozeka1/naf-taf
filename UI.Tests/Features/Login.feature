Feature: Login

Login with valid credentials
Login with invalid credentials

Scenario: Login with valid credentials
	When I click login link
	And I enter login details
	| UserName | Password |
	| admin    | password |
	Then I successfully login

Scenario: Login with invalid credentials
	When I click login link
	And I enter login details
	| UserName | Password        |
	| admin    | invalidPassword |
	Then I see 'Invalid login attempt' validation error
