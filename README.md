
#  Haiii!



Ha gyakorolsz a feladatokra, vagy szeretnéd a gyakorló feladatok megoldását, akkor kérlek használd ezt a repót.

Ha pedig van olyan feladat, amit te megoldottál, de nem látod itt, kérlek készíts egy új branchet és töltsd fel oda!

#  Laravel API 101 Kevin részéről:

https://github.com/ArpKevin/lara_api_gyorstalpi  

#  Feladatok feltöltése

Ha valaki nem tudná, hogy hogyan kell, itt egy step-by-step tutorial **Git Bash** használatával:
  
###  1. Repo klónozása (hogy up-to-date legyen a lokális verzió)
  
```bash
git  clone  https://github.com/mmiillmm/vizsgaFeladatokRepo.git
cd  vizsgaFeladatokRepo
```
  
---
  
###  2. Új üres branch létrehozása
  
```bash
git  checkout  --orphan  branch-nev
```
  
**Példa:**
  
```bash
git  checkout  --orphan  feladat-random
```
  
---
  
###  3. Létező fájlok törlése (nem kötelező)
  
```bash
rm  -rf  *
```
  
---
  
###  4. Saját fájlok bemásolása a repo mappájába
  
```bash
cp  -r  /lemez/Felhasználók/neved/tefeladatmappád/*  .
```
  
---
  
###  5. Fájlok stagelése és commitolása
  
```bash
git  add  .
git  commit  -m  "Commit üzenet"
```
  
---

###  6. Feltöltés GitHubra

```bash
git  push  -u  origin  branch-nev
```

---

# Feladatok letöltése (minden branch külön mappába)

###  1. Repo URL beállítása

```bash
REPO_URL="https://github.com/mmiillmm/vizsgaFeladatokRepo.git"
```

---

###  2. Szülőmappa elnevezése

```bash
PARENT_DIR="mappanev"
```

---

###  3. Szülőmappa létrehozása

```bash
mkdir  -p  "$PARENT_DIR"
```

---

###  4. Remote branchek listázása

```bash
BRANCHES=$(git  ls-remote  --heads  $REPO_URL  |  awk  '{print $2}'  |  sed  's|refs/heads/||')
```

---

###  5. Összes branch klónozása külön mappákba

  

```bash
for  BRANCH  in  $BRANCHES;  do
	git  clone  --single-branch  --branch  "$BRANCH"  "$REPO_URL"  "$PARENT_DIR/$BRANCH"
done
```
