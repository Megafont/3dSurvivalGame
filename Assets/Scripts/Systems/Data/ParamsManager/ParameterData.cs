using System;

namespace SurvivalGame.Systems.Data.ParamsManager
{
    public class ParameterData<T>
    {
        public event EventHandler<ParameterData_ValueChangedEventArgs<T>> ValueChanged;
        
        private T _Value;

        
        public ParameterData(ParameterIDs id, T value)
        {
            ID = id;
            Value = value;
        }

        

        public ParameterIDs ID { get; private set; }

        public T Value
        {
            get => _Value;
            set
            {
                T oldValue = _Value;
                _Value = value;
                ValueChanged?.Invoke(this, new ParameterData_ValueChangedEventArgs<T>(_Value, oldValue));
            }
        }        
    }

    public class ParameterData_ValueChangedEventArgs<T> : EventArgs
    {
        public T NewValue { get; private set; }
        public T OldValue { get; private set; }

        public ParameterData_ValueChangedEventArgs(T newValue, T oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }

}
    
    