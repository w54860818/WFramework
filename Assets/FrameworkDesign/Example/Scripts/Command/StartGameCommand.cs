using WFramework.Example;

namespace FrameworkDesign.Example.Scripts.Command
{
    public class StartGameCommand : AbstractCommand
    {
        public override void OnExecute()
        {
            this.SendEvent<GameStartEvent>();
        }
    }
}