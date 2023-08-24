using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test002Dlg : MonoBehaviour
{
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    public Text m_txtResult = null;
    public InputField m_inputField = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }

    void OnClick_Ok()
    {
        if (string.IsNullOrEmpty(m_inputField.text)) return;
        int score = int.Parse(m_inputField.text);
        if (score < 0 || score > 100) return;
        string rank = "F";
        if(score >= 90)
            rank = "A";
        else if(score >= 80)
            rank = "B";
        else if(score >= 70)
            rank = "C";
        else if(score >= 60)
            rank = "D";
        m_txtResult.text = string.Format("당신은 {0}입니다.", rank);
        Debug.Log(rank);
        
    }
    void OnClick_Clear()
    {
        m_inputField.text = string.Empty;
        m_txtResult.text = string.Empty;
    }
}
