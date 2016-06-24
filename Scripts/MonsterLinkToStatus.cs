using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterLinkToStatus : MonoBehaviour
{
    private Monster m_Monster;

    public Image m_Hp;
    public Image m_HpBack;
    public Image m_Attribute;
    public Image m_Weakness;
    
    public BattleControl m_battleControl;

    public Sprite[] m_AttributeSprite;
    public Sprite[] m_WeaknessSprite;

    private float m_PrevMonsterHp;

	void Update ()
    {
        if(m_Monster != null)
        {
            m_PrevMonsterHp = m_Monster.m_Hp;

            m_Hp.fillAmount = Mathf.Lerp(m_Hp.fillAmount, m_Monster.m_Hp / m_Monster.m_MaxHp, Time.deltaTime * 3.0f);
            m_HpBack.fillAmount = Mathf.Lerp(m_HpBack.fillAmount, m_Hp.fillAmount, Time.deltaTime);
        }
        
	}

    public void BattleStart()
    {
        m_Monster = m_battleControl.m_Monster;

        if(m_Monster.m_Attribute == 0)
        {
            m_Attribute.sprite = m_AttributeSprite[0];
            m_Weakness.sprite = m_WeaknessSprite[2];
        }
        else if(m_Monster.m_Attribute == 1)
        {
            m_Attribute.sprite = m_AttributeSprite[1];
            m_Weakness.sprite = m_WeaknessSprite[1];
        }
        else if(m_Monster.m_Attribute == 2)
        {
            m_Attribute.sprite = m_AttributeSprite[2];
            m_Weakness.sprite = m_WeaknessSprite[0];
        }
    }
}
