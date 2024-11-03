namespace ADO.Net.Models.Employee
{
    public interface IEmployee
    {
        Employee SelectEmployee(int id);

        Employee SaveEmployee(Employee employee);

        bool DeleteEmployee(int id);
    }
}
