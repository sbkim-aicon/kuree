using UnityEngine;
using System.Collections;

public class KureeChatAniController : MonoBehaviour {

    public Material faceMaterial;
    public Texture2D[] faceTexture;
    public AudioSource kureeSound;
    public AudioClip[] kureeSoundClip;
    public Animation kureeAni;
    public ANIMATION_STATE aniState;

    public enum ANIMATION_STATE
    {
        Normal = 0,
        Dubious,
        Excied,
        Fear,
        Happy,
        Litsen,
        No,
        Yes
    };

    void Start ()
    {
	
	}

    public void AniStateChange(ANIMATION_STATE state)
    {
        aniState = state;
        switch (aniState)
        {
            case ANIMATION_STATE.Normal: //0
                kureeSound.clip = kureeSoundClip[0];
                faceMaterial.mainTexture = faceTexture[0];
                kureeAni.Play("emo_normal");
                break;
            case ANIMATION_STATE.Dubious: //1
                kureeSound.clip = kureeSoundClip[1];
                faceMaterial.mainTexture = faceTexture[1];
                kureeAni.Play("emo_dubious");
                break;
            case ANIMATION_STATE.Excied: //2
                kureeSound.clip = kureeSoundClip[2];
                faceMaterial.mainTexture = faceTexture[2];
                kureeAni.Play("emo_excied");
                break;
            case ANIMATION_STATE.Fear: //3
                kureeSound.clip = kureeSoundClip[3];
                faceMaterial.mainTexture = faceTexture[3];
                kureeAni.Play("emo_fear");
                break;
            case ANIMATION_STATE.Happy: //4
                kureeSound.clip = kureeSoundClip[4];
                faceMaterial.mainTexture = faceTexture[4];
                kureeAni.Play("emo_happy");
                break;
            case ANIMATION_STATE.Litsen: //5
                kureeSound.clip = kureeSoundClip[5];
                faceMaterial.mainTexture = faceTexture[5];
                kureeAni.Play("emo_litsen");
                break;
            case ANIMATION_STATE.No: //6
                kureeSound.clip = kureeSoundClip[6];
                faceMaterial.mainTexture = faceTexture[6];
                kureeAni.Play("emo_no");
                break;
            case ANIMATION_STATE.Yes: //7
                kureeSound.clip = kureeSoundClip[7];
                faceMaterial.mainTexture = faceTexture[7];
                kureeAni.Play("emo_yes");
                break;
        }

        kureeSound.Play();
    }

    void Update ()
    {
	    if(!kureeAni.isPlaying)
        {
            if(!kureeAni.IsPlaying("emo_normal"))
            {
                AniStateChange(ANIMATION_STATE.Normal);
            }
        }
	}
}
