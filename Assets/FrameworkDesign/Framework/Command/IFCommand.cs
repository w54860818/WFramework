
namespace FrameworkDesign
{
    public interface IFCommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanGetUtility, ICanSendEvent, ICanSendCommand
    { 
        void Execute();
    }
    
    public abstract class AbstractCommand : IFCommand
    {
        private IArchitecture _mArchitecture;
        
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return _mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            _mArchitecture = architecture;
        }

        void IFCommand.Execute()
        {
            OnExecute();
        }

        public abstract void OnExecute();
    }
}