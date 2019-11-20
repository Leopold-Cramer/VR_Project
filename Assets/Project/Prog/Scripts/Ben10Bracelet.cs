﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using VRTK.Controllables.ArtificialBased;

public class Ben10Bracelet : MonoBehaviour {

    VRTK_ArtificialPusher _pusher;
    VRTK_ArtificialRotator _rotator;

    Animator _animator;

    float _timer;

    bool _opening;

    [SerializeField] string[] _tags = new string[4];

    void Start()
    {
        _pusher = GetComponentInChildren<VRTK_ArtificialPusher>();
        _rotator = GetComponentInChildren<VRTK_ArtificialRotator>();

        //_animator = GetComponent<Animator>();

        _pusher.MinLimitExited += (object sender, ControllableEventArgs e) =>
        {
            //_animator.SetTrigger("Open");
        };

        _rotator.ValueChanged += (object sender, ControllableEventArgs e) =>
        {
            float a = Mathf.Atan2(_rotator.transform.up.y, _rotator.transform.up.z);
            a += Mathf.PI;
            a /= 2f * Mathf.PI;
            a = Mathf.RoundToInt(a * 100f);

            //Debug.Log(a);

            int quotient = Mathf.RoundToInt(a / 25);

            //Debug.Log(quotient);

            if(quotient == 4)
            {
                CubeInstanciater.Instance.enabled = false;
                CubeRemover.Instance.enabled = true;
            }
            else
            {
                CubeInstanciater.Instance.enabled = true;
                CubeRemover.Instance.enabled = false;
                CubeInstanciater.Instance.ChangeSelectedCube(_tags[quotient]);
            }
        };
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 4; x += 1)
        {
            for (float z = 0; z < lenght; z += size)
            {
                var point = ;
                Gizmos.DrawSphere(point, new Vector3(.2f, .2f, .2f));
            }
        }
    }*/
}
