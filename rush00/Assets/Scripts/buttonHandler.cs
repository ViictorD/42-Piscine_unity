using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour {

    // Use this for initialization
    public string next_scenes;
    public string titleMenu;
    public UnityEngine.UI.Button backToTitleButton, quitButton, startButton, restartButton;

	void Start () {
        if (quitButton)
            quitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Application has quit"); });
        if (startButton)
            startButton.onClick.AddListener(() => {UnityEngine.SceneManagement.SceneManager.LoadScene(next_scenes); Debug.Log("Next Level was Loaded"); });
        if (restartButton)
        {
            Debug.Log("Button assigned");
            restartButton.onClick.AddListener(() => {UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel); Debug.Log("Next Level was Loaded"); });
        }
        if (backToTitleButton)
            backToTitleButton.onClick.AddListener(() => {UnityEngine.SceneManagement.SceneManager.LoadScene(titleMenu); Debug.Log("Next Level was Loaded"); });

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
