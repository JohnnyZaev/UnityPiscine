using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetectLight : MonoBehaviour
{
	[SerializeField] private Image detection;
	public float currentDetection;

	private void Update()
	{
		detection.fillAmount = currentDetection / 100;
		if (currentDetection > 0)
			currentDetection -= 10 * Time.deltaTime;
		if (currentDetection >= 100)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
