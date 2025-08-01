using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class OnNewGame : MonoBehaviour
{
    public bool skipCutscene;
    public bool testingForGameplay;
    public Transform catTransform;
    public GameObject firsCutscene;
    public PlayableDirector firstSceneDirector;
    public GameObject Pet;
    public Camera petCamera;
    public UnityEvent OnNewGameEvents;
    public UnityEvent OnPrevGameEvents;



    
    private void Start()
    {
        petCamera.enabled = false;
        firstSceneDirector.stopped += FirstSceneDirector_stopped;
    }

    private void OnDisable()
    {
        firstSceneDirector.stopped -= FirstSceneDirector_stopped;
    }

    private void FirstSceneDirector_stopped(PlayableDirector obj)
    {
        firstSceneDirector.gameObject.SetActive(false);
        Pet.SetActive(true);
        OnNewGameEvents?.Invoke();
        petCamera.enabled = true;
    }

    private void OnEnable()
    {        
        Invoke("Enable", 0.05f);
    }

    void Enable()
    {
        if (testingForGameplay)
        {
            firstSceneDirector.gameObject.SetActive(false);
            Pet.SetActive(true);
            petCamera.enabled = true;
            OnPrevGameEvents?.Invoke();
            return;
        }
        if (skipCutscene)
        {
            if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnPrevGameEvents?.Invoke();
            }
            else
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnNewGameEvents?.Invoke();
                petCamera.enabled = true;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Tutorial") == 1)
            {
                firstSceneDirector.gameObject.SetActive(false);
                Pet.SetActive(true);
                OnPrevGameEvents?.Invoke();
            }
            else
            {
                Pet.SetActive(false);
                firstSceneDirector.gameObject.SetActive(true);
                firstSceneDirector.Play();
            }

        }
    }
}
