using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Test010Dlg : MonoBehaviour
{
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    public ScrollRect m_scrollRect = null;
    public Text m_txtResult = null;

    public GameObject m_itemPrefab = null; 
    List<ItemText> m_listItem = new List<ItemText>();
    public List<string> listAnimal = new List<string>() { "사자","고양이","호랑이","독수리" };
    int selectedValue = 0;

    private void Start()
    {
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);

        for(int i=0;i<listAnimal.Count;i++)
        {
            SpawnItem(listAnimal[i],i);
        }
    }

    void SpawnItem(string name, int idx)
    {
        GameObject go = Instantiate(m_itemPrefab,m_scrollRect.content);
        ItemText kText = go.GetComponent<ItemText>();
        kText.Initialize(name, idx);
        m_listItem.Add(kText);
        kText.m_btn.onClick.AddListener(()=>OnClick_Selected(idx));
    }

    public void OnClick_Selected(int idx)
    {
        for (int i = 0; i < m_listItem.Count; i++)
        {
            m_listItem[i].SetColor(false);
        }
        m_listItem[idx].SetColor(true);
        m_txtResult.text = listAnimal[idx];
        selectedValue = idx;
    }
    public void OnClick_Ok()
    {
        if (m_txtResult.text == string.Empty) return;
        m_txtResult.text = string.Format("선택된 동물은 <color=#FF0000>{0}</color>입니다", listAnimal[selectedValue]);
    }
    public void OnClick_Clear()
    {
        m_txtResult.text = string.Empty;
        for (int i = 0; i < m_listItem.Count; i++)
        {
            m_listItem[i].SetColor(false);
        }
    }
}