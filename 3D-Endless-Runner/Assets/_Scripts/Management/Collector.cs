using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLATFORM_TAG || other.tag == Tags.HEALTH_TAG || other.tag == Tags.MONSTER_TAG)
        {
            other.gameObject.SetActive(false);
        }
    }
}
