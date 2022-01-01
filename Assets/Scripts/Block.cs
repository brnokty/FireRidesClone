using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _renderers = new List<MeshRenderer>();

    public Transform hookPoint;
    [SerializeField] Color ColorOne = Color.blue;
    [SerializeField] Color ColorTwo = Color.cyan;
    public int index = 0;

    void Start()
    {
        if (index % 2 == 0)
        {
            SetColor(ColorOne);
        }
        else
        {
            SetColor(ColorTwo);
        }
    }


    private void SetColor(Color color)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material.color = color;
        }
    }
}