# 5.Monkey_Cache

Benytter [jamesmontemagno/monkey-cache](https://github.com/jamesmontemagno/monkey-cache) library. 

Tilf�j NuGet pakken **MonkeyCache.SQLite** til alle Xamarin-projekterne.

Tilf�j f�lgende i App.xaml.cs lige efter `InitializeComponent()`:

`MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";`

I Service-klassen laves forskellige test, som hvis de opfyldes vil returnere data fra Cachen:
- Mangler der en aktiv netv�rksforbindelse?
- Er tiden for *expired* ikke g�et endnu?

Hvis ingen af disse test resulterer i at Cachen l�ses, bliver Cachen opdateret med friske data.
Her ses et eksempel fra `GetItemsAsync()`:

```csharp
public async Task<IEnumerable<Item>> GetItemsAsync()
{
    UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
    {
        Path = ApiConstants.ItemsEndpoint
    };

    string url = builder.Path;

    if (Connectivity.NetworkAccess == NetworkAccess.None)
    {
        return Barrel.Current.Get<IEnumerable<Item>>(key: url);
    }
    if (!Barrel.Current.IsExpired(key: url))
    {
        return Barrel.Current.Get<IEnumerable<Item>>(key: url);
    }

    var items = await _genericRepository.GetAsync<IEnumerable<Item>>(builder.ToString());
    //Saves the cache and pass it a timespan for expiration
    Barrel.Current.Add(key: url, data: items, expireIn: TimeSpan.FromSeconds(20));
    return items;
}
```

Demo: S�t breakpoints i ItemService-klassen ved de to betingelser for l�sning af Cachen, samt for ny-indl�sning.
Bem�rk at Airplane mode kun virker p� en fysisk device!

&nbsp;
