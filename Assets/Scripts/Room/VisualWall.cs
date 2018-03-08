using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualWall : MonoBehaviour {

    public List<Sprite> spritePool;

	void Start() {
        var sprite = spritePool[Random.Range(0, spritePool.Count)];
        GetComponent<SpriteRenderer>().sprite = sprite;
	}
}
