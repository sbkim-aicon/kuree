using UnityEngine;
using System.Collections;

public class FadeAniCotroller : MonoBehaviour {

    public AllUIController allUIControll;
    public Animation ani;

    public string changeUIName = "";

    void Start ()
    {
	
	}
	
    public void ChangeUI()
    {
        if(changeUIName == "chat")
        {
            allUIControll.OpenChat();
        }
        else if(changeUIName == "adventure")
        {
            allUIControll.OpenAdventure();
        }
        else if(changeUIName == "activity")
        {
            allUIControll.OpenActivity();
        }
    }

	void Update ()
    {
	
	}
}
