using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class MoveEvent : MonoBehaviour
{
    float mUseTime = 0;
    float mStart = -1;
    Rigidbody mRigidbody;

    [SerializeField]
    string mTarget;
    [SerializeField]
    Vector3 mFrom = new Vector3();
    [SerializeField]
    Vector3 mTo = new Vector3();
    [SerializeField]
    UnityEvent mStartEvent = new UnityEvent();
    [SerializeField]
    UnityEvent mUpdateEvent = new UnityEvent();
    [SerializeField]
    UnityEvent mEndEvent = new UnityEvent();

    private void Start()
    {
        var target = GameObject.Find(mTarget);
        mRigidbody = target.GetComponent<Rigidbody>();
        if (mRigidbody != null) { return; }
        mRigidbody = target.AddComponent<Rigidbody>();
        mRigidbody.useGravity = false;
    }

    public void SetFrom(Vector3 vec)
    {
        mFrom = vec;
    }

    public void SetTo(Vector3 vec)
    {
        mTo = vec;
    }

    public void Move(float time)
    {
        mStart = Time.time;
        mUseTime = time;
    }

    private void FixedUpdate()
    {
        if (mStart < 0) { return; }

        var pass = Time.time - mStart;
        pass = (pass > 1) ? 1 : pass;

        var per = pass / mUseTime;
        var vec = mTo - mFrom;
        var pos = Vector3Util.Scale(vec, per);
        mRigidbody.MovePosition(pos);

        if (pass < 1) { return; }
        mStart = -1;
    }
}

