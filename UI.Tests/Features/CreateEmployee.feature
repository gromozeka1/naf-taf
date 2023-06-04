Feature: CreateEmployee

Create new employee with valid data

Scenario: Create new employee with valid data
	Given I logged in
	When I go to 'Employee List' page
	And I click 'Create New' link
	And I populate employee fields with 'valid' data
	Then I see 'no' validation error
	When I click 'Create' button
	Then I see created employee
