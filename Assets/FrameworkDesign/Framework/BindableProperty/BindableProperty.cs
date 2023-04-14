using System;

namespace FrameworkDesign
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T _mValue = default(T);

        public T Value
        {
            get => _mValue;

            set
            {
                if (value.Equals(_mValue)) return;
                
                _mValue = value;
                OnValueChanged?.Invoke(_mValue);
            }
        }

        public Action<T> OnValueChanged;
    }
}