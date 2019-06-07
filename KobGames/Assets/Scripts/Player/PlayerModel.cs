using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour,IModel
{


    [SerializeField]
    private CharacterStates _CharacterStates;
    [SerializeField]
    private CharacterData _CharData;
    public CharacterData GetCharData()
    {
        return _CharData;
    }

    public void UpdateState(CharacterStates _State)
    {
        switch (_State)
        {
            case CharacterStates.Running:
                break;
        }
    }
}
