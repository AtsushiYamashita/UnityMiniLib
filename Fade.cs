using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

/// <summary>
/// カメラの前に置かれたスプライトについて、
/// 指定された時間での
/// フェードイン、フェードアウトを行う
/// </summary>
public class Fade : MonoBehaviour
{
    private SpriteRenderer mSprite;
    private float mFadeStart = 0;
    private FadeType mFadeType = FadeType.NONE;
    private float mFadeEnd = 0;

    [SerializeField]
    private float mFadeTime = 0;

    [SerializeField]
    UnityEvent mFadeEndEvent = new UnityEvent();

    [SerializeField]
    UnityEvent mFadeStartEvent = new UnityEvent();

    /// <summary>
    /// フェードの種類
    /// </summary>
    public enum FadeType
    {
        NONE, FADE_OUT, FADE_IN
    }

    private void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(mSprite);
        mFadeStartEvent.Invoke();
    }

    public void FadeIn() { FadeStart(FadeType.FADE_IN); }

    public void FadeOut() { FadeStart(FadeType.FADE_OUT); }

    /// <summary>
    /// この関数を呼び出すと、typeに合わせてフェードを開始する
    /// </summary>
    /// <param name="type">フェードの方向性</param>
    public void FadeStart(FadeType type)
    {
        Assert.IsTrue(mFadeType == FadeType.NONE);
        mFadeStart = Time.time;
        mFadeType = type;
    }

    private Color CalcFadingColor(float passed, Color baseColor)
    {
        var color = baseColor;
        color.a = (mFadeType == FadeType.FADE_IN ? 1 - passed : passed);
        return color;
    }

    private void Update()
    {
        if (mFadeType == FadeType.NONE) { return; }
        var passed = (Time.time - mFadeStart) / mFadeTime;
        mSprite.color = CalcFadingColor(passed,Color.black);

        if (passed < 1.0f) { return; }
        mFadeType = FadeType.NONE;
        mFadeEndEvent.Invoke();
    }
}
