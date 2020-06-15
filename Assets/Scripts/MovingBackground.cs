using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MovingBackground : MonoBehaviour {
    public List<Sprite> backgrounds;
    public List<float> relativeSlowdowns;
    public GameObject physicalBackgroundPrefab;

    Camera mainCamera;

    private List<SlowBackground> slowBackgrounds;

    private PhysicalBackground physicalBackgroundInstance;
    // Start is called before the first frame update
    void Start()
    {
        slowBackgrounds = new List<SlowBackground>();
        var mainCameraObj = GameObject.Find("Main Camera");
        mainCamera = mainCameraObj.GetComponent<Camera>();
        float smaller = 2;
        for (var i = 0; i < backgrounds.Count; i++) {
            slowBackgrounds.Add(
                new SlowBackground(backgrounds[i], relativeSlowdowns[i], mainCamera.transform.position.x, smaller, i)
            );
            smaller = smaller - 0.5f;
        }
        
        physicalBackgroundInstance = new PhysicalBackground(physicalBackgroundPrefab);
    }

    // Update is called once per frame
    void FixedUpdate() {
        var cameraXMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        // apply relative slowdowns 
        for (var i = 0; i < slowBackgrounds.Count; i++) {
            slowBackgrounds[i].update(cameraXMin, mainCamera.transform.position.x);
        }
        physicalBackgroundInstance.update(cameraXMin);
    }

}

class PhysicalBackground {
    private Queue<GameObject> instances;
    private float physicalWidth;
    private int spriteCount;
    private GameObject prefab;
    public PhysicalBackground(GameObject prefab) {
        this.prefab = prefab;
        physicalWidth = prefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        instances = new Queue<GameObject>();
        spriteCount = 3;
        for (var i = 0; i < spriteCount; i++) {
            var instance = Object.Instantiate(prefab, new Vector3(i * physicalWidth, 0, 0), Quaternion.identity);
            instances.Enqueue(instance);
        }
    }

    public void update(float cameraXMin) {
        GameObject last = instances.Peek();
        
        var maxX = last.transform.position.x + physicalWidth / 2;
        if (cameraXMin > maxX) {
            var nextPos = last.transform.position.x + spriteCount * physicalWidth;
            instances.Enqueue(
                Object.Instantiate(prefab, new Vector3(nextPos, 0, 0), Quaternion.identity)
            );
            Object.Destroy(instances.Dequeue());
        }

    }
}


class SlowBackground {
    private List<GameObject> backgrounds;
    public float slowdown;


    private float spriteWidth;
    private float lastCameraPos;
    private int spriteCount;
    public SlowBackground(Sprite backgroundSprite, float slowdown, float cameraPos, float smaller, int layerIdx) {
        lastCameraPos = cameraPos;
        backgrounds = new List<GameObject>();
        spriteWidth = 0f;
        spriteCount = 3;
        for (var i = 0; i < spriteCount; i++) {
            var go = new GameObject();
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Background";
            sr.sortingOrder = layerIdx;
            sr.sprite = backgroundSprite;
            
            spriteWidth = go.GetComponent<Renderer>().bounds.size.x * .99f;
            var oldPos = go.transform.position;
            go.transform.position = new Vector3(oldPos.x + i * spriteWidth, oldPos.y);
            
            backgrounds.Add(go);
        }
        this.slowdown = slowdown;
    }

    public void update(float cameraXMin, float cameraPos) {
        var slowdownStep = (cameraPos - lastCameraPos) * slowdown;
        lastCameraPos = cameraPos;
        for (var i = 0; i < backgrounds.Count; i++) {
            var go = backgrounds[i];
            var jump = 0f;
            var maxX = go.transform.position.x + spriteWidth / 2;
            if (cameraXMin > maxX) {
                 jump = spriteCount * spriteWidth;
            }
            
            var oldPos = go.transform.position;
            go.transform.position = new Vector3(oldPos.x + slowdownStep + jump, oldPos.y);
        }
    }
}
