using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>タイトル画面のUI</summary>
public class ButtonUI : MonoBehaviour
{
    [SerializeField]
    float _maxLove = 100;
    
    [SerializeField]
    float _currentLove; 

    [SerializeField]
    Slider _slider;

    [SerializeField]
    Text _gageText;

    void Start()
    {
        _slider.maxValue = _maxLove;
        _currentLove = _slider.minValue;
        _slider.value = _currentLove;
        _gageText.text = _slider.value.ToString();
    }

    /// <summary>正解</summary>
    public void YesButton()
    {
        _currentLove += 25;
        _slider.value = _currentLove;
        Debug.Log("現在の恋愛度" + _slider.value);
        _gageText.text = _slider.value.ToString();
        TextUI.I.False();
        TextUI.I.ButtonFalse();
        TextUI.I.StartCoroutine(TextUI.I.Cotext());
    }

    /// <summary>不正解</summary>
    public void NoButton()
    {
        _currentLove -= 25;
        _slider.value = _currentLove;
        Debug.Log("現在の恋愛度" + _slider.value);
        _gageText.text = _slider.value.ToString();
        TextUI.I.False();
        TextUI.I.ButtonFalse();
        TextUI.I.StartCoroutine(TextUI.I.Cotext());
    }
    public void Update()
    {
        if (_currentLove == 100)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
