using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;
    public bool HasKeys { get; set; }

    private void Awake()
    {
	    if (Instance == null)
		    Instance = this;

	    HasKeys = false;
    }
}
