using UnityEngine;

public class LeftRight2D : MonoBehaviour
{
    private float platformSpeed = 2f;
    private bool endPoint;

    private void Update()
    {
        if (endPoint)
        {
            transform.position += Vector3.right * platformSpeed * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right*platformSpeed * Time.deltaTime;
        }

        if (transform.position.x > 3.25f)
        {
            endPoint = false;

        }

        if (transform.position.x < -3.25f)
        {
            endPoint = true;
        }
        
        
        
    }
}
