using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Test001Dlg : MonoBehaviour
{
    public Text m_txtResult = null;
    public Button m_btnConfirm = null;
    public Button m_btnClear = null;

    //확인 버튼을 누르면
    //a = 100 b = 200
    //text 


    // Start is called before the first frame update
    void Start()
    {
        m_btnConfirm.onClick.AddListener(() => OnClick_Confirm());
        m_btnClear.onClick.AddListener(() => OnClick_Clear());
        m_txtResult.text = string.Empty;
    }

    void OnClick_Confirm()
    {
        int a = 100;
        int b = 200;
        m_txtResult.text += "Result\n-----------------\n";
        m_txtResult.text += string.Format("a = {0}, b = {1}\n", a, b);
        m_txtResult.text += string.Format("a + b = {0}\n", Sum(a, b));
        Swap1(a, b);
        m_txtResult.text += string.Format("Swap1 : a = {0}, b = {1}\n", a, b);
        Swap2(ref a, ref b);
        m_txtResult.text += string.Format("Swap2 :a = {0}, b = {1}\n", a, b);
    }
    void OnClick_Clear()
    {
         m_txtResult.text = string.Empty; 
    }

    void Swap1(int a,int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    void Swap2(ref int a, ref int b)
    {
        int temp = a;
        a = b;   
        b = temp;
    }

    int Sum(int a, int b)
    {
        return a + b;
    }

}
