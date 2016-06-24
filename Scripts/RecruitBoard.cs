using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecruitBoard : MonoBehaviour
{
    private bool m_bScroll = false;
    private RectTransform[] m_Applicant;
    private Vector3[] m_ApplicantPosition;
    private Quaternion[] m_ApplicantRotation;
    private LinkedList<Card> m_SaleList;

    private Vector3[] m_DestPos;
    private Quaternion[] m_DestRot;

    private int m_Focus = 0;
    public Card m_FocusingCard;

    [Range(0.01f, 10.0f)]
    public float m_LerpSpeedMagnification = 10.0f;

	void Start ()
    {
        m_Applicant = GetComponentsInChildren<RectTransform>(false);
        m_SaleList = new LinkedList<Card>();
        
        m_ApplicantPosition = new Vector3[13];
        m_ApplicantRotation = new Quaternion[13];

        m_DestPos = new Vector3[13];
        m_DestRot = new Quaternion[13];

        // i = 0은 board 자신
        for(int i = 0; i < 13; i++)
        {
            m_ApplicantPosition[i] = m_Applicant[i+1].anchoredPosition3D;
            m_ApplicantRotation[i] = m_Applicant[i+1].rotation;
        }

        int index = 0;
        foreach(DictionaryEntry pair in CardManager.Instance.m_Cards)
        {
            Card value = (Card)pair.Value;

            //if(!value.m_Bought)
            {
                m_SaleList.AddLast(value);
                m_Applicant[++index].gameObject.GetComponent<Image>().sprite = value.m_FullSprite;
            }
        }

        Focus();
	}
	
	void Update ()
    {
        if(m_bScroll)
        {
            if(Input.GetAxis("MouseWheel") > 0) {
                LeftWheel();
            }
            else if(Input.GetAxis("MouseWheel") < 0) {
                RightWheel();
            }
        }

        Focus();

        int index = 0;
        foreach(Card card in m_SaleList)
        {
            if(card.m_Bought) {
                m_Applicant[++index].gameObject.GetComponent<Image>().color = Color.white;
            }
            else {
                m_Applicant[++index].gameObject.GetComponent<Image>().color = Color.gray;
            }
            
        }
	}

    public void LeftWheel()
    {
        m_Focus = Mathf.Clamp((m_Focus - 1), 0, m_SaleList.Count - 1);
        Debug.Log("Focus: " + m_Focus);
    }

    public void RightWheel()
    {
        m_Focus = Mathf.Clamp((m_Focus + 1), 0, m_SaleList.Count - 1);
        Debug.Log("Focus: " + m_Focus);
    }

    void Focus()
    {
        Vector3 position = new Vector3(0.0f, 1250.0f, 1250.0f);
        Quaternion rotation = Quaternion.identity;
        
        for(int i = 0; i < 13; i++)
        {
            int index = (6 - m_Focus + i);
            
            if(index >= 0 && index < 13)
            {
                m_DestPos[i] = m_ApplicantPosition[index];
                m_DestRot[i] = m_ApplicantRotation[index];

                m_Applicant[i + 1].anchoredPosition3D = Vector3.Lerp(m_Applicant[i + 1].anchoredPosition3D, m_DestPos[i], Time.deltaTime * m_LerpSpeedMagnification);
                m_Applicant[i + 1].rotation = Quaternion.Slerp(m_Applicant[i + 1].rotation, m_DestRot[i], Time.deltaTime * m_LerpSpeedMagnification);

                if(index == 6) {
                    m_Applicant[i + 1].localScale = Vector3.Lerp(m_Applicant[i + 1].localScale, new Vector3(1.11f, 1.11f, 1.0f), Time.deltaTime * (m_LerpSpeedMagnification * 0.28f));
                }
                else {
                    m_Applicant[i + 1].localScale = Vector3.Lerp(m_Applicant[i + 1].localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * m_LerpSpeedMagnification);
                }
            }
            else
            {
                if(index < 0) {
                    position.x = -780.0f;
                }
                else if(index > 12) {
                    position.x = 780.0f;
                }

                rotation.SetLookRotation(Vector3.forward, Vector3.up);

                m_DestPos[i] = position;
                m_DestRot[i] = rotation;

                m_Applicant[i + 1].anchoredPosition3D = Vector3.Lerp(m_Applicant[i + 1].anchoredPosition3D, m_DestPos[i], Time.deltaTime * m_LerpSpeedMagnification);
                m_Applicant[i + 1].rotation = Quaternion.Slerp(m_Applicant[i + 1].rotation, m_DestRot[i], Time.deltaTime * m_LerpSpeedMagnification);
                
            }
        }
    }

    public Card GetFocusingCard()
    {
        int i = 0;
        foreach(Card card in m_SaleList)
        {
            if(m_Focus == i)
            {
                m_FocusingCard = card;
            }
            i++;
        }

        return m_FocusingCard;
    }

    public void UseScroll(bool scroll)
    {
        m_bScroll = scroll;
    }
}