using Model.Context;
using Ninject;
using StaffAIS.Global;

namespace StaffAIS.Services;

public class BaseService
{
    public StaffDbContext db;
    public BaseService()
    {
        db = GlobalKernel.Kernel.Get<StaffDbContext>();
    }
}