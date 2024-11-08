using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public LevelManager LevelManager { get; private set; }
    public static GameManager Instance { get; private set; }


    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
            {
                child.gameObject.SetActive(false);
            }
        }
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Camera"));
    }
}