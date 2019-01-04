using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchStage : Stage
{
    public override void OnOpen()
    {
        base.OnOpen();

        StartCoroutine(DelayOpen());
    }

    IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(1.0f);

        StageManager.Instance.Open<TitleStage>();

        Close();
    }
}
