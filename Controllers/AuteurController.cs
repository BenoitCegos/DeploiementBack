using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BDDNET.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

//Donner nom du projet au namespace
namespace BDDNET.Controllers;

public class AuteursController : Controller
{
    private readonly ILogger<AuteursController> _logger;
    private readonly BiblioContext _DB;

    public AuteursController(ILogger<AuteursController> logger, BiblioContext context)
    {
        _logger = logger;
        _DB = context;
    }
    //Affichage Liste
    [HttpGet("Auteurs")]
    public IActionResult Index()
    {
        List<Auteur> auteurs = _DB.Auteurs.Include(auteur => auteur.Livres).ToList();
        return Json(auteurs);
    }

    //Ajout auteur
    [HttpPost("Auteurs")]
    public IActionResult Add([FromBody] Auteur auteurToAdd)
    {
        Auteur newAuteur = new Auteur();
        newAuteur.NomAuteur = auteurToAdd.NomAuteur;
        newAuteur.PrenomAuteur = auteurToAdd.PrenomAuteur;
        newAuteur.Livres = auteurToAdd.Livres;

        foreach (Livre livre in newAuteur.Livres)
        {
            livre.ResumerLivre ??= "default";
        }

        _DB.Auteurs.Add(newAuteur);
        _DB.SaveChanges();
        return Json(newAuteur);
    }

    // Delete Auteur
    [HttpDelete("Auteurs/{id}")]
    public IActionResult Index(int id)
 
    {
        Auteur aSupprimerAuteur = _DB.Auteurs.Find(id);
        if (aSupprimerAuteur != null)
        {
            _DB.Auteurs.Remove(aSupprimerAuteur);
            foreach (var livre in _DB.Livres)
            {
                if (livre.IdAuteur == id) { }
                _DB.Livres.Remove(livre);
            }
            _DB.SaveChanges();
            return Json(_DB.Auteurs);
        }
        else
        {
            return NotFound();
        }
    }
    //Modif Auteur
    [HttpPut("Auteurs")]

    public IActionResult PutModifAuteur([FromBody] Auteur auteurToUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Auteur? aModifierAuteur = _DB.Auteurs.Find(auteurToUpdate.IdAuteur);
        if (aModifierAuteur == null)
        {
            return NotFound();
        }
        aModifierAuteur.NomAuteur=auteurToUpdate.NomAuteur;
        aModifierAuteur.PrenomAuteur = auteurToUpdate.PrenomAuteur;
        //_DB.Update(aModifierAuteur);
        _DB.SaveChanges();
        return Json(_DB.Auteurs);

        /*foreach (Auteur kAuteur in _DB.Auteurs)
        {
            if (kAuteur.IdAuteur==auteurToUpdate.IdAuteur)
            {
                aModifierAuteur.IdAuteur=kAuteur.IdAuteur;
                aModifierAuteur.NomAuteur=auteurToUpdate.NomAuteur;
                aModifierAuteur.PrenomAuteur = auteurToUpdate.PrenomAuteur;

                _DB.Update(aModifierAuteur);
                _DB.SaveChanges();
                return Json(aModifierAuteur);
            }
          }*/

    }
    [HttpDelete("Auteurs/{id1}/{id2}")]
    public IActionResult DeleteEnsembleAuteurs(int id1,int id2)
    {
        Console.WriteLine($"-----------------------NOS ID---------------------");
        Console.WriteLine(id1);
        Console.WriteLine(id2);
        Console.WriteLine($"--------------------------------------------------");
        for (int i = id1; i<= id2; i++)
        {
            Auteur auteurASupprimer2 = _DB.Auteurs.Find(i);
            if (auteurASupprimer2 != null)
            {
                 _DB.Auteurs.Remove(auteurASupprimer2);
                //Supprimer les livres liés à l'auteur avant de faire le SaveChanges 
                foreach (var livre in _DB.Livres)
                {
                    if (livre.IdAuteur == i) { }
                    _DB.Livres.Remove(livre);
                }
            }
        }
        _DB.SaveChanges();
        return Json(_DB.Auteurs); 
    }

}