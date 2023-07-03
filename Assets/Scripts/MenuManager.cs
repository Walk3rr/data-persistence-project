using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string PlayerName; 

    private void Awake()
    {
        if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    
}
