/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// This handler of controller.
    /// </summary>
    [System.Serializable]
    public class ControllerEventHandler : MonoBehaviour
    {
        [SerializeField]
        protected Dictionary<string, UnityEvent>
         mEventDictionary = new Dictionary<string, UnityEvent>();

        [SerializeField]
        protected string[] mEventNames = new string[0];

        protected List<ReflectiveValue<bool>> mEventWatcher = new List<ReflectiveValue<bool>>();

        [SerializeField]
        protected UnityEvent mUpdateing;

        public void AddEvent ( string eventname, UnityAction cb )
        {
            var value = mEventWatcher.Find( watcher => watcher.mValueName == eventname );
            if (value == null)
            {
                value = new ReflectiveValue<bool>().SetName( eventname );
                mEventWatcher.Add( value );
            }
            value.AddCallBack( ( name, old, now ) => cb.Invoke() );
        }

        /// <summary>
        /// this process assign to reflection value from event dictionary
        /// </summary>
        private void Awake ( )
        {
            foreach (var elm in mEventDictionary)
            {
                AddEvent( elm.Key, ( ) => elm.Value.Invoke() );
            }
        }

        private void Update ( )
        {
            mUpdateing.Invoke();
        }
    }
}
