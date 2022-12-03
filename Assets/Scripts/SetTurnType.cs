using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SetTurnType : MonoBehaviour
{

    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    public void SetTypeFromIndex (int index)
    {
        if(index ==0)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
        else
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
    }
}
