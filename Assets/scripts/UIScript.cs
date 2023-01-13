using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public GameObject DeathMenu;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DeathMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
