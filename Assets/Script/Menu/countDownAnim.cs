using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countDownAnim : MonoBehaviour {

    TextMeshProUGUI textMesh;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }


    int i = 4;
    int count = 3;

    private void Update()
    {
        if (i > 0)
        {
            if (!AnimatorIsPlaying())
            {
                if (count > 0)
                {
                    textMesh.text = count.ToString();
                    count--;
                }
                else
                {
                    textMesh.text = "GO";
                }

                animator.Play(0);
                i--;
            }
        }
    }


    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

}
