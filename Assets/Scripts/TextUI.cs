using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>CSV管理</summary>
public class TextUI : MonoBehaviour
{
    public static TextUI I = null;

    /// <summary>CSVデータの保存場所</summary>
    List<string[]> _csvData = new List<string[]>();

    /// <summary>シナリオデータ</summary>
    [SerializeField]
    [Header("シナリオデータ")]
    TextAsset _textFail;

    /// <summary>UITextスクリプト</summary>
    [SerializeField]
    [Header("UITextスクリプト")]
    UIText _uitext;

    /// <summary>現在の行数</summary>
    int _textID = 1;

    /// <summary>イベントフラグ</summary>
    bool _eventflag = false;

    /// <summary>イベント名</summary>
    string _eventName;

    void Start() => LoadCSV();

    [SerializeField]
    [Header("正解")]
    Text _yesText;

    [SerializeField]
    [Header("不正解")]
    Text _noText;

    [SerializeField]
    GameObject _yesButton;

    [SerializeField]
    GameObject _noButton;
    void Awake() => I = this;

    /// <summary>CSVを読み込む</summary>
    public void LoadCSV()
    {
        StringReader reader = new StringReader(_textFail.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _csvData.Add(line.Split(','));
        }

        StartCoroutine(Cotext());
    }

    public void Update()
    {
        _eventName = _csvData[_textID][3];

        if (_csvData[_textID][2] == "TRUE")
        {
            _eventflag = true;
            ButtonTrue();
        }

        switch (_eventName/*なんのイベントフラグが立っているか*/)
        {
            //イベントの種類
            case "返信":
                //イベントの処理:返信
                _yesText.text = "「分かった。気をつけてね」";
                _noText.text = "「分かった。待ってるよ」";
                break;
            case "行き先":
                //イベントの処理:行き先
                _yesText.text = "「ご飯を食べに行く」";
                _noText.text = "「イルミネーションを見に行く」";
                break;
            case "行き先１":
                //イベントの処理:2
                _yesText.text = "「公園に行く」";
                _noText.text = "「買い物をする」";
                break;
            case "エンディング":
                //イベントの処理:3
                _yesText.text = "「今日は俺の家に泊まっていかない」";
                _noText.text = "「今日はありがとうね」";
                break;

        }
    }

    /// <summary>クリックでテキストを一気に表示</summary>
    IEnumerator Skip()
    {
        while (_uitext.Playing) yield return null;
        while (!_uitext.IsClicked()) yield return null;
    }

    /// <summary>CSVを上から一行ずつ出力</summary>
    public IEnumerator Cotext()
    {
        Debug.Log($"現在：{_textID}行");

        _uitext.DrawText(_csvData[_textID][0], _csvData[_textID][1]); //(名前,セリフ)
        yield return StartCoroutine(Skip());//クリックで進む
        _textID++; //次の行へ

        EventCheck();
    }

    /// <summary>イベントフラグ確認</summary>
    public void EventCheck()
    {
        if(!_eventflag)StartCoroutine(Cotext());
    }

    /// <summary>イベントフラグを解除</summary>
    public void False() => _eventflag = false;

    public void ButtonFalse()
    {
        _yesButton.SetActive(false);
        _noButton.SetActive(false);
    }
    public void ButtonTrue()
    {
        _yesButton.SetActive(true);
        _noButton.SetActive(true);
    }
}
