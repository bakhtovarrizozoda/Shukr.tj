namespace Domain.Entities;

public class Employee
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Position { get; set; }
    public List<Shift> Shifts { get; set; }
}
