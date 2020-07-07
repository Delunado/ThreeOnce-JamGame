using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetButtonDown(LevelControlTags.RESET_LEVEL))
        {
            ResetLevel();
        }

#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }


    public void LoadLevel(string levelName)
    {
        Initiate.Fade(levelName, Color.black, 4f);
    }

    public void ResetLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }
}
