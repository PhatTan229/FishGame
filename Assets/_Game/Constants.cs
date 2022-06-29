using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const float ONLINE_MATCHING_TIME = 3f;
    public const float GAMEPLAY_SCROLL_SPEED = 5f;

    //player
    public const float DASH_FORCE = 100;
    public const float MAX_STAMINA = 5;
    public const float DECREASE_STAMINA_SPEED = 1f;
    public const float PLAYER_SPEED = 4f;
    public const float PLAYER_DASH_SPEED_RATIO = 2f;
    public const float MIN_PLAYER_SIZE = 0.3f;
    public const float MAX_PLAYER_SIZE = 0.5f;

    //camera
    public const float CAMERA_MIN_SIZE = 2.5f;
    public const float CAMERA_MAX_SIZE = 5f;

    public const string TAG_CANDY = "Candy";
    public const string TAG_CAKE = "Cake";
    public const string TAG_PLAYER = "Player";

    public const float EAT_TIME = 0.2f;
}
