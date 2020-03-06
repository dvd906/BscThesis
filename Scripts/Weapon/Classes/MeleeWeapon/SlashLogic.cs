using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashLogic : ISlashLogic
{
    float timeToWait;
    ISlashInfo currentSlash;
    List<ISlashInfo> slashes;
    int slashIdx;
    int currentSlashIdx;
    int maxSlash;

    public int CurrentSlashIndex { get { return this.currentSlashIdx; } }

    public float BetweenTimeSlash { get { return this.timeToWait; } }

    public ISlashInfo CurrentSlash { get { return this.currentSlash; } }

    public SlashLogic(float timeToWait, ref List<ISlashInfo> slashes)
    {
        this.slashIdx = 0;
        this.timeToWait = timeToWait;
        this.slashes = slashes;
        this.maxSlash = slashes.Count;
        if (maxSlash > 0)
        {
            currentSlash = this.slashes[slashIdx];
            this.currentSlashIdx = slashIdx;
        }
    }

    public void NextSlash()
    {
        currentSlashIdx = slashIdx;
        currentSlash = slashes[currentSlashIdx];
        slashIdx++;
        if (slashIdx == maxSlash)
        {
            slashIdx = 0;
        }
    }

    public void Reset()
    {
        slashIdx = 0;
        currentSlashIdx = slashIdx;
        currentSlash = slashes[slashIdx];
    }
}
