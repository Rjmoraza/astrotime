using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager manager;
    [SerializeField] TMP_Text timerText;
    [SerializeField] string nextLevel;

    float timer = 60;
    bool levelComplete = false;

    void Awake()
    {
        manager.ResetLevel();
    }

    void Start()
    {
        manager.OnCompleteLevel += () => { levelComplete = true; };
        StartCoroutine(LevelProgression());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LevelProgression()
    {
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = string.Format("{0:0.00}", timer);
            yield return null;

            if(levelComplete)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
