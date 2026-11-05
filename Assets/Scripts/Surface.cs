using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DetecteurBoule))]
public class Surface : MonoBehaviour
{
    /// <summary>
    /// Référence au détecteur de boule attaché à cette surface.
    /// </summary>
    
    private Animator _animator;
    private int piecesRequiredPause = 5;

    [SerializeField, Tooltip("ref a la Gestionnaire Piece")]
    private GestionnairePieces pieceGestionnaire;

    [SerializeField, Tooltip("ref a la Boule")]
    private Boule boule;
    private bool isPausing = false;
    private float duree = 3f;

    public DetecteurBoule Detecteur { get; private set; }

    [SerializeField, Tooltip("Positions de génération des objets sur la surface.")]
    private Vector3[] positionGeneration;

    private void Awake()
    {
        Detecteur = GetComponent<DetecteurBoule>(); 
    }

    private void Start()
    {
        Boule.StartPause += ArreterOscillentAnimation;
        OnCommenceAnmimation();
    }
    private void OnDestroy()
    {
        Boule.StartPause-= ArreterOscillentAnimation;
        Boule.jeuCommence -= OnCommenceAnmimation;
    }

    /// <summary>
    /// Ajoute un objet à la surface à une position aléatoire parmi les positions de génération définies.
    /// </summary>
    /// <param name="prefab">Le prefab de l'objet à ajouter à la surface.</param>
    public void AjouterObjet(GameObject prefab)
    {
        if (positionGeneration.Length == 0)
        {
            Debug.LogWarning("Aucune position de génération définie pour la surface.");
            return;
        }

        // Sélectionne une position aléatoire parmi les positions disponibles
        Vector3 positionChoisie = positionGeneration[Random.Range(0, positionGeneration.Length)];
        // Instancie l'prefab à la position choisie
        GameObject objetGenere = Instantiate(prefab, transform);
        objetGenere.transform.localPosition = positionChoisie; // Utilise localPosition pour positionner l'objet par rapport à la surface
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 position in positionGeneration)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + position, 0.2f);
        }
    }
    private void OnCommenceAnmimation()
    {
        _animator.SetBool("start", true);
    }
    private void ArreterOscillentAnimation()
    {
        
        if (pieceGestionnaire.GetNombrePieces() >= piecesRequiredPause && !isPausing)
        {
            pieceGestionnaire.setNombrePieces(piecesRequiredPause);
            StartCoroutine(StartPauseCouroutine());
        }
    }

    IEnumerator StartPauseCouroutine()
    {
        isPausing = true;
        _animator.SetBool("start", false);
        yield return new WaitForSeconds(duree);
        isPausing = false;
        _animator.SetBool("start", true);
    }
}
