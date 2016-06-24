using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundSwitch : MonoBehaviour
{
    public Sprite m_Stage1Back;
    public Sprite m_Stage1Boss;
    public Sprite m_Stage2Back;
    public Sprite m_Stage2Boss;

    private Image m_WalkBack;

	void Start () {
        m_WalkBack = GetComponent<RectTransform>().Find("WalkEffect").gameObject.GetComponent<Image>();
	}

    public void FrontSwitch()
    {
        if(GameManager.Instance.m_Stage == 1.0f)
        {
            if(GameManager.Instance.m_NeroProg < 2.0f) {
                m_WalkBack.sprite = m_Stage1Back;
            }
            else {
                m_WalkBack.sprite = m_Stage1Boss;
            }

        }
        else if(GameManager.Instance.m_Stage == 2.0f)
        {
            if(GameManager.Instance.m_NeroProg < 3.0f) {
                m_WalkBack.sprite = m_Stage2Back;
            }
            else {
                m_WalkBack.sprite = m_Stage2Boss;
            }
        }
    }

    public void BackSwitch()
    {
        if(GameManager.Instance.m_Stage == 1.0f)
        {
            if(GameManager.Instance.m_NeroProg < 2.0f) {
                GetComponent<Image>().sprite = m_Stage1Back;
            }
            else {
                GetComponent<Image>().sprite = m_Stage1Boss;
            }
            
        }
        else if(GameManager.Instance.m_Stage == 2.0f)
        {
            if(GameManager.Instance.m_NeroProg < 3.0f) {
                GetComponent<Image>().sprite = m_Stage2Back;
            }
            else {
                GetComponent<Image>().sprite = m_Stage2Boss;
            }
        }
    }
}
