using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 0.0f, -10.0f);

    [SerializeField]
    PlayerController _player = null;

    public void SetPlayer(PlayerController player) { _player = player; }

    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _delta;
    }
}
