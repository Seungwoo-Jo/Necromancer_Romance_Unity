using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetUpSlot : MonoBehaviour
{
    public Sprite m_None;
    private LinkedList<GameObject> m_Slot;
    private Text m_Gold;
    private Text m_Soul;

    void Start()
    {
        m_Slot = new LinkedList<GameObject>();

        for(int i=0; i<4; i++)
        {
            GameObject obj = GameObject.Find("Slot" + i.ToString());
            m_Slot.AddLast(obj);
        }

        m_Gold = GetComponent<RectTransform>().Find("Gold").GetComponent<Text>();
        m_Soul = GetComponent<RectTransform>().Find("Soul").GetComponent<Text>();
    }

	void Update ()
    {
        int i = 0;
        foreach(var slot in m_Slot)
        {
            Card card = GameManager.Instance.GetParty(i);

            if(card != null)
            {
                slot.GetComponent<Image>().enabled = true;
                slot.GetComponent<CardLinkToSlot>().EnableInfo(true);
                slot.GetComponent<Image>().sprite = card.m_SlotSprite;

                slot.GetComponent<CardLinkToSlot>().LinkToSlot(card);
            }
            else
            {
                //slot.GetComponent<Image>().enabled = false;
                slot.GetComponent<CardLinkToSlot>().EnableInfo(false);
                slot.GetComponent<Image>().sprite = m_None;

                if(slot.GetComponent<Image>().material != null) {
                    slot.GetComponent<Image>().material.SetFloat("_SliceAmount", 0.0f);
                }
            }

            i++;
        }

        m_Gold.text = GameManager.Instance.m_Gold.ToString();
        m_Soul.text = GameManager.Instance.m_Soul.ToString();
	}
}
