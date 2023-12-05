using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float camSpeed;

    public float maxDistance;
    private bool center;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!center || Vector2.Distance(target.position, transform.position) > maxDistance)
        {

            Vector2 position = Vector2.Lerp(transform.position, target.position, camSpeed * Time.deltaTime);
            transform.position = new Vector3(position.x, position.y, transform.position.z);
            center = false;

        }

        if (Vector2.Distance(target.position, transform.position) < 1)
        {
            center = true;

        }
    }
}
