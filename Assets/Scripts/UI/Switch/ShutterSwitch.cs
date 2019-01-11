using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterSwitch : Switch
{
    Animation mStageAnim;
    AnimationState mAnimationState;

    public System.Action OnShutterClosed;

    public override void OnOpen()
    {
        base.OnOpen();

        mStageAnim = GetComponent<Animation>("Anim");
        mAnimationState = mStageAnim[mStageAnim.clip.name];
        mAnimationState.normalizedTime = 1;
        mAnimationState.speed = -1.0f;
        mStageAnim.Play(mAnimationState.name);
    }

    public override void Update()
    {
        base.Update();

        if (!mStageAnim.isPlaying)
        {
            if (mAnimationState.speed > 0)
            {
                Close();
            }
            else
            {
                OnShutterClosed?.Invoke();
                // move to the topmost.
                Transform.SetAsLastSibling();

                mAnimationState.speed = 1;
                mStageAnim.Play(mAnimationState.name);
            }
        }
    }
}
