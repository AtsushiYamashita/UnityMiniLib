using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using AY_Util;

/// <summary>
/// Audio source を持っているオブジェクトから、
/// 音声関係のイベントを発信、利用するためのコンポネント
/// </summary>
public class AudioEvent : MonoBehaviour
{
    private AudioSource mAudioSource;
    private bool mPlayState = false;

    [SerializeField]
    private List<UnityEventSet> mEvents = new List<UnityEventSet>();

    public enum Event
    {
        NotPlay, PlayStart, PlayUpdate, PlayEnd
    }

    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(mAudioSource);
    }

    private Event GetState()
    {
        var state = mAudioSource.isPlaying;
        if (!mPlayState && !state) { return Event.NotPlay; }
        if (!mPlayState && state) { return Event.PlayStart; }
        if (mPlayState && state) { return Event.PlayUpdate; }
        return Event.PlayEnd;
    }

    /// <summary>
    /// 常にUpdateを呼び出し、配列の中を洗っているので、
    /// 必要でない時はactiveを切っておくべき。
    /// </summary>
    void Update()
    {
        var state = GetState();
        mPlayState = mAudioSource.isPlaying;
        foreach (var eve in mEvents)
        {
            if (eve.GetFirst() != state) { continue; }
            eve.GetSecond().Invoke();
        }
    }

    /// <summary>
    /// 直列化用クラス。
    /// </summary>
    [Serializable]
    public class UnityEventSet : Pair<Event, UnityEvent> { }
}
