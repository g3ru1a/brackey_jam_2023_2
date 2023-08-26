using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionDuration = 1f;


    public void LoadLevel(string name){
        StartCoroutine(LoadLevelAsync(name));
    }

    IEnumerator LoadLevelAsync(string name)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(name);
    }
}
