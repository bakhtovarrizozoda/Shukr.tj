using Domain.Dtos.Employee;
using Domain.Wrapper;

namespace Infrastructure.Services.EmployeeSevice;

public interface IEmployeeService
{
    public Task<Response<List<GetEmployeeDto>>> GetAllEmployees();
    public Task<Response<GetEmployeeDto>> AddEmployee(AddEmployeeDto employee);
    public Task<Response<GetEmployeeDto>> UpdateEmployee(int id, AddEmployeeDto employee);
    public Task<Response<bool>> DeleteEmployee(int id);
}
