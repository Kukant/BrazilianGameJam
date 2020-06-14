using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class plex_atacks : MonoBehaviour {
    public ATTACK ActiveAttack = ATTACK.MEELE;
    public bool attacking = false;
    private int keyShifted = 0; // Just so that ctrl changing is more user-friendly

    public float MeeleRadius = 25f;
    public float MeelePower = 10f;
    public int MeeleCooldown = 50;
    public int MeeleCooling = 0;
    private CircleCollider2D meeleCollider;
    private Animator[] meeleSlashAnimators;
    
    public float GunRadius = 10f;
    public float GunPower = 10f;
    public int GunCooldown = 5;
    public int GunCooling = 0;
    
    public float LaserRadius = 10f;
    public float LaserPower = 1f;
    public int LaserCooldown = 0;
    public int LaserCooling = 0;
    
    private PolygonCollider2D laserCollider;
    private SpriteRenderer laserBeamSprite;
    private Animator laserBeamAnimator;
    
    public enum ATTACK {
        MEELE,
        GUN,
        LASER,
    }

    // Start is called before the first frame update
    void Start() {
        meeleSlashAnimators = transform.GetChild(1).GetComponentsInChildren<Animator>();
        meeleCollider = transform.GetChild(1).GetComponents<CircleCollider2D>()[0];
        foreach (Animator animator in meeleSlashAnimators) {
            animator.enabled = false;
        }
        meeleCollider.radius = MeeleRadius;
        meeleCollider.enabled = false;

        laserBeamSprite = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        laserBeamAnimator = transform.GetChild(0).GetComponentInChildren<Animator>();
        laserCollider = transform.GetChild(0).GetComponents<PolygonCollider2D>()[0];
        laserBeamSprite.enabled = false;
        laserBeamAnimator.enabled = false;
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
        laserBeamSprite.enabled = false;
        attacking = false;

        handleAttackSelection();
        handleAttackUsage();
    }

    private void attack(float angleZ) {
        switch (ActiveAttack) {
            case ATTACK.MEELE:
                laserBeamAnimator.enabled = false;
                
                if (MeeleCooling == 0) {
                    MeeleCooling = MeeleCooldown;
                    meeleCollider.enabled = true;
                    foreach (Animator animator in meeleSlashAnimators) {
                        animator.Rebind();
                        if (!animator.enabled) {
                            animator.enabled = true;
                        }
                    }
                }
                angleZ = 0;
                break;
            case ATTACK.GUN:
                laserBeamAnimator.enabled = false;
                GetComponentInChildren<RocketLauncher>().Launch();
                
                angleZ = 0;
                break;
            case ATTACK.LASER:
                if (LaserCooling == 0) {
                    LaserCooling = LaserCooldown;
                    laserCollider.enabled = true;
                    laserBeamSprite.enabled = true;
                    if (!laserBeamAnimator.enabled) {
                        laserBeamAnimator.Rebind();
                        laserBeamAnimator.enabled = true;
                    }
                }
                break;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
        
        attacking = meeleCollider.enabled || laserCollider.enabled;
    }

    private void handleAttackUsage() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Keypad8)) {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
                attack(45);
            } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
                attack(135);
            } else {
                attack(90);
            }
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Keypad5)) {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
                attack(-45);
            } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
                attack(-135);
            } else {
                attack(-90);
            }
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
            attack(180);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
            attack(0);
        } else {
            laserBeamAnimator.enabled = false;
        }
    }

    private void handleAttackSelection() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            ActiveAttack = ATTACK.MEELE;
            GetComponentInChildren<RocketLauncher>().Activate(false);
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            ActiveAttack = ATTACK.GUN;
            GetComponentInChildren<RocketLauncher>().Activate(true);
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            ActiveAttack = ATTACK.LASER;
            GetComponentInChildren<RocketLauncher>().Activate(false);
        } else if (Input.GetKey(KeyCode.LeftControl) && keyShifted == 0) {
            ActiveAttack = (ATTACK)(((int)ActiveAttack + 1) % 3);
            GetComponentInChildren<RocketLauncher>().Activate(false);
            keyShifted = 10;
        }
    }
}
