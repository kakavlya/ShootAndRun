using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Crosshair : MonoBehaviour
{
    public event UnityAction OnShoot;

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out Enemy enemy))
                {
                    OnShoot?.Invoke();
                    enemy.GetHit();
                }
            }
        }
    }
}
