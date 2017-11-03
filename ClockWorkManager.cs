/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.Events;

    /// <summary>
    /// This class express clock event.
    /// </summary>
    public class ClockEvent : UnityEvent<ClockWorkManager> { };

    /// <summary>
    /// This class manage time event of two type.
    /// One, every frame called.
    /// Two, that is called in target time.
    /// </summary>
    public class ClockWorkManager : ManagerHandler.IManager
    {
        /// <summary>
        /// this member contain events of called in every frame.
        /// </summary>
        [SerializeField]
        private ClockEvent mEvents = new ClockEvent();

        /// <summary>
        /// this member contain event of called when target time.
        /// </summary>
        [SerializeField]
        private List<MineAction> mMineActions = new List<MineAction>();

        /// <summary>
        /// named time memory.
        /// </summary>
        [SerializeField]
        private Dictionary<string, float> mTimeMemory = new Dictionary<string, float>();

        /// <summary>
        /// this process assign process of every called
        /// </summary>
        /// <param name="proc"></param>
        public void AddEvent ( UnityAction<ClockWorkManager> proc )
        {
            mEvents.AddListener( proc );
        }

        /// <summary>
        /// this process add memory named time value.
        /// </summary>
        /// <param name="name"></param>
        public void AddMemory ( string name )
        {
            var contain = mTimeMemory.ContainsKey( name );
            var message = name + " is not include this clock work memory.";
            Assert.IsTrue( contain, message );
            if (contain) { return; }

            mTimeMemory.Add( name, Time.time );
        }

        /// <summary>
        /// This process assign mine event with fire time.
        /// </summary>
        /// <param name="time">fire time.</param>
        /// <param name="proc">fire process.</param>
        public void AddMineEvent ( float time, UnityAction<ClockWorkManager> proc )
        {
            var ma = new MineAction().SetMine( time, proc );
            mMineActions.Add( ma );
            mMineActions.Sort( ( a, b ) =>
            {
                return ( int )( a.GetFirst() - b.GetFirst() );
            } );
        }

        /// <summary>
        /// this written for implementing interface.
        /// </summary>
        public void IInitialize ( )
        {
        }

        /// <summary>
        /// event and mine invoke in every frame.
        /// </summary>
        public void IUpdating ( )
        {
            mEvents.Invoke( this );
            var called = AllMineCall();
            FiredMineRemove( called );
        }

        /// <summary>
        ///
        /// </summary>
        private List<MineAction> AllMineCall ( )
        {
            List<MineAction> ret = null;

            foreach (var mine in mMineActions)
            {
                var time = mine.GetFirst();
                if (time < Time.time) { break; }
                var action = mine.GetSecond();
                action.Invoke( this );
                ret = ret != null ? ret : new List<MineAction>();
                ret.Add( mine );
            }
            return ret;
        }

        /// <summary>
        /// This process will remove all fired mine.
        /// </summary>
        private void FiredMineRemove ( List<MineAction> list )
        {
            if (list == null) { return; }
            foreach (var target in list)
            {
                mMineActions.RemoveAll( e =>
                {
                    return e == target;
                } );
            }
        }
    }

    /// <summary>
    /// This class made for short cord.
    /// Mine is fired in target time overred.
    /// </summary>
    public class MineAction : Pair<float, UnityAction<ClockWorkManager>>
    {
        /// <summary>
        /// setter.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns>return this instance for chain.</returns>
        public MineAction SetMine ( float obj1, UnityAction<ClockWorkManager> obj2 )
        {
            mFirst = obj1;
            mSecond = obj2;
            return this;
        }
    }
}
