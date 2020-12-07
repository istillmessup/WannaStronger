using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewMonoBehaviour1 : MonoBehaviour
{
    public void Onclick()
    {
        SceneManager.LoadScene(0);
    }

    public void hint()
    {
        transform.Find("Hint").gameObject.SetActive(true);
    }

    public void closehint()
    {
        transform.Find("Hint").gameObject.SetActive(false);
    }
}
