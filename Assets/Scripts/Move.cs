using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Move : MonoBehaviour
{
    public PlayableDirector playableDirector;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player"){
            playableDirector.Play();
        }
    }
}