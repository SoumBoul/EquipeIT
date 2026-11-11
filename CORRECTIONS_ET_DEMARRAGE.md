# EquipIT — corrections et démarrage

## Démarrer sous Windows

Décompressez le ZIP dans un nouveau dossier, puis ouvrez EquipIT.sln.
Conservez votre ancien dossier et son historique Git. Cette archive ne contient pas les dossiers .git, .vs, bin et obj.
.NET SDK 10 est nécessaire. Le premier lancement restaure les packages NuGet, dont SwaggerUI.

Depuis le dossier EquipeIT, dans PowerShell :

```powershell
dotnet dev-certs https --trust
dotnet restore EquipIT.sln
dotnet test EquipIT.sln
dotnet run --project .\EquipIT.Api --launch-profile https
```

Gardez ce terminal ouvert. Dans un deuxième terminal, depuis le même dossier :

```powershell
dotnet run --project .\EquipIT.Front --launch-profile https
```

- Front : https://localhost:7211
- Swagger : https://localhost:7093/swagger/index.html
- Document OpenAPI : https://localhost:7093/openapi/v1.json
- Liste JSON : https://localhost:7093/api/equipment/All

Ouvrez d'abord l'API pour vérifier le certificat HTTPS si le navigateur refuse la connexion.
Les deux applications doivent tourner en même temps. Si un exécutable est verrouillé, arrêtez l'ancienne instance dans Visual Studio ou son terminal avant de relancer.

## Modifications

- Ajout de l'interface SwaggerUI, reliée au document OpenAPI existant et disponible en Development.
- Conservation de l'enregistrement IEquipmentService / EquipmentService.
- Autorisation CORS des deux adresses locales du front (HTTPS 7211 et HTTP 5200).
- GET : liste vide autorisée et lecture sans suivi EF Core.
- Emprunt : DTO JSON FirstName / LastName, validation des noms et formulaire front opérationnel.
- Retour : suppression des paramètres prénom/nom inutiles, cohérent avec l'appel du front.
- Correction du contrôle null sur un équipement introuvable et de FindEquipmentAsync qui levait toujours une exception.
- Réponses 204 pour une modification réussie, 400 pour une requête invalide, 404 si absent, 409 si déjà emprunté/disponible.
- Affichage des erreurs de chargement avec bouton Réessayer, puis actualisation de la liste après emprunt/retour.
- Ajout de tests sur les équipements absents, les noms vides et une liste vide.

La base fournie contient Monitor, Keyboard et Mouse. Elle est conservée dans l'archive.
Le système de traduction existant n'a pas été modifié.

## Vérification manuelle

1. Vérifiez les trois équipements sur le front.
2. Empruntez un équipement avec prénom et nom : ils doivent apparaître, avec la date et le bouton Retour.
3. Rendez-le : il redevient disponible et la date de retour apparaît.
4. Dans Swagger, testez un identifiant absent (404), un second emprunt (409) et un nom vide (400).

## Validation dans l’environnement de correction

La base SQLite a passé le contrôle d’intégrité et contient les trois équipements.
Les fichiers de projet XML, les ressources et les configurations JSON ont été vérifiés.
La compilation et les tests .NET n’ont pas pu être exécutés : la restauration NuGet a été bloquée par les restrictions réseau de cet environnement.
Lancez les commandes ci-dessus sur votre PC pour terminer la validation, puis effectuez le parcours manuel.
