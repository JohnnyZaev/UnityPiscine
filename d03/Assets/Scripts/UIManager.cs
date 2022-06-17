using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
	public static bool onDrag = false;
    
	[SerializeField] private new Camera camera;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject gameMenu;
	[SerializeField] private Texture2D cursor;
	[SerializeField] private TMP_Text hp;
	[SerializeField] private TMP_Text energy;

	private RectTransform towerRadialMenuRectTransform;

	private void Awake()
	{
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
	}

	public void SpawnTower(GameObject prefab, Vector2 screenPosition)
	{
		var worldPosition = camera.ScreenToWorldPoint(screenPosition);
		worldPosition.z = 0;
		gameManager.gm.SpawnTower(prefab, worldPosition);
	}

	private void Update()
	{
		hp.text = gameManager.gm.playerHp.ToString();
		energy.text = gameManager.gm.playerEnergy.ToString();
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
