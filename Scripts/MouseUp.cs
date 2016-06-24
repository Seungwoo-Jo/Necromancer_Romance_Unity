using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseUp : MonoBehaviour, IPointerUpHandler
{
    public bool m_UseEvent = true;
    
    public AudioSource m_PlayAudioSource;
    public AudioClip m_MouseUpSound;

    public UnityEvent m_MouseUp;

	void Start ()
    {
        m_UseEvent = true;
	}

    public void OnPointerUp(PointerEventData eventData)
    {
        if(m_UseEvent)
        {
            if(m_PlayAudioSource != null &&
            m_MouseUpSound != null) {
                m_PlayAudioSource.clip = m_MouseUpSound;
                m_PlayAudioSource.Play();
            }

            //Debug.Log(gameObject.name + " Mouse Up");
            m_MouseUp.Invoke();
        }
    }
}
