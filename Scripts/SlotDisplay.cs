using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotDisplay : MonoBehaviour
{
    public bool m_UseHide = true;
    public Vector2 m_DisplayPos;
    public Vector2 m_HidePos;
    public bool m_CurrentDisplay = true;
    public bool m_Fade = true;
    
    [Range (0.01f, 5.0f)]
    public float m_LerpSpeedMagnification = 1.0f;
    
    private float m_Value = -1;

    private Image[] m_ChildImage;
    private Text[] m_ChildText;

    void Start()
    {
        m_ChildImage = GetComponentsInChildren<Image>();
        m_ChildText = GetComponentsInChildren<Text>();

        Init();
        
    }

    void Update()
    {
        if(m_UseHide)
        {
            Reaction();
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = m_DisplayPos;
            GetComponent<Image>().color = Color.white;
        }
        
    }

    public void Display()
    {
        m_CurrentDisplay = true;

        if(m_Value <= 0.0f) {
            m_Value = 0.0f;
        }
        else {
            m_Value = 1.0f - m_Value;
        }
    }

    public void Hide()
    {
        m_CurrentDisplay = false;

        if(m_Value <= 0.0f) {
            m_Value = 0.0f;
        }
        else {
            m_Value = 1.0f - m_Value;
        }
    }

    void Reaction()
    {
        if(m_Value >= 0.0f)
        {
            Color col = Color.white;
            if(m_CurrentDisplay)
            {
                GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(m_HidePos, m_DisplayPos, m_Value);

                if(m_Fade) {
                    col = Color.Lerp(Color.clear, Color.white, m_Value);

                    GetComponent<Image>().color = col;

                    foreach(Image obj in m_ChildImage) {
                        obj.color = col;
                    }

                    foreach(Text obj in m_ChildText) {
                        obj.color = col;
                    }
                }
            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(m_DisplayPos, m_HidePos, m_Value);

                if(m_Fade)
                {
                    col = Color.Lerp(Color.white, Color.clear, m_Value);

                    GetComponent<Image>().color = col;

                    foreach(Image obj in m_ChildImage) {
                        obj.color = col;
                    }

                    foreach(Text obj in m_ChildText) {
                        obj.color = col;
                    }
                }
            }

            m_Value += Time.deltaTime * m_LerpSpeedMagnification;

            if(m_Value > 1.0f) {
                m_Value = -1.0f;

                Init();
            }
        }
    }

    void Init()
    {
        if(m_CurrentDisplay) {
            GetComponent<RectTransform>().anchoredPosition = m_DisplayPos;
            GetComponent<Image>().color = Color.white;

            foreach(Image obj in m_ChildImage) {
                obj.color = Color.white;
            }

            foreach(Text obj in m_ChildText) {
                obj.color = Color.white;
            }
        }
        else {
            GetComponent<RectTransform>().anchoredPosition = m_HidePos;
            GetComponent<Image>().color = Color.clear;

            foreach(Image obj in m_ChildImage) {
                obj.color = Color.clear;
            }

            foreach(Text obj in m_ChildText) {
                obj.color = Color.clear;
            }
        }
    }
}
