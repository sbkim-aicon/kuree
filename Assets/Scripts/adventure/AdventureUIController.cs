using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdventureUIController : MonoBehaviour {

    public AllUIController allUIControll;
    public KureeAdventureAniController kureeAdventureAni;
    public GameObject kureeObj;
    public Toggle toggleMikeBtn;

    bool bBtnPress = false;
    int aniState = 0;

    void Start ()
    {
	
	}

	public void Init()
	{
		kureeObj.transform.localPosition = new Vector3 (-400, -450, 0);
	}

    public void MikeToggleButton()
    {
        if(toggleMikeBtn.isOn)
        {
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Listen);
        }
        else
        {
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Normal);
        }
    }

    public void BackButton()
    {
        allUIControll.FadeChangUI("chat");
    }

    public void GoRightTestBtnUp()
    {
        bBtnPress = false;
        kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Normal);
    }

    public void GoRightTestBtnDown()
    {
        bBtnPress = true;
        kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Walk);
    }

    public void SnakeTestBtn()
    {
        allUIControll.FadeChangUI("activity");
    }

    public void KureeAniChangeButton()
    {
        aniState++;
        if (aniState == 11)
            aniState = 1;

        if (aniState == 1)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Normal_talk);
        else if (aniState == 2)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Stop);
        else if (aniState == 3)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Look_aheadup);
        else if (aniState == 4)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Look_around);
        else if (aniState == 5)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Look_down);
        else if (aniState == 6)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Look_up);
        else if (aniState == 7)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Point_aheadup);
        else if (aniState == 8)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Point_around);
        else if (aniState == 9)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Point_down);
        else if (aniState == 10)
            kureeAdventureAni.AniStateChange(KureeAdventureAniController.ANIMATION_STATE.Point_up);
    }

    void Update ()
    {
	    if(bBtnPress)
        {
            if(kureeObj.transform.localPosition.x < 2850f)
            {
                Vector3 pos = kureeObj.transform.localPosition;
                pos.x = pos.x + ( Time.deltaTime * 200f );

                kureeObj.transform.localPosition = pos;
            }
        }
	}
}
