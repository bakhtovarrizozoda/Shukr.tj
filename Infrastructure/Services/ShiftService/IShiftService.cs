using Domain.Dtos.Shift;
using Domain.Wrapper;

namespace Infrastructure.Services.ShiftService;

public interface IShiftService
{
    public Task<List<GetShiftAllDto>> AllShift();
    public Task<Response<GetShiftDto>> StartShift(int employeeId, DateTime startTime);
    public Task<Response<GetShiftDto>> EndShift(int employeeId, DateTime endTime, AddShiftDto shift);
}
