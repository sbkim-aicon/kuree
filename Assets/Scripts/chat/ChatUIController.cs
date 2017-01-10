using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChatUIController : MonoBehaviour {

    public AllUIController allUIcontroll;
    public KureeChatAniController kureeChatAni;

    public Animation bagUIAni, mapUIAni;
    public Animation[] bubbleBtnAni;
    public Toggle toggleBagBtn, toggleMapBtn, toggleMikeBtn, toggleBubbleTestBtn;

    public GameObject panelBubbleBtns;

    int aniState = 0;

    void Start ()
    {
	
	}

    public void BagUIAniEvent()
    {
        if(toggleBagBtn.isOn)
        {
            bagUIAni.Play("bagUIAni_in");
        }
        else
        {
            bagUIAni.Play("bagUIAni_out");
        }
    }

    public void MapUIAniEvent()
    {
        allUIcontroll.FadeChangUI("adventure");

        if (toggleMapBtn.isOn)
        {
            //mapUIAni.Play("mapUIAni_in");
        }
        else
        {
            //mapUIAni.Play("mapUIAni_out");
        }
    }

    public void MikeToggleButton()
    {
        if(toggleMikeBtn.isOn)
        {
            kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Litsen);
        }
        else
        {
            kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Normal);
        }
    }

    public void BubbleTestToggleButton()
    {
        if(toggleBubbleTestBtn.isOn)
        {
            panelBubbleBtns.SetActive(true);
        }
        else
        {
            bubbleBtnAni[0].Play("bubbleBtn01Ani_out");
            bubbleBtnAni[1].Play("bubbleBtn02Ani_out");
            bubbleBtnAni[2].Play("bubbleBtn03Ani_out");
            bubbleBtnAni[3].Play("bubbleBtn04Ani_out");

            StartCoroutine(ClosePanelBubbleBtns());
        }
    }

    public IEnumerator ClosePanelBubbleBtns()
    {
        yield return new WaitForSeconds(0.3f);
        panelBubbleBtns.SetActive(false);
    }

    public void KureeAniChangeButton()
    {
        if (kureeChatAni.kureeAni.IsPlaying("emo_normal"))
        {
            aniState++;
            if (aniState == 7)
                aniState = 1;

            if (aniState == 1)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Dubious);
            else if (aniState == 2)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Excied);
            else if (aniState == 3)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Fear);
            else if (aniState == 4)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Happy);
            else if (aniState == 5)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.No);
            else if (aniState == 6)
                kureeChatAni.AniStateChange(KureeChatAniController.ANIMATION_STATE.Yes);
        }
    }
	
	void Update ()
    {
	
	}
}
