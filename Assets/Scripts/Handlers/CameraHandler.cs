using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private Transform target;
    [SerializeField] private float lerpSpeed = 5f;

    #endregion

    #region Private Variables

    private Vector3 offset;

    #endregion

    #region Unity Methods

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpSpeed * Time.deltaTime);
    }

    #endregion
}