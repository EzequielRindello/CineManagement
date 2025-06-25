# CinemaManagement
 Cree el readme con la estructura del proyecto y un breve resumen de cada archivo para ayudar a entender la organizacion del codigo.
 Cada clase/metodo sigue Single Responsability Principle, y esta maximizando el paradigma clase - objeto.
---
# Requisitos
- Cada pelicula solo puede ser dirigida por un director. Un director puede dirigir muchas películas.
- Puede haber máximo 10 funciones durante el día por director.
- Las películas internacionales tienen máximo 8 funciones asignadas. Las películas nacionales, para impulsar el cine, no poseen dicho límite.
---
## Estructura del proyecto
CineManagement/
├── Models/
│ ├── Movie.cs # Representa una pelicula
│ ├── Director.cs # Representa un director
│ └── MovieFunction.cs # Representa una funcion de cine
├── Services/
│ ├── FileService.cs # Maneja la carga de archivos/datos (movies.txt, directors.txt)
│ ├── ValidationService.cs # Valida las reglas de negocio
│ └── FunctionService.cs # Manejo de funciones ABM (crear, editar, eliminar)
├── UI/
│ └── MenuService.cs # Maneja toda la interfaz de usuario (UI)
├── CinemaManager.cs # Orquesta toda la aplicacion
└── Data/                 
    ├── movies.txt       # Archivo para almacenar peliculas
    └── directors.txt     # Archivo para almacenar directores
