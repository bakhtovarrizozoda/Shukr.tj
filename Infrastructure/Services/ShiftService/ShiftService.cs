using System.Net;
using AutoMapper;
using Domain.Dtos.Shift;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ShiftService;

public class ShiftService : IShiftService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ShiftService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetShiftAllDto>> AllShift()
    {
    var result = (
    from s in _context.Shifts
    join e in _context.Employees on s.EmployeeID equals e.ID
    select new GetShiftAllDto
    {
        ID = s.ID,
        EmployeeName = e.FirstName,
        StartTime = s.StartTime,
        WorkedHours = s.WorkedHours,
        EndTime = s.EndTime
    }
    )
    .AsEnumerable()
    .Where(ti => ti.StartTime.TimeOfDay > new TimeSpan(9, 0, 0) || ti.EndTime.TimeOfDay < new TimeSpan(18, 0, 0))
    .ToList();

    return result;
    }
    public async Task<Response<GetShiftDto>> EndShift(int employeeId, DateTime endTime, AddShiftDto shift)
    {
       try
{
    var employee = _context.Employees.FirstOrDefault(e => e.ID == employeeId);
    if (employee == null)
    {
        return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, "Employee not found");
    }

    var existingShift = _context.Shifts.FirstOrDefault(s => s.EmployeeID == employeeId && s.EndTime == null);
    if (existingShift == null)
    {
        return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, "No active shift found for this employee");
    }

    existingShift.EndTime = endTime;
    existingShift.WorkedHours = (endTime - existingShift.StartTime).TotalHours;

    if (employee.Position == "Тестировщик свечей" && endTime.TimeOfDay < new TimeSpan(21, 0, 0))
    {
        existingShift.Remarks += "Late to finish shift. ";
    }

    await _context.SaveChangesAsync();
    return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, "Shift ended successfully");

}
catch (Exception ex)
{
    return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, ex.Message);
}
    }

    public async Task<Response<GetShiftDto>> StartShift(int employeeId, DateTime startTime)
    {
        try
    {
        var employee = _context.Employees.FirstOrDefault(e => e.ID == employeeId);
        if (employee == null)
        {
            return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, "Employee not found");
        }

        var shift = new Shift
        {
            StartTime = startTime,
            Employee = employee
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<GetShiftDto>(shift);
        return new Response<GetShiftDto>(result);
    }
    catch (Exception e)
    {
        return new Response<GetShiftDto>(HttpStatusCode.InternalServerError, e.Message);
    }
    }

}
