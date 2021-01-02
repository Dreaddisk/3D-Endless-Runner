using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    public float lifeTime = 5f;
    private float startY;
    #endregion


    private void Start()
    {
        startY = transform.position.y;
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    IEnumerator TurnOffBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.MONSTER_TAG || other.tag == Tags.PLAYER_TAG || other.tag == Tags.MONSTER_BULLET_TAG
            || other.tag == Tags.PLAYER_BULLET_TAG)
        {
            gameObject.SetActive(false);
        }
    }




} // Bullet class
