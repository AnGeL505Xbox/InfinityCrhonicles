using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{

    public void MainMenuController(int index)
    {
        SceneManager.LoadScene(index);
    }
}
