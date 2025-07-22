using System.Collections.Generic;
using UnityEngine;


namespace SurvivalGame.Systems.Data.ParamsManager
{
    /// <summary>
    /// This class is a wrapper around the standard Dictionary class.
    /// It conceals the presence of one of the generic type parameters of the internal dictionary.
    /// This makes it possible for the ParametersManager to define its main dictionary such that it
    /// can contain dynamically allocated dictionaries for each data type as needed.
    /// </summary>
    /// <typeparam name="T">The data type of the parameters this dictionary will store.</typeparam>
    public class ParametersDictionary<T> : IParametersDictionary
    {
        private readonly Dictionary<ParameterIDs, ParameterData<T>> _dict = new();

        
        public void Add(ParameterIDs id, object value)
        {
            //Debug.Log($"id {id}    {typeof(T).Name}, actual value type: {value?.GetType()}");
            _dict[id] = new ParameterData<T>(id, (T) value);
        }

        public ParameterData<T> GetParameterData(ParameterIDs id)
        {
            bool result = _dict.TryGetValue(id, out var data);
            if (result)
                return data;

            return null;
        }

        public bool TryGetValue(ParameterIDs id, out ParameterData<T> data)
        {
            data = null;
            
            return _dict.TryGetValue(id, out data);
        }
    }
}