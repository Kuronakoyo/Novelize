using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExGameManeger : MonoBehaviour
{
    // 親のオブジェクト(Messagewindow)
    [SerializeField]
    private Transform parent = null;
    //　文字の表示速度
    [SerializeField]
    private float fontstepTimer = 0.1f;

    //一文字表示のためのプレハブ
    [SerializeField]
    private GameObject onceTextPrefab = null;
    //表示開始座標
    [SerializeField]
    private Vector2 startPosition = new Vector2(-870, -450);
    // 横の文字数
    [SerializeField]
    private int wideStringLengthMax = 30;
    // 文字の間隔
    [SerializeField]
    private float widePadding = 56;
    //行の間隔
    [SerializeField]
    private float heightPadding = 66;
    // 先頭からの文字数
    private int currentPosition = 0;

    //表示するメッセージオブジェクトを保持するためのリスト配列
    private List<GameObject> messageFonts = new List<GameObject>();
    //表示するテキストデータ
    private string scenarioText = "あいうえお書きくださいああああああああああああああああああああああ";
    //　文字表示中フラグ
    private bool isDisplayFont = false;
    // 文字表示終了フラグ
    private bool isDisplayEnd = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDisplayFont && !isDisplayEnd)
        {
            // 文字表示中モードに設定
            isDisplayFont = true;
            // 文字表示
            StartCoroutine(FontDisp());
        }
        //　全ての表示が終わって、マウス右クリックしていた場合
        if (Input.GetMouseButtonDown(1) && isDisplayFont && isDisplayEnd)
        {
            // 表示中モードを解除
            isDisplayFont = false;
            // すべての文字オブジェクトの重力を有効にする
            messageFonts.ForEach(msg => msg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic);
            // 一定時間後にフェードアウトするように設定する
            messageFonts.ForEach(msg => msg.GetComponent<OnceTextView>().FadeOutText(GetRandomTime(5, 10),DestroyOneText));

        }
    }
    IEnumerator FontDisp()
    {
        //　メッセージの最後まで表示していなければ文字表示を行う
        while (scenarioText.Length > currentPosition)
        {
            //　現在のポジションから一文字取り出す
            var msg = scenarioText.Substring(currentPosition, 1);
            // 既定の座標にテキストオブジェクトを生成して文字を表示する
            SetFont(msg, GetDispPosition());
            // 現在のポジションを更新する
            currentPosition++;
            // 一定時間を待つ
            yield return new WaitForSeconds(fontstepTimer);
        }
        //　すべて表示終了フラグを立てる
        isDisplayEnd = true;
    }

    /// <summary>
    /// 文字生成及び表示処理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="pos"></param>
    private void SetFont(string msg, Vector2 pos)
    {
        var onceMessageFont = Instantiate(onceTextPrefab, parent);
        onceMessageFont.GetComponent<RectTransform>().localPosition = pos;
        onceMessageFont.GetComponent<Text>().text = msg;
        onceMessageFont.GetComponent<OnceTextView>().FadeInOneceText();
        messageFonts.Add(onceMessageFont);
    }
    private Vector2 GetDispPosition()
    {
        return new Vector2(startPosition.x + (currentPosition % wideStringLengthMax) * widePadding,
            startPosition.y - (currentPosition / wideStringLengthMax) * heightPadding);
    }

    ///<summary>
    ///指定されたゲームオブジェクトを削除するコールバック関数
    ///</summary>
    ///<param name="gobj"></param>
    private void DestroyOneText(GameObject gobj)
    {
        Destroy(gobj);
    }
    ///<summary>
    ///ランダムの時間習得
    ///</summary>
    ///<param name="min">最小値</param>
    ///<param name="max">最大値</param>
    private float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}