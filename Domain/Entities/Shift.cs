namespace Domain.Entities;

public class Shift
{
    public int ID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double WorkedHours { get; set; }
    public int EmployeeID { get; set; }
    public Employee Employee { get; set; }
    public string Remarks { get; set; }
}
