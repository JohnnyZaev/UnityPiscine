using UnityEngine;

public class camera_follow : MonoBehaviour {

	public GameObject charRoot;
	private Vector3 offset;

	private void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
		transform.position = charRoot.transform.position + offset;
	}
}
