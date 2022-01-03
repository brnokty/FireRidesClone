using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform target;
    [SerializeField] private BlockController blockController;
    [SerializeField] private int moveDistance = 7;

    #endregion

    #region Private Variables

    private Vector3 targetPos;
    private Tween lineTween;
    private HingeJoint joint;
    private int lastColliderIndex = 0;

    #endregion

    #region Unity Methods

    void Start()
    {
        ResetLine();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (Input.GetMouseButtonDown(0))
        {
            ResetLine();
            target = blockController.GetNearHook(transform);
            lineTween = DOTween.To(() => targetPos, x => targetPos = x, target.position, .1f).OnComplete(() =>
            {
                joint = gameObject.AddComponent<HingeJoint>();
                joint.connectedBody = target.GetComponent<Rigidbody>();
                joint.axis = new Vector3(0, 0, 1);
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = Vector3.zero;
                joint.anchor = target.position - transform.position;
                var motor = joint.motor;
                motor.force = 300f;
                motor.targetVelocity = 150f;
                motor.freeSpin = false;
                joint.motor = motor;
                joint.useMotor = true;
                // print("target-" + target.position + "\nPlayer-" + transform.position + "\nLocal-" +
                //       (target.position - transform.position));
            });
            _rigidbody.isKinematic = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineTween.Kill();
            ResetLine();
        }


        if (Input.GetMouseButton(0))
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, targetPos);

            if (joint != null)
            {
                joint.anchor = target.position - transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            var block = other.transform.parent.GetComponent<Block>();
            if (lastColliderIndex < block.index)
            {
                lastColliderIndex = block.index;
                if (block.index >= moveDistance)
                {
                    blockController.RefreshBlocks();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Block"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #endregion

    #region Private Methods

    private void ResetLine()
    {
        targetPos = transform.position;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);

        if (joint != null)
            Destroy(joint);
    }

    #endregion
}