﻿
namespace KFramework.Common
{
    public delegate void OnSingletonInitializeEventHandler();
    public delegate void OnSingletonUninitializeEventHandler();

    public class SingletonInterface
    {
        public virtual void Launch() { }
        public virtual void SingletonInterfaceOnInitialize() { }
        public virtual void SingletonInterfaceOnUninitialize() { }
    }

    public class SingletonBase : SingletonInterface
    {
        public event OnSingletonInitializeEventHandler OnInitializeHandler;
        public event OnSingletonUninitializeEventHandler OnUninitializeHandler;

        private bool isInit = false;
        private bool isUninit = false;

        public sealed override void Launch() { }

        public sealed override void SingletonInterfaceOnInitialize()
        {
            if (!isInit)
            {
                isInit = true;
                OnInitialize();
                OnInitializeHandler?.Invoke();
            }
        }

        public sealed override void SingletonInterfaceOnUninitialize()
        {
            if (!isUninit)
            {
                isUninit = true;
                OnUninitializeHandler?.Invoke();
                OnUninitialize();
            }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnUninitialize() { }
        
        protected virtual void Dispose(){}
    }
}
