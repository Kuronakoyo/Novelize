using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDataControl : MonoBehaviour
{
    //シナリオデータを保存する場所を確保
    private ScenarioModel scenarioModel = new ScenarioModel();
    /// <summary>
    ///　読み込んだシナリオデータを仕分けして保存する
    /// </summary>
    /// <param name="scenarioText">読み込んだシナリオテキスト</param>
    public void InitScenarioData(string scenarioText)
    {
        // テキストを改行で区切りリスト化する
        scenarioModel.scenario = scenarioText.Split('\n');
        //先頭を選択するように初期化
        scenarioModel.nowSelectline = 0;


    }
    /// <summary>
    /// メッセージを取得する
    /// </summary>
    /// <returns>テキストに表示する文字を習得する</returns>
    public string LoadMessage()
    {
        //一行読み込み
        var msg = scenarioModel.scenario[scenarioModel.nowSelectline++];
        // Json形式を定義したクラスデータに変換する
        var messages = JsonUtility.FromJson<MessageModel>(msg);
        // 取得したメッセージ情報を返す
        return messages.GetMessage();
    }
    ///<summary>

    public bool IsEndOfLine()
    {
        return (scenarioModel.MaxLineCount == scenarioModel.nowSelectline);
    }

}
