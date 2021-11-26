using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 2f;
    float horizontal;
    Rigidbody rg;
    public Animator anim;
    public GameObject panel;
    public static PlayerManager instance;
    
  

    private GameObject LipsColor1;
    private GameObject LipsColor2;
    private GameObject LipsColor3;
    private GameObject LipsColor4;

    private bool IsHitting;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        instance = this;

        LipsColor1 = transform.GetChild(4).GetChild(1).gameObject;
        LipsColor2 = transform.GetChild(4).GetChild(2).gameObject;
        LipsColor3 = transform.GetChild(4).GetChild(3).gameObject;
        LipsColor4 = transform.GetChild(4).GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        PlayerInput();
#endif
#if PLATFORM_ANDROID
        Debug.Log("aaa");
        PlayerInputAd();
#endif
    }
    public void PlayerInput()
    {

        if (!IsHitting)
        {
            rg.AddForce(Vector3.forward * 50 * Time.deltaTime,ForceMode.Acceleration);
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal<0 && transform.position.x>-7f)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if(horizontal>0 && transform.position.x<-0.6f )
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    public void PlayerInputAd()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if(touch.position.x< Screen.width/2 && transform.position.x > -7f)
                    {
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }
                    if (touch.position.x > Screen.width / 2 && transform.position.x < -0.6f)
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
        if (collision.gameObject.tag == "Wall")
        {
            StartCoroutine(HitMe());
            

        }
        if (collision.gameObject.tag == "Lips")
        {
          
            if (collision.gameObject.GetComponent<LipsManager>().isCollactable)
            {
                GameManager.Instance.LinkedPlayers.Add(collision.gameObject.transform);
                collision.gameObject.GetComponent<LipsManager>().offset = new Vector3(0, 0, GameManager.Instance.LinkedPlayers.Count * 2f);
                collision.gameObject.GetComponent<LipsManager>().smooth = GameManager.Instance.LinkedPlayers.Count * 0.1f;
                collision.gameObject.GetComponent<LipsManager>().isCollactable = false;
                collision.gameObject.GetComponent<Animator>().SetBool("run", true);
            }

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

    IEnumerator HitMe()
    {
          IsHitting = true;
          anim.SetBool("Hit", IsHitting);
          rg.AddForce(Vector3.back * 100 * Time.deltaTime, ForceMode.Impulse);
          yield return new WaitForSeconds(0.75f);
          IsHitting = false;
        rg.AddForce(Vector3.forward * 10 * Time.deltaTime);
        yield return new WaitForSeconds(1.25f);
          anim.SetBool("Hit", IsHitting);

    }
  
}
