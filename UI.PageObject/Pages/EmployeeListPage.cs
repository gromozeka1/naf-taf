using UI.PageObject.Elements;

namespace UI.PageObject.Pages;

public interface IEmployeeListPage
{
    void CreateNewEmployee();
    void SearchEmployeeByName(string employeeName);
}

public class EmployeeListPage : BasePage, IEmployeeListPage
{
    private readonly Link _createNewButton;
    private readonly Input _searchInput;
    private readonly Button _searchButton;

    public EmployeeListPage(IDriver driver) : base(driver)
    {
        _createNewButton = FindByRole<Link>(AriaRole.Link, "Create New");
        _searchInput = Find<Input>("//input[@name='searchTerm']");
        _searchButton = Find<Button>("//input[@value='Search']");
    }

    public void CreateNewEmployee()
    {
        _createNewButton.Click();
    }

    public void SearchEmployeeByName(string employeeName)
    {
        _searchInput.Fill(employeeName);
        _searchButton.Click();
    }
}
