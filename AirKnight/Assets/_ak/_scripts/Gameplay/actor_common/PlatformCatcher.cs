
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformCatcher : MonoBehaviour
{
    List<GameObject> attachItems = new List<GameObject>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryAttach(collision);
    }

    
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     Debug.LogWarning("[Stay2D]");
    //     TryAttach(collision);
    // }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TryDetach(collision);
    }
    private void TryAttach(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (attachItems.IndexOf(other) < 0)
        {
            attachItems.Add(other);
            other.transform.parent = transform;
        }
    }

    private void TryDetach(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        int index = attachItems.IndexOf(other);
        if (index >= 0)
        {
            attachItems.RemoveAt(index);
            other.transform.parent = null;
        }
    }

}
