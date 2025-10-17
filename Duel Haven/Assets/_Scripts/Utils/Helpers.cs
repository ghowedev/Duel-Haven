using UnityEngine;

public static class Helpers
{
    public static Camera mainCamera;

    public static Directions GetDirectionFromVector(Vector2 input)
    {
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        if (angle >= 337.5f || angle < 22.5f) return Directions.Right;
        if (angle >= 22.5f && angle < 67.5f) return Directions.UpRight;
        if (angle >= 67.5f && angle < 112.5f) return Directions.Up;
        if (angle >= 112.5f && angle < 157.5f) return Directions.UpLeft;
        if (angle >= 157.5f && angle < 202.5f) return Directions.Left;
        if (angle >= 202.5f && angle < 247.5f) return Directions.DownLeft;
        if (angle >= 247.5f && angle < 292.5f) return Directions.Down;
        if (angle >= 292.5f && angle < 337.5f) return Directions.DownRight;

        return Directions.Down;
    }

    public static Vector2 GetVectorFromDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Up: return Vector2.up;
            case Directions.Down: return Vector2.down;
            case Directions.Left: return Vector2.left;
            case Directions.Right: return Vector2.right;
            case Directions.UpRight: return new Vector2(1, 1).normalized;
            case Directions.UpLeft: return new Vector2(-1, 1).normalized;
            case Directions.DownRight: return new Vector2(1, -1).normalized;
            case Directions.DownLeft: return new Vector2(-1, -1).normalized;
            default: return Vector2.down;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp mouse position to screen size. EDIT: THIS CODE CAUSES X VALUE TO FLICKER, RE-ENABLE AND BUG FIX LATER IF NEEDED
        // mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        // mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition.z = 0f;

        return worldPosition;

    }
    public static Vector3 GetMousePlayerPosition(Transform playerTransform)
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp mouse position to screen size. EDIT: THIS CODE CAUSES X VALUE TO FLICKER, RE-ENABLE AND BUG FIX LATER IF NEEDED
        // mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        // mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition -= playerTransform.position;

        worldPosition.z = 0f;

        return worldPosition;

    }
}












/*


    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;

    }

    public static string GetNearestDirectionFromAngle(float angle)
    {
        string direction = "Down";

        angle = (angle + 360) % 360;

        if (angle < 22.5 || angle >= 337.5)
            direction = "Right";
        else if (angle < 67.5)
            direction = "Up Right";
        else if (angle < 112.5)
            direction = "Up";
        else if (angle < 157.5)
            direction = "Up Left";
        else if (angle < 202.5)
            direction = "Left";
        else if (angle < 247.5)
            direction = "Down Left";
        else if (angle < 292.5)
            direction = "Down";
        else if (angle < 337.5)
            direction = "Down Right";
        else
            Debug.Log(angle);

        if (direction != null)
            return direction;

        return "Down";
    }
}

    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return directionVector;
    }



    public static int GetNearestDirectionFromAngleInt(float angle)
    {
        int direction = 2;

        angle = (angle + 360) % 360;

        if (angle < 22.5 || angle >= 337.5)
            // direction = "Right";
            direction = 6;
        else if (angle < 67.5)
            // direction = "Up Right";
            direction = 7;
        else if (angle < 112.5)
            // direction = "Up";
            direction = 8;
        else if (angle < 157.5)
            // direction = "Up Left";
            direction = 7;
        else if (angle < 202.5)
            // direction = "Left";
            direction = 4;
        else if (angle < 247.5)
            // direction = "Down Left";
            direction = 1;
        else if (angle < 292.5)
            // direction = "Down";
            direction = 2;
        else if (angle < 337.5)
            // direction = "Down Right";
            direction = 3;
        else
            Debug.Log(angle);

        return direction;
    }

    public static int GetAimDirection(float angle)
    {
        // Normalize angle to 0-360
        angle = (angle + 360) % 360;

        // Get the angle thresholds for the current direction
        float currentLowerThreshold = GetLowerThreshold(currentDirection);
        float currentUpperThreshold = GetUpperThreshold(currentDirection);

        // Add hysteresis to create a "sticky" zone around current direction
        float hysteresisLower = (currentLowerThreshold - HYSTERESIS + 360) % 360;
        float hysteresisUpper = (currentUpperThreshold + HYSTERESIS) % 360;

        // Check if we're still within the hysteresis zone of current direction
        if (IsAngleInRange(angle, hysteresisLower, hysteresisUpper))
        {
            return currentDirection;
        }

        // If we're outside the hysteresis zone, switch to new direction
        currentDirection = GetDirectionFromAngle(angle);
        return currentDirection;
    }


private static bool IsAngleInRange(float angle, float lower, float upper)
{
    // Handle wrap-around at 360 degrees
    if (lower > upper)
    {
        return angle >= lower || angle <= upper;
    }
    return angle >= lower && angle <= upper;
}

private static float GetLowerThreshold(int direction)
{
    switch (direction)
    {
        case 6: return 337.5f; //right
        case 9: return 22.5f; //upright
        case 8: return 67.5f; //up
        case 7: return 112.5f; //upleft
        case 4: return 157.5f; //left
        case 1: return 202.5f; //downleft
        case 2: return 247.5f; //down
        case 3: return 292.5f; //downright
        default: return 337.5f;
    }
}

private static float GetUpperThreshold(int direction)
{
    switch (direction)
    {
        case 6: return 22.5f; //right
        case 9: return 67.5f; //upright
        case 8: return 112.5f; //up
        case 7: return 157.5f; //upleft
        case 4: return 202.5f; //left
        case 1: return 247.5f; //downleft
        case 2: return 292.5f; //down
        case 3: return 337.5f; //downright
        default: return 22.5f;
    }
}

private static int GetDirectionFromAngle(float angle)
{
    if (angle <= 22.5f || angle > 337.5f) return 6; //right
    else if (angle <= 67.5f) return 9; //upright
    else if (angle <= 112.5f) return 8; //up
    else if (angle <= 157.5f) return 7; //upleft
    else if (angle <= 202.5f) return 4; //left
    else if (angle <= 247.5f) return 1; //downleft
    else if (angle <= 292.5f) return 2; //down
    else return 3;
}

*/
