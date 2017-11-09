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
        private BehaviorProcess mStrt;

        /// <summary>
        /// update process.
        /// </summary>
        [SerializeField]
        private BehaviorProcess mUpdate;

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        public delegate void BehaviourFunc(BehaviorNode node);

        /// <summary>
        /// scene event type
        /// </summary>
        public enum Process
        {
            START, UPDATE, CLOSE, SIZE
        }

        /// <summary>
        /// list up enable and living children.
        /// </summary>
        /// <returns></returns>
        public List<BehaviorNode> EnableChildren()
        {
            var list = new List<BehaviorNode>();
            var count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var ch = transform.GetChild(i);
                if (ch.gameObject.activeSelf == false) { continue; }

                var node = ch.GetComponent<BehaviorNode>();
                var enable = node == null || node.enabled == false;
                if (enable) { continue; }

                list.Add(node);
                list.AddRange(node.EnableChildren());
            }
            return list;
        }

        /// <summary>
        /// get process instance.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public BehaviorProcess GetProcessInstance(Process proc)
        {
            return mProcesses[proc];
        }

        /// <summary>
        /// this virtual function for extends.
        /// </summary>
        virtual protected void ProcessStart() { }

        /// <summary>
        /// This state update.
        /// </summary>
        virtual protected void ProcessUpdate()
        {
            foreach (var proc in mProcesses) { proc.Value.Action(this); }
        }

        /// <summary>
        /// this function call process initialize for extends.
        /// </summary>
        private void Start()
        {
            mProcesses.AddChain(Process.START, mStrt)
                .AddChain(Process.UPDATE, mUpdate)
                .AddChain(Process.CLOSE, mClose);
            mClose.AddListener(node => node.gameObject.SetActive(false));
            mClose.SetFlg(false);
            ProcessStart();
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
            mJudge = judge; return this;
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
    public class ProcessDictionary : Dictionary<BehaviorNode.Process, BehaviorProcess>
    {
        /// <summary>
        /// This update the add process for chain method.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>return this instance for chain.</returns>
        public ProcessDictionary AddChain(BehaviorNode.Process a, BehaviorProcess b)
        {
            Add(a, b);
            return this;
        }
    }
}
