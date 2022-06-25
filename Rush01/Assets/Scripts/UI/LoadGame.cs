using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("TestSceneGame", LoadSceneMode.Single);
        }
    }
}
