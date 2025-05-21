using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovin : MonoBehaviour
{
    Movin samurai;

    private void Start () {

        samurai = new Movin(transform, "json/data");
        samurai.Play();
    }
}
