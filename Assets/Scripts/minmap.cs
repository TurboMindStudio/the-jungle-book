using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minmap : MonoBehaviour
{
    public Transform player;
    public float YPos;
    public void LateUpdate()
    {
        Vector3 camOffset = new Vector3(0, YPos, 0);
        this.transform.position = player.position+camOffset;
    }
}
