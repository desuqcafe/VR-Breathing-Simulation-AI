using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaPlant : MonoBehaviour
{

    private Animator mAnimator;

    public int alti;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            mAnimator.SetInteger("alti", alti);
        }
    }
}
