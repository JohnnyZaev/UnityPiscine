using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class MapEditor : EditorWindow
{
	public static GameObject	LastTile;
	public const int			SizeX = 2;
	public const int			SizeY = 2;

	private static bool					_isActive;

	[Obsolete("Obsolete")]
	static MapEditor()
	{
		SceneView.onSceneGUIDelegate += OnSceneInteract;
	}

	private static void OnSceneInteract(SceneView sceneView)
	{
		var e = Event.current;

		if (!_isActive) return;
		var mousePos = Event.current.mousePosition;
		mousePos.y = sceneView.camera.pixelHeight - mousePos.y;
		var position = sceneView.camera.ScreenPointToRay(mousePos).origin;
		var roundedPosition = new Vector3(Mathf.Round((position.x / SizeX) * SizeX), Mathf.Round((position.y / SizeY) * SizeY), 0);
		switch (e.type)
		{
			case EventType.MouseDown when e.button == 0:
			{
				if (Selection.activeGameObject && Selection.activeGameObject.CompareTag("Tile"))
					LastTile = Selection.activeGameObject;
				if (LastTile)
				{	
					GameObject tile = Instantiate(LastTile, roundedPosition, Quaternion.identity);
					tile.name = LastTile.name;
				}

				break;
			}
			case EventType.MouseDown when e.button == 1:
			{
				GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
				foreach (GameObject tile in tiles)
				{
					if (tile.transform.position == roundedPosition)
						DestroyImmediate(tile);
				}

				break;
			}
		}
	}
	
	[MenuItem("Tools/Map Editor")]	
	public static void	ShowWindow()
	{
		GetWindow(typeof(MapEditor));
	}

	private void OnGUI()
	{
		GUILayout.Label($"Selected tile: {((LastTile) ? LastTile.name : "None")}");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Active Map Editor");
		_isActive = GUILayout.Toggle(_isActive, "");
		EditorGUILayout.EndHorizontal();
		Repaint();
	}

	private void OnInspectorUpdate()
	{
		Repaint ();
	}
}
