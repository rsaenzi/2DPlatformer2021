using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public float amplitude;
    public float speed;

    Vector3 initialScale;


    void Start() {
        initialScale = this.transform.localScale;
    }

    void Update() {
        float sin = Mathf.Sin(Time.time * speed) * amplitude;
        this.transform.localScale = new Vector3(initialScale.x + sin, initialScale.y + sin);
    }
}