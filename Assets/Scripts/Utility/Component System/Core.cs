using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Logger;

namespace Utility.Component_System
{
    public sealed class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

        public UnityLogger Logger { get; } = new UnityLogger();

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
            if (_coreComponents.Contains(component))
            {
                Logger.Log($"{component.GetType()} is already a component in Core. Will not add this component to avoid duplication.");
                return;
            }
            
            _coreComponents.Add(component);
        }

        public T GetCoreComponent<T>() 
            where T : CoreComponent
        {
            T component = _coreComponents.OfType<T>().FirstOrDefault();

            if (component == null)
            {
                Logger.LogWarning($"{typeof(T)} not found on {transform.name}");
            }

            return component;
        }
    }
}