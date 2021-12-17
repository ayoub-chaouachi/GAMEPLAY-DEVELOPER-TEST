using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
     private GameObject Player;
     [SerializeField] float bounceForce;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == Player)
        {
            Player.GetComponent<Rigidbody>().AddForce(Vector3.back * bounceForce);
        }
    }

}
