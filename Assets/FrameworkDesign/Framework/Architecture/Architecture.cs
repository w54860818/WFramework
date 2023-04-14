using System;
using System.Collections.Generic;

namespace FrameworkDesign
{
    public interface IArchitecture
    {
        /// <summary>
        /// model注册
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterModel<T>(T model) where T : IModel;
        /// <summary>
        /// 工具注册
        /// </summary>
        /// <param name="utility"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterUtility<T>(T utility) where T : IUtility;

        /// <summary>
        /// 系统注册
        /// </summary>
        /// <param name="system"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterSystem<T>(T system) where T : ISystem;

        T GetModel<T>() where T :class, IModel;
        T GetUtility<T>() where T: class, IUtility;

        T GetSystem<T>() where T : class, ISystem;

        void SendCommand<T>() where T : IFCommand, new();
        void SendCommand<T>(T command) where T : IFCommand;
        
        void SendEvent<T>() where T : new ();
        void SendEvent<T>(T e);

        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool _mInited = false;

        private static T _mArchitecture;

        /// <summary>
        /// 类似单例的用法
        /// </summary>
        public static IArchitecture Interface
        {
            get
            {
                if (_mArchitecture == null)
                {
                    MakeSureArchitecture();
                }

                return _mArchitecture;
            }
        }

        public static Action<T> OnRegisterPatch = architecture => { };
        
        static void MakeSureArchitecture()
        {
            if (_mArchitecture == null)
            {
                _mArchitecture = new T();
                _mArchitecture.Init();

                OnRegisterPatch?.Invoke(_mArchitecture);

                //model层初始化
                foreach (var model in _mArchitecture.mModels)
                {
                    model.Init();
                }
                _mArchitecture.mModels.Clear();
                
                //system层初始化
                foreach (var system in _mArchitecture.mSystems)
                {
                    system.Init();
                }
                _mArchitecture.mSystems.Clear();
                
                _mArchitecture._mInited = true;
            }
        }

        /// <summary>
        /// 向框架注册模块
        /// </summary>
        protected abstract void Init();



    #region 注册方法 Register
    
        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();

        private IOCContainer _mContainer = new IOCContainer();

        public static void Register<T>(T instance)
        {
            MakeSureArchitecture();
            _mArchitecture._mContainer.Register<T>(instance);
        }

        public void RegisterModel<T>(T model) where T : IModel
        {
            model.SetArchitecture(this);
            _mContainer.Register<T>(model);

            if (!_mInited)
            {
                mModels.Add(model);
            }
            else
            {
                model.Init();
            }
            
        }

        public void RegisterUtility<T>(T utility) where T : IUtility
        {
            _mArchitecture._mContainer.Register<T>(utility);
        }

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.SetArchitecture(this);
            _mContainer.Register<T>(system);

            if (!_mInited)
            {
                mSystems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

    #endregion

    #region 获取方法 Get
    
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return _mArchitecture._mContainer.Get<T>();
        }
        
        public T GetModel<T>() where T : class, IModel
        {
            return _mContainer.Get<T>();
        }

        public T GetUtility<T>() where T : class, IUtility
        {
            return _mContainer.Get<T>();
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return _mContainer.Get<T>();
        }

    #endregion
        
    #region 命令模式 Command

        public void SendCommand<T>() where T : IFCommand, new()
                {
                    var command = new T();
                    command.SetArchitecture(this);
                    command.Execute();
                    command.SetArchitecture(null);
                }
        
                public void SendCommand<T>(T command) where T : IFCommand
                {
                    command.SetArchitecture(this);
                    command.Execute();
                    command.SetArchitecture(null);
                }

    #endregion
        

    #region 事件系统 EventSystem

        private ITypeEventSystem _mTypeEventSystem = new TypeEventSystem();
        
        public void SendEvent<T>() where T : new()
        {
            _mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            _mTypeEventSystem.Send(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return _mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            _mTypeEventSystem.UnRegister<T>(onEvent);
        }

    #endregion

       
    }
}