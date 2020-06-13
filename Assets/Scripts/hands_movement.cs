using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hands_movement : MonoBehaviour {
    public Sprite HandFront0Sprite;
    public Sprite HandFront1Sprite;
    public Sprite HandFront2Sprite;
    public Sprite HandBack0Sprite;
    public Sprite HandBack1Sprite;
    public Sprite HandBack2Sprite;
    
    
    private plex_movement.Direction direction;
    private SpriteRenderer frontHandSprite;
    private Animator frontHandAnimator;
    private SpriteRenderer backHandSprite;
    private Animator backHandAnimator;
    private bool idle;

    private plex_movement parentMovement;
    private plex_atacks parentAttacks;
    
    // Start is called before the first frame update
    void Start() {
        parentMovement = GetComponentInParent<plex_movement>();
        parentAttacks = transform.parent.parent.parent.GetComponentInChildren<plex_atacks>();
        
        frontHandSprite = GetComponentsInChildren<SpriteRenderer>()[0];
        frontHandAnimator = GetComponentsInChildren<Animator>()[0];

        backHandSprite = GetComponentsInChildren<SpriteRenderer>()[1];
        backHandAnimator = GetComponentsInChildren<Animator>()[1];
        
        frontHandSprite.sprite = HandFront0Sprite;
        idle = true;
    }

    // Update is called once per frame
    void Update() {
        idle = parentAttacks.attacking;
        handsMovement();
    }

    private void handsMovement() {
        frontHandAnimator.enabled = false;
        backHandAnimator.enabled = false;
        
        switch (parentMovement.PlexDirection) {
            case plex_movement.Direction.RIGHT:
                frontHandSprite.sprite = HandFront2Sprite;
                backHandSprite.sprite = HandBack2Sprite;
                break;
            case plex_movement.Direction.DOWN_R:
                frontHandSprite.sprite = HandFront1Sprite;
                backHandSprite.sprite = HandBack1Sprite;
                break;
            case plex_movement.Direction.DOWN:
                frontHandSprite.sprite = HandFront1Sprite;
                backHandSprite.sprite = HandBack1Sprite;
                break;
            case plex_movement.Direction.DOWN_L:
                frontHandSprite.sprite = HandFront0Sprite;
                backHandSprite.sprite = HandBack0Sprite;
                break;
            case plex_movement.Direction.LEFT:
                frontHandSprite.sprite = HandFront0Sprite;
                backHandSprite.sprite = HandBack0Sprite;
                break;
            case plex_movement.Direction.UP_L:
                frontHandSprite.sprite = HandFront0Sprite;
                backHandSprite.sprite = HandBack0Sprite;
                break;
            case plex_movement.Direction.UP:
                frontHandSprite.sprite = HandFront1Sprite;
                backHandSprite.sprite = HandBack1Sprite;
                break;
            case plex_movement.Direction.UP_R:
                frontHandSprite.sprite = HandFront1Sprite;
                backHandSprite.sprite = HandBack1Sprite;
                break;
            case plex_movement.Direction.NONE:
                frontHandSprite.sprite = HandFront1Sprite;
                backHandSprite.sprite = HandBack1Sprite;
                frontHandAnimator.enabled = true;
                backHandAnimator.enabled = true;
                break;
        }
    }
}
