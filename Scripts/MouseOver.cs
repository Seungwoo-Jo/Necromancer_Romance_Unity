using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseOver : MonoBehaviour, IPointerEnterHandler
{
    public bool m_UseEvent = true;

    public AudioSource m_PlayAudioSource;
    public AudioClip m_MouseOverSound;

    public UnityEvent m_MouseOver;

    void Start()
    {
        m_UseEvent = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_UseEvent)
        {
            if(m_PlayAudioSource != null &&
                m_MouseOverSound != null)
            {
                m_PlayAudioSource.clip = m_MouseOverSound;
                m_PlayAudioSource.Play();
            }

            //Debug.Log(gameObject.name + " Mouse Over");
            m_MouseOver.Invoke();
        }
    }
}

