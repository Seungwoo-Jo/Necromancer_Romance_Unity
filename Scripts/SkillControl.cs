using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillControl : MonoBehaviour {

    public int m_SlotNum;
    public Animator m_SkillAnimator;

    public BattleControl m_battleControl;
    public BattleLog m_battleLog;

    public RectTransform m_Skill0;
    public RectTransform m_Skill1;
    public RectTransform m_Wait;

    public Text[] m_DamageText;

    [HideInInspector]
    public float m_TickDamage = 0.0f;

    [HideInInspector]
    public float m_TotalDamage = 0.0f;

    void Start()
    {
        Card card = GameManager.Instance.GetParty(m_SlotNum);
        if(card != null)
        {
            m_Skill0.Find("Name").gameObject.GetComponent<Text>().text = card.m_SkillName[0];
            m_Skill0.Find("Ap").gameObject.GetComponent<Text>().text = SkillManager.Instance.GetSkill(card.m_SkillName[0]).m_UseAp.ToString();
            m_Skill1.Find("Name").gameObject.GetComponent<Text>().text = card.m_SkillName[1];
            m_Skill1.Find("Ap").gameObject.GetComponent<Text>().text = SkillManager.Instance.GetSkill(card.m_SkillName[1]).m_UseAp.ToString();
            m_Wait.Find("Name").gameObject.GetComponent<Text>().text = "대기";
            m_Wait.Find("Ap").gameObject.GetComponent<Text>().text = "0";
        }
    }

    void OnEnable()
    {
        m_Skill0.gameObject.GetComponent<MouseUp>().m_UseEvent = true;
        m_Skill1.gameObject.GetComponent<MouseUp>().m_UseEvent = true;
        m_Wait.gameObject.GetComponent<MouseUp>().m_UseEvent = true;
    }

    public void UseSkill(int num)
    {
        Card card = GameManager.Instance.GetParty(m_SlotNum);

        if(card != null)
        {
            Monster monster = m_battleControl.m_Monster;
            Skill skill = SkillManager.Instance.GetSkill(GameManager.Instance.GetParty(m_SlotNum).m_SkillName[num]);

            if(card.m_Ap >= skill.m_UseAp) {
                m_SkillAnimator.SetTrigger(skill.m_Trigger);

                m_TotalDamage = ((card.m_Atk * skill.m_DamageMagnification) * (monster.m_Def / (monster.m_Def + 100.0f)));
                m_TickDamage = m_TotalDamage / skill.m_AttackNumberOfTime;

                card.m_Ap -= skill.m_UseAp;

                monster.m_Hp -= (int)m_TotalDamage;

                m_DamageText[0].text = ((int)m_TotalDamage).ToString();

                m_battleLog.Log_Attack(card.m_KoreanName, monster.m_KoreanName, m_TotalDamage);

                StartCoroutine("NextTurnAfterSec", 1.1f);
            }
        }
    }

    public void Wait()
    {
        m_battleControl.NextTurn();
    }

    public float GetTickDamage()
    {
        return m_TickDamage;
    }

    public float GetTotalDamage()
    {
        return m_TotalDamage;
    }

    IEnumerator NextTurnAfterSec(float second)
    {
        m_Skill0.gameObject.GetComponent<MouseUp>().m_UseEvent = false;
        m_Skill1.gameObject.GetComponent<MouseUp>().m_UseEvent = false;
        m_Wait.gameObject.GetComponent<MouseUp>().m_UseEvent = false;

        yield return new WaitForSeconds(second);

        m_Skill0.gameObject.GetComponent<MouseUp>().m_UseEvent = true;
        m_Skill1.gameObject.GetComponent<MouseUp>().m_UseEvent = true;
        m_Wait.gameObject.GetComponent<MouseUp>().m_UseEvent = true;

        m_battleControl.NextTurn();
    }
}
