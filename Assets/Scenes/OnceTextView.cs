using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnceTextView : MonoBehaviour
{
    [SerializeField]
    private float FadeInTimer = 0.1f;
    [SerializeField]
    private float FadeOutTimer = 0.3f;
    [SerializeField]
    private CanvasGroup onceCanvasGroup = null;
    // Start is called before the first frame update
    
    //<summary>
    //</summary>
    public void FadeInOneceText()
    {
        //透明度を0にする
        onceCanvasGroup.alpha = 0.0f;
        //フェードインの開始
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        // オブジェクトの透明度が1（不透明）になるまで処理を行う
        while (onceCanvasGroup.alpha < 1.0f)
        {
            //　所定時間をかけて透明度を加算する
            onceCanvasGroup.alpha += (1.0f / FadeInTimer)*Time.deltaTime;
            //透明度のMAXが1なのでそれを超えたら1になるように修正する

            if (onceCanvasGroup.alpha >= 1.0f)
                onceCanvasGroup.alpha = 1.0f;
            //１フレーム待機する
            yield return null;

        }
    }
    public void FadeOutOneceText(float delayTime = 0.0f)
    {
        onceCanvasGroup.alpha = 0.0f;
        StartCoroutine(FadeOut(delayTime));
    }

    private IEnumerator FadeOut(float delayTime)
    {
        //一定時間を待つ
        yield return new WaitForSeconds(delayTime);
        // オブジェクトの透明度が0（透明）になるまで処理を行う
        while (onceCanvasGroup.alpha < 0.0f)
        {
            //　所定時間をかけて透明度を加算する
            onceCanvasGroup.alpha -=( 1.0f / FadeOutTimer)*Time.deltaTime;
            //透明度のMAXが1なのでそれを超えたら1になるように修正する

            if (onceCanvasGroup.alpha <= 0.0f)
                onceCanvasGroup.alpha = 0.0f;
            //１フレーム待機する
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
