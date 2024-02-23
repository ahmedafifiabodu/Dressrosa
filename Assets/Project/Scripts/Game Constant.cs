using UnityEngine;

public static class GameConstant
{
    public static int sortingOrderPixelPerUnit = 100;

    //Animator Hashes
    public static int MOVEMAGNITUDE = Animator.StringToHash("Move Magnitude");

    public static int MOVEX = Animator.StringToHash("Move X");
    public static int MOVEY = Animator.StringToHash("Move Y");

    public static int LASTMOVEX = Animator.StringToHash("Last Move X");
    public static int LASTMOVEY = Animator.StringToHash("Last Move Y");

    public static int ISIDLE = Animator.StringToHash("isIdle");
    public static int ISWALKING = Animator.StringToHash("isWalking");

    public static string PLAYERTAG = "Player";
}