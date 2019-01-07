using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipSlot : Activity
{
    PlayingStage mPlayingStage;
    ChipType mChipType;
    Transform mChipTemplate;
    Dictionary<Chip, Transform> mChipNodeMap = new Dictionary<Chip, Transform>();
    Queue<Chip> mActiveChips = new Queue<Chip>();

    public ChipSlot(GameObject go, PlayingStage playingStage, ChipType chipType)
        : base(go)
    {
        mPlayingStage = playingStage;
        mChipType = chipType;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        mChipTemplate = FindChild("Chip");
        if (!mChipTemplate) Debug.LogError("There is no child name: [Chip]", GameObject);
        mChipTemplate.gameObject.SetActive(false); // deactive it for template.
    }

    public override void Update()
    {
        base.Update();

        var firstChip = true;
        mPlayingStage.ForAllChipsDrawing(mChipType, (Chip chip, int index, float drawTime, float utterTime, float adjustPos) =>
        {
            // remove all chips before the first active chip.
            // because the chip are sorted list base on time.
            if (firstChip)
            {
                firstChip = false;
                CheckForOutTimeChip(chip);
            }
            var chipNode = GetChipNode(chip);
            chipNode.localPosition = new Vector3(0, adjustPos, 0);
        });
    }

    private void CheckForOutTimeChip(Chip firstActiveChip)
    {
        while (mActiveChips.Count > 0 && mActiveChips.Peek() != firstActiveChip)
        {
            var removedChip = mActiveChips.Dequeue();
            var chipNode = mChipNodeMap[removedChip];
            chipNode.gameObject.SetActive(false);
        }
    }

    private Transform GetChipNode(Chip chip)
    {
        var retNode = (Transform)null;
        if (mChipNodeMap.TryGetValue(chip, out retNode))
            return retNode;

        // first try to find a disabled node to reuse.
        for (var i = 0; i < mChipTemplate.parent.childCount; i++)
        {
            var childNode = mChipTemplate.parent.GetChild(i);
            if (!childNode.gameObject.activeSelf)
            {
                retNode = childNode;
                break;
            }
        }

        // create a new node base on the template.
        if (retNode == null)
            retNode = Object.Instantiate(mChipTemplate.gameObject, mChipTemplate.parent).transform;

        // active this node now.
        retNode.gameObject.SetActive(true);

        // setup barline number if this chip is a bar line.
        if (chip.ChipType == ChipType.BarLine)
            retNode.Find("Text").GetComponent<Text>().text = chip.BarNumber.ToString();

        // register this chip.
        mChipNodeMap[chip] = retNode;
        mActiveChips.Enqueue(chip);
        return retNode;
    }
}
