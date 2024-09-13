using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class MissileStuck : MonoBehaviour

{
    public AudioSource _audio;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            _audio.Play();
        }


    }


}
