# Cuestionario

1. ¿Por qué se usa SCOPE_IDENTITY() y qué beneficio aporta?
R// Se usa cuando insertás un nuevo jugador y querés saber cuál fue el ID que le asignó la base. Esto es útil si después necesitás ese ID para seguir trabajando. SCOPE_IDENTITY() te lo devuelve justo después del insert.

2. ¿Por qué se verifica la existencia de elementos en el inventario antes de eliminar un jugador?
R// pues si borras un jugador que todavía tiene cosas, vas a dejar referencias rotas. Es como borrar un usuario pero no borrar sus cosas. Evita que se rompa el sistema o aparezcan errores.

3. ¿Qué ventaja ofrece el using var connection = ... frente a abrir/cerrar manual?
R// Te asegura que la conexión se cierra sola, incluso si hay errores. Si lo hacés manual y te olvidás de cerrarla, podés saturar la base de datos.

4. ¿Por qué _connectionString está readonly y qué pasaría si no lo fuera?
R// Se pone readonly para que no se pueda cambiar una vez definido. Si alguien lo modifica en medio del código, podría redirigir la conexión a cualquier lado, incluso algo malicioso.

5. ¿Qué harías para agregar logros?
R// Crearía una clase Logro, y una lista que relacione jugadores con logros (JugadorLogro). Luego, métodos para asignar logros, consultarlos, y quizás quitarlos.

6. ¿Qué pasa con la conexión en un bloque using si hay una excepción?
R// Igual se cierra. Por eso es tan útil. No tenés que preocuparte por liberar recursos.

7. ¿Qué pasa si no hay jugadores en ObtenerTodos()?
R// Te devuelve una lista vacía. Y eso está bien, porque evita errores tipo null reference.

8. ¿Cómo registrarías tiempo jugado por jugador?
R// Agregaría un campo TiempoJugado en la clase Jugador, y después un método tipo SumarTiempo(int minutos) para irlo actualizando.

9. ¿Para qué sirve el try-catch en TestConnection() y por qué devolver un booleano?
R// Sirve para atrapar errores si la conexión falla. Devolver true o false es más amigable que lanzar un error que corte todo el programa.

10. ¿Por qué se separan las clases en carpetas como Models, Services, etc.?
R// Porque te ayuda a tener todo ordenado. Sabés dónde está cada cosa, y si alguien nuevo entra al proyecto, no se pierde.

11. ¿Por qué usar una transacción en AgregarItem de inventario?
R// Si estás haciendo varios cambios y algo falla en el medio, con la transacción podés volver todo atrás. Sin eso, podrías dejar el sistema en un estado roto.

12. ¿Por qué JugadorService recibe un DatabaseManager y no lo crea?
R// Es para que todos los servicios compartan la misma base. También es parte de un patrón llamado inyección de dependencias, que hace que el código sea más limpio y fácil de testear.

13. ¿Qué pasa si buscás un jugador con un ID que no existe?
R// Lo más común es que te devuelva null. También podrías hacer que devuelva un error o un mensaje personalizado, depende del diseño.

14. ¿Cómo harías un sistema de "amigos" entre jugadores?
R// Creás una clase Amistad con JugadorId1 y JugadorId2. Después, agregás funciones para enviar, aceptar o eliminar amistades.

15. ¿Cómo se maneja la fecha de creación de un jugador?
R// En tu proyecto no se maneja, pero podrías agregar un campo FechaCreacion y setearlo en el constructor. O dejar que la base lo haga con un valor por defecto.

16. ¿Por qué GetConnection() crea una nueva conexión cada vez?
R// Porque las conexiones no se comparten por temas de seguridad y estabilidad. Reutilizar una abierta puede traer problemas con datos mezclados o bloqueos.

17. ¿Qué pasaría si dos usuarios modifican el mismo recurso al mismo tiempo?
R// Puede haber conflictos. Para evitar eso, podés usar bloqueo de registros o control de versiones (tipo ver si alguien más ya lo cambió antes de guardar).

18. ¿Por qué es importante verificar rowsAffected después de un UPDATE?
R// Porque te dice si el cambio realmente afectó algo. Si es cero, capaz no existía ese jugador, o no cambiaste nada. Es info útil para el usuario.

19. ¿Cómo harías un sistema de logging sin romper el código actual?
R// Crearía una clase Logger y la llamaría desde los métodos importantes. También podés meterlo como parte de los servicios sin tocar demasiado lo que ya funciona.

20. ¿Cómo agregarías un "Mundo" donde cada jugador puede estar en varios?
R// Crearías una clase Mundo y otra intermedia tipo JugadorMundo. Así podés tener relaciones muchos a muchos. En los servicios, funciones para mover jugadores entre mundos, listar mundos por jugador, etc.

21. ¿Qué es un SqlConnection y cómo se usa?
R// Es un objeto que sirve para conectarte a una base de datos SQL. Lo abrís, hacés tus queries, y lo cerrás. En tu proyecto actual no se usa porque todo está en memoria.

22. ¿Para qué sirven los SqlParameter?
R// Se usan para pasar valores seguros a las consultas SQL y evitar inyecciones. Son muy importantes si tu sistema tiene inputs de usuarios.

