using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>CSV�Ǘ�</summary>
public class TextUI : MonoBehaviour
{
    public static TextUI I = null;

    /// <summary>CSV�f�[�^�̕ۑ��ꏊ</summary>
    List<string[]> _csvData = new List<string[]>();

    /// <summary>�V�i���I�f�[�^</summary>
    [SerializeField]
    [Header("�V�i���I�f�[�^")]
    TextAsset _textFail;

    /// <summary>UIText�X�N���v�g</summary>
    [SerializeField]
    [Header("UIText�X�N���v�g")]
    UIText _uitext;

    /// <summary>���݂̍s��</summary>
    int _textID = 1;

    /// <summary>�C�x���g�t���O</summary>
    bool _eventflag = false;

    /// <summary>�C�x���g��</summary>
    string _eventName;

    void Start() => LoadCSV();

    [SerializeField]
    [Header("����")]
    Text _yesText;

    [SerializeField]
    [Header("�s����")]
    Text _noText;

    [SerializeField]
    GameObject _yesButton;

    [SerializeField]
    GameObject _noButton;
    void Awake() => I = this;

    /// <summary>CSV��ǂݍ���</summary>
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

        switch (_eventName/*�Ȃ�̃C�x���g�t���O�������Ă��邩*/)
        {
            //�C�x���g�̎��
            case "�ԐM":
                //�C�x���g�̏���:�ԐM
                _yesText.text = "�u���������B�C�����Ăˁv";
                _noText.text = "�u���������B�҂��Ă��v";
                break;
            case "�s����":
                //�C�x���g�̏���:�s����
                _yesText.text = "�u���т�H�ׂɍs���v";
                _noText.text = "�u�C���~�l�[�V���������ɍs���v";
                break;
            case "�s����P":
                //�C�x���g�̏���:2
                _yesText.text = "�u�����ɍs���v";
                _noText.text = "�u������������v";
                break;
            case "�G���f�B���O":
                //�C�x���g�̏���:3
                _yesText.text = "�u�����͉��̉Ƃɔ��܂��Ă����Ȃ��v";
                _noText.text = "�u�����͂��肪�Ƃ��ˁv";
                break;

        }
    }

    /// <summary>�N���b�N�Ńe�L�X�g����C�ɕ\��</summary>
    IEnumerator Skip()
    {
        while (_uitext.Playing) yield return null;
        while (!_uitext.IsClicked()) yield return null;
    }

    /// <summary>CSV���ォ���s���o��</summary>
    public IEnumerator Cotext()
    {
        Debug.Log($"���݁F{_textID}�s");

        _uitext.DrawText(_csvData[_textID][0], _csvData[_textID][1]); //(���O,�Z���t)
        yield return StartCoroutine(Skip());//�N���b�N�Ői��
        _textID++; //���̍s��

        EventCheck();
    }

    /// <summary>�C�x���g�t���O�m�F</summary>
    public void EventCheck()
    {
        if(!_eventflag)StartCoroutine(Cotext());
    }

    /// <summary>�C�x���g�t���O������</summary>
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
