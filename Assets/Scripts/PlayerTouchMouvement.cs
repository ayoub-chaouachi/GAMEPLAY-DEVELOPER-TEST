using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMouvement : MonoBehaviour
{
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;


    private void Update()
    {
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
             
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
        if (fingerDown)
        {
            
            if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("MOVED LEFT");
              

            }
           
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("MOVED RIGHT");
              

            }
        }
    }
}
