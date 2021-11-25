using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public Transform camPos;
    public Vector3 offset;
    public float smooth = 0.3f;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        offset = camPos.position - target.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targgetPosition = target.position + offset;
        camPos.position = Vector3.SmoothDamp(camPos.position, targgetPosition,ref velocity ,smooth);
    }
}
