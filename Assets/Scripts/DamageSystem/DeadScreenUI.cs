using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreenUI : MonoBehaviour
{

    public GameObject deadUI;
    public void DeadScreen(){
        deadUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart(){
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
             Destroy(o);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}
