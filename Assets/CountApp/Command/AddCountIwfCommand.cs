using CountApp.Editor;
using FrameworkDesign;

namespace CountApp.Command
{
    public class AddCountIwfCommand : AbstractCommand
    {
        public override void OnExecute()
        {
            this.GetModel<ICounterApp>().Count.Value++;
        }
    }
}
