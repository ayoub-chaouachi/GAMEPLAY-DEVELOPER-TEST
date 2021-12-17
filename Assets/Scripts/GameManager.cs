using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> LinkedPlayers;
    public float detachRange;
    int levelIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        levelIndex = PlayerPrefs.GetInt("LevelIndex",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
        levelIndex++;
        PlayerPrefs.SetInt("LevelIndex", levelIndex);
    }
    public int getLevelIndex()
    {
        return levelIndex;
    }

    public void detachFromBody(Transform transformObj)
    {
        List<Transform> objectToRemove = new List<Transform>(LinkedPlayers.GetRange(LinkedPlayers.IndexOf(transformObj), LinkedPlayers.Count - LinkedPlayers.IndexOf(transformObj)));
        LinkedPlayers.RemoveRange(LinkedPlayers.IndexOf(transformObj), LinkedPlayers.Count - LinkedPlayers.IndexOf(transformObj));
        foreach (Transform t in objectToRemove)
        {
            t.gameObject.GetComponent<LipsManager>().isCollactable = true;
            t.gameObject.GetComponent<Animator>().SetBool("run", false);
            t.gameObject.GetComponent<Animator>().SetBool("bounce", false);
            DropToRAndom(t);

        }
       
    }
    public void DropToRAndom(Transform t)
    {
       int randomx = Random.Range(3, -11);
        int randomz = Random.Range(4, 20);
        Vector3 fixedPos = new Vector3(randomx, t.transform.position.y, (t.transform.position.z+randomz));
        t.position = fixedPos;
    }
    public void DestroyObject(Transform t)
    {
        List<Transform> objectToDestroy = new List<Transform>(LinkedPlayers.GetRange((LinkedPlayers.IndexOf(t)), LinkedPlayers.Count - (LinkedPlayers.IndexOf(t))));
        LinkedPlayers.RemoveRange((LinkedPlayers.IndexOf(t) ), LinkedPlayers.Count - (LinkedPlayers.IndexOf(t)));
        foreach (Transform tran in objectToDestroy)
        {
            Destroy(tran.gameObject);
            
        }
       
    }

}
