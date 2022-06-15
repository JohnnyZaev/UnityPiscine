using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private string playerUnitsLayer;
    [SerializeField] private string enemyTargetLayer;
    
    [SerializeField] private new Camera camera;

    private readonly List<UnitRTS> _currentSelection = new List<UnitRTS>();
    private readonly Collider2D[] _selectionCache = new Collider2D[5];
    
    private LayerMask _playerUnitsLayerMask;
    private LayerMask _enemyTargetLayerMask;

    private void Start()
    {
        _playerUnitsLayerMask = LayerMask.GetMask(playerUnitsLayer);
        _enemyTargetLayerMask = LayerMask.GetMask(enemyTargetLayer);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPointNonAlloc(worldPoint, _selectionCache, _enemyTargetLayerMask) > 0)
            {
                HealthSystem target = null;
                foreach (var t in _selectionCache)
                {
	                if (t
	                    && ((1 << t.gameObject.layer) & _enemyTargetLayerMask) != 0
	                    && t.TryGetComponent<HealthSystem>(out target))
	                {
		                break;
	                }
                }
                _currentSelection.ForEach(x => x.UpdateTarget(target));
            }
            else if (Physics2D.OverlapPointNonAlloc(worldPoint, _selectionCache, _playerUnitsLayerMask) > 0)
            {
	            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
                {
                    _currentSelection.Clear();
                }

	            foreach (var t in _selectionCache)
	            {
		            if (!t || !t.TryGetComponent<UnitRTS>(out var unit)) continue;
		            _currentSelection.Add(unit);
		            unit.Select();
		            break;
	            }
            }
            else
            {
                _currentSelection.ForEach(x =>
                {
                    if (x)
                    {
                        x.UpdateTargetPosition(worldPoint);
                    }
                });
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _currentSelection.Clear();
        }
    }
}
