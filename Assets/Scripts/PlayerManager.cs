using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 2f;
    float horizontal;
    Rigidbody rg;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rg.AddForce(Vector3.forward * 25 * Time.deltaTime);
        PlayerInput();
    }
    public void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal<0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if(horizontal>0 )
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lips")
        {
            Debug.Log("qqq");
            if (collision.gameObject.GetComponent<LipsManager>().isCollactable)
            {
                GameManager.Instance.linkedPlayers.Add(collision.gameObject.transform);
                collision.gameObject.GetComponent<LipsManager>().offset = new Vector3(0, 0, GameManager.Instance.linkedPlayers.Count * 2f);
                collision.gameObject.GetComponent<LipsManager>().smooth = GameManager.Instance.linkedPlayers.Count * 0.1f;
                collision.gameObject.GetComponent<LipsManager>().isCollactable = false;
                anim.SetBool("run", true);
            }

        }
    }
}
