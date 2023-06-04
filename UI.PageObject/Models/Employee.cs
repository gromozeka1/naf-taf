using Bogus;

namespace UI.PageObject.Models;

public class Employee
{
    public string Name { get; set; } = string.Empty;
    public float Salary { get; set; }
    public int DurationWorked { get;set; }
    public int Grade { get; set; }
    public string Email { get; set; } = string.Empty;

    public static Employee GetValidEmployeeData()
    {
        var faker = new Faker();
        return new Employee
        {
            Name = faker.Name.FullName(),
            Salary = faker.Random.Float(min: 0, max: 99999),
            DurationWorked = faker.Random.Int(min: 0, max: 15),
            Grade = faker.Random.Int(min: 0, max: 10),
            Email = faker.Person.Email,
        };
    }
}
