Feature: Breeds

Get All Cat Breeds
Get Limited Amount Of Cat Breeds

Scenario: Get All Cat Breeds
    When I request all cat breeds
    Then I should receive a list of cat breeds

Scenario: Get Limited Amount Of Cat Breeds
    When I request a cat breed with limited amount of results '2'
    Then I should receive '2' cat breeds
