using FrameworkDesign.Example.Scripts;
using UnityEditor;
using UnityEngine;

namespace FrameworkDesign
{
    public interface IController : IBelongToArchitecture, ICanSendCommand, ICanGetSystem, ICanGetModel, ICanRegisterEvent
    {
        
    }
    
    /// <summary>
    /// 这里可以为项目定制
    /// </summary>
    public abstract class AbstractPointGameController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            //替换项目的总入口
            return PointGame.Interface;
        }
    }
    
    public abstract class AbstractCountAppController : EditorWindow, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            //替换项目的总入口
            return CountApp.CountApp.Interface;
        }
    }
}