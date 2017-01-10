using UnityEngine;
using System.Collections;

public class KureeAdventureAniController : MonoBehaviour {

    public GameObject[] aniObj;
    public ANIMATION_STATE aniState;
    public GAF.Core.GAFMovieClip movieClip;

    GameObject aniStateObj;

    public enum ANIMATION_STATE
    {
        Normal = 0,
        Normal_talk,
        Stop,
        Walk,
        Listen,
        Look_aheadup,
        Look_around,
        Look_down,
        Look_up,
        Point_aheadup,
        Point_around,
        Point_down,
        Point_up
    };

    void Start ()
    {
        aniStateObj = aniObj[0];
        movieClip = aniStateObj.GetComponent<GAF.Core.GAFMovieClip>();
    }
	
    public void AniStateChange(ANIMATION_STATE state)
    {
        aniState = state;
        aniStateObj.SetActive(false);
        switch (aniState)
        {
            case ANIMATION_STATE.Normal: //0
                aniStateObj = aniObj[0];
                break;
            case ANIMATION_STATE.Normal_talk: //1
                aniStateObj = aniObj[1];
                break;
            case ANIMATION_STATE.Stop: //2
                aniStateObj = aniObj[2];
                break;
            case ANIMATION_STATE.Walk: //3
                aniStateObj = aniObj[3];
                break;
            case ANIMATION_STATE.Listen: //4
                aniStateObj = aniObj[4];
                break;
            case ANIMATION_STATE.Look_aheadup: //5
                aniStateObj = aniObj[5];
                break;
            case ANIMATION_STATE.Look_around: //6
                aniStateObj = aniObj[6];
                break;
            case ANIMATION_STATE.Look_down: //7
                aniStateObj = aniObj[7];
                break;
            case ANIMATION_STATE.Look_up: //8
                aniStateObj = aniObj[8];
                break;
            case ANIMATION_STATE.Point_aheadup: //9
                aniStateObj = aniObj[9];
                break;
            case ANIMATION_STATE.Point_around: //10
                aniStateObj = aniObj[10];
                break;
            case ANIMATION_STATE.Point_down: //11
                aniStateObj = aniObj[11];
                break;
            case ANIMATION_STATE.Point_up: //12
                aniStateObj = aniObj[12];
                break;
        }
        movieClip = aniStateObj.GetComponent<GAF.Core.GAFMovieClip>();
		movieClip.gotoAndPlay (1);
        aniStateObj.SetActive(true);
    }

    void Update ()
    {
        /*if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("walk");
            AniStateChange(ANIMATION_STATE.Walk);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("normal");
            AniStateChange(ANIMATION_STATE.Normal);
        }*/

        if(!movieClip.isPlaying())
        {
            if(aniState != ANIMATION_STATE.Normal && aniState != ANIMATION_STATE.Walk && aniState != ANIMATION_STATE.Listen)
            {
                AniStateChange(ANIMATION_STATE.Normal);
            }
        }
    }
}
