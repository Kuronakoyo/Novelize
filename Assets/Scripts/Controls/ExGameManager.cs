using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExGameManager : MonoBehaviour
{
    //  親のオブジェクト（MessageWindow）
    [SerializeField]
    private Transform parent = null;

    //  一文字表示のためのプレハブ
    [SerializeField]
    private GameObject onceTextPrefab = null;

    //  文字の表示速度
    [SerializeField]
    private float fontStepTimer = 0.1f;

    //  表示開始座標
    [SerializeField]
    private Vector2 startPosition = new Vector2(-870, -450);

    //  横の文字数
    [SerializeField]
    private int wideStringLengthMax = 30;

    //  文字の間隔
    [SerializeField]
    private float widePadding = 56;

    //  行の間隔
    [SerializeField]
    private float heightPadding = 66;

    //  先頭からの文字数
    private int currentPosition = 0;

    //  表示するメッセージオブジェクトを保持するためのリスト配列
    private List<GameObject> messageFonts = new List<GameObject>();
    //  表示するテキストデータ
    private string scenarioText = "祇園精舎の鐘の声、諸行無常の響きあり。沙羅双樹の花の色、盛者必衰の理をあらはす。奢れる人も久からず、ただ春の夜の夢のごとし。猛き者も遂にはほろびぬ、偏ひとへに風の前の塵におなじ。";
 
    //  文字表示中フラグ
    private bool isDisplayFont = false;

    //  表示終了フラグ
    private bool isDisplayEnd = false;

    // Update is called once per frame
    void Update()
    {
        //  文字表示中ではなく、マウス左クリックしていた場合
        if(Input.GetMouseButtonDown(0) && !isDisplayFont && !isDisplayEnd)
        {
            //  文字表示中モードに設定
            isDisplayFont = true;
            //  文字表示
            StartCoroutine(FontDisp());
        }
        //  すべての表示が終わって、マウス右クリックしていた場合
        if(Input.GetMouseButtonDown(1) && isDisplayFont && isDisplayEnd)
        {
            //  表示中モードを解除
            isDisplayFont = false;
            //  すべての文字オブジェクトの重力を有効にする
            messageFonts.ForEach(msg => msg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic);
            //  一定時間後にフェードアウトするように設定する
            messageFonts.ForEach(msg => msg.GetComponent<OnceTextView>().FadeOutText(5));
        }
    }

    /// <summary>
    /// 文字表示のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator FontDisp()
    {
        //  メッセージの最後まで表示していなければ文字表示を行う
        while(scenarioText.Length > currentPosition)
        {
            //  現在のポジションから一文字取り出す
            var msg = scenarioText.Substring(currentPosition, 1);
            //  所定の座標にテキストオブジェクトを生成して文字を表示する
            SetFont(msg, GetDispPosition());
            //  現在のポジションを更新する
            currentPosition++;
            //  一定時間待つ
            yield return new WaitForSeconds(fontStepTimer);
        }
        //  すべて表示終了フラグを立てる
        isDisplayEnd = true;
    }




    /// <summary>
    /// 文字生成及び表示処理
    /// </summary>
    /// <param name="msg">表示する文字</param>
    /// <param name="pos">表示する座標</param>
    private void SetFont(string msg, Vector2 pos)
    {
        //  1文字表示するためのオブジェクトをスクリーンに生成する
        //  （ヒエラルキーとして親を messageWindow に設定する）
        var onceMessageFont = Instantiate(onceTextPrefab, parent);
        //  生成したオブジェクトを指定された座標に配置する
        onceMessageFont.GetComponent<RectTransform>().localPosition = pos;
        //  生成したテキストオブジェクトに1文字表示する
        onceMessageFont.GetComponent<Text>().text = msg;
        //  文字をフェードインさせる
        onceMessageFont.GetComponent<OneTextView>().FadeInOnceText();
        //  物理挙動を制御するためにリストに登録する
        messageFonts.Add(onceMessageFont);
    }

    /// <summary>
    /// 現在の文字から表示座標を取得
    /// </summary>
    /// <returns>表示座標</returns>
    private Vector2 GetDispPosition()
    {
        return new Vector2(startPosition.x + (currentPosition % wideStringLengthMax) * widePadding,
                        startPosition.y - (currentPosition / wideStringLengthMax) * heightPadding);
    }
}
