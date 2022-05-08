# 4.NetworkResiliencePolly

Demo af Nuget pakken Polly, der benyttes til at "hærde" en netværksforbindelse eller dens Resilience.

> [Github: App-vNext/Polly](https://github.com/App-vNext/Polly)
> 
> [Welcome to the Polly wiki!](https://github.com/App-vNext/Polly/wiki)
> 
> [Fault Handling with Polly – A Beginners Guide](https://dotnetplaybook.com/fault-handling-with-polly-a-beginners-guide/)

### Demo af Polly 

Forudsætninger:

- ItemsController har variablen errorPercent = 100, svarende til at den i alle tilfælde returnerer HTTP 500 error. Variablen findes i WebApi og ItemsController klassen.
- WebAPI projektet har et LogLevel = Debug i appsettings.Development.json filen
- WebAPI projektet hostes i Kestrel og på port 5001
- Både HttpClientDemo og WebApi projekterne startes samtidigt
- Når begge projekter er loaded, skal både consollen for WebAPI og output-vinduet for HttpClientDemo vises.

##### Polly med RetryAsync policy

1. Når der klikkes på tabben BROWSE i simulatoren, prøver clienten 10 gange, hvilket kan ses i console vinduet for WebAPI.
2. Når den prøver den 11. gang, opstår der en `HttpRequestExceptionEx` i Output vinduet.
3. Sæt variablen errorPercent = 50 og se at nu er det tilfældigt om vi får statuskode 200 eller om den skal prøve et antal gange, dog maks. 10.

##### Polly med WaitRetryAsync policy

Udkommenter `.RetryAsync(10)` i `GetAsync<T>` metoden i Repository. Nu demonstreres tre forskellige udgaver af RetryAsync policy

1. Sæt variablen errorPercent = 100 igen. 
2. Når der klikkes på tabben BROWSE i simulatoren, prøver clienten 5 gange med 3 sekunders mellemrum, hvilket kan ses i console vinduet for WebAPI.
2. Når den prøver den 6. gang, opstår der en `HttpRequestExceptionEx` i Output vinduet.
3. En overloaded udgave med *exponential back-off* demonstreres. Der ventes 2, 4, 8, 16, 32 sekunder, hvorefter den igen fejler.
4. Endnu en overloaded udgave der kan kalde en action når hvert forsøg påbegyndes. Udskriver hvor lang tid den vil vente inden næste gang.

#### Policy in selvstændig klasse

I Policies/RetryPolicy.cs static klassen oprettes en metode `GetRetryPolicy()`, der returnerer en policy, som eksekveres som tidligere.