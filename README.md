# HttpClient Demo

## 2.Refit 

Et simpelt WebApi, der benytter klassen Item.

BaseUrl: https:localhost:5001

```
GET: api/items      : liste af items
GET: api/items/3    : item med id=3
POST: api/item      : add af nyt item
PUT: api/item       : update af item
DELETE: api/item/3  : slet item=3
```

Klienten består af et Console app. som benytter [Refit](https://github.com/reactiveui/refit) library.

&nbsp;


