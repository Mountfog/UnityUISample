using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test011Dlg : MonoBehaviour
{
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    public ScrollRect m_scrollRect = null;
    public Text m_txtResult = null;

    public GameObject m_itemPrefab = null;
    List<ImageText> m_listItem = new List<ImageText>();
    public List<string> listAnimal = new List<string>() { "사자", "고양이", "호랑이", "독수리" };
    int selectedValue = 0;
    ImageText curitem = null;

    private void Start()
    {
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);

        for (int i = 0; i < listAnimal.Count; i++)
        {
            SpawnItem(listAnimal[i], i);
        }
    }

    void SpawnItem(string name, int idx)
    {
        GameObject go = Instantiate(m_itemPrefab, m_scrollRect.content);
        ImageText kText = go.GetComponent<ImageText>();
        kText.Initialize(name, idx);
        m_listItem.Add(kText);
        kText.m_btn.onClick.AddListener(() => OnClick_Selected(idx));
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
        curitem = m_listItem[idx];
    }
    public void OnClick_Ok()
    {
        if (m_txtResult.text == string.Empty) return;
        if (curitem == null) return;
        m_txtResult.text = string.Format("선택된 동물은 <color=#FF0000>{0}</color>입니다", curitem.m_txt.text);
    }
    public void OnClick_Clear()
    {
        if (curitem == null) return;
        m_txtResult.text = string.Empty;
        listAnimal.Remove(curitem.m_txt.text);
        m_listItem.Remove(curitem);
        listAnimal.Remove(curitem.m_txt.text);
        m_listItem.Remove(curitem);
        Destroy(curitem.gameObject);

        for (int i = 0; i < m_listItem.Count; i++)
        {
            ImageText kText = m_listItem[i];
            int idx = i;
            kText.Initialize(listAnimal[i], idx);
            m_listItem[i] = kText;
            kText.m_btn.onClick.RemoveAllListeners();
            kText.m_btn.onClick.AddListener(() => OnClick_Selected(idx));
        }

        selectedValue = -1;
        curitem = null;
    }
}
