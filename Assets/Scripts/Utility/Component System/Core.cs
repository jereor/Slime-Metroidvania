using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Component_System
{
    public sealed class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

        private void Update()
        {
            LogicUpdate();
        }

        public void LogicUpdate()
        {
            foreach (CoreComponent coreComponent in _coreComponents)
            {
                coreComponent.LogicUpdate();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            if (_coreComponents.Contains(component) == false)
            {
                _coreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() 
            where T : CoreComponent
        {
            T component = _coreComponents.OfType<T>().FirstOrDefault();

            if (component == null)
            {
                Debug.LogWarning($"{typeof(T)} not found on {transform.name}");
            }

            return component;
        }
    }
}