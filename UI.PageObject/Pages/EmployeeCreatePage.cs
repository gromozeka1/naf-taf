using UI.PageObject.Elements;
using UI.PageObject.Models;

namespace UI.PageObject.Pages;

public interface IEmployeeCreatePage
{
    void ClickCreate();
    void PopulateForm(Employee employee);
}

public class EmployeeCreatePage : BasePage, IEmployeeCreatePage
{
    private readonly Input _name;
    private readonly Input _salary;
    private readonly Input _durationWorked;
    private readonly Input _grade;
    private readonly Input _email;

    private readonly Button _createButton;

    public EmployeeCreatePage(IDriver driver) : base(driver)
    {
        _name = Find<Input>("#Name");
        _salary = Find<Input>("#Salary");
        _durationWorked = Find<Input>("#DurationWorked");
        _grade = Find<Input>("#Grade");
        _email = Find<Input>("#Email");
        _createButton = Find<Button>("text=Create");
    }

    public void PopulateForm(Employee employee)
    {
        _name.Fill(employee.Name);
        _salary.Fill(employee.Salary.ToString());
        _durationWorked.Fill(employee.DurationWorked.ToString());
        _grade.Fill(employee.Grade.ToString());
        _email.Fill(employee.Email);
    }

    public void ClickCreate()
    {
        _createButton.Click();
    }
}
