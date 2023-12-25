namespace Domain.Dtos.Shift;

public class GetShiftAllDto
{
    public int ID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double WorkedHours { get; set; }
    public string EmployeeName { get; set; }
}
