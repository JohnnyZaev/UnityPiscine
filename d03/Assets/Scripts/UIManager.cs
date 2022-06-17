using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static bool onDrag = false;
    
	[SerializeField] private new Camera camera;

	private RectTransform _towerRadialMenuRectTransform;
	
	public void SpawnTower(GameObject prefab, Vector2 screenPosition)
	{
		var worldPosition = camera.ScreenToWorldPoint(screenPosition);
		worldPosition.z = 0;
		gameManager.gm.SpawnTower(prefab, worldPosition);
	}
}
