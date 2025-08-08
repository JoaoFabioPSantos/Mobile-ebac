using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
   public enum ArtType
    {
        GROUNDS,
        CAVE,
        SPACE,
    }
    public List<ArtSetup> artSetups;

    public ArtSetup GetSetupByType(ArtType artType)
    {
       return artSetups.Find(i => i.artType == artType); 
    }
}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
