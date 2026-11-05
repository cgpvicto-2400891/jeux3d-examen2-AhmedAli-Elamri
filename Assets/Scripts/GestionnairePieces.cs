using UnityEngine;

public class GestionnairePieces : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int nombrrePieceCollecter = 0;
    void Start()
    {
        Piece.PieceCollecter += MettreAjourNombreDePieces;
    }
     
    
    private void OnDestroy()
    {
        Piece.PieceCollecter -= MettreAjourNombreDePieces;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MettreAjourNombreDePieces()
    {
        nombrrePieceCollecter++;
    }
    public int GetNombrePieces()
    {
        return nombrrePieceCollecter;
    }
    public void setNombrePieces(int nombre)
    {
        int nomvrePiecesSoustrait = nombrrePieceCollecter - nombre;
        if (nomvrePiecesSoustrait < 0) 
        {
            return;
        }
        else
        {
            this.nombrrePieceCollecter -= nombre;
        }

    }
}
