using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingSpeed : Activity
{
    Text mSpeedText;
    Slider mSpeedSlider;

    public float InterpSpeed { get; private set; }

    const float MinSpeed = 0.5f;
    const float MaxSpeed = 8.0f;
    const float InterpStep = 2.0f;

    public PlayingSpeed(GameObject gameObject)
        : base(gameObject)
    {

    }

    public override void OnOpen()
    {
        base.OnOpen();

        mSpeedText = GetComponent<Text>("Text");
        mSpeedSlider = GetComponent<Slider>("");

        SetSpeed(InterpSpeed = UserManager.Instance.LoggedOnUser.ScrollSpeed);
    }

    public override void Update()
    {
        base.Update();

        var userSpeed = UserManager.Instance.LoggedOnUser.ScrollSpeed;
        if (InterpSpeed < userSpeed)
        {
            InterpSpeed += InterpStep * Time.deltaTime;
            if (InterpSpeed > userSpeed)
                InterpSpeed = userSpeed;
        }
        else if (InterpSpeed > userSpeed)
        {
            InterpSpeed -= InterpStep * Time.deltaTime;
            if (InterpSpeed < userSpeed)
                InterpSpeed = userSpeed;
        }

        CheckInput();
    }

    private void CheckInput()
    {
        if (InputManager.Instance.HasMoveUp())
        {
            var user = UserManager.Instance.LoggedOnUser;
            SetSpeed(user.ScrollSpeed + 0.5f);
        }

        if (InputManager.Instance.HasMoveDown())
        {
            var user = UserManager.Instance.LoggedOnUser;
            SetSpeed(user.ScrollSpeed - 0.5f);
        }
    }

    public void SetSpeed(float speed)
    {
        speed = Mathf.Min(Mathf.Max(MinSpeed, speed), MaxSpeed);
        UserManager.Instance.LoggedOnUser.ScrollSpeed = speed;

        mSpeedText.text = speed.ToString();
        mSpeedSlider.value = (speed - MinSpeed) / (MaxSpeed - MinSpeed);
    }
}
