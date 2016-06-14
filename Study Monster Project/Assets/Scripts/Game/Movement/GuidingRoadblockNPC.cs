using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GuidingRoadblockNPC : NPCController {

    public int ProgressIndex;

    public BasicMovements DeltaMovement;
    public Vector3 DeltaPosition;

    void Start()
    {
        if(GameManager.Instance.GetProgressIndex() > ProgressIndex)
        {
            transform.position += DeltaPosition;
        }
    }

    public override List<string> Interact()
    {
        if(GameManager.Instance.GetProgressIndex() == ProgressIndex)
        {
            GameManager.Instance.IncrementProgress();
            PerformMove(DeltaMovement);
        }
        return base.Interact();
    }

}
