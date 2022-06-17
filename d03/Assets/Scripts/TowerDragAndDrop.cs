using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class TowerDragFinishedEvent : UnityEvent<GameObject, Vector2> { }

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class TowerDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private TowerDragFinishedEvent onTowerDragFinished;
	[SerializeField] private GameObject towerPrefab;
	
	[Header("UI")]
	[SerializeField] private TMP_Text waitLabel;
	[SerializeField] private TMP_Text powerLabel;
	[SerializeField] private TMP_Text rangeLabel;
	[SerializeField] private TMP_Text energyLabel;
	
	private Color enabledColor = Color.white;
	private Color disabledColor = Color.red;
	
	private bool buyAvailable;
	private bool onDrag;
	private Vector2 startingPosition;
	private towerScript towerScript;

	private RectTransform rectTransform;
	private Image image;
	
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		image = GetComponent<Image>();
        
		towerScript = towerPrefab.GetComponent<towerScript>();
		waitLabel.text = towerScript.fireRate.ToString();
		powerLabel.text = towerScript.damage.ToString();
		rangeLabel.text = towerScript.range.ToString();
		energyLabel.text = towerScript.energy.ToString();
	}

	private void Start()
	{
		startingPosition = rectTransform.position;
	}

	private void Update()
	{
		if (onDrag)
		{
			rectTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.back * 10;
		}
		if (gameManager.gm.playerEnergy >= towerScript.energy)
		{
			image.color = enabledColor;
			buyAvailable = true;
		}
		else
		{
			image.color = disabledColor;
			buyAvailable = false;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
    {
	    if (!buyAvailable || uiManager.onDrag)
		    return;
	    uiManager.onDrag = true;
	    onDrag = true;
    }

	public void OnPointerUp(PointerEventData eventData)
    {
	    if (!buyAvailable)
		    return;
	    uiManager.onDrag = false;
	    onDrag = false;
	    rectTransform.position = startingPosition;
	    onTowerDragFinished?.Invoke(towerPrefab, eventData.position);
    }
}
