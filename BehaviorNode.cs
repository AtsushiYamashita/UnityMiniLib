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
    /// Behavior tree's node.
    /// This node will do three process when be enable.
    /// </summary>
    [System.Serializable]
    public class BehaviorNode : MonoBehaviour
    {
        /// <summary>
        /// node name.
        /// </summary>
        [SerializeField]
        private string mNodeName = "";

        /// <summary>
        /// process in this node.
        /// </summary>
        private ProcessEventWrapper[] mProcesses = new ProcessEventWrapper[( int )Process.SIZE];

        /// <summary>
        /// scene event type
        /// </summary>
        public enum Process
        {
            START, UPDATE, CLOSE, SIZE
        }

        /// <summary>
        /// get process instance.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public ProcessEventWrapper GetProcessInstance ( Process proc )
        {
            return mProcesses[( int )proc];
        }

        /// <summary>
        /// list up enable and living children.
        /// </summary>
        /// <returns></returns>
        public List<BehaviorNode> EnableChildren ( )
        {
            var list = new List<BehaviorNode>();
            var count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var ch = transform.GetChild( i );
                if (ch.gameObject.activeSelf == false) { continue; }

                var bn = ch.GetComponent<BehaviorNode>();
                if (bn == null) { continue; }
                if (bn.enabled == false) { continue; }

                list.Add( bn );
                list.AddRange( bn.EnableChildren() );
            }
            return list;
        }

        /// <summary>
        /// this virtual function for extends.
        /// </summary>
        virtual protected void ProcessInitialize ( ) { }

        /// <summary>
        /// This state update.
        /// </summary>
        virtual protected void ProcessUpdate ( )
        {
            foreach (var proc in mProcesses) { proc.Action( this ); }
        }

        /// <summary>
        /// this function call process initialize for extends.
        /// </summary>
        private void Start ( )
        {
            ProcessInitialize();
        }

        /// <summary>
        /// MonoBehaviour's update.
        /// and this node's update.
        /// </summary>
        private void Update ( )
        {
            ProcessUpdate();
        }

        /// <summary>
        /// Wrapper class of unity event.
        /// </summary>
        public class BehaviourEvent : UnityEvent<BehaviorNode> { }

        /// <summary>
        /// Behavior state event wrapper.
        /// </summary>
        [System.Serializable]
        public class ProcessEventWrapper
        {
            /// <summary>
            /// unity event wrapper of behavior.
            /// </summary>
            [SerializeField]
            private BehaviourEvent mEvent = new BehaviourEvent();

            /// <summary>
            /// doable judge function.
            /// </summary>
            private Judge mJudge = ( node ) => { return true; };

            /// <summary>
            /// doable judge function delegate.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public delegate bool Judge ( BehaviorNode obj );

            /// <summary>
            /// state action.
            /// </summary>
            /// <param name="node"></param>
            public void Action ( BehaviorNode node )
            {
                if (IsDoable( node ) == false) { return; }
                mEvent.Invoke( node );
            }

            /// <summary>
            /// set doable judge function.
            /// </summary>
            /// <param name="jucge"></param>
            /// <returns></returns>
            public ProcessEventWrapper SetJudge ( Judge jucge )
            {
                mJudge = jucge; return this;
            }

            /// <summary>
            /// is this action doable check.
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            protected bool IsDoable ( BehaviorNode node )
            {
                return mJudge( node );
            }
        }
    }
}
