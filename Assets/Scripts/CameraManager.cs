using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector2 limitMax = Vector2.zero;
    [SerializeField] private Vector2 limitMin = Vector2.zero;

    private bool isMoveMode = false;

    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isMoveMode = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMoveMode = false;
        }

        if (isMoveMode)
        {
            direction = lastMousePosition - Input.mousePosition;
            direction.z = direction.y;
            direction.y = 0;

            targetPosition = transform.position + direction * speed * Time.deltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, limitMin.x, limitMax.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, limitMin.y, limitMax.y);

            transform.position = targetPosition;

            lastMousePosition = Input.mousePosition;
        }
    }
}
