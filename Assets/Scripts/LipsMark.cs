using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipsMark : MonoBehaviour
{
   [SerializeField] GameObject mark;
   [SerializeField] GameObject water;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lips")
        {
            //mark.SetActive(true);
            Vector3 newpos = new Vector3(collision.transform.position.x, collision.transform.position.y + 2.3f, collision.transform.position.z+0.29f);
            Instantiate(mark, newpos, Quaternion.identity);
            Instantiate(water, newpos, Quaternion.identity);
        }
    }
}
