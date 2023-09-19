using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Test001Dlg : MonoBehaviour
{
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] InputField m_inputField = null;
    [SerializeField] Text m_txtResult = null;
    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_inputField.onEndEdit.AddListener(OnEndEdit);
        m_inputField.onSubmit.AddListener(OnSubmit);
    }
    public void OnClick_Ok()
    {
        m_txtResult.text = string.Format("����� �Է��� ���� <color=#FF0000>{0}</color>�Դϴ�",m_inputField.text);
        //dafsdfasfdsa
    }
    void OnEndEdit(string s)
    {
        Debug.Log("OnEndEdit");
        m_txtResult.text = string.Format("<color=#FF0000>{0}</color>�� ����Ǿ����ϴ�", s);
    }

    void OnSubmit(string s)
    {
        Debug.Log("OnSubmit");
        m_txtResult.text = string.Format("����� ���� <color=#FF0000>{0}</color>�Դϴ�", s);
    }
    public void OnClick_Clear()
    {
        m_txtResult.text = string.Empty;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
