using UnityEngine;

public class MouseTarget : MonoBehaviour
{
	private RaycastHit _hit;
	private Ray _ray;
	private Vector3 _mouse;

    public bool isTarget;

    public AliveObject target;
    private Camera _camera;

    public void Start()
    {
	    _camera = Camera.main;
	    _mouse = new Vector3();
    }
    
    // Update is called once per frame
    private void Update()
    {
        _mouse = Input.mousePosition;
        _mouse.z = 10;
        _ray = _camera.ScreenPointToRay(_mouse);
        if (!Physics.Raycast(_ray, out _hit)) return;
        if (_hit.transform.gameObject.CompareTag("EnemyObject"))
        {
	        GamaManager.gm.target(_hit.transform.gameObject.GetComponent<AliveObject>());
        }
        else if (isTarget)
        {
	        GamaManager.gm.target(target);
	        if (target.hp > 0) return;
	        isTarget = false;
	        target = null;
        }
        else 
        {
	        GamaManager.gm.targetViewEnemy(false);
        }
    }
}
