using UnityEngine;
using System.Collections;

public class DoorUnlockable : DoorScript {

    public int ProgressIndex;
    private bool Locked = false;

    new void Start()
    {
        base.Start();
        if (GameManager.Instance.GetProgressIndex() > ProgressIndex)
            LockUnlockDoor(true);
        else
            LockUnlockDoor(false);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.GetProgressIndex() > ProgressIndex)
            LockUnlockDoor(true);
        else
            LockUnlockDoor(false);

        base.OnTriggerEnter2D(col);
    }

    private void LockUnlockDoor(bool Key)
    {
        ReadInput = Key;
        Locked = Key;
    }

    public bool GetLockStatus()
    {
        return Locked;
    }
}
