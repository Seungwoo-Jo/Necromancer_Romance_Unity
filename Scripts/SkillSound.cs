using UnityEngine;
using System.Collections;

public class SkillSound : MonoBehaviour
{
    public AudioSource m_EffectAudio;


	// Use this for initialization
	void Start () {
	
	}
	
    public void PlaySound(AudioClip clip)
    {
        m_EffectAudio.PlayOneShot(clip);
    }
}
