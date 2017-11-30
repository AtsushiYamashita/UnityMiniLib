using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AY_Util;

[System.Serializable]
public class DebugInitableNode : BehaviorNode
{
    /// <summary>
    /// When you need debug init then into true.
    /// </summary>
    [SerializeField, TooltipAttribute("デバッグ時trueでmDebugInitを最初に実行する")]
    private bool mDebugInitFlg = false;

    /// <summary>
    /// When you want to start in debug from this node.
    /// Then this event setup for that situation.
    /// </summary>
    [SerializeField]
    private BehaviorProcess mDebugInit;

    virtual protected void ProcessDebugInit() {
        mDebugInit.Invoke(this);
    }

    public void SetDebugInit(bool flg)
    {
        mDebugInitFlg = flg;
    }

    /// <summary>
    /// this virtual function for extends.
    /// </summary>
    override protected void ProcessStart()
    {
        if (mDebugInitFlg) { ProcessDebugInit(); }
        base.ProcessStart();
    }

    /// <summary>
    /// This state update.
    /// </summary>
    override protected void ProcessUpdate()
    {
        base.ProcessUpdate();
    }

}

