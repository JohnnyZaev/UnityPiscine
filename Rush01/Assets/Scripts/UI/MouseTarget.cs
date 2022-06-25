using UnityEngine;

public class MouseTarget : MonoBehaviour
{

    
    
    private RaycastHit hit;

    private Ray ray;

    private Vector3 mouse;

    public bool isTarget;

    public AliveObject target;
    private Camera _camera;

    public void Start()
    {
	    _camera = Camera.main;
	    mouse = new Vector3();
    }
    
    // Update is called once per frame
    private void Update()
    {
        mouse = Input.mousePosition;
        mouse.z = 10;
        ray = _camera.ScreenPointToRay(mouse);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("EnemyObject"))
            {
                GamaManager.gm.target(hit.transform.gameObject.GetComponent<AliveObject>());
            }
            else if (isTarget)
            {
                GamaManager.gm.target(target);
                if (target.hp <= 0)
                {
                    isTarget = false;
                    target = null;
                }
            }
            else 
            {
                GamaManager.gm.targetViewEnemy(false);
            }
        }
    }
}
