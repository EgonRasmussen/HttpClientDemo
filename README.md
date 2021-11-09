## Debug af Android-projektet i forhold til netv�rksprotokollen
Nu skal vi arbejde p� Android emulatoren og det giver nogle netv�rks- og sikkerhedsm�ssige udfordringer:

### localhost

WebApi'et k�rer p� adressen `http://localhost:5001`. Imidlertid benytter Android Emulatoren et helt andet net, og derfor skal man benytte adressen `https:10.0.2.2:5001`
for at ramme WebApi og localhost.

&nbsp;

### HTTP

Android tillader ikke at man tilg�r http-ressourcer uden kryptering, men kr�ver TLS. Dette kan man imidlertid omg� ved at tilf�je f�lgende attribut `android:usesCleartextTraffic="true"` til 
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

Android tillader som udgangspunkt ikke at man tilg�r https-ressourcer, som ikke har et gyldigt certifikat. Og selv om man installerer sit developer-certificat, s� er det ikke gyldigt. 
I Debug mode kan man lave en Bypass Certificate Validation ved at justere oprettelsen af HttpClient objektet p� f�lgende m�de:

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
..... �vrigt kode udeladt
```

Der skal desuden linkes til filen i AndroidManifest.xml:

&nbsp;

## F� adgang til WebApi hosted lokalt vha. Conveyor by Keyoti

En anden mulighed at er at installere en gratis extension til Visual Studio, som kaldes [Conveyor from Keyoti](https://conveyor.cloud/).

I Visual Studio v�lges *EXTENSIONS | Manage Extensions* og der s�ges efter *Conveyor from Keyoti*. Efter genstart af VS f�lger man [vejledningen](https://conveyor.cloud/Home/How_To_Install) og �bner for nogle porte i firewall. 

Kig p� denne guide for at benytte IIS Express: [How to setup a remote connection to IIS Express](https://conveyor.cloud/Help/Setup_remote_connection_Visual_Studio_IIS_Express)

Det er n�dvendigt at installere Conveyor's Root Certificate for at browseren accepterer HTTPS kommunikationen, hvilket er beskrevet i denne guide: [TLS and SSL Certificate Install Help](https://conveyor.cloud/Help/SSL)