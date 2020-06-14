using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_expressions : MonoBehaviour {
    public STATE state;
    public Sprite LaserFace;
    public Sprite IdleFace;
    public Sprite GunFace;
    public Sprite HurtFace;
    
    public enum STATE {
        LASER,
        IDLE,
        GUN,
        HURT
    };

    private SpriteRenderer faceSprite;
    private plex_atacks attacking;
    
    // Start is called before the first frame update
    void Start() {
        state = STATE.IDLE;
        faceSprite = GetComponent<SpriteRenderer>();
        attacking = transform.parent.parent.parent.GetComponentInChildren<plex_atacks>();
    }

    // Update is called once per frame
    void Update() {
        updateState();
        updateFace();
    }

    private void updateState() {
        if (false) { // Gets Hurt
            state = STATE.HURT;
        } else if (attacking.ActiveAttack == plex_atacks.ATTACK.LASER) {
            state = STATE.LASER;
        } else if (attacking.ActiveAttack == plex_atacks.ATTACK.GUN) {
            state = STATE.GUN;
        } else {
            state = STATE.IDLE;
        }
    }

    private void updateFace() {
        switch (state) {
            case STATE.LASER:
                faceSprite.sprite = LaserFace;
                break;
            case STATE.IDLE:
                faceSprite.sprite = IdleFace;
                break;
            case STATE.GUN:
                faceSprite.sprite = GunFace;
                break;
            case STATE.HURT:
                faceSprite.sprite = HurtFace;
                break;
        }
    }
}
