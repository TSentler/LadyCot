
using UnityEngine;

public class Samurai : MonoBehaviour {
    
    Movin samurai;

    void Start () {

        samurai = new Movin(transform, "json/t1");
        samurai.Play();

    }
}
