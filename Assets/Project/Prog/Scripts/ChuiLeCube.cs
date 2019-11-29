﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuiLeCube : MonoBehaviour {

    [SerializeField] List<GameObject> _faces;

    Rigidbody _rig;

    [SerializeField] float _rayDuration;
    [SerializeField] float _rayLenght;
    [SerializeField] float _force;
    [SerializeField] float _shift;
    [Range(-1.0f, 1.0f)] [SerializeField] float _backDash;

    [SerializeField] GameObject _rebondFX;

    Vector3 _speed;

    public bool _detect;

    private void OnEnable()
    {
        _rig = GetComponent<Rigidbody>();
        _shift /= 100;
    }

    private void Update()
    {
        _detect = false;

        foreach (var item in _faces)
        {
            float zMove;

            if (item.name == "Front" || item.name == "Back")
            {
                zMove = -2 *_backDash;
            }
            else
            {
                zMove = _backDash;
            }

            RaycastHit hit = new RaycastHit();
            List<GameObject> touchedItem = new List<GameObject>();

            if (Physics.Raycast(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(item.transform.lossyScale.x / 2 - _shift, item.transform.lossyScale.x / 2 - _shift, zMove)),
                item.transform.forward, out hit, _rayLenght))
            {
                if(hit.collider.GetComponentInParent<AssignFaceColorByTag>() != null)
                {
                    touchedItem.Add(hit.collider.GetComponentInParent<AssignFaceColorByTag>().gameObject);
                }
            }
            Debug.DrawRay(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(item.transform.lossyScale.x / 2 - _shift, item.transform.lossyScale.x / 2 - _shift, zMove)),
                item.transform.forward * _rayLenght, Color.red, _rayDuration);

            if (Physics.Raycast(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(-item.transform.lossyScale.x / 2 + _shift, item.transform.lossyScale.x / 2 - _shift, zMove)),
                item.transform.forward, out hit, _rayLenght))
            {
                if (hit.collider.GetComponentInParent<AssignFaceColorByTag>() != null)
                {
                    touchedItem.Add(hit.collider.GetComponentInParent<AssignFaceColorByTag>().gameObject);
                }
            }
            Debug.DrawRay(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(-item.transform.lossyScale.x / 2 + _shift, item.transform.lossyScale.x / 2 - _shift, zMove)),
                item.transform.forward * _rayLenght, Color.red, _rayDuration);

            if (Physics.Raycast(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(item.transform.lossyScale.x / 2 - _shift, -item.transform.lossyScale.x / 2 + _shift, zMove)),
                item.transform.forward, out hit, _rayLenght))
            {
                if (hit.collider.GetComponentInParent<AssignFaceColorByTag>() != null)
                {
                    touchedItem.Add(hit.collider.GetComponentInParent<AssignFaceColorByTag>().gameObject);
                }
            }
            Debug.DrawRay(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(item.transform.lossyScale.x / 2 - _shift, -item.transform.lossyScale.x / 2 + _shift, zMove)),
                item.transform.forward * _rayLenght, Color.red, _rayDuration);

            if (Physics.Raycast(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(-item.transform.lossyScale.x / 2 + _shift, -item.transform.lossyScale.x / 2 + _shift, zMove)),
                item.transform.forward, out hit, _rayLenght))
            {
                if (hit.collider.GetComponentInParent<AssignFaceColorByTag>() != null)
                {
                    touchedItem.Add(hit.collider.GetComponentInParent<AssignFaceColorByTag>().gameObject);
                }
            }
            Debug.DrawRay(item.transform.position + item.transform.InverseTransformDirection(
                new Vector3(-item.transform.lossyScale.x / 2 + _shift, -item.transform.lossyScale.x / 2 + _shift, zMove)),
                item.transform.forward * _rayLenght, Color.red, _rayDuration);

            int goodtouch = new int();

            if (touchedItem.Count == 4)
            {
                if (touchedItem[0] == touchedItem[1])
                {
                    if (touchedItem[0] == touchedItem[2])
                    {
                        if (touchedItem[0] == touchedItem[3])
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (touchedItem[i].CompareTag(item.tag))
                                {
                                    goodtouch++;
                                }
                            }

                            if (goodtouch == 4)
                            {
                                transform.position = touchedItem[0].transform.position + touchedItem[0].transform.forward.normalized / 4;

                                Vector3 v3 = transform.rotation.eulerAngles;

                                if(v3.y < 90 && v3.y > -90)
                                {
                                    v3.y = 0;
                                }
                                else
                                {
                                    v3.y = 180;
                                }

                                transform.rotation = Quaternion.Euler(v3.x, v3.y, v3.z);

                                _detect = true;
                                Debug.Log("hit");
                                _rig.velocity = new Vector3(0, 0, 0) - item.transform.forward * _force;

                                Destroy(Instantiate(_rebondFX, item.transform.position, item.transform.rotation), 3f);
                                switch (item.tag)
                                {
                                    case "Green":

                                        Shader.SetGlobalFloat("Color_Trail", 1);
                                        break;

                                    case "Red":
                                        Shader.SetGlobalFloat("Color_Trail", 2);
                                        break;

                                    case "White":
                                        Shader.SetGlobalFloat("Color_Trail", 9);
                                        break;

                                    case "Blue":
                                        Shader.SetGlobalFloat("Color_Trail", 6);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
 