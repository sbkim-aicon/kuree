using UnityEngine;
using System.Collections;

public class AllUIController : MonoBehaviour {

	[SerializeField] private tmp_controller tmpCtr;
	[SerializeField] private AdventureUIController adUICtr;

    public FadeAniCotroller fadeAniControll;
    public ActivityController activityControll;

    public GameObject chatObj, adventureObj, activityObj;

	void Start ()
    {
        InitUI();
    }

    public void InitUI()
    {
        chatObj.SetActive(true);
        adventureObj.SetActive(false);
        activityObj.SetActive(false);
    }

    public void FadeChangUI(string uiName)
    {
		tmpCtr.botText.text = "";
		tmpCtr.userText.text = "";
        fadeAniControll.changeUIName = uiName;
        fadeAniControll.ani.Play();
		adUICtr.Init ();
    }

    public void OpenChat()
    {
        InitUI();
    }

    public void OpenAdventure()
    {
        chatObj.SetActive(false);
        adventureObj.SetActive(true);
        activityObj.SetActive(false);
    }

    public void OpenActivity()
    {
        chatObj.SetActive(false);
        adventureObj.SetActive(false);
        activityObj.SetActive(true);

        activityControll.ActivityInit();
    }

	void Update ()
    {
	    
	}
}
