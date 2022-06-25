using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("TestSceneGame", LoadSceneMode.Single);
        }
    }
}
