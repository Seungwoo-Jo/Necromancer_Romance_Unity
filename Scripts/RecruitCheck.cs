using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecruitCheck : MonoBehaviour
{
    public GameObject m_RecruitButton;
    public GameObject m_RegisterButton;
    public GameObject m_AlreadyButton;

    private Card m_FocusingCard;

	void Start () {
        m_FocusingCard = null;
        GameManager.Instance.m_EnterInnNum++;
	}
	
	void Update ()
    {
        m_FocusingCard = GetComponentInChildren<RecruitBoard>().GetFocusingCard();

        bool already = false;

        if(m_FocusingCard.m_Bought)
        {
            m_RecruitButton.SetActive(false);

            m_RegisterButton.SetActive(true);
            m_AlreadyButton.SetActive(false);

            already = false;
            for(int i=0; i<4; i++)
            {
                if(m_FocusingCard.m_Name.Equals(GameManager.Instance.m_Party[i]))
                {
                    already = true;
                }
            }

            if(already)
            {
                m_RegisterButton.SetActive(false);
                m_AlreadyButton.SetActive(true);
            }
            else
            {
                m_RegisterButton.SetActive(true);
                m_AlreadyButton.SetActive(false);
            }
            
        }
        else
        {
            m_RecruitButton.SetActive(true);
            m_RegisterButton.SetActive(false);
            m_AlreadyButton.SetActive(false);

            m_RecruitButton.GetComponent<RectTransform>().Find("Price").gameObject.GetComponent<Text>().text = GameManager.Instance.m_RecruitCost.ToString();

            if(EnoughGold())
            {
                m_RecruitButton.GetComponent<MouseDown>().m_UseEvent = true;
                m_RecruitButton.GetComponent<MouseUp>().m_UseEvent = true;
                m_RecruitButton.GetComponent<MouseOver>().m_UseEvent = true;
                m_RecruitButton.GetComponent<MouseExit>().m_UseEvent = true;
            }
            else
            {
                m_RecruitButton.GetComponent<MouseDown>().m_UseEvent = false;
                m_RecruitButton.GetComponent<MouseUp>().m_UseEvent = false;
                m_RecruitButton.GetComponent<MouseOver>().m_UseEvent = false;
                m_RecruitButton.GetComponent<MouseExit>().m_UseEvent = false;
            }
        }
	}

    

    public void Recruit()
    {
        if(EnoughGold())
        {
            GameManager.Instance.m_Gold -= GameManager.Instance.m_RecruitCost;

            m_FocusingCard.m_Bought = true;

            GameManager.Instance.m_HaveCards.Add(m_FocusingCard.m_Name, m_FocusingCard);

            GameManager.Instance.m_RecruitCost += (int)((float)(GameManager.Instance.m_RecruitCost) * 1.1f);

            if(GameManager.Instance.m_EnterInnNum == 1) {
                GameManager.Instance.m_InnTut = true;
            }
            else {
                GameManager.Instance.m_InnTut = false;
            }
        }
    }

    bool EnoughGold()
    {
        bool result = true;

        if(GameManager.Instance.m_Gold < GameManager.Instance.m_RecruitCost)
        {
            result = false;
        }
        else {
            result = true;
        }

        return result;
    }
}
