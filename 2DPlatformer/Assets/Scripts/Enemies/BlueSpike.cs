using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpike : MonoBehaviour
    {
    public float amplitude;
    public float speed;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start() {
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        float sin = Mathf.Sin(Time.time * speed) * amplitude;
        float cos = Mathf.Cos(Time.time * speed) * amplitude;
        this.transform.position = new Vector2(initialPosition.x + sin, initialPosition.y + cos);
    }
}
