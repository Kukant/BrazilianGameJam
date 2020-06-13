using System;
using UnityEngine;
using UnityEngine.XR;

public class plex_atacks : MonoBehaviour {
    public ATTACK ActiveAttack = ATTACK.MEELE;
    public bool attacking = false;
    private int keyShifted = 0; // Just so that ctrl changing is more user-friendly
    
    public float MeeleRadius = 25f;
    public float MeelePower = 10f;
    public int MeeleCooldown = 50;
    public int MeeleCooling = 0;
    private CircleCollider2D meeleCollider;
    
    public float GunRadius = 10f;
    public float GunPower = 10f;
    public int GunCooldown = 5;
    public int GunCooling = 0;
    
    public float LaserRadius = 10f;
    public float LaserPower = 1f;
    public int LaserCooldown = 0;
    public int LaserCooling = 0;
    private PolygonCollider2D laserCollider;
    
    public enum ATTACK {
        MEELE,
        GUN,
        LASER,
        SHITBOMB
    }

    // Start is called before the first frame update
    void Start() {
        meeleCollider = transform.GetComponentsInChildren<CircleCollider2D>()[0];
        meeleCollider.radius = MeeleRadius;
        meeleCollider.offset = new Vector2(MeeleRadius, 0);
        meeleCollider.enabled = false;
        
        laserCollider = transform.GetComponentsInChildren<PolygonCollider2D>()[0];
        laserCollider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        MeeleCooling = Math.Max(0, MeeleCooling - 1);
        GunCooling = Math.Max(0, GunCooling - 1);
        LaserCooling = Math.Max(0, LaserCooling - 1);

        keyShifted = Math.Max(0, keyShifted - 1);
        
        meeleCollider.enabled = false;
        laserCollider.enabled = false;
        attacking = false;

        handleAttackSelection();
        handleAttackUsage();
    }

    private void attack(float dx, float dy, float angleZ) {
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
        switch (ActiveAttack) {
            case ATTACK.MEELE:
                if (MeeleCooling == 0) {
                    MeeleCooling = MeeleCooldown;
                    meeleCollider.enabled = true;
                }
                break;
            case ATTACK.GUN: break;
            case ATTACK.LASER:
                if (LaserCooling == 0) {
                    LaserCooling = LaserCooldown;
                    laserCollider.enabled = true;
                }
                break;
            case ATTACK.SHITBOMB:
                GetComponent<ShittingBomber>().active = true;
                break;
        }

        attacking = meeleCollider.enabled || laserCollider.enabled;
    }

    private void handleAttackUsage() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Keypad8)) {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
                attack(1, -1, 45);
            } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
                attack(-1, -1, 135);
            } else {
                attack(0, -1 , 90);
            }
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Keypad5)) {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
                attack(1, 1, -45);
            } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
                attack(-1, 1, -135);
            } else {
                attack(0, 1, -90);
            }
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
            attack(-1, 0, 180);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
            attack(1, 0, 0);
        }
    }

    private void handleAttackSelection() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            ActiveAttack = ATTACK.MEELE;
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            ActiveAttack = ATTACK.GUN;
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            ActiveAttack = ATTACK.LASER;
        } else if (Input.GetKey(KeyCode.Alpha4)) {
            ActiveAttack = ATTACK.SHITBOMB;
        } else if (Input.GetKey(KeyCode.LeftControl) && keyShifted == 0) {
            ActiveAttack = (ATTACK)(((int)ActiveAttack + 1) % 3);
            keyShifted = 10;
        }
    }
}
