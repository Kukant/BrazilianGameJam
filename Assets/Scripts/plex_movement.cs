using System;
using UnityEngine;

public class plex_movement : MonoBehaviour {
    public Direction PlexDirection = Direction.NONE;
    public float RotationSpeed = .05f;
    
    public enum Direction {
        UP,
        UP_L,
        LEFT,
        DOWN_L,
        DOWN,
        DOWN_R,
        RIGHT,
        UP_R,
        NONE,
    };
    
    public static float GetRotation(Direction d) {
        switch (d) {
            case Direction.UP: return .30f;
            case Direction.UP_L: return .45f;
            case Direction.LEFT: return .65f;
            case Direction.DOWN_L: return .45f;
            case Direction.DOWN: return .30f;
            case Direction.DOWN_R: return .15f;
            case Direction.RIGHT: return -.20f;
            case Direction.UP_R: return .15f;
        }
        return 0f;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        handleMovements();
    }
    
    private void setRotationZ(float zAngle) {
        Quaternion rot = transform.rotation;
        transform.rotation = new Quaternion(rot.x, rot.y, zAngle, rot.w);
    }

    private void rotateTowards(Direction direction) {
        PlexDirection = direction;
        float endRotation = GetRotation(direction);
        if (endRotation > 0) {
            setRotationZ(Math.Min(transform.rotation.z + RotationSpeed, endRotation));
        } else if (endRotation < 0) {
            setRotationZ(Math.Max(transform.rotation.z - RotationSpeed, endRotation));
        } else if (transform.rotation.z > 0) {
            setRotationZ(Math.Max(transform.rotation.z - 2*RotationSpeed, endRotation));
        } else if (transform.rotation.z < 0) {
            setRotationZ(Math.Min(transform.rotation.z + 2*RotationSpeed, endRotation));
        }
    }

    private void handleMovements() {
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.D)) {
                rotateTowards(Direction.UP_R);
            } else if (Input.GetKey(KeyCode.A)) {
                rotateTowards(Direction.UP_L);
            } else {
                rotateTowards(Direction.UP);
            }
        } else if (Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.D)) {
                rotateTowards(Direction.DOWN_R);
            } else if (Input.GetKey(KeyCode.A)) {
                rotateTowards(Direction.DOWN_L);
            } else {
                rotateTowards(Direction.DOWN);
            }
        } else if (Input.GetKey(KeyCode.A)) {
            rotateTowards(Direction.LEFT);
        } else if (Input.GetKey(KeyCode.D)) {
            rotateTowards(Direction.RIGHT);
        } else {
            rotateTowards(Direction.NONE);
        }
    }
}
