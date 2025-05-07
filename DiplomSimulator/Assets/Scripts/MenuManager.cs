using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class MenuManager : MonoBehaviour
{
    public void LoadEarthquake()
    {
        SceneManager.LoadScene("earthquake");
    }

    public void LoadTornado()
    {
        SceneManager.LoadScene("TornadoScene");
    }

    public void ExitSimulator()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}