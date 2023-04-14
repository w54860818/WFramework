﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public struct TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEventSystem TypeEventSystem;
        public Action<T> OnEvent;
        public void UnRegister()
        {
            TypeEventSystem.UnRegister<T>(OnEvent);

            TypeEventSystem = null;

            OnEvent = null;
        }
    }

    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> _mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            _mUnRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in _mUnRegisters)
            {
                unRegister.UnRegister();
            }
            
            _mUnRegisters.Clear();
        }
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }

            trigger.AddUnRegister(unRegister);
        }
    }
    
    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {
            
        }

        public class Registerations<T> : IRegistrations
        {
            public Action<T> OnEvent = (e) => { };
        }

        private Dictionary<Type, IRegistrations> mEventRegistration = new Dictionary<Type, IRegistrations>();

        public void Send<T>() where T : new()
        {
            var e = new T();
            Send(e);
        }

        public void Send<T>(T e)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registerations<T>).OnEvent(e);
            }
        }

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                
            }
            else
            {
                registrations = new Registerations<T>();
                mEventRegistration.Add(type, registrations);
            }

            (registrations as Registerations<T>).OnEvent += onEvent;

            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                TypeEventSystem = this
            };
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registerations<T>).OnEvent -= onEvent;
            }
        }
    }
}