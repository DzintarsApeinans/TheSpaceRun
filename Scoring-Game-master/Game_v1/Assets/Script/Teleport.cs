﻿using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public int code;

    private float disableTimer = 0f;
    private Vector3 dir;

    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {//foreach loop and if condition that looks for a next teleport in play field with same code and which isnt itself
            foreach (Teleport tp in FindObjectsOfType<Teleport>())
            {
                if (tp.code == code && tp != this && disableTimer <= 0)
                {
                    tp.disableTimer = 0.1f;

                    Vector3 pos = tp.gameObject.transform.position;

                    collider.gameObject.transform.position = pos;
                }
            }
        }
    }
}
