using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleControl : MonoBehaviour
{
    struct QueuePair
    {
        public string m_Name;
        public float m_Spd;
        public float m_Wait;
        public bool m_bMonster;
    }

    public Monster m_Monster;

    private QueuePair[] m_TurnQueue;
    
    private List<Monster> m_StageMonster;

    public Card m_TurnCard;
    public BattleLog m_battleLog;
    public Image m_MonsterMold;
    public SlotTurnActivate m_TurnActivator;
    public Text[] m_Damage;
    public Animator m_Skill;

    [HideInInspector]
    public bool m_bBattle = false;

    [HideInInspector]
    public int m_TurnSlot;

    [HideInInspector]
    public bool m_Tutorial;

	void Start ()
    {
        
	}
	
    void Awake()
    {
        m_TurnQueue = new QueuePair[5];
        m_StageMonster = new List<Monster>();
        m_Tutorial = false;
    }

    public void Tutorial()
    {
        if(m_TurnQueue == null) {
            //Start();
        }
        m_Tutorial = true;
    }

    public void BattleStart()
    {
        if(m_Tutorial)
        {
            Monster monster = new Monster();
            monster.m_Atk = 500.0f;
            monster.m_Def = 1000.0f;
            monster.m_Spd = 52.0f;
            monster.m_MaxHp = 1000.0f;
            monster.m_Hp = monster.m_MaxHp;
            monster.m_Attribute = 0;
            monster.m_Sprite = ((Monster)MonsterManager.Instance.m_Monsters["Shoa"]).m_Sprite;

            m_Monster = monster;
        }
        else
        {
            if(GameManager.Instance.m_Boss == 0)
            {
                // 만날 적을 골라봅시다.
                foreach(DictionaryEntry obj in MonsterManager.Instance.m_Monsters) {
                    Monster monster = (Monster)obj.Value;
                    if(monster.m_AppearStage == GameManager.Instance.m_Stage && !monster.m_Boss) {
                        m_StageMonster.Add(monster);
                    }
                }

                int pick = Random.Range(0, m_StageMonster.Count);

                int index = 0;
                foreach(Monster monster in m_StageMonster) {
                    if(index == pick) {
                        m_Monster = monster;
                        break;
                    }
                    index++;
                }
                // 적 고르기 끝
            }
            else if(GameManager.Instance.m_Boss == 1) // 루퍼스
            {
                m_Monster = (Monster)MonsterManager.Instance.m_Monsters["Lupus"];
            }
            else if(GameManager.Instance.m_Boss == 2) // 서큐버스
            {
                m_Monster = (Monster)MonsterManager.Instance.m_Monsters["Succubus"];
            }
            
        }
        
        

        // 큐 넣기
        for(int i = 0; i < 4; i++)
        {
            Card card = GameManager.Instance.GetParty(i);
            
            if(card != null)
            {
                m_TurnQueue[i].m_Name = card.m_Name;
                m_TurnQueue[i].m_Spd = card.m_Spd;
                m_TurnQueue[i].m_Wait = 0.0f;
                m_TurnQueue[i].m_bMonster = false;

            }
            else
            {
                m_TurnQueue[i].m_Name = "";
                m_TurnQueue[i].m_Wait = -1.0f;
            }
        }

        m_TurnQueue[4].m_Name = m_Monster.m_Name;
        m_TurnQueue[4].m_Spd = m_Monster.m_Spd;
        m_TurnQueue[4].m_Wait = 0.0f;
        m_TurnQueue[4].m_bMonster = true;
        // 큐 넣기 끝

        m_Monster.m_Hp = m_Monster.m_MaxHp;

        m_MonsterMold.gameObject.SetActive(true);
        m_MonsterMold.sprite = m_Monster.m_Sprite;
        m_MonsterMold.GetComponentInChildren<MonsterLinkToStatus>().BattleStart();

        NextTurn();
    }

    public Card NextTurn()
    {
        // 다음 턴은 누구냐
        for(int i = 0; i < 5; i++)
        {
            if(m_TurnQueue[i].m_Wait >= 0.0f)
            {
                if(i >= 0 && i < 4)
                {
                    Card card = null;

                    if(m_TurnQueue[i].m_Name != null)
                    {
                        card = (Card)GameManager.Instance.m_HaveCards[m_TurnQueue[i].m_Name];
                    }

                    if(card != null)
                    {
                        if(card.m_Hp > 0.0f) {
                            m_TurnQueue[i].m_Wait += m_TurnQueue[i].m_Spd;
                        }
                        else {
                            m_TurnQueue[i].m_Spd = 0.0f;
                            m_TurnQueue[i].m_Wait = -1.0f;
                        }
                    }
                    else {
                        Debug.Log(i.ToString() + " null");
                    }
                }
                else {
                    m_TurnQueue[i].m_Wait += m_TurnQueue[i].m_Spd;
                }
                
                
            }
        }

        // Wait 수치가 가장 높은 캐릭터가 다음 턴
        QueuePair turn = m_TurnQueue[0];
        m_TurnSlot = 0;

        for(int i = 1; i < 5; i++)
        {
            // Wait 수치가 같으면 속도가 낮은 카드가 우선
            if(m_TurnQueue[i].m_Wait == turn.m_Wait)
            {
                if(m_TurnQueue[i].m_Spd < turn.m_Spd)
                {
                    turn = m_TurnQueue[i];
                    m_TurnSlot = i;
                }
            }
            else if(m_TurnQueue[i].m_Wait > turn.m_Wait)
            {
                turn = m_TurnQueue[i];
                m_TurnSlot = i;
            }
        }

        m_TurnQueue[m_TurnSlot].m_Wait = 0.0f;

        if(turn.m_bMonster)
        {
            m_TurnCard = null;
            m_TurnActivator.TurnActivate(4);
            MonsterTurn();
        }
        else
        {
            m_TurnCard = GameManager.Instance.GetParty(turn.m_Name);
            m_TurnActivator.TurnActivate(m_TurnSlot);
        }

        return m_TurnCard;
    }

    public void Trap()
    {
        int slot = 0;
        Card card = null;

        while(card == null)
        {
            slot = Random.Range(0, 4);
            card = GameManager.Instance.GetParty(slot);
        }

        float damage = card.m_MaxHp * 0.15f;
        card.m_Hp -= damage;

        m_battleLog.Log_Trap(card.m_KoreanName, damage);

        if(card.m_Hp <= 0.0f) {
            CardDie(slot);
        }
    }

    public void Treasure()
    {
        int addGold = Random.Range(10, 51) * 10;

        GameManager.Instance.m_Gold += addGold;

        m_battleLog.Log_GetGold(addGold);
    }

    public void Win()
    {
        GameManager.Instance.m_Soul += 1;

        m_battleLog.Log_GetSoul(1);

        m_MonsterMold.gameObject.SetActive(false);
        m_TurnActivator.TurnActivate(4);
        
    }

    public void Lose()
    {
        GameManager.Instance.m_NeroProg = GameManager.Instance.m_Stage - 1.0f;
        m_TurnActivator.TurnActivate(4);
    }

    public string CardDie(int slot)
    {
        Card card = GameManager.Instance.GetParty(slot);

        card.m_Hp = 0.0f;

        GameManager.Instance.m_Party[slot] = "";

        m_TurnActivator.Dead(slot);
        m_battleLog.Log_Die(card.m_KoreanName);

        return card.m_Name;
    }


    // 이하 몬스터 AI
    public void MonsterTurn()
    {
        int slot = 0;
        Card target = null;

        while(target == null)
        {
            slot = Random.Range(0, 4);
            target = GameManager.Instance.GetParty(slot);
        }

        float damage = m_Monster.m_Atk * (target.m_Def / (target.m_Def + 100.0f));
        target.m_Hp -= (int)damage;

        m_Damage[0].text = ((int)damage).ToString();

        string trigger = "MonsterAttack" + slot.ToString();
        m_Skill.SetTrigger(trigger);

        m_battleLog.Log_Attack(m_Monster.m_KoreanName, target.m_KoreanName, damage);

        if(target.m_Hp <= 0.0f)
        {
            string name = CardDie(slot);
            Debug.Log(name + " is Dead.");
        }

        StartCoroutine("NextTurnAfterSec", 1.0f);
    }

    IEnumerator NextTurnAfterSec(float second)
    {
        yield return new WaitForSeconds(second);
        NextTurn();
    }
}
