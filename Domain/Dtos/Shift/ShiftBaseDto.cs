namespace Domain.Dtos.Shift;

public class ShiftBaseDto
{
    public int ID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double WorkedHours { get; set; }
    public int EmployeeID { get; set; }
    public string Remarks { get; set; }
}
