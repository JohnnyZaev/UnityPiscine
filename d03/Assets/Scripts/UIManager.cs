using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static bool onDrag = false;
    
	[SerializeField] private new Camera camera;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject gameMenu;
	[SerializeField] private GameObject towerUpgradeObject;
	[SerializeField] private Texture2D cursor;
	[SerializeField] private TMP_Text hp;
	[SerializeField] private TMP_Text energy;
	[SerializeField] private TMP_Text rank;

	private RectTransform towerRadialMenuRectTransform;
	private TowerUpgrade _towerUpgrade;

	private void Awake()
	{
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
		_towerUpgrade = towerUpgradeObject.GetComponent<TowerUpgrade>();
		towerRadialMenuRectTransform = towerUpgradeObject.GetComponent<RectTransform>();
	}

	public void SpawnTower(GameObject prefab, Vector2 screenPosition)
	{
		var worldPosition = camera.ScreenToWorldPoint(screenPosition);
		worldPosition.z = 0;
		gameManager.gm.SpawnTower(prefab, worldPosition);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			var hit = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("turret"));
			if (hit && hit.transform.TryGetComponent<towerScript>(out var towerScript))
			{
				towerRadialMenuRectTransform.position = camera.WorldToScreenPoint(towerScript.transform.position);
				towerUpgradeObject.SetActive(true);
                
				_towerUpgrade.SetTower(towerScript);
			}
		}
		hp.text = gameManager.gm.playerHp.ToString();
		energy.text = gameManager.gm.playerEnergy.ToString();
		rank.text = gameManager.gm.playerHp switch
		{
			20 when gameManager.gm.playerEnergy > 200 => "Rank: SSS",
			< 20 when gameManager.gm.playerEnergy > 150 => "Rank: S",
			< 15 when gameManager.gm.playerEnergy > 100 => "Rank: A",
			< 10 when gameManager.gm.playerEnergy > 50 => "Rank: B",
			_ => "Rank: C"
		};
		if (!Input.GetKeyDown(KeyCode.Escape)) return;
		pauseMenu.SetActive(true);
		gameManager.gm.pause(true);
		gameMenu.SetActive(false);
	}

	public void Unpause()
	{
		pauseMenu.SetActive(false);
		gameManager.gm.pause(false);
		gameMenu.SetActive(true);
	}
}
