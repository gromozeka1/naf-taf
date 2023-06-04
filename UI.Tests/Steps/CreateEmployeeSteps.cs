namespace UI.Tests.Steps;

[Binding]
[Scope(Feature = "CreateEmployee")]
public class CreateEmployeeSteps
{
    private Employee? _employee;
    private readonly IEmployeeCreatePage _employeeCreatePage;
    private readonly IEmployeeListPage _employeeListPage;

    public CreateEmployeeSteps(IEmployeeCreatePage employeeCreatePage, IEmployeeListPage employeeListPage)
    {
        _employeeCreatePage = employeeCreatePage;
        _employeeListPage = employeeListPage;
    }

    [When(@"I populate employee fields with '(valid|invalid)' data")]
    public void WhenIPopulateEmployeeFieldsWithData(string condition)
    {
        if (condition == "valid")
        {
            _employee = Employee.GetValidEmployeeData();
            _employeeCreatePage.PopulateForm(_employee);
        }
        else
        {
            ScenarioContext.StepIsPending();
        }
    }

    [Then(@"I see created employee")]
    public void ThenISeeCreatedEmployee()
    {
        _employeeListPage.SearchEmployeeByName(_employee!.Name!);
    }
}
