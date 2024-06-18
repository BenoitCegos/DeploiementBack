using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
namespace Library;
using BDDNET.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

public class Repository<T> where T : class // specification de la classe
{
    // T est donc inconnu
    private BiblioContext _db;
    private DbSet<T> _table;
    // private ?? table

    public Repository(BiblioContext db)
    {
        _db = db;
        _table = db.Set<T>();
        Console.WriteLine("Le repo est creer");
    }

    public void add(T entity)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                _table.Add(entity); // <- double id entite
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
    }

    public void Update()
    {
        _db.SaveChanges();
    }
    //CRUD
}