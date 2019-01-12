using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPlayGo : Switch
{
    protected override void PlayCloseAnim()
    {
        mAnimationState.normalizedTime = 0;
        mAnimationState.speed = 1.0f;
        mStageAnim.Play(mAnimationState.name);
    }

    protected override void PlayOpenAnim()
    {
        mStageAnim.Play(mAnimationState.name + "_Open");
    }
}
