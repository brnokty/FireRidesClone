using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockController : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private List<Block> _blocks = new List<Block>();
    [SerializeField] private Vector2 yMinMax = new Vector2(-5f, 5f);
    [SerializeField] private Vector2 yDistanceMinMax = new Vector2(-0.2f, 0.2f);
    [SerializeField] Color ColorOne = Color.blue;
    [SerializeField] Color ColorTwo = Color.cyan;
    [SerializeField] float nearDistance = 2.4f;

    #endregion

    #region Private Variables

    private int lastBlock = 0;

    #endregion

    #region Unity Methods

    void Start()
    {
        StartAlign();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            RefreshBlocks();
        }
    }

    #endregion

    #region Public Methods

    public Transform GetNearHook(Transform player)
    {
        float minDistance = 1000f;
        Transform nearHookPoint = null;
        var pos = player.position;
        pos.x += nearDistance;


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

    public void RefreshBlocks()
    {
        var block = _blocks[0];
        block.index += _blocks.Count;
        _blocks.RemoveAt(0);
        _blocks.Add(block);

        var pos = block.transform.position;
        pos.y = _blocks[_blocks.Count - 2].transform.position.y + Random.Range(yDistanceMinMax.x, yDistanceMinMax.y);
        pos.x = (block.index - 1) * 1.5f;
        block.transform.position = pos;

        // lastBlock++;
        //
        // if (lastBlock >= _blocks.Count)
        //     lastBlock = 0;
    }

    #endregion

    #region Private Methods

    private void StartAlign()
    {
        _blocks[0].index = 0;
        for (int i = 1; i < _blocks.Count; i++)
        {
            var block = _blocks[i];
            block.index = i;
            var pos = block.transform.position;
            do
            {
                pos.y = _blocks[i - 1].transform.position.y + Random.Range(yDistanceMinMax.x, yDistanceMinMax.y);
            } while (pos.y < yMinMax.x || pos.y > yMinMax.y);

            block.transform.position = pos;
        }
    }

    #endregion
}