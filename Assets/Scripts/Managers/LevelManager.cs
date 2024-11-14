using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class LevelManager : MonoBehaviour
{
    
    [SerializeField] private Animator animator;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    private void Awake()
    {
        if (animator == null)
        {
            animator.enabled = false;
        }
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
        //animator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(sceneName);
        animator.SetTrigger("EndTransition");
    }

}