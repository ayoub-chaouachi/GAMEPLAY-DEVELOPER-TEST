using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Text level;
    
    void Start()
    {
        level.text = "" + GameManager.Instance.getLevelIndex();
    }

   
    void Update()
    {
        
    }
}
