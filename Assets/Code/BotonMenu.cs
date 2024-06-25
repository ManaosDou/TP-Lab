using UnityEngine.SceneManagement;
using UnityEngine;

public class BotonMenu : MonoBehaviour
{
    public void VolverAMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
