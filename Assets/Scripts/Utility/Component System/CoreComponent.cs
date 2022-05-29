using UnityEngine;

namespace Utility.Component_System
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core Core;
        
        protected virtual void Awake()
        {
            Core = transform.GetComponent<Core>();

            if (Core == null)
            { 
                Debug.LogError("There is no Core on this game object.");
            }
            
            Core.Logger.Log($"Adding {this} to Core.");
            Core.AddComponent(this);
        }

        public virtual void LogicUpdate()
        {
            
        }
    }
}
