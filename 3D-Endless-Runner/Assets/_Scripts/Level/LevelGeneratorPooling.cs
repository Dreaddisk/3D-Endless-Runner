using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Transform platform, platform_parent;

    [SerializeField]
    private Transform monster, monster_parent;

    [SerializeField]
    private Transform health_Collectable, health_Collectable_Parent;

    [SerializeField]
    private int levelLength = 100;

    [SerializeField]
    private float distance_Between_Platforms = 15f;

    [SerializeField]
    private float MIN_Position_Y = 0f, MAX_Position_Y = 7f;

    [SerializeField]
    private float chanceForMonsterExistence = 0.25f, chanceForHealthCollectableExistence = 0.1f;

    [SerializeField]
    private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;

    private float platformLastPositionX;

    private Transform[] platform_Array;
    #endregion


    private void Start()
    {
        CreatePlatforms();
    }

    private void CreatePlatforms()
    {
        platform_Array = new Transform[levelLength];

        for (int i = 0; i < platform_Array.Length; i++)
        {
            Transform newPlatform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
            platform_Array[i] = newPlatform;
        }

        for (int i = 0; i < platform_Array.Length; i++)
        {
            float platformPositionY = Random.Range(MIN_Position_Y, MAX_Position_Y);

            Vector3 platformPosition;

            if (i < 2)
            {
                platformPositionY = 0f;
            }

            platformPosition = new Vector3(distance_Between_Platforms * i,
                platformPositionY, 0);

            platformLastPositionX = platformPosition.x;

            platform_Array[i].position = platformPosition;
            platform_Array[i].parent = platform_parent;

            SpawnHealthAndMonster(platformPosition, i, true);
        }
    }

    public void PoolingPlatforms()
    {
        for (int i = 0; i < platform_Array.Length; i++)
        {
            if (!platform_Array[i].gameObject.activeInHierarchy)
            {
                platform_Array[i].gameObject.SetActive(true);

                float platformPositionY = Random.Range(MIN_Position_Y, MAX_Position_Y);

                Vector3 platformPosition = new Vector3(distance_Between_Platforms + platformLastPositionX,
                    platformPositionY, 0);

                platform_Array[i].position = platformPosition;

                platformLastPositionX = platformPosition.x;

                SpawnHealthAndMonster(platformPosition, i, false);
            }

        }
    }

    void SpawnHealthAndMonster(Vector3 platformPosition, int i, bool gameStarted)
    {
        if(i > 2)
        {
            if(Random.Range(0f, 1f) < chanceForMonsterExistence)
            {
                if(gameStarted)
                {
                    platformPosition = new Vector3(distance_Between_Platforms * i,
                        platformPosition.y + 0.1f, 0);
                }
                else
                {
                    platformPosition = new Vector3(distance_Between_Platforms + platformLastPositionX,
                        platformPosition.y + 0.1f, 0);
                }

                Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0));
                createMonster.parent = monster_parent;
            }   // if statement for monster

            if (Random.Range(0f, 1f) < chanceForHealthCollectableExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_Between_Platforms * i,
                        platformPosition.y + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
                }
                else
                {
                    platformPosition = new Vector3(distance_Between_Platforms + platformLastPositionX,
                        platformPosition.y + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
                }

                Transform createHealthCollectable = (Transform)Instantiate(health_Collectable,
                    platformPosition, Quaternion.identity);
                createHealthCollectable.parent = health_Collectable_Parent;
            }
        }
    }







} // LevelGeneratorPooling Class
