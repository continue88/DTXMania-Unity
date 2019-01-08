using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPlayingState : System.ICloneable
{
    protected Chip mChip = null;

    public bool Visiable { get; set; } = true;
    public bool Invisiable { get { return !Visiable; } set { Visiable = !value; } }

    public bool Hitted { get; set; } = false;
    public bool NotHitted { get { return !Hitted; } set { Hitted = !value; } }

    public bool Uttered { get; set; } = false;
    public bool InUttered { get { return !Uttered; } set { Uttered = !value; } }


    public ChipPlayingState(Chip chip)
    {
        mChip = chip;
        PreStatus();
    }

    public void PreStatus()
    {
        Visiable = (UserManager.Instance.LoggedOnUser.DrumChipProperty[mChip.ChipType].DisplayChipType != DisplayChipType.Unknown);
        Hitted = false;
        Uttered = false;
    }

    public void HitStatus()
    {
        Visiable = false;
        Hitted = true;
        Uttered = true;
    }

    // IClonable 実装
    public ChipPlayingState Clone()
    {
        return (ChipPlayingState)this.MemberwiseClone();
    }

    object System.ICloneable.Clone()
    {
        return this.Clone();
    }
}
