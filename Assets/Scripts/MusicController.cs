using System.Collections.Generic;
using UnityEngine;

public static class MusicController {
    public static Dictionary<SOUNDS, AudioSource> soundCollection = InitSounds();


    public enum SOUNDS {
        MENU,
        GAME_INTRO,
        GAME_LOOP,
        FIREBALL_SPAWN,
        FIREBALL_SHOT,
        SNAKE_LASER,
        ROCKET_LAUNCH,
        MEELE_SLASH,
        EXPLOSION,
        LASER_BEAM,
        ENEMY_DEATH,
        ULTRAPLEX_DEATH,
        ULTRAPLEX_HURT,
    };
    private static Dictionary<SOUNDS, AudioSource> InitSounds() {
        return new Dictionary<SOUNDS, AudioSource>() {
            {SOUNDS.MENU, GameObject.Find("MenuMusic").GetComponent<AudioSource>()},
            {SOUNDS.GAME_INTRO, GameObject.Find("GameIntroMusic").GetComponent<AudioSource>()},
            {SOUNDS.GAME_LOOP, GameObject.Find("GameLoopMusic").GetComponent<AudioSource>()},
            {SOUNDS.FIREBALL_SPAWN, GameObject.Find("FireballSpawn").GetComponent<AudioSource>()},
            {SOUNDS.FIREBALL_SHOT, GameObject.Find("FireballShot").GetComponent<AudioSource>()},
            {SOUNDS.SNAKE_LASER, GameObject.Find("SnakeLaser").GetComponent<AudioSource>()},
            {SOUNDS.ROCKET_LAUNCH, GameObject.Find("RocketLaunch").GetComponent<AudioSource>()},
            {SOUNDS.MEELE_SLASH, GameObject.Find("MeeleSlash").GetComponent<AudioSource>()},
            {SOUNDS.EXPLOSION, GameObject.Find("Explosion").GetComponent<AudioSource>()},
            {SOUNDS.LASER_BEAM, GameObject.Find("LaserBeam").GetComponent<AudioSource>()},
            {SOUNDS.ENEMY_DEATH, GameObject.Find("EnemyDeath").GetComponent<AudioSource>()},
            {SOUNDS.ULTRAPLEX_DEATH, GameObject.Find("UltraplexDeath").GetComponent<AudioSource>()},
            {SOUNDS.ULTRAPLEX_HURT, GameObject.Find("UltraplexHurt").GetComponent<AudioSource>()},
        };
    }
    
    public static void SoundController(SOUNDS key, bool play) {
        AudioSource s;
        if (soundCollection.TryGetValue(key, out s)) {
            if (play) {
                s.Play();
            } else if (s.isPlaying) {
                s.Stop();
            }
        }
    }
}
