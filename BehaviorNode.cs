/// <summary>
/// This AY Utility sets.
/// <author>Atsushi. Yamashita.</author>
/// </summary>
namespace AY_Util
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.Events;

    /// <summary>
    /// Behavior tree's node.
    /// This node will do three process when be enable.
    /// </summary>
    [System.Serializable]
    public class BehaviorNode : MonoBehaviour
    {
        /// <summary>
        /// this node state
        /// </summary>
        [SerializeField]
        private ProcessState mState;

        /// <summary>
        /// close process.
        /// </summary>
        [SerializeField]
        private BehaviorProcess mClose;

        /// <summary>
        /// process in this node.
        /// </summary>
        private ProcessDictionary mProcesses = new ProcessDictionary();

        /// <summary>
        /// start process.
        /// </summary>
        [SerializeField]
        private BehaviorProcess mStart;

        /// <summary>
        /// update process.
        /// </summary>
        [SerializeField]
        private BehaviorProcess mUpdate;


        /// <summary>
        /// this node state setting
        /// </summary>
        public void SetState(string state)
        {
            Debug.Log("StateChange : @" + name + " /to " + state);
            mState = EnumUtil<ProcessState>.StringTo(state);
        }

        /// <summary>
        /// scene event type
        /// </summary>
        public enum ProcessState
        {
            START, UPDATE, CLOSE, SIZE, NULL
        }

        /// <summary>
        /// list up enable and living children.
        /// </summary>
        /// <returns></returns>
        public List<BehaviorNode> EnableChildren()
        {
            var objects = GameObjectUtil.GetChildren(gameObject);
            var activeObjects = objects
                .Where((elm) => { return elm.activeSelf; });
            var activenodes = activeObjects
                .Select((elm) => { return elm.GetComponent<BehaviorNode>(); })
                .Where((elm) => { return elm != null; })
                .Where((elm) => { return elm.enabled; });
            var activeAll = activenodes
                .SelectMany((elm) =>
                {
                    var ret = elm.EnableChildren();
                    ret.Add(elm);
                    return ret;
                });
            return activeAll as List<BehaviorNode>;
        }

        /// <summary>
        /// get process instance.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public BehaviorProcess GetProcessInstance(ProcessState proc)
        {
            return mProcesses[proc];
        }

        /// <summary>
        /// this virtual function for extends.
        /// </summary>
        virtual protected void ProcessStart()
        {
            Debug.Log(this.name + "ProcessStart");
            mStart.Action(this);
        }

        /// <summary>
        /// This state update.
        /// </summary>
        virtual protected void ProcessUpdate()
        {
            if (mState == ProcessState.START)
            {
                ProcessStart();
                return;
            }
            if (mState == ProcessState.UPDATE)
            {
                mUpdate.Action(this);
                return;
            }
            if (mState == ProcessState.CLOSE)
            {
                Debug.Log(name + "ProcessState.CLOSE");
                mClose.Action(this);
                gameObject.SetActive(false);
                mState = ProcessState.START;
            }
        }

        public void PhaseCloseCheck()
        {
            var children = GameObjectUtil.GetChildren(gameObject);
            var doable = children
                .Select((e) => { return e.GetComponent<BehaviorNode>(); })
                .SelectMany((e) => { return e.EnableChildren(); });
            var converted = doable as BehaviorNode;
            if (converted == null) { mState = ProcessState.CLOSE; }
        }


        /// <summary>
        /// this function call process initialize for extends.
        /// </summary>
        private void Start()
        {
        }

        /// <summary>
        /// MonoBehaviour's update.
        /// and this node's update.
        /// </summary>
        private void Update()
        {
            ProcessUpdate();
        }
    }

    /// <summary>
    /// Behavior state event wrapper.
    /// </summary>
    [System.Serializable]
    public class BehaviorProcess : UnityEvent<BehaviorNode>
    {
        /// <summary>
        /// doable judge function.
        /// </summary>
        protected Judge mJudge = null;

        [SerializeField]
        protected bool mJudgeFlg = true;

        /// <summary>
        /// doable judge function delegate.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public delegate bool Judge(BehaviorNode obj);

        /// <summary>
        /// state action.
        /// </summary>
        /// <param name="node"></param>
        public void Action(BehaviorNode node)
        {
            if (IsDoable(node) == false) { return; }
            Invoke(node);
        }

        /// <summary>
        /// this process is use or is not use.
        /// </summary>
        /// <param name="flg"></param>
        public void SetFlg(bool flg)
        {
            mJudgeFlg = flg;
        }

        /// <summary>
        /// set doable judge function.
        /// </summary>
        /// <param name="judge"></param>
        /// <returns></returns>
        public BehaviorProcess SetJudge(Judge judge)
        {
            mJudge = judge;
            return this;
        }

        /// <summary>
        /// is this action doable check.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected bool IsDoable(BehaviorNode node)
        {
            if (mJudge == null) { mJudge = (none) => mJudgeFlg; }
            return mJudge(node);
        }
    }

    /// <summary>
    /// This class use for short cording and readability.
    /// </summary>
    [System.Serializable]
    public class ProcessDictionary : Dictionary<BehaviorNode.ProcessState, BehaviorProcess>
    {
        /// <summary>
        /// This update the add process for chain method.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>return this instance for chain.</returns>
        public ProcessDictionary AddChain(BehaviorNode.ProcessState a, BehaviorProcess b)
        {
            Add(a, b);
            return this;
        }
    }
}
