using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform target;
    [SerializeField] private BlockController blockController;
    private Vector3 targetPos;
    private Tween lineTween;
    private Joint joint;

    void Start()
    {
        ResetLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetLine();
            target = blockController.GetNearHook(transform);
            lineTween = DOTween.To(() => targetPos, x => targetPos = x, target.position, .1f).OnComplete(() =>
            {
                joint = gameObject.AddComponent<HingeJoint>();
                joint.autoConfigureConnectedAnchor = false;
                // joint.anchor = new Vector3(0, Vector3.Distance(transform.position, target.position), 0);
                print("target-" + target.position + "\nPlayer-" + transform.position + "\nLocal-" +
                      (target.position - transform.position));
                joint.anchor = target.position - transform.position;
                joint.axis = new Vector3(0, 0, 1);
                joint.connectedBody = target.GetComponent<Rigidbody>();
                joint.connectedAnchor = Vector3.zero;
                joint.connectedMassScale = 10f;
            });
            _rigidbody.isKinematic = false;
        }

        if (Input.GetMouseButton(0))
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, targetPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineTween.Kill();
            ResetLine();
        }
    }


    private void ResetLine()
    {
        targetPos = transform.position;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);

        if (joint != null)
            Destroy(joint);
    }
}