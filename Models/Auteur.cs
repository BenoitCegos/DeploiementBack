using System;
using System.Collections.Generic;

namespace BDDNET.Models;

public partial class Auteur
{
    public int IdAuteur { get; set; }

    public string NomAuteur { get; set; } = null!;

    public string PrenomAuteur { get; set; } = null!;

    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();

    //Methode ajouter pour creer auteur
    public static void CreateInDB(
        string nomAuteur,
        string prenomAuteur,
        BiblioContext DB,
        List<Livre>? livres = null
    )
    {
        if (livres == null)
            livres = new List<Livre>();
        Auteur auteur_to_add = new Auteur()
        {
            NomAuteur = nomAuteur,
            PrenomAuteur = prenomAuteur,
            Livres = livres
        };

        DB.Auteurs.Add(auteur_to_add);
        DB.SaveChanges();
    }

}
