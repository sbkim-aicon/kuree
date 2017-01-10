using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActivityController : MonoBehaviour {

    public AllUIController allUIControll;

    public GameObject popupEnd;
    public GameObject[] btnSnake;
    public GameObject[] imgNum;
    public Sprite[] imgNumSprite;
    public GameObject imgEffect;

    int findSnakesNum = 0;

	void Start ()
    {
	    
	}

    public void ActivityInit()
    {
        findSnakesNum = 0;
        popupEnd.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            btnSnake[i].SetActive(true);
            imgNum[i].SetActive(false);
        }
    }

    public void SnakeButton( GameObject btn )
    {
        findSnakesNum++;
        int num = 0;
        if (btn.name == "btn_snakeA")
            num = 1;
        else if (btn.name == "btn_snakeB")
            num = 2;
        else if (btn.name == "btn_snakeC")
            num = 3;

        imgEffect.GetComponent<RectTransform>().localPosition = btn.GetComponent<RectTransform>().localPosition;
        btn.SetActive(false);
        StartCoroutine(SnakeBtnEvent(num));
    }

    public IEnumerator SnakeBtnEvent( int num )
    {
        imgEffect.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        imgEffect.SetActive(false);
        imgNum[num - 1].GetComponent<Image>().sprite = imgNumSprite[findSnakesNum - 1];
        imgNum[num-1].SetActive(true);

        if (findSnakesNum == 3)
        {
            yield return new WaitForSeconds(0.5f);
            popupEnd.SetActive(true);
        }
    }

    public void PopupEndButton()
    {
        allUIControll.FadeChangUI("adventure");
    }
	
	void Update ()
    {
	    
	}
}
