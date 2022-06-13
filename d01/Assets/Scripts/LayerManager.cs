using UnityEngine;

public class LayerManager : MonoBehaviour
{
	private Rigidbody2D _playerRb;
	[SerializeField] private int[] ignoredLayers;

	[SerializeField] private int myLayer;

	private void Start()
	{
		foreach (var layer in ignoredLayers)
		{
			Physics2D.IgnoreLayerCollision(myLayer, layer, true);
		}
	}
}
