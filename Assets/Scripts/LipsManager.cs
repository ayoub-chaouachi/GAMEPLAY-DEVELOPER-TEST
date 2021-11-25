using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipsManager : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth;
    public bool isCollactable;
    private float velocity = 0;
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        isCollactable = true;
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
        if (collision.gameObject.tag == "Lips")
        {
            if (collision.gameObject.GetComponent<LipsManager>().isCollactable)
            {
                GameManager.Instance.linkedPlayers.Add(collision.gameObject.transform);
                collision.gameObject.GetComponent<LipsManager>().offset = new Vector3(0, 0, GameManager.Instance.linkedPlayers.Count * 2f);
                collision.gameObject.GetComponent<LipsManager>().smooth = GameManager.Instance.linkedPlayers.Count * 0.1f;
                collision.gameObject.GetComponent<LipsManager>().isCollactable = false;
                Debug.Log("Collect");
                anim.SetBool("run", true);
            }

        }
    }
}
