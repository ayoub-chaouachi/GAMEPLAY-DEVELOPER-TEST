using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LipsManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject panel;
    public Vector3 offset;
    public float smooth;
    public bool isCollactable;
    public static LipsManager instance;
    public GameObject[] prefabs;
    private float velocity = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isCollactable = true;
        anim = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    public void Follow()
    {
        if (!isCollactable)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocity, smooth), targetPosition.y, targetPosition.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            panel.SetActive(true);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Lips")
        {
            
            LipsManager Lips = collision.gameObject.GetComponent<LipsManager>();
            anim.SetBool("bounce", true);
            if (Lips.isCollactable)
            {
                
                Lips.anim.SetBool("run", true);
                
                GameManager.Instance.LinkedPlayers.Add(collision.gameObject.transform);
                Lips.offset = new Vector3(0, 0, GameManager.Instance.LinkedPlayers.Count * 1f);
                Lips.smooth = GameManager.Instance.LinkedPlayers.Count * 0.1f;
                Lips.isCollactable = false;
                


            }
            
            
        }
        if (collision.gameObject.tag == "Enemey")
        {
            GameManager.Instance.detachFromBody(this.transform);

        }
        if (collision.gameObject.tag == "Wall")
        {
            GameManager.Instance.DestroyObject(this.transform);


        }
        
    }
      private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Gate")
        {
            
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (i == prefabs.Length-1)
                {
                    break;
                }
                if (prefabs[i].activeSelf)
                 {
                     prefabs[i].SetActive(false);
                     prefabs[i + 1].SetActive(true);
                     return;
                 }

                
            }
            
        }




    }
   
}
