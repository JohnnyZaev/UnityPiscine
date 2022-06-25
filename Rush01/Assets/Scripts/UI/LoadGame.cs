using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
	private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("TestSceneGame", LoadSceneMode.Single);
        }
    }
}
