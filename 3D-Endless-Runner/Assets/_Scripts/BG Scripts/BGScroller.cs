using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    #region Variables
    public float offsetSpeed = -0.0006f;
    private Renderer MyRenderer;

    [HideInInspector]
    public bool canScroll;
    #endregion

    private void Awake()
    {
        MyRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(canScroll)
        {
            MyRenderer.material.mainTextureOffset -= new Vector2(offsetSpeed, 0)
;        }
    }
}
