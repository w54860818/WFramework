using CountApp.Editor;
using FrameworkDesign;

namespace CountApp.Command
{
    public class SubCountIwfCommand : AbstractCommand
    {
        public override void OnExecute()
        {
            this.GetModel<ICounterApp>().Count.Value--;
        }
    }
}
