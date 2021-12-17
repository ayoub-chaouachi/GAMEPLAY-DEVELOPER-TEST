using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Text level;
    // Start is called before the first frame update
    void Start()
    {
        level.text = "" + GameManager.Instance.getLevelIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
