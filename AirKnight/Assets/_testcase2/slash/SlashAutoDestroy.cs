using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAutoDestroy : MonoBehaviour
{
    public void DestroySelf()
    {
        Debug.Log("DestroySelf ...");
        Destroy(gameObject);
    }
}
