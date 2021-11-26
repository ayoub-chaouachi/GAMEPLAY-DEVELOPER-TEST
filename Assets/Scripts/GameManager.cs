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

    /*public void detachFromBody(Transform transformObj)
    {
        List<Transform> objectToRemove = new List<Transform>(LinkedPlayers.GetRange(LinkedPlayers.IndexOf(transformObj), LinkedPlayers.Count - LinkedPlayers.IndexOf(transformObj)));
        //make them collectable
        foreach (Transform t in objectToRemove)
        {
            t.gameObject.GetComponent<LipsManager>().isCollactable = true;
            //DropToRAndom(t); this method is suppose to handle the lips parts animation whenever they are hit with an enemy
            // but it doesn't work as expected
        }

        LinkedPlayers.RemoveRange(LinkedPlayers.IndexOf(transformObj), LinkedPlayers.Count - LinkedPlayers.IndexOf(transformObj));
    }*/
    //public void DropToRAndom(Transform t)
    //{
    //    Vector3 randomPos = UnityEngine.Random.insideUnitSphere * detachRange;
    //    Vector3 fixedPos = new Vector3(randomPos.x, 2f, randomPos.z);
    //    t.position = fixedPos;
    //}
    public void DestroyObject(Transform t)
    {
        List<Transform> objectToDestroy = new List<Transform>(LinkedPlayers.GetRange(LinkedPlayers.IndexOf(t), LinkedPlayers.Count - LinkedPlayers.IndexOf(t)));
        LinkedPlayers.RemoveRange(LinkedPlayers.IndexOf(t), LinkedPlayers.Count - LinkedPlayers.IndexOf(t));
        foreach(Transform tran in objectToDestroy)
        {
            Destroy(tran.gameObject);
        }
    }

}
