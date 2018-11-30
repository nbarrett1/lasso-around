using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{


    private Vector3 defaultPosition = new Vector3(0, 1, -100);
    private int defaultSize = 55;

    [SerializeField]
    private Animator camAnimator;

    public Button startButton;




    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        camAnimator.SetTrigger("zoom");

        Invoke("LoadScene", 0.75f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }


}
