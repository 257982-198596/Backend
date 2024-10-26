# Backend

API - Acceso a Datos

git clone
copiamos los archivos desde github al pc
luego abrimos el archivo LogicaNegocio.csproj

git status
vemos en que rama estamos, y cambios pendientes a commitear

git checkout
cambiar de rama

git checkout -b "feature/nombre-de-la-rama"
me cambio de rama y a la vez la creo con el nuevo nombre

git branch -a 
listar ramas locales y remotas (antes del pull muestra informacion vieja!)

git pull
lo que tengo que hacer siempre que vaya a arrancar a trabajar, sino veo informacion vieja\
Este comando me baja los metadatos desde github con la informacion mas actual

git rm --cached *.cache
remover de seguimiento archivos .cache


# Quitar archivos .cache
*.cache

# Directorio de compilación y NuGet
obj/
*.nuget.*
project.assets.json

#### Trunkbase con feature branch

Toda la implementación se fue generando, creando distintas ramas feature/xxxx-xxxx

![trunk](./imagenes/trunk.png)

Desde la rama principal (trunk), crea una nueva rama para la característica:


```markdown
git checkout main
git pull origin main
git checkout -b feature/nueva-funcionalidad
```
##### Desarrollar en la feature branch:
Realiza los cambios necesarios en el código. Asegúrate de realizar commits pequeños y frecuentes:

```markdown
git add .
git commit -m "Implementa parte de la nueva funcionalidad"
git push
```
##### Pull Request
Finalmente para correcto seguimiento del código por parte del equipo, los cambios se implementan en la rama main, previa aprobación para este merge en github.

##### Eliminar la feature branch:
Después de que la rama ha sido fusionada y el código ha sido desplegado, se elimina la feature branch tanto local como remotamente:

```markdown
git branch -d feature/nueva-funcionalidad
git push origin --delete feature/nueva-funcionalidad
```