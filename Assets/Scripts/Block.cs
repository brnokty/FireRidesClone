using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private List<MeshRenderer> _renderers = new List<MeshRenderer>();
    public Transform hookPoint;

    #endregion

    #region Public Variables

    [HideInInspector] public int index = 0;

    #endregion

    #region Public Methods

    public void SetColor(Color color)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material.color = color;
        }
    }

    #endregion
}