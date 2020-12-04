using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class MessageModel
{
    // コマンド名
    public string name;
    //メッセージの行数
    public int msgCount;
    //メッセージリスト
    public List<string> msg;
    //メッセージを一つにまとめる
    public string GetMessage()
    {
        //返り値に使用する文字列を用意
        var message = "";
        // 一行目から順番につなげる（改行付き）
        msg.ForEach(m => message += m + Environment.NewLine);
        // 合成した文字列を返す
        return message;
    }
}
