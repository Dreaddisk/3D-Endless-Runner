using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSmoke : MonoBehaviour
{
    #region Variables
    public GameObject smokeEffect;
    public GameObject smokePosition;
    #endregion


    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.PLATFORM_TAG)
        {
            if(smokePosition.activeInHierarchy)
            {
                Instantiate(smokeEffect, smokePosition.transform.position, Quaternion.identity);
            }
        }
    }
} // PlayerFootSmoke class
