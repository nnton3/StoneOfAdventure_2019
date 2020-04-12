using StoneOfAdventure.Artifacts;
using UnityEngine;

public class LocationCompletedSignal { }
public class PlayerStartNextLevel { }

public class LocationPointsUpdated
{
    public int currentValue;
}

public class LocationPointsTargetValueUpdated
{
    public int targetValue;
}

public class PlayerStartWalk { }
public class PlayerStopWalk { }

public class BrokenClockTriggered
{
    public float reducedTime;
}

public class ArtifactSelected
{
    public GameObject art;
}
