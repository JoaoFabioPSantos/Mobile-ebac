using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLegacy : MonoBehaviour
{
    public Animation animation;

    public AnimationClip run;
    public AnimationClip idle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (animation.clip == idle)
            {
                PlayAnimation(run);
            }
            else
            {
                PlayAnimation(idle);
            }
        }
        /*em vez de animation.Play(), ele aceita também:
        *animation.Play(run.ToString());
        *animation.[run.ToString()].animationTime;
        */
    }

    private void PlayAnimation(AnimationClip c)
    {
        //crossfade faz com q n precise disso animation.clip = c;
        //crossfade é um play que integra aos poucos o novo clip
        animation.CrossFade(c.name);
    }
}
