using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Scene_Event : MonoBehaviour
{
public void LoadingSceneCinematic(int _newScene)
    {
        SceneManager.LoadScene(_newScene);
    }
}
