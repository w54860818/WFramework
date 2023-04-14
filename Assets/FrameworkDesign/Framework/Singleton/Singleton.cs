using System;
using System.Reflection;

namespace FrameworkDesign
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T _mInstance;

        public static T Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    var type = typeof(T);
                    //获得所有的构造函数
                    var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    //寻找不需要传参的构造函数
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                    if (ctor == null)
                    {
                        throw new Exception("Non Public Constructor Not Found in " + type.Name);
                    }

                    _mInstance = ctor.Invoke(null) as T;
                }
                
                return _mInstance;
            }
        }
    }
}