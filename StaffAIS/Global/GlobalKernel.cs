using Model.Context;
using Ninject;

namespace StaffAIS.Global;


    public class GlobalKernel
    {
        public static IKernel Kernel;
    
        static GlobalKernel()
        {
            Kernel=new StandardKernel();
            Kernel.Bind<StaffDbContext>().ToSelf().InSingletonScope(); 
        }
    }
