using Domain.Dtos.Employee;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Services.EmployeeSevice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeService;

    public EmployeesController(IEmployeeService employeService)
    {
        _employeService = employeService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllEmployees()
    {
        var result = await _employeService.GetAllEmployees();
        return Ok(result);
    }

    [HttpPost("AddEmployee")]
    public async Task<ActionResult> AddEmployee([FromQuery]AddEmployeeDto employee)
    {
        var result = await _employeService.AddEmployee(employee);
        return Ok(result);
    }

    [HttpPut("UpdateEmployee")]
    public async Task<ActionResult> UpdateEmployee([FromQuery]int id, AddEmployeeDto employee)
    {
        var result = await _employeService.UpdateEmployee(id, employee);
        return Ok(result);
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        var result = await _employeService.DeleteEmployee(id);
        return Ok(result);
    }
}
