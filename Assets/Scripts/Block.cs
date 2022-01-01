using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _renderers = new List<MeshRenderer>();

    public Transform hookPoint;
    public int index = 0;


    public void SetColor(Color color)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material.color = color;
        }
    }
}