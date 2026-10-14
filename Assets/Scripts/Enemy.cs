using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemy {
    public class Enemy : MonoBehaviour {
        public GameObject hantei;
        public GameObject mitame;
        public void hitted()
        {
            hantei.SetActive(true);
            Destroy(mitame);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}