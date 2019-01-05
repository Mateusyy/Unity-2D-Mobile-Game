using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [Header("CHOICE MODE")]
    public bool isLevelMode = false;

    //public GameObject part;
    public Sprite block_org;
    public Sprite block_left;
    public Sprite block_right;

    public Transform generationPoint;

    [SerializeField]
    private float partHeight = 3.6f;
    public GameObject finishLine;
    public GameObject[] gameElements;
    public GameObject[] moneyPoints_1;
    public GameObject[] moneyPoints_2;
    public GameObject[] moneyPoints_3;

    [HideInInspector]
    public int index;
    private int howManyGenerate;

    private string partTag = "PlatformGame";
    private string enemyPartTag = "EnemyPlatformGame";
    public Color orgColor;
    public Color enemyColor;


    Transform myChild;
    Transform partOfPlatform;

    private void Start() {
        if (gameElements.Length <= 0) {
            Debug.LogError("MapGeneration: list of gameElements is empty!");
        }
        index = 0;
        howManyGenerate = 0;
    }

    private void Update() {
        if (generationPoint.position.y < transform.position.y) {
            transform.position = new Vector3(transform.position.x, transform.position.y - partHeight, transform.position.z);

            if (isLevelMode) {
                if (howManyGenerate < FindObjectOfType<GameManager>().currentBestLevel + 1)
                    GeneratePart();
                else
                    Instantiate(finishLine, new Vector3(-0.4f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            } else {
                GeneratePart();
            }
        }
        Debug.Log(howManyGenerate+"/"+ FindObjectOfType<GameManager>().currentBestLevel + 1);
    }

    void GeneratePart() {
        howManyGenerate++;
        index++;
        if (index > 2) index = 0;

        GameObject partGO = gameElements[index];
        partGO.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        
        for (int i = 0; i < 4; i++) {
            int num_1 = Random.Range(0, 7);
            int num_2 = num_1 / 2;
            int num_money = Random.Range(1, 4);

            for (int j = 0; j < 10; j++) {
                myChild = partGO.gameObject.transform.GetChild(i);
                partOfPlatform = myChild.gameObject.transform.GetChild(j);


                partOfPlatform.GetComponent<SpriteRenderer>().sprite = block_org;
                if (j == num_1 || j == num_1 + 1) {
                    partOfPlatform.gameObject.SetActive(false);
                }else {
                    if (!partOfPlatform.gameObject.activeSelf) {
                        partOfPlatform.gameObject.SetActive(true);

                        
                        partOfPlatform.GetComponent<SpriteRenderer>().material.color = orgColor;
                        partOfPlatform.tag = partTag;
                    }
                }

                if (j == num_2 || j == num_2 + 1) {
                    if (partOfPlatform.gameObject.activeSelf) {
                        partOfPlatform.GetComponent<SpriteRenderer>().material.color = enemyColor;
                        partOfPlatform.tag = enemyPartTag;
                    }
                }
                
                if(j == num_1 - 1)
                    partOfPlatform.GetComponent<SpriteRenderer>().sprite = block_right;
                if(j == num_1 + 2)
                    partOfPlatform.GetComponent<SpriteRenderer>().sprite = block_left;

                if(num_1 - 1 < 0) {
                    if(j == 9)
                        partOfPlatform.GetComponent<SpriteRenderer>().sprite = block_right;
                }


                //money Points
                for (int n = 0; n < num_money; n++) {
                    switch (index) {
                        case 0:
                            moneyPoints_1[n].SetActive(true);
                            break;
                        case 1:
                            moneyPoints_2[n].SetActive(true);
                            break;
                        case 2:
                            moneyPoints_3[n].SetActive(true);
                            break;
                    }
                }
                
                for (int n = num_money; n < 4; n++) {
                    switch (index) {
                        case 0:
                            moneyPoints_1[n].SetActive(false);
                            break;
                        case 1:
                            moneyPoints_2[n].SetActive(false);
                            break;
                        case 2:
                            moneyPoints_3[n].SetActive(false);
                            break;
                    }
                }
            }
        }
    }
}
