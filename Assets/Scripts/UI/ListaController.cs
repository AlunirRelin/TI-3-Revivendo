using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaController : MonoBehaviour
{
    public static GameObject ficha;
    public GameObject ficha2;
    public ListaDePedidos lista = new ListaDePedidos();
    [SerializeField] private GameObject painelPedidos;
    private static ListaController gambilista; //gambiarra feia horrível nojenta outro dia eu sento com vcs e ajeito tudo -alu

    void Start()
    {


    }
    private void Awake()
    {
        ficha = ficha2;
        gambilista = FindFirstObjectByType<ListaController>(); //gambiarra extendida
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            lista.Adicionar();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lista.Remover();
        }
    }

    public static void AddPedido(ClientBehaviour client)    //gambiarra total
    {
        client.pedido = gambilista.lista.Adicionar();
    }
    
    public class ListaDePedidos
    {
        public Pedido head;
        public Pedido tail;
        public int index;

        public ListaDePedidos()
        {
            head = new Pedido(head);
            tail = head;
        }
        public Pedido Adicionar()
        {
            Pedido p = Instantiate(ficha).GetComponent<Pedido>();
            p.gameObject.transform.SetParent(ListaController.gambilista.painelPedidos.transform);
            p.gameObject.transform.localPosition = new Vector3(50 + index * 100, 57);
            tail.seg = p;
            tail = p;
            index++;
            return p;
        }
        public void Remover()
        {
            Destroy(head.seg.gameObject);
            head.seg = head.seg.seg;
            index--;
            Arrumar();
        }
        public void Arrumar()
        {
            int temp = 0;
            for(Pedido p = head.seg;p != null;p = p.seg)
            {
                p.transform.localPosition = new Vector3(50 + temp * 100, 57);
                temp++;
            }
        }
    }
}
