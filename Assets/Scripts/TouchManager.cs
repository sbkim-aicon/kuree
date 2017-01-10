using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TouchManager : MonoBehaviour {

    public Camera targetCam;
    public UnityEvent onTouch;

    private void isButtonDown()
    {
        
    }

    private void isButtonUp()
    {
        if(onTouch != null)
            onTouch.Invoke();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GetRayCastHit_Obj();

            if (obj != null && obj.Equals(gameObject))
            {
                isButtonDown();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject obj = GetRayCastHit_Obj();

            if (obj != null && obj.Equals(gameObject))
            {
                isButtonUp();
            }
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                GameObject obj = GetRayCastHit_Obj();

                if (obj != null && obj.Equals(gameObject))
                {
                    isButtonDown();
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                GameObject obj = GetRayCastHit_Obj();

                if (obj != null && obj.Equals(gameObject))
                {
                    isButtonUp();
                }
            }
        }
#endif
    }

    private GameObject GetRayCastHit_Obj()
    {
        RaycastHit hit;
#if UNITY_EDITOR
        Ray ray = targetCam.ScreenPointToRay(Input.mousePosition);
#elif UNITY_IOS || UNITY_ANDROID
        Vector2 pos = Input.GetTouch(0).position;
        Vector3 touchPos = new Vector3(pos.x, pos.y, 0f);
        Ray ray = targetCam.ScreenPointToRay(touchPos);
#endif

        Physics.Raycast(ray, out hit, Mathf.Infinity);
        
        if (hit.transform == null)
            return null;

        return hit.transform.gameObject;
    }
}
