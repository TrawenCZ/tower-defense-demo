using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MusicHandler : MonoBehaviour
{
    private void Awake()
    {
        int result = FindObjectsOfType<MusicHandler>().Length;
        if (result >= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // object u are added to
        }
    }

}