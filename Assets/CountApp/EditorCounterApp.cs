using CountApp.Command;
using CountApp.Editor;
using FrameworkDesign;
using UnityEditor;
using UnityEngine;

namespace CountApp
{
    
    public class EditorCounterApp : AbstractCountAppController
    {
        [MenuItem(("EditorConterApp/Open"))]
        static void Open()
        {
            CountApp.OnRegisterPatch += architecture =>
            {
                architecture.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };
            
            var window = GetWindow<EditorCounterApp>();
            window.position = new Rect(100, 100, 400, 600);
            window.titleContent = new GUIContent("计数器");
            window.Show();
        }
        

        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                this.SendCommand<AddCountIwfCommand>();
            }
            
            GUILayout.Label(this.GetModel<ICounterApp>().Count.Value.ToString());
            
            if (GUILayout.Button("-"))
            {
                this.SendCommand<SubCountIwfCommand>();
            }
        }
    }
}
