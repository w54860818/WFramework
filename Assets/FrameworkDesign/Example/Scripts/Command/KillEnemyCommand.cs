using WFramework.Example;

namespace FrameworkDesign.Example.Scripts.Command
{
    public class KillEnemyCommand : AbstractCommand
    {
        public override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;
            
            if (gameModel.KillCount.Value == 10)
            {
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}