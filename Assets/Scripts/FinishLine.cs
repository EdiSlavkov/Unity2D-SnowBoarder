using Assets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem finishEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Utils.PlayerTag))
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            StartCoroutine(DelayReloadingScene());
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(Utils.FirstLevel);
    }
    
    private IEnumerator DelayReloadingScene()
    {
        yield return new WaitForSeconds(loadDelay);
        ReloadScene();
    }
}