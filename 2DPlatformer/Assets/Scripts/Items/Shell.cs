using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision) {

        // Shell collider with Ghost or Red Turtle
        if (collision.gameObject.tag == "Ghost" || collision.gameObject.tag == "RedTurtle") {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        // Shell collider with Spikes
        if (collision.gameObject.tag == "YellowSpike" || collision.gameObject.tag == "BlueSpike") {
            Destroy(this.gameObject);
        }
    }
}