using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        if (speed == 0)
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        else
        {
            transform.Rotate(new Vector3(15, 30, 45) * (Time.deltaTime * speed));
        }
    }
}
