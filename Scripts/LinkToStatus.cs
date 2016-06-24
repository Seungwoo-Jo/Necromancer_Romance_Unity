using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LinkToStatus : MonoBehaviour {

    public RecruitBoard m_Board;
    public Text m_MaxHp;
    public Text m_Hp;
    public Text m_MaxAp;
    public Text m_Ap;
    public Text m_Atk;
    public Text m_Def;
    public Text m_Spd;
    public Text m_SkillName_0;
    public Text m_SkillAp_0;
    public Text m_SkillName_1;
    public Text m_SkillAp_1;
    public Text m_SkillName_2;
    public Text m_SkillAp_2;
    public Image m_AttributeImage;
    public Text m_AttributeExplain;
    public Text m_Home;
    public Text m_Explain;
    public Text m_SkillExplain;
    public GameObject m_CenterCard;

    public RectTransform m_Left;
    public RectTransform m_Right;

    private Image[] m_ChildImage;
    private Text[] m_ChildText;
    private Card m_CurCard;

    private Vector3 m_LeftDestPos;
    private Vector3 m_RightDestPos;

    private float m_LerpValue;

    [Range(0.01f, 5.0f)]
    public float m_LerpSpeedMagnification = 1.0f;
    public bool m_Show = false;

	void Start ()
    {
        m_Left = GetComponent<RectTransform>().Find("Left").GetComponent<RectTransform>();
        m_Right = GetComponent<RectTransform>().Find("Right").GetComponent<RectTransform>();

        m_LeftDestPos = m_Left.anchoredPosition3D;
        m_RightDestPos = m_Right.anchoredPosition3D;

        m_ChildImage = GetComponentsInChildren<Image>(true);
        m_ChildText = GetComponentsInChildren<Text>(true);

        m_Left.gameObject.SetActive(false);
        m_Right.gameObject.SetActive(false);

        m_AttributeImage.gameObject.SetActive(false);

        m_LerpValue = 0.0f;

        m_Show = false;
	}

	void Update ()
    {
        m_Left.anchoredPosition3D = Vector3.Lerp(Vector3.zero, m_LeftDestPos, m_LerpValue * m_LerpSpeedMagnification);
        m_Right.anchoredPosition3D = Vector3.Lerp(Vector3.zero, m_RightDestPos, m_LerpValue * m_LerpSpeedMagnification);

        foreach(Image obj in m_ChildImage) {
            obj.color = Color.Lerp(Color.clear, Color.white, m_LerpValue * m_LerpSpeedMagnification);
        }

        foreach(Text obj in m_ChildText) {
            obj.color = Color.Lerp(Color.clear, Color.white, m_LerpValue * m_LerpSpeedMagnification);
        }
        
        m_CenterCard.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.white, m_LerpValue * m_LerpSpeedMagnification);

        if(m_Show) {
            m_LerpValue += Time.deltaTime;
        }
        else {
            m_LerpValue -= Time.deltaTime;
        }

        if(m_LerpValue < 0.0f) {
            m_LerpValue = 0.0f;
            m_CenterCard.SetActive(false);
            m_Left.gameObject.SetActive(false);
            m_Right.gameObject.SetActive(false);
        }
        else if(m_LerpValue > 1.0f) {
            m_LerpValue = 1.0f;
        }

	}

    public void Open()
    {
        m_Show = true;
        m_CenterCard.SetActive(true);
        m_Left.gameObject.SetActive(true);
        m_Right.gameObject.SetActive(true);
    }

    public void Close()
    {
        m_Show = false;
    }

    public void DataUpdate()
    {
        m_CurCard = m_Board.GetFocusingCard();

        m_MaxHp.text = m_CurCard.m_MaxHp.ToString();
        m_Hp.text = m_CurCard.m_Hp.ToString();
        m_MaxAp.text = m_CurCard.m_MaxAp.ToString();
        m_Ap.text = m_CurCard.m_Ap.ToString();
        m_Atk.text = m_CurCard.m_Atk.ToString();
        m_Def.text = m_CurCard.m_Def.ToString();
        m_Spd.text = m_CurCard.m_Spd.ToString();
        m_SkillName_0.text = m_CurCard.m_SkillName[0];
        m_SkillAp_0.text = SkillManager.Instance.GetSkill(m_CurCard.m_SkillName[0]).m_UseAp.ToString();
        m_SkillName_1.text = m_CurCard.m_SkillName[1];
        m_SkillAp_1.text = SkillManager.Instance.GetSkill(m_CurCard.m_SkillName[1]).m_UseAp.ToString();
        m_SkillName_2.text = "대기";
        m_SkillAp_2.text = "0";
        m_Home.text = m_CurCard.m_Home;
        m_Explain.text = m_CurCard.m_Explain;

        m_CenterCard.GetComponent<Image>().sprite = m_CurCard.m_FullSprite;

        m_AttributeImage.sprite = GetComponentInChildren<CardAttribute>().SetAttributeSprite(m_CurCard.m_Attribute);
        m_AttributeExplain.text = GetComponentInChildren<CardAttribute>().SetAttributeExplain(m_CurCard.m_Attribute);

    }
}
