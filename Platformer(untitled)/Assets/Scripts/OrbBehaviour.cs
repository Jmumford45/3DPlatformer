using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
