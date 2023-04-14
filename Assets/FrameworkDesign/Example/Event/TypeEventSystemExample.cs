using FrameworkDesign;
using UnityEngine;

public class TypeEventSystemExample : MonoBehaviour
{
    public struct EventA
    {
        
    }

    public struct EventB
    {
        public int ParamB;
    }
    
    public interface IEventGroup
    {
        
    }
    
    public struct EventC: IEventGroup
    {
        
    }
    
    public struct EventD: IEventGroup
    {
        
    }

    private TypeEventSystem _mTypeEventSystem = new TypeEventSystem();
    
    private void Start()
    {
        _mTypeEventSystem.Register<EventA>(OnEventA);
        _mTypeEventSystem.Register<EventB>(b =>
        {
            Debug.Log("OnEventB:" + b.ParamB);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        _mTypeEventSystem.Register<IEventGroup>(e =>
        {
            Debug.Log(e.GetType());
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnEventA(EventA obj)
    {
        Debug.Log("OnEventA");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mTypeEventSystem.Send<EventA>();
        }

        if (Input.GetMouseButtonDown(1))
        {
            _mTypeEventSystem.Send(new EventB()
            {
                ParamB = 123
            });
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _mTypeEventSystem.Send<IEventGroup>(new EventC());
            _mTypeEventSystem.Send<IEventGroup>(new EventD());
        }
    }

    private void OnDestroy()
    {
        _mTypeEventSystem.UnRegister<EventA>(OnEventA);
    }
}
