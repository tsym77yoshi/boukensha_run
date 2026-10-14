using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace boss
{
    public class Boss : MonoBehaviour
    {
        private int hitpoint;
        public GameObject[] lanes;
        public GameObject[] redlanes;
        public GameObject mitame;
        public Animator ParentAnimator;
        public Animator ModelAnimator;
        private void Start()
        {
            hitpoint = 3;
        }
        public void hitted()
        {
            hitpoint -= 1;
            Debug.Log("HP:" + hitpoint);
            lanes[hitpoint].SetActive(true);
            redlanes[hitpoint].SetActive(false);
            ParentAnimator.SetInteger("Hitted", hitpoint);
            ModelAnimator.SetTrigger("Boss");
            if (hitpoint == 0)
            {
                Destroy(mitame);
                this.GetComponent<BoxCollider>().enabled = false;
            }
            Debug.Log("HP:" + hitpoint);
        }
    }
}