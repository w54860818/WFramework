using FrameworkDesign;

namespace WFramework.Example
{
    public class Game : AbstractPointGameController
    {
        private void Awake()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart)
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnDestroy()
        {
        }

        private void OnGameStart( GameStartEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }
    }

}