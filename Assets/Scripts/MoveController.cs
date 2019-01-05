using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public GameObject[] checkPoints;

    public bool isMobile = false;

    public GameObject movePart;
    [SerializeField]

    [Header ("mobile = 6; pc = 2")]
    private float speed = 300f;
    private bool isPress = false;

    //Mobile
    float pointerX_mobile = Input.GetAxis("Mouse X");

    


    private void Update() {

        if(isMobile == false) {
            //PC -- speedRotate = 300f;
            if (Input.GetMouseButtonDown(0))
                isPress = true;
            if (Input.GetMouseButtonUp(0))
                isPress = false;
        } else {
            //MOBILE -- speedRotate = 30f;
            if (Input.touchCount > 0) {
                isPress = true;
            }

            if (Input.touchCount <= 0)
                isPress = false;
        }

        if(FindObjectOfType<GameManager>().isGame && !FindObjectOfType<GameManager>().MenuPanel.activeSelf)
            Move();
    }



    void Move() {
        if (isPress) {
            float moveX = Input.GetAxis("Mouse X") * speed;

            if (isMobile == false) {
                // PC
                movePart.transform.Translate(Vector3.right * moveX * Time.deltaTime);
            } else {
                //MOBILE
                pointerX_mobile = Input.touches[0].deltaPosition.x;
                moveX = pointerX_mobile * speed * Mathf.Deg2Rad;

                movePart.transform.Translate(Vector3.right * moveX * Time.deltaTime);
            }
        }
    }
}
