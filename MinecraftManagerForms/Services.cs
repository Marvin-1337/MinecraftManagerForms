using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Jugador
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class Bloque
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public string Rareza { get; set; }
}

public class Inventario
{
    public int Id { get; set; }
    public int JugadorId { get; set; }
    public int BloqueId { get; set; }
}

// Simulador de base de datos
public class DatabaseManager
{
    public List<Jugador> Jugadores = new List<Jugador>();
    public List<Bloque> Bloques = new List<Bloque>();
    public List<Inventario> Inventarios = new List<Inventario>();

    public DatabaseManager()
    {
        Jugadores.Add(new Jugador { Id = 1, Nombre = "Steve" });
        Jugadores.Add(new Jugador { Id = 2, Nombre = "Alex" });

        Bloques.Add(new Bloque { Id = 1, Nombre = "Piedra", Tipo = "Natural", Rareza = "Común" });
        Bloques.Add(new Bloque { Id = 2, Nombre = "Diamante", Tipo = "Mineral", Rareza = "Épico" });
        Bloques.Add(new Bloque { Id = 3, Nombre = "Tierra", Tipo = "Natural", Rareza = "Común" });

        Inventarios.Add(new Inventario { Id = 1, JugadorId = 1, BloqueId = 1 });
        Inventarios.Add(new Inventario { Id = 2, JugadorId = 1, BloqueId = 2 });
        Inventarios.Add(new Inventario { Id = 3, JugadorId = 2, BloqueId = 3 });
    }
}

// Servicios
public class JugadorService
{
    private DatabaseManager _db;
    public JugadorService(DatabaseManager db) { _db = db; }

    public List<Jugador> ObtenerTodos() => _db.Jugadores;
    public void Agregar(string nombre)
    {
        int nuevoId = _db.Jugadores.Count + 1;
        _db.Jugadores.Add(new Jugador { Id = nuevoId, Nombre = nombre });
    }
    public void Eliminar(int id)
    {
        var jugador = _db.Jugadores.Find(j => j.Id == id);
        if (jugador != null) _db.Jugadores.Remove(jugador);
    }
}

public class BloqueService
{
    private DatabaseManager _db;
    public BloqueService(DatabaseManager db) { _db = db; }

    public List<Bloque> ObtenerTodos() => _db.Bloques;
    public List<Bloque> ObtenerPorRareza(string rareza) => _db.Bloques.FindAll(b => b.Rareza == rareza);
}

public class InventarioService
{
    private DatabaseManager _db;
    private JugadorService _jugadorService;
    private BloqueService _bloqueService;

    public InventarioService(DatabaseManager db, JugadorService jugadorService, BloqueService bloqueService)
    {
        _db = db;
        _jugadorService = jugadorService;
        _bloqueService = bloqueService;
    }

    public List<dynamic> ObtenerTodo()
    {
        return _db.Inventarios.Select(i => new
        {
            Jugador = _jugadorService.ObtenerTodos().FirstOrDefault(j => j.Id == i.JugadorId)?.Nombre,
            Bloque = _bloqueService.ObtenerTodos().FirstOrDefault(b => b.Id == i.BloqueId)?.Nombre
        }).ToList<dynamic>();
    }

    public int ContarBloques(int jugadorId)
    {
        return _db.Inventarios.Count(i => i.JugadorId == jugadorId);
    }
}
