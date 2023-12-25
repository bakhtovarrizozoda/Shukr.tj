using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos.Employee;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.EmployeeSevice;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public EmployeeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetEmployeeDto>>> GetAllEmployees()
    {
        try
        {
            var model = await _context.Employees.ToListAsync();
            var result = _mapper.Map<List<GetEmployeeDto>>(model);
            return new Response<List<GetEmployeeDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetEmployeeDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetEmployeeDto>> AddEmployee(AddEmployeeDto employee)
    {
        try
        {
            var model = _mapper.Map<Employee>(employee);
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetEmployeeDto>(model);
            return new Response<GetEmployeeDto>(result);
        }
        catch (Exception e)  
        {
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetEmployeeDto>> UpdateEmployee(int id, AddEmployeeDto employee)
    {
        try
        {
            if (id != employee.ID)
            {
                return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, "Invalid employee ID");
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, "Employee not found");
            }

            _mapper.Map(employee, existingEmployee);
            _context.Entry(existingEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var result = _mapper.Map<GetEmployeeDto>(existingEmployee);
            return new Response<GetEmployeeDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteEmployee(int id)
    {
        try
        {
            var model = await _context.Employees.FindAsync(id);
            if (model == null) return new Response<bool>(HttpStatusCode.InternalServerError, "Employee not found");
            _context.Employees.Remove(model);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
