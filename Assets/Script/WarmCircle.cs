using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmCircle : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        var g = other.gameObject;
        if (g.CompareTag("Ice"))
        {
            var ice = g.GetComponent<Ice>();
            ice.warmSpeed = Vector3.Distance(ice.transform.position, transform.position) * .001f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var g = other.gameObject;
        if (g.CompareTag("Ice"))
        {
            var ice = g.GetComponent<Ice>();
            ice.warmSpeed = 0;
        }
    }
}
