using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float amplitude;
    public float speed;

    Vector3 initialPosition;


    void Start() {
        initialPosition = this.transform.position;
    }

    void Update() {
        float sin = Mathf.Sin(Time.time * speed) * amplitude;
        this.transform.position = new Vector2(initialPosition.x, initialPosition.y + sin);
    }
}