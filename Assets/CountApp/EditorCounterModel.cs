using FrameworkDesign;

namespace CountApp.Editor
{

    public interface ICounterApp : IModel
    {
        public BindableProperty<int> Count { get; }
    }
    
    public class EditorCounterModel : AbstractModel, ICounterApp
    {
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);

            Count.OnValueChanged += count =>
            {
                storage.SaveInt("COUNTER_COUNT", count);
            };
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

    }
}
