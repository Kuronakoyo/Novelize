using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class OneTextView : MonoBehaviour
{
    [SerializeField]
    private float fadeInTimer = 0.1f;
    [SerializeField]
    private float fadeOutTimer = 0.3f;
    [SerializeField]
    private CanvasGroup onceCanvasGroup = null;

    private void Awake() 
    {
        onceCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 文字をフェードインさせる
    /// </summary>
    public void FadeInOnceText()
    {
        //  透明度を０にする
        onceCanvasGroup.alpha = 0;
        //  フェードインの開始
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        //  オブジェクトの透明度が１（不透明）になるまで処理を行う
        while(onceCanvasGroup.alpha < 1.0f)
        {
            //  所定時間をかけて透明度を加算する
            onceCanvasGroup.alpha += (1.0f / fadeInTimer) * Time.deltaTime;
            //  透明度のMAXが１なのでそれを超えたら1になるように修正する
            if(onceCanvasGroup.alpha >= 1.0f)
                onceCanvasGroup.alpha = 1.0f;
            //  1フレーム待機する
            yield return null;
        }
    }

    /// <summary>
    /// 文字をフェードアウトさせる
    /// <param name="delaytime">遅延時間</param>
    /// </summary>
    public void FadeOutText(float delayTime = 0.0f)
    {
        //  不透明に設定する
        onceCanvasGroup.alpha = 1.0f;
        //  フェードアウト開始
        StartCoroutine(FadeOut(delayTime));
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="delaytime">遅延時間</param>
    /// <returns></returns>
    private IEnumerator FadeOut(float delayTime)
    {
        //  一定時間待つ
        yield return new WaitForSeconds(delayTime);
        //  オブジェクトの透明度が０（透明）になるまで処理を行う
        while(onceCanvasGroup.alpha > 0.0f)
        {
            //  所定時間をかけて透明度を加算する
            onceCanvasGroup.alpha -= (1.0f / fadeOutTimer) * Time.deltaTime;
            //  透明度のMINが 0 なのでそれを超えたら 0 になるように修正する
            if(onceCanvasGroup.alpha <= 0.0f)
                onceCanvasGroup.alpha = 0.0f;
            //  1フレーム待機する
            yield return null;
        }
    }
}
