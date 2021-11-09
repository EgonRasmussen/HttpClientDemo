## Debug af Android-projektet i forhold til netværksprotokollen
Nu skal vi arbejde på Android emulatoren og det giver nogle netværks- og sikkerhedsmæssige udfordringer:

### localhost

WebApi'et kører på adressen `http://localhost:5001`. Imidlertid benytter Android Emulatoren et helt andet net, og derfor skal man benytte adressen `https:10.0.2.2:5001`
for at ramme WebApi og localhost.

&nbsp;

### HTTP

Android tillader ikke at man tilgår http-ressourcer uden kryptering, men kræver TLS. Dette kan man imidlertid omgå ved at tilføje følgende attribut `android:usesCleartextTraffic="true"` til 
`application`-noden i *AndroidManifest.xml* (som ligger i *Properties* folderen):

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" 
        android:versionName="1.0" package="com.companyname.httpclientdemo">
    <uses-sdk android:minSdkVersion="21" android:targetSdkVersion="28" />
      <application android:label="HttpClientDemo.Android" 
          android:theme="@style/MainTheme" 
          android:usesCleartextTraffic="true">
       </application>
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
</manifest>
```

&nbsp;

### HTTPS

Android tillader som udgangspunkt ikke at man tilgår https-ressourcer, som ikke har et gyldigt certifikat. Og selv om man installerer sit developer-certificat, så er det ikke gyldigt. 
I Debug mode kan man lave en Bypass Certificate Validation ved at justere oprettelsen af HttpClient objektet på følgende måde:

```csharp
public class GenericRepository : IGenericRepository
{
    private HttpClient httpClient;

    HttpClientHandler httpClientHandler = new HttpClientHandler();

    public GenericRepository()
    {
#if DEBUG
        httpClientHandler.ServerCertificateCustomValidationCallback = 
            (message, certificate, chain, sslPolicyErrors) => true;
#endif
        httpClient = new HttpClient(httpClientHandler);
    }
..... øvrigt kode udeladt
```

Der skal desuden linkes til filen i AndroidManifest.xml:

&nbsp;

## Få adgang til WebApi hosted lokalt vha. Conveyor by Keyoti

En anden mulighed at er at installere en gratis extension til Visual Studio, som kaldes [Conveyor from Keyoti](https://conveyor.cloud/).

I Visual Studio vælges *EXTENSIONS | Manage Extensions* og der søges efter *Conveyor from Keyoti*. Efter genstart af VS følger man [vejledningen](https://conveyor.cloud/Home/How_To_Install) og åbner for nogle porte i firewall. 

Kig på denne guide for at benytte IIS Express: [How to setup a remote connection to IIS Express](https://conveyor.cloud/Help/Setup_remote_connection_Visual_Studio_IIS_Express)

Det er nødvendigt at installere Conveyor's Root Certificate for at browseren accepterer HTTPS kommunikationen, hvilket er beskrevet i denne guide: [TLS and SSL Certificate Install Help](https://conveyor.cloud/Help/SSL)