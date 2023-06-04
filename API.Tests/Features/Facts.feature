Feature: Facts

Get Random Cat Fact
Get Random Cat Fact with Specific Length

Scenario: Get Random Cat Fact
  When I request a random cat fact
  Then I should receive a cat fact

Scenario: Get Random Cat Fact with Specific Length
  When I request a random cat fact with length less than 100
  Then I should receive a cat fact with length less than 100