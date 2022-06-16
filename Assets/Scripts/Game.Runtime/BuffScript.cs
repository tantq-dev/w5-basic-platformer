using UnityEngine;

public class BuffScript : MonoBehaviour
{
   public enum typeOfBuff
    {
        HP,
        ATK,
        JumpForce
    }

    public typeOfBuff types;

    public int point;
}
