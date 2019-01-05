using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour {

    public float left_limit;
    public float right_limit;

    public List<GameObject> blocks;
    

    private GameObject last_left_block;
    private GameObject last_right_block;

    private readonly float block_width = 0.16f;

    private void Start() {

        for (int i = 0; i < transform.childCount; i++) {
            blocks.Add(transform.GetChild(i).gameObject);
        }

        last_left_block = blocks.First();
        last_right_block = blocks.Last();

        
    }

    private void FixedUpdate() {

        //Debug.Log(FindObjectOfType<MoveController>().checkPoints[0].transform.position.x);

        //przesuwamy w lewo
        if (last_right_block.transform.position.x >= right_limit) {
            last_right_block.transform.position = new Vector3(last_left_block.transform.position.x - block_width, last_right_block.transform.position.y, last_right_block.transform.position.z);
            blocks.RemoveAt(blocks.Count - 1);
            blocks.Insert(0, last_right_block);

            recalculateLimitsBlocks();
            MoveController checkPointsHolder = FindObjectOfType<MoveController>();

            for (int i = 0; i < 3; i++) {
                checkPointsHolder.checkPoints[i].transform.position = new Vector3(0f, checkPointsHolder.checkPoints[i].transform.position.y, checkPointsHolder.checkPoints[i].transform.position.z);
            }
        }
        //przesuwamy w prawo
        if (last_left_block.transform.position.x <= left_limit) {
            last_left_block.transform.position = new Vector3(last_right_block.transform.position.x + block_width, last_left_block.transform.position.y, last_left_block.transform.position.z);
            blocks.RemoveAt(0);
            blocks.Insert(blocks.Count, last_left_block);

            recalculateLimitsBlocks();
            MoveController checkPointsHolder = FindObjectOfType<MoveController>();

            for (int i = 0; i < 3; i++) {
                checkPointsHolder.checkPoints[i].transform.position = new Vector3(0f, checkPointsHolder.checkPoints[i].transform.position.y, checkPointsHolder.checkPoints[i].transform.position.z);
            }
        }
    }


    void recalculateLimitsBlocks() {
        last_left_block = blocks.First();
        last_right_block = blocks.Last();
    }

    public void ChangeBlocks() {

        for (int i = 0; i < blocks.Count; i++) {
            if (blocks[i].activeSelf) {
                if(i == 0) {
                    if (!blocks[blocks.Count - 1].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_right;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                    if (!blocks[i+1].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_left;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                } else if(i > 0 && i < blocks.Count) {
                    if (!blocks[i - 1].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_right;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                    if (!blocks[i + 1].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_left;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                } else {
                    if (!blocks[i -1].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_right;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                    if (!blocks[0].activeSelf)
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_left;
                    else
                        blocks[i].GetComponent<SpriteRenderer>().sprite = FindObjectOfType<MapGeneration>().block_org;
                }
            }
        }
    }
}
