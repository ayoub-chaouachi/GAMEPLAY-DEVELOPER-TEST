using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipsManager : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth;
    public bool isCollactable;
    public GameObject panel;
    public Animator anim;
  

    private GameObject LipsColor1;
    private GameObject LipsColor2;
    private GameObject LipsColor3;
    private GameObject LipsColor4;
    private float velocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        isCollactable = true;
    
        LipsColor1 = transform.GetChild(4).GetChild(1).gameObject;
        LipsColor2 = transform.GetChild(4).GetChild(2).gameObject;
        LipsColor3 = transform.GetChild(4).GetChild(3).gameObject;
        LipsColor4 = transform.GetChild(4).GetChild(4).gameObject;
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
            transform.position = new Vector3 (Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocity, smooth), 0.57f, targetPosition.z);
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
            if (collision.gameObject.GetComponent<LipsManager>().isCollactable)
            {
                GameManager.Instance.LinkedPlayers.Add(collision.gameObject.transform);
                collision.gameObject.GetComponent<LipsManager>().offset = new Vector3(0, 0, GameManager.Instance.LinkedPlayers.Count * 2f);
                collision.gameObject.GetComponent<LipsManager>().smooth = GameManager.Instance.LinkedPlayers.Count * 0.1f;
                collision.gameObject.GetComponent<LipsManager>().isCollactable = false;
               
                anim.SetBool("run", true);
            }

        }
        /*if (collision.gameObject.tag == "Enemey")
        {
            GameManager.Instance.detachFromBody(this.transform);
        }*/
        if (collision.gameObject.tag == "Wall")
        {
            GameManager.Instance.DestroyObject(this.transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Gate")
        {
           

            if (LipsColor1.activeSelf)
            {
                LipsColor1.SetActive(false);
                LipsColor2.SetActive(true);
                return;
            }
            if (LipsColor2.activeSelf)
            {
                LipsColor2.SetActive(false);
                LipsColor3.SetActive(true);
                return;

            }
            if (LipsColor3.activeSelf)
            {
                LipsColor3.SetActive(false);
                LipsColor4.SetActive(true);
                return;

            }
        }
    }
   



}
