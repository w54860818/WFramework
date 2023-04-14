

namespace FrameworkDesign
{
    public interface ICanSendCommand : IBelongToArchitecture
    {
        
    }
    
    public static class CanSendCommandExtension
    {
        public static void SendCommand<T>(this ICanSendCommand self) where T : IFCommand, new()
        {
            self.GetArchitecture().SendCommand<T>();
        }
        
        public static void SendCommand<T>(this ICanSendCommand self, T command) where T : IFCommand
        {
            self.GetArchitecture().SendCommand<T>(command);
        }
    }
}