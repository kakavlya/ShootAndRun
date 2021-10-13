using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableObject : MonoBehaviour
{

    public abstract void GetHit();

    protected IEnumerator DieAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
