using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 yon;
    public float ileriHiz;
    public float maxHiz;
    private int gidilenSerit = 0;  // -1:Sol Þerit 0:Orta Þerit 1:Sað Þerit
    public float seritMesafesi = 2;  // x ekseninde iki þerit arasýndaki mesafe
    public float ziplamaGucu;
    public float yerCekimi = -20;
    public bool yerdeyim;
    public bool temas = false;
    private bool yuvarlaniyor = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animator;
    public AudioSource gameOver;
    public int can = 3;
    public Text CanText;
    [SerializeField] Material _material;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _material.color = Color.yellow;
    }

    void Update()
    {
        if (!GameManager.oyunBasladi)
            return;
        if (ileriHiz < maxHiz)
        ileriHiz += 0.1f * Time.deltaTime;
        CanText.text = "Can: " + can;
        animator.SetBool("oyunBasladi", true);
        yon.z = ileriHiz;
        yerdeyim = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("yerdeyim", yerdeyim);
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Ziplama();
            }
        }
        else
        {
            yon.y += yerCekimi * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !yuvarlaniyor)
        {
            StartCoroutine(Yuvarlan());
            yon.y = -ziplamaGucu;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gidilenSerit++;
            if (gidilenSerit == 2)
                gidilenSerit = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gidilenSerit--;
            if (gidilenSerit == -2)
                gidilenSerit = -1;
        }
        Vector3 hedefPozisyon = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (gidilenSerit == -1)
        {
            hedefPozisyon += Vector3.left * seritMesafesi;
        }
        else if (gidilenSerit == 1)
        {
            hedefPozisyon += Vector3.right * seritMesafesi;
        }
        if (transform.position != hedefPozisyon)
        {
            Vector3 fark = hedefPozisyon - transform.position;
            Vector3 haraketYon = 50 * Time.deltaTime * fark.normalized;
            if (haraketYon.sqrMagnitude < fark.magnitude)
                _controller.Move(haraketYon);
            else
                _controller.Move(fark);
        }
        _controller.Move(yon * Time.deltaTime);
    }
    private void Ziplama()
    {
        yon.y = ziplamaGucu;
    }
    private IEnumerator Yuvarlan()
    {
        yuvarlaniyor = true;
        animator.SetBool("yuvarlan", true);
        _controller.center = new Vector3(0, 0.2f, 0);
        _controller.height = 0.5f;
        yield return new WaitForSeconds(1f);
        _controller.center = new Vector3(0, 0.9f, 0);
        _controller.height = 1.8f;
        animator.SetBool("yuvarlan", false);
        yuvarlaniyor = false;
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Engel") && !temas)
        {
            temas = true;
            can -= 1;
            _material.color = Color.red;
            Invoke("MateryalGeri", 1.5f);
            if (can == 0)
            {
                gameOver.Play();
                GameManager.gameOver = true;
            }            
        }
    }
    public void MateryalGeri()
    {
        _material.color = Color.yellow;
        temas = false;
    }

}
