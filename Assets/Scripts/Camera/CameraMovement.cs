using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [SerializeField] private float panSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] [Range(0, 1)] private float panSmoothing;    //quantidade de "follow through" do movimento
    [SerializeField] [Range(0, 1)] private float rotSmoothing;    //quantidade de "follow through" da rota��o

    //Guardam a input pra acontecer no pr�ximo frame de f�sica
    private Vector2 panInput;   //nomes horr�veis, eu sei mas n�o sei do que chamar     -alu
    private float rotInput;     //eu sei que isso parece que mente sobre como funciona pro resto do c�digo mas me parece adequado pra separar a input no update
                                //das coisas no fixedupdate, al�m de n�o deixar o resto do c�digo comunicar diretamente com propriedades. Instinto de POO talvez.   -alu

    private Transform cam;  //cam.parent pra ser usado em tudo menos zoom.  -alu

    private void Awake()
    {
        cam = Camera.main.transform;
        if (cam.parent == null)
        {
            cam.SetParent(Instantiate(new GameObject("cameraParent"), Vector3.zero, Quaternion.identity, null).transform);
        }
    }

    private void FixedUpdate()
    {
        //Aplica��o da input em movimento
        cam.parent.Translate(panInput.x * panSpeed, 0, panInput.y * panSpeed);
        panInput = Vector2.Lerp(Vector2.zero, panInput, panSmoothing);  //lerps pro movimento parar lentamente ao inv�s de de forma brusca
        cam.parent.Rotate(0, rotInput * rotSpeed, 0);
        rotInput = Mathf.Lerp(0, rotInput, rotSmoothing);
    }

    public void PanCamera(Vector2 vector)
    {
        panInput = (panInput + vector).normalized;
    }

    public void RotateCamera(float amount)
    {
        rotInput += amount;
    }
}
