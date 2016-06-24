using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotTurnActivate : MonoBehaviour
{
    private GameObject[] m_Slots;
    public BattleControl m_battleControl;

	void Start ()
    {
        
	}

    void Awake()
    {
        m_Slots = new GameObject[4];

        for(int i = 0; i < 4; i++) {
            m_Slots[i] = GetComponent<RectTransform>().Find("Slot" + i.ToString()).gameObject;
        }
    }

	public void TurnActivate(int slot)
    {
        if(m_Slots == null) {
            //Start();
        }

        for(int i = 0; i < 4; i++)
        {
            m_Slots[i].GetComponent<RectTransform>().Find("Skills").gameObject.SetActive(false);
        }
        
        if(slot == 4) {

        }
        else {
            m_Slots[slot].GetComponent<RectTransform>().Find("Skills").gameObject.SetActive(true);

            
        }
    }

    public void Dead(int slot)
    {
        m_Slots[slot].GetComponent<RectTransform>().Find("HP").gameObject.SetActive(false);
        m_Slots[slot].GetComponent<RectTransform>().Find("AP").gameObject.SetActive(false);
        m_Slots[slot].GetComponent<RectTransform>().Find("Skills").gameObject.SetActive(false);

        StartCoroutine("DeadEffect", slot);
    }

    IEnumerator DeadEffect(int slot)
    {
        float value = 0.0f;

        while(value <= 1.0f)
        {
            //m_Slots[slot].GetComponent<Image>().material.SetFloat("_SliceAmount", value);
            
            yield return null;

            value += Time.deltaTime;
        }
    }
}
