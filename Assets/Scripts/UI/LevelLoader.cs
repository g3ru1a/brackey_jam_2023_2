using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionDuration = 1f;


    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            LoadNextLevel();
        }
    }

    void LoadNextLevel(){
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int buildIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(buildIndex);
    }
}
