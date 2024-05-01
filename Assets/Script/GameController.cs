using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static CameraShake camShake;

    private void Awake()
    {
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }
}
