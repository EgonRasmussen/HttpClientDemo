# HttpClient Demo

## 1.ConsoleHttpClient_WebAPI

#### WebApi

Et simpelt WebApi, der benytter klassen `Item`.

**BaseUrl:** https://localhost:5001

```
GET: api/items      : liste af items
GET: api/items/3    : item med id=3
POST: api/item      : add af nyt item
PUT: api/item       : update af item
DELETE: api/item/3  : slet item=3
```

#### Client

Klienten best�r af en Console application. Der oprettes et nyt Item, alle Items hentes og til sidst hentes det Item, der har Id = 5.
Der benyttes et simpelt repository, der ogs� indeholder Item-klassen.

&nbsp;

## S�dan foretages en demo

Begge projekter skal startes op. Det g�res nemmest s�ledes:

H�jre klik p� Solution og v�lg Properties. V�lg *Startup Project | Multiple startup projects*
og s�t b�de *WebApi* og Console project til Start. 
Start ved at trykk F5 eller den gr�nne pil-knap.
