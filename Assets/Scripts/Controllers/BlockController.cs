using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks = new List<Block>();
    [SerializeField] private Vector2 yMinMax = new Vector2(-0.2f, 0.2f);
    [SerializeField] Color ColorOne = Color.blue;
    [SerializeField] Color ColorTwo = Color.cyan;
    private int lastBlock = 0;

    void Start()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].index % 2 == 0)
            {
                _blocks[i].SetColor(ColorOne);
            }
            else
            {
                _blocks[i].SetColor(ColorTwo);
            }
        }
    }
    

    public Transform GetNearHook(Transform player)
    {
        float minDistance = 1000f;
        Transform nearHookPoint = null;
        var pos = player.position;
        pos.x += 3.4f;


        for (int i = 0; i < _blocks.Count; i++)
        {
            var distance = Vector3.Distance(_blocks[i].hookPoint.position, pos);
            if (distance <= minDistance)
            {
                nearHookPoint = _blocks[i].hookPoint;
                minDistance = distance;
            }
        }

        return nearHookPoint;
    }

    private void RefreshBlocks()
    {
        var block = _blocks[lastBlock];
        block.index += _blocks.Count;

        var pos = block.transform.position;
        pos.y = Random.Range(yMinMax.x, yMinMax.y);
        pos.x = block.index * 1.5f;
        block.transform.position = pos;

        lastBlock++;

        if (lastBlock >= _blocks.Count)
            lastBlock = 0;
    }
}