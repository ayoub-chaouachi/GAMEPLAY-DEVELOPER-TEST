using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float speedRL = 6f;
    [SerializeField] float speed = 10f;
    float horizontal;
    Rigidbody rg;
    LipsManager Lips;
    public Animator anim;
    public GameObject panel;
    public static PlayerManager instance;
    public GameObject[] prefabs;
    private Vector3 lastVelocity;
    void Start()

    {
        anim = GetComponent<Animator>();
        Lips = GetComponent<LipsManager>();
        rg = GetComponent<Rigidbody>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();
        PlayerInputAd();
    }
    public void PlayerInput()
    {


       
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
            
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0 && transform.position.x > -11f)
        {
            transform.Translate(Vector3.right * speedRL * Time.deltaTime);
        }
        if (horizontal > 0 && transform.position.x < 3f)
        {
            transform.Translate(Vector3.left * speedRL * Time.deltaTime);
        }
    }
    public void PlayerInputAd()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                    if (touch.position.x < Screen.width / 2 && transform.position.x > -11f)
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }
                    if (touch.position.x > Screen.width / 2 && transform.position.x < 3f)
                    {
                        transform.Translate(Vector3.left * speed * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    break;
            }
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
            collision.gameObject.GetComponent<Animator>().SetBool("bounce", true);
            if (collision.gameObject.GetComponent<LipsManager>().isCollactable)
            {
                collision.gameObject.GetComponent<Animator>().SetBool("run", true);
                GameManager.Instance.LinkedPlayers.Add(collision.gameObject.transform);
                LipsManager lips = collision.gameObject.GetComponent<LipsManager>();
                lips.offset = new Vector3(0, 0, GameManager.Instance.LinkedPlayers.Count * 1f);
                lips.smooth = GameManager.Instance.LinkedPlayers.Count * 0.1f;
                lips.isCollactable = false;

                

            }

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gate")
        {
           
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (i == prefabs.Length - 1)
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
