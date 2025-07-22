using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRestarter : MonoBehaviour
{

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}