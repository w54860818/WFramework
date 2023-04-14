using WFramework.Example;

namespace FrameworkDesign.Example.Scripts
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());
        }
    }
}