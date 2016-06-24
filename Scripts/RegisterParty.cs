using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterParty : MonoBehaviour
{
    public bool m_UseRegister = false;
    public bool m_UseFollow = false;
    public GameObject m_CancelButton;

    private Card m_FocusingCard;
    public GameObject m_FollowImage;
	
    void Start () {
	
	}
	
    void Update()
    {
        if(m_UseFollow)
        {
            Vector3 pos = m_FollowImage.GetComponent<RectTransform>().anchoredPosition3D;
            pos = Input.mousePosition - (new Vector3(640.0f, 360.0f, 0.0f));
            m_FollowImage.GetComponent<RectTransform>().anchoredPosition3D = pos;
        }
        
    }

    public void UseRegister(bool reg)
    {
        m_UseRegister = reg;
        m_FocusingCard = GetComponentInChildren<RecruitBoard>().GetFocusingCard();
        m_CancelButton.SetActive(reg);

        m_FollowImage.SetActive(reg);
        m_FollowImage.GetComponent<Image>().sprite = m_FocusingCard.m_SlotSprite;

        if(reg) {
            UseFollow(false);
        }
    }

    public void Register(int slot)
    {
        if(m_UseRegister)
        {
            if(!GameManager.Instance.m_Party[slot].Equals("Nero"))
            {
                GameManager.Instance.m_Party[slot] = m_FocusingCard.m_Name;
                UseRegister(false);
            }
        }
    }

    public void UseFollow(bool follow)
    {
        m_UseFollow = follow;
    }
}
