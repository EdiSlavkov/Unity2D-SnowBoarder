using Assets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private AudioClip crashSFX;
    private bool hasCrashed = false;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Utils.GroundTag) && !hasCrashed)
        {
            hasCrashed = true;
            GetComponent<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
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