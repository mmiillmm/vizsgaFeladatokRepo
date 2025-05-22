# Haiii!

Ha gyakorolsz a feladatokra, vagy szeretnéd a gyakorló feladatok megoldását akkor kérlek használd ezt a repot. Ha pedig van olyan feladat amit te megoldottál, de nem látod itt, kérlek készíts egy új branchet és töltsd fel oda!


# Feladatok feltöltése

Ha valaki nem tudná hogy hogy kell, itt egy step-by-step tutorial git bash használatával.

## 1. Repo clone (hogy up-to-date legyen a lokális verzió)

```git clone https://github.com/mmiillmm/vizsgaFeladatokRepo.git```

```cd vizsgaFeladatokRepo```

## 2.  Új üres branch

```git checkout --orphan branch-nev```

Például:

```git checkout --orphan feladat-random```

## 3. Létező fájlok törlése (nem muszáj)

```rm -rf *```

## 4. Saját fájlok másolása a mappába

```cp -r  /lemez/Felhasználók/neved/tefeladatmappád/* .```



## 5. Stagelés és commitolás

```git add .```
```git commit -m "Commit üzenet"```

## 6. Pusholás GitHubra

```git push -u origin branch-nev```


# :3
