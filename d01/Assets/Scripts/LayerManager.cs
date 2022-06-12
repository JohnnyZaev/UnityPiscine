using UnityEngine;

public class LayerManager : MonoBehaviour
{
	private Rigidbody2D _playerRb;
	[SerializeField] private int[] ignoredLayers;

	[SerializeField] private int myLayer;
	private const int PlayerLayer = 3;

	public void Ignore()
	{
		foreach (var layer in ignoredLayers)
		{
			Physics2D.IgnoreLayerCollision(PlayerLayer, layer, true);
		}
		Physics2D.IgnoreLayerCollision(PlayerLayer, myLayer, false);
	}
}
