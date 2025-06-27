# CinemaManagement
 Cree el readme con la estructura del proyecto y un breve resumen de cada archivo para ayudar a entender la organizacion del codigo.
 Cada clase/metodo sigue Single Responsability Principle, y esta maximizando el paradigma clase - objeto.

# Requisitos
- Cada pelicula solo puede ser dirigida por un director. Un director puede dirigir muchas películas.
- Puede haber máximo 10 funciones durante el día por director.
- Las películas internacionales tienen máximo 8 funciones asignadas. Las películas nacionales, para impulsar el cine, no poseen dicho límite.

## Estructura del proyecto
```
CinemaManagement/
├── Models/
│   ├── Movie.cs            # Representa una película
│   ├── Director.cs         # Representa un director
│   └── MovieFunction.cs    # Representa una función de cine (horario, película, sala)
│
├── Services/
│   ├── FileService.cs        # Carga y guarda datos desde/hacia archivos de texto
│   ├── ValidationService.cs  # Aplica las reglas de negocio (ej: límites de funciones)
│   └── FunctionService.cs    # Alta, baja y modificación de funciones de cine
│
├── UI/
│   └── MenuService.cs        # Maneja el menú de opciones e interacción con el usuario
│
├── Data/
│   ├── movies.txt            # Contiene los datos de las películas
│   └── directors.txt         # Contiene los datos de los directores
│
└── CinemaManager.cs         # Clase principal que coordina la ejecución de la aplicación
```
