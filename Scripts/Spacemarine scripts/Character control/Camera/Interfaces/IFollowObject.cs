using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows the current object
/// </summary>
public interface IFollowObject
{
    float FollowLerpSpeed { get; set; }
    Transform ObjectToFollow { get; set; }
    void FollowObject(float time);
}
