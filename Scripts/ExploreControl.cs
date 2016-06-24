using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExploreControl : MonoBehaviour
{
    public GameObject m_ProgressBar;

    public RectTransform m_NeroPos;
    public RectTransform m_HeroPos;
    public RectTransform m_Stage1Start;
    public RectTransform m_Stage1End;
    public RectTransform m_Stage2Start;
    public RectTransform m_Stage2End;

    public GameObject m_SelectWindow;
    public GameObject m_ResultWindow;

    public Animator m_WalkAnimate;

    public BattleControl m_battleControl;

    public AudioSource m_BackAudio;
    public AudioSource m_EffectAudio;
    public AudioClip m_Stage1Back;
    public AudioClip m_Stage1BattleBack;
    public AudioClip m_Stage1Boss;
    public AudioClip m_Stage2Back;
    public AudioClip m_Stage2BattleBack;
    public AudioClip m_Stage2Boss;

    public AudioClip m_Win;

	void Start ()
    {
        StartCoroutine("SelectWindowActive");
	}

    public void GoTown()
    {
        GameManager.Instance.m_NeroProg = GameManager.Instance.m_Stage;
        GameObject.Find("Canvas").GetComponent<SceneSwitch>().SetNextScene("Town");
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("SceneEnd");
        //GameManager.Instance.Restore();
    }

    public void Explore()
    {
        GameManager.Instance.m_NeroProg += 0.125f;
        GameManager.Instance.m_HeroProg += 0.03125f;

        if(GameManager.Instance.m_NeroProg >= GameManager.Instance.m_Stage + 1.0f) {
            GameManager.Instance.m_NeroProg = GameManager.Instance.m_Stage + 1.0f;
            GameManager.Instance.m_Boss = (int)GameManager.Instance.m_Stage;
        }
        if(GameManager.Instance.m_HeroProg >= 3.0f) {
            GameManager.Instance.m_HeroProg = 3.0f;
        }

        m_SelectWindow.SetActive(false);
        m_WalkAnimate.SetTrigger("Walk");
        StartCoroutine("NeroMove", false);
    }

    public void HardExplore()
    {
        GameManager.Instance.m_NeroProg += 0.250f;
        GameManager.Instance.m_HeroProg += 0.0625f;

        if(GameManager.Instance.m_NeroProg >= GameManager.Instance.m_Stage + 1.0f) {
            GameManager.Instance.m_NeroProg = GameManager.Instance.m_Stage + 1.0f;
            GameManager.Instance.m_Boss = (int)GameManager.Instance.m_Stage;
        }
        if(GameManager.Instance.m_HeroProg >= 3.0f) {
            GameManager.Instance.m_HeroProg = 3.0f;
        }

        m_SelectWindow.SetActive(false);
        m_WalkAnimate.SetTrigger("Walk");
        StartCoroutine("NeroMove", true);
    }

    public void ForciblyBattle()
    {
        m_battleControl.BattleStart();
        StartCoroutine("BattleEndCheck");
            
        if(!m_battleControl.m_Tutorial)
        {
            if(m_ProgressBar != null)
                m_ProgressBar.SetActive(false);

            m_ResultWindow.SetActive(false);
        }
    }

    IEnumerator SelectWindowActive()
    {
        yield return new WaitForSeconds(1.75f);
        
        if(!m_battleControl.m_Tutorial) {
            m_SelectWindow.SetActive(true);
        }
        
    }

    IEnumerator NeroMove(bool hard)
    {
        Vector3 start;
        Vector3 end;

        Vector3 StageStart = Vector3.zero;
        Vector3 StageEnd = Vector3.zero;

        if(GameManager.Instance.m_Stage == 1.0f) {
            StageStart = m_Stage1Start.position;
            StageEnd = m_Stage1End.position;
        }
        else if(GameManager.Instance.m_Stage == 2.0f) {
            StageStart = m_Stage2Start.position;
            StageEnd = m_Stage2End.position;
        }

        if(!hard)
        {
            start = Vector3.Lerp(StageStart, StageEnd, GameManager.Instance.m_NeroProg - GameManager.Instance.m_Stage - 0.125f);
            end = Vector3.Lerp(StageStart, StageEnd, GameManager.Instance.m_NeroProg - GameManager.Instance.m_Stage);
        }
        else
        {
            start = Vector3.Lerp(StageStart, StageEnd, GameManager.Instance.m_NeroProg - GameManager.Instance.m_Stage - 0.250f);
            end = Vector3.Lerp(StageStart, StageEnd, GameManager.Instance.m_NeroProg - GameManager.Instance.m_Stage);
        }

        float lerpValue = 0.0f;
        yield return null;

        while(lerpValue < 1.0f)
        {
            m_NeroPos.position = Vector3.Lerp(start, end, lerpValue);
            
            yield return null;
            
            lerpValue += Time.deltaTime;
        }

        m_NeroPos.position = Vector3.Lerp(start, end, 1.0f);

        float value = Random.value;

        

        if(value < 0.6f)
        {
            //전투
            m_ResultWindow.SetActive(true);
            m_ResultWindow.GetComponent<ResultWindow>().Encount();

            if(GameManager.Instance.m_Stage == 1.0f)
            {
                if(GameManager.Instance.m_NeroProg == GameManager.Instance.m_Stage + 1.0f)
                {
                    if(m_BackAudio.clip != m_Stage1BattleBack) {
                        m_BackAudio.clip = m_Stage1BattleBack;
                        m_BackAudio.Play();
                    }
                }
                else
                {
                    if(m_BackAudio.clip != m_Stage1Boss) {
                        m_BackAudio.clip = m_Stage1Boss;
                        m_BackAudio.Play();
                    }
                }
            }
            else if(GameManager.Instance.m_Stage == 2.0f)
            {
                if(GameManager.Instance.m_NeroProg == GameManager.Instance.m_Stage + 1.0f)
                {
                    if(m_BackAudio.clip != m_Stage2BattleBack) {
                        m_BackAudio.clip = m_Stage2BattleBack;
                        m_BackAudio.Play();
                    }

                }
                else
                {
                    if(m_BackAudio.clip != m_Stage2Boss) {
                        m_BackAudio.clip = m_Stage2Boss;
                        m_BackAudio.Play();
                    }
                }
            }
            

            yield return new WaitForSeconds(2.5f);
            
            m_battleControl.BattleStart();
            StartCoroutine("BattleEndCheck");
            m_ProgressBar.SetActive(false);
            m_ResultWindow.SetActive(false);
        }
        else if(value < 0.8f)
        {
            //함정
            m_ResultWindow.SetActive(true);
            m_ResultWindow.GetComponent<ResultWindow>().Trap();
            
            yield return new WaitForSeconds(1.0f);
            
            m_battleControl.Trap();

            yield return new WaitForSeconds(2.0f);
            m_ResultWindow.SetActive(false);
            m_SelectWindow.SetActive(true);
        }
        else
        {
            // 보물
            m_ResultWindow.SetActive(true);
            m_ResultWindow.GetComponent<ResultWindow>().Treasure();
            
            yield return new WaitForSeconds(1.0f);
            
            m_battleControl.Treasure();

            yield return new WaitForSeconds(2.0f);
            m_ResultWindow.SetActive(false);
            m_SelectWindow.SetActive(true);
        }

    }
    
    IEnumerator HeroMove(bool hard)
    {
        Vector3 start;
        Vector3 end;

        if(!hard) {
            start = Vector3.Lerp(m_Stage2Start.position, m_Stage2End.position, GameManager.Instance.m_HeroProg - 2.0f - 0.03125f);
            end = Vector3.Lerp(m_Stage2Start.position, m_Stage2End.position, GameManager.Instance.m_HeroProg - 2.0f);
        }
        else {
            start = Vector3.Lerp(m_Stage2Start.position, m_Stage2End.position, GameManager.Instance.m_HeroProg - 2.0f - 0.0625f);
            end = Vector3.Lerp(m_Stage2Start.position, m_Stage2End.position, GameManager.Instance.m_HeroProg - 2.0f);
        }

        float lerpValue = 0.0f;
        yield return null;

        while(lerpValue < 1.0f)
        {
            m_HeroPos.position = Vector3.Lerp(start, end, lerpValue);

            yield return null;

            lerpValue += Time.deltaTime;
        }

        m_NeroPos.position = Vector3.Lerp(start, end, 1.0f);
    }

    IEnumerator BattleEndCheck()
    {
        Monster monster = m_battleControl.m_Monster;
        bool end = true; 

        while(monster.m_Hp > 0.0f)
        {
            end = true;
            for(int i = 0; i < 4; i++)
            {
                Card card = GameManager.Instance.GetParty(i);

                if(card != null) {
                    end = false;
                }
            }


            if(end) {
                Debug.Log("Battle Is End");
                break;
            }

            yield return new WaitForEndOfFrame();
            //yield return null;
        }

        if(end)
        {
            Debug.Log("Battle End Process");
            //yield return new WaitForSeconds(1.0f);
            //m_ResultWindow.SetActive(true);
            //m_ResultWindow.GetComponent<ResultWindow>().Lose();

            //yield return new WaitForSeconds(2.5f);
            //m_ResultWindow.SetActive(false);
            //m_SelectWindow.SetActive(false);

            // 마을로 가자
            //m_battleControl.Lose();
            GameManager.Instance.m_Lost = true;

            if(m_battleControl.m_Tutorial) {
                GameManager.Instance.m_TownTut = true;
            }

            GameObject.Find("Canvas").GetComponent<SceneSwitch>().SetNextScene("Town");
            GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("SceneEnd");

            yield return new WaitForSeconds(1.5f);

        }
        else
        {
            if(monster.m_Name.Equals("Lupus")) {
                GameManager.Instance.m_ClearLupus = true;
                GameManager.Instance.m_Stage = 2.0f;
            }
            else if(monster.m_Name.Equals("Succubus")) {
                GameManager.Instance.m_ClearSuccubus = true;
            }
            else if(monster.m_Name.Equals("Shoa")) {
                GameManager.Instance.m_ClearShoa = true;
            }
            else if(monster.m_Name.Equals("God")) {
                GameManager.Instance.m_ClearGod = true;
            }

            yield return new WaitForSeconds(1.0f);
            m_ResultWindow.SetActive(true);
            m_ResultWindow.GetComponent<ResultWindow>().Win();
            m_battleControl.Win();

            m_EffectAudio.PlayOneShot(m_Win);

            yield return new WaitForSeconds(3.0f);
            m_ResultWindow.SetActive(false);

            m_SelectWindow.SetActive(true);
            m_ProgressBar.SetActive(true);

            if(GameManager.Instance.m_Stage == 1.0f)
            {
                if(m_BackAudio.clip != m_Stage1Back) {
                    m_BackAudio.clip = m_Stage1Back;
                    m_BackAudio.Play();
                }
            }
            else if(GameManager.Instance.m_Stage == 2.0f)
            {
                if(m_BackAudio.clip != m_Stage2Back) {
                    m_BackAudio.clip = m_Stage2Back;
                    m_BackAudio.Play();
                }
            }
        }
    }
}
