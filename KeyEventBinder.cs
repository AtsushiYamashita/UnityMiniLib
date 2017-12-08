using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UnityEventとKeyEventを繋ぎこむ
/// </summary>
public class KeyEventBinder : MonoBehaviour
{
    /// <summary>
    /// Inspectorで選択肢を表示するための定数
    /// </summary>
    private readonly Func<KeyCode, bool>[] mEventFuncs = {
        Input.GetKeyDown,
        Input.GetKey,
        Input.GetKeyUp,
    };

    /// <summary>
    /// Inspectorで選択肢を表示するための定数
    /// </summary>
    public enum EventType
    {
        GetKeyDown, GetKey, GetKeyUp
    }

    [SerializeField]
    EventSet[] mEvents = new EventSet[0];

    /// <summary>
    /// キーイベントとキーが合致したら、紐づいた関数を実行する。
    /// 全ての項目を確認するループを回すため、
    /// 出来る限りactiveにするタイミングは絞ったほうが良い。
    /// </summary>
    void Update()
    {
        foreach (var eve in mEvents)
        {
            var index = eve.GetEventType();
            var isDo = mEventFuncs[index](eve.mCode);
            if (isDo == false) { continue; }
            eve.mEvent.Invoke();
        }
    }

    /// <summary>
    /// キーイベントとキーを、イベント関数と紐づけるためのクラス。
    /// </summary>
    [System.Serializable]
    public class EventSet
    {
        [SerializeField]
        EventType mType;

        [SerializeField]
        public KeyCode mCode;

        [SerializeField]
        public UnityEvent mEvent;

        /// <summary>
        /// イベント定数を数値として返す。
        /// </summary>
        /// <returns></returns>
        public int GetEventType() { return (int)mType; }
    }
}
