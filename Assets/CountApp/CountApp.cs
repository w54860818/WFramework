using CountApp.Editor;
using FrameworkDesign;

namespace CountApp
{
    public class CountApp : Architecture<CountApp>
    {
        protected override void Init()
        {
            RegisterModel<ICounterApp>(new EditorCounterModel());
            RegisterUtility<IStorage>(new EditorPrefsStorage());
        }
    }
}