using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetAtCrosshair : MonoBehaviour
{
    public event UnityAction OnShoot;

    private void FixedUpdate()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if(Physics.Raycast(rayOrigin, out hitInfo))
        {
            if(hitInfo.collider != null)
            {
                if(hitInfo.collider.TryGetComponent(out Enemy enemy)){

                    hitInfo.collider.gameObject.SetActive(false);
                    OnShoot?.Invoke();
                }                
            }
        }
    }
}
