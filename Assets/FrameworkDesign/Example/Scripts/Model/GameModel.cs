using FrameworkDesign;

namespace WFramework.Example
{

    public enum GameStates
    {
        NotStart,
        Started,
        Passed,
    }

    public interface IGameModel : IModel
    {
        public BindableProperty<int> KillCount { get; }

        public BindableProperty<int> Gold { get; }

        public BindableProperty<int> Score { get; }

        public BindableProperty<int> BestScore { get; }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        protected override void OnInit()
        {
            
        }

        public GameStates CurrentStates = GameStates.NotStart;
        
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> Gold { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> Score { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> BestScore { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

    }
}
