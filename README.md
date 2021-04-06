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

Klienten består af en Console application. Der oprettes et nyt Item, alle Items hentes og til sidst hentes det Item, der har Id = 5.
Der benyttes et simpelt repository, der også indeholder Item-klassen.

&nbsp;

## Sådan foretages en demo

Begge projekter skal startes op. Det gøres nemmest således:

Højre klik på Solution og vælg Properties. Vælg *Startup Project | Multiple startup projects*
og sæt både *WebApi* og Console project til Start. 
Start ved at trykk F5 eller den grønne pil-knap.
