using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainControl : MonoBehaviour
{
    //  アドベンチャーテキストエリア
    [SerializeField]
    private Text text = null;
    //  シナリオテキスト
    private string scenarioText = "";


    //  文字の表示数
    private int dispCounter = 0;

    [SerializeField]
    float dispTimeMax = 0.1f;

    [SerializeField]
    private ScenarioDataControl scenarioDataControl = null;

    // 暫定的に停止用フラグを用意する
    private bool isKeyWait = false;
    // 文字列表示中フラグを用意する
    private bool isDisplayActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //  メッセージエリアを初期化
        text.text = "";
        //ファイルをリソースから読み込む
        TextAsset textAsset = Resources.Load("Datas/scenario", typeof(TextAsset)) as TextAsset;
        scenarioText = textAsset.text;
        //シナリオを取り込む
        scenarioDataControl = new ScenarioDataControl();
        scenarioDataControl.InitScenarioData(scenarioText);
       scenarioText = scenarioDataControl.LoadMessage();
     
    }

    // Update is called once per frame
    private void Update()
    {
        if (isKeyWait)
        {
            //  マウスの左クリックチェック
            if (Input.GetMouseButtonDown(0)&& false ==scenarioDataControl.IsEndOfLine() )
            {
                isKeyWait = false;
                // 前に表示した文章を消す
                text.text = "";
                // 次のテキストを読み込み
                scenarioText = scenarioDataControl.LoadMessage();
                // 次の文字表示を開始する
                isDisplayActive = false;
            }
        }
        //文字の表示中ではない場合表示を開始する
        else if(!isDisplayActive)
        {
             // 表示中に設定する
            isDisplayActive = true;
            //  コルーチンを呼び出して一文字ずつ表示する
            StartCoroutine(DispFont());
        }

    }
    
    IEnumerator DispFont()
    {
        //  文字を表示しきっていない場合は繰り返す
        while(scenarioText.Length >= dispCounter)
        {
            //  先頭から dispCountr 数分だけ切り出す
            text.text = scenarioText.Substring(0, dispCounter);
            //  指定時間分の待ち時間を設定する（時間が経過すると続きを実行する）
            yield return new WaitForSeconds(dispTimeMax);
            dispCounter++;
        }
        // キー入力待ちに入る
        isKeyWait = true;
    }
    
   
}
