using FrameworkDesign;
using FrameworkDesign.Example.Scripts.Command;

namespace WFramework.Example
{
    public class Enemy : AbstractPointGameController
    {
        private void OnMouseDown()
        {
            Destroy(gameObject);

            this.SendCommand<KillEnemyCommand>();
        }
    }
}
