using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSoundEvent : MonoBehaviour {

    public Toggle toggleBtn;
    public AudioSource audioSource;
    public AudioClip onClip, offClip;

	void Start ()
    {
	
	}

    public void PlaySound()
    {
        if(toggleBtn.isOn)
            audioSource.clip = onClip;
        else
            audioSource.clip = offClip;
        audioSource.Play();
    }
	
	void Update ()
    {
	
	}
}
