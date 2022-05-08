# 4.NetworkResiliencePolly

Demo af Nuget pakken Polly, der benyttes til at "h�rde" en netv�rksforbindelse eller dens Resilience.

> [Github: App-vNext/Polly](https://github.com/App-vNext/Polly)
> 
> [Welcome to the Polly wiki!](https://github.com/App-vNext/Polly/wiki)
> 
> [Fault Handling with Polly � A Beginners Guide](https://dotnetplaybook.com/fault-handling-with-polly-a-beginners-guide/)

### Demo af Polly 

Foruds�tninger:

- ItemsController har variablen errorPercent = 100, svarende til at den i alle tilf�lde returnerer HTTP 500 error. Variablen findes i WebApi og ItemsController klassen.
- WebAPI projektet har et LogLevel = Debug i appsettings.Development.json filen
- WebAPI projektet hostes i Kestrel og p� port 5001
- B�de HttpClientDemo og WebApi projekterne startes samtidigt
- N�r begge projekter er loaded, skal b�de consollen for WebAPI og output-vinduet for HttpClientDemo vises.

##### Polly med RetryAsync policy

1. N�r der klikkes p� tabben BROWSE i simulatoren, pr�ver clienten 10 gange, hvilket kan ses i console vinduet for WebAPI.
2. N�r den pr�ver den 11. gang, opst�r der en `HttpRequestExceptionEx` i Output vinduet.
3. S�t variablen errorPercent = 50 og se at nu er det tilf�ldigt om vi f�r statuskode 200 eller om den skal pr�ve et antal gange, dog maks. 10.

##### Polly med WaitRetryAsync policy

Udkommenter `.RetryAsync(10)` i `GetAsync<T>` metoden i Repository. Nu demonstreres tre forskellige udgaver af RetryAsync policy

1. S�t variablen errorPercent = 100 igen. 
2. N�r der klikkes p� tabben BROWSE i simulatoren, pr�ver clienten 5 gange med 3 sekunders mellemrum, hvilket kan ses i console vinduet for WebAPI.
2. N�r den pr�ver den 6. gang, opst�r der en `HttpRequestExceptionEx` i Output vinduet.
3. En overloaded udgave med *exponential back-off* demonstreres. Der ventes 2, 4, 8, 16, 32 sekunder, hvorefter den igen fejler.
4. Endnu en overloaded udgave der kan kalde en action n�r hvert fors�g p�begyndes. Udskriver hvor lang tid den vil vente inden n�ste gang.

#### Policy in selvst�ndig klasse

I Policies/RetryPolicy.cs static klassen oprettes en metode `GetRetryPolicy()`, der returnerer en policy, som eksekveres som tidligere.