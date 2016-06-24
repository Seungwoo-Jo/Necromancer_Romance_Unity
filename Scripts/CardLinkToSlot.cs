using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardLinkToSlot : MonoBehaviour
{
    private Image m_HpBar;
    private Text m_HpNum;
    private Text m_ApNum;

	void Start ()
    {
        m_HpBar = GetComponent<RectTransform>().Find("HP").GetComponent<RectTransform>().Find("Bar").GetComponent<Image>();
        m_HpNum = GetComponent<RectTransform>().Find("HP").GetComponent<RectTransform>().Find("Number").GetComponent<Text>();
        m_ApNum = GetComponent<RectTransform>().Find("AP").GetComponent<Text>();
	}
	
    public void LinkToSlot(Card card)
    {
        m_HpBar.fillAmount = (card.m_Hp / card.m_MaxHp);
        m_HpNum.text = card.m_Hp.ToString();
        m_ApNum.text = card.m_Ap.ToString() + " / " + card.m_MaxAp.ToString();
    }

    public void EnableInfo(bool enable)
    {
        m_HpBar.gameObject.SetActive(enable);
        m_HpNum.gameObject.SetActive(enable);
        m_ApNum.gameObject.SetActive(enable);
    }
}
