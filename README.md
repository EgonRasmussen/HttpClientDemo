## Debug af Android-projektet i forhold til netværksprotokollen
Nu skal vi arbejde på Android emulatoren og det giver nogle netværks- og sikkerhedsmæssige udfordringer:

### localhost

WebApi'et kører på adressen `https://localhost:5001`. Imidlertid benytter Android Emulatoren et helt andet net, og derfor skal man benytte adressen `https:10.0.2.2:5001`
for at ramme WebApi og localhost.

&nbsp;

### HTTPS

Android tillader som udgangspunkt ikke at man tilgår https-ressourcer, som ikke har et gyldigt certifikat. Og selv om man installerer sit developer-certificat, så er det ikke gyldigt. 
I Debug mode kan man lave et *dirty fix*, også kaldet en *Bypass Certificate Validation* ved at justere oprettelsen af HttpClient objektet på følgende måde:

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

Se evt. alternativ teknik til sidst i denne fil.
&nbsp;

### HTTP

Ønsker man at benytte HTTP, skal man være opmærksom på at Android ikke tillader at man tilgår http-ressourcer uden kryptering, men kræver TLS. 
Dette kan man imidlertid omgå ved at tilføje følgende attribut `android:usesCleartextTraffic="true"` til 
`application`-noden i *AndroidManifest.xml* (som ligger i *Properties* folderen). Her ses et eksempel, hvor den nye attribut ligger på linje 7:

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

## Få adgang til WebApi hosted lokalt vha. Conveyor by Keyoti

En anden mulighed at er at installere en gratis extension til Visual Studio, som kaldes [Conveyor from Keyoti](https://conveyor.cloud/).

I Visual Studio vælges *EXTENSIONS | Manage Extensions* og der søges efter *Conveyor from Keyoti*. Efter genstart af VS følger man [vejledningen](https://conveyor.cloud/Home/How_To_Install) og åbner for nogle porte i firewall. 

Kig på denne guide for at benytte IIS Express: [How to setup a remote connection to IIS Express](https://conveyor.cloud/Help/Setup_remote_connection_Visual_Studio_IIS_Express)

Det er nødvendigt at installere Conveyor's Root Certificate for at browseren accepterer HTTPS kommunikationen, hvilket er beskrevet i denne guide: [TLS and SSL Certificate Install Help](https://conveyor.cloud/Help/SSL)

---------
&nbsp;

### Alternativ teknik til at benytte HTTPS (ikke helt færdig)

Android tillader ikke længere at man tilgår https-ressourcer, som ikke har et gyldigt certifikat. Og selv om man installerer sit developer-certificat, så er det ikke gyldigt. 
Dette kan man imidlertid klare ved at tilføje en lille XML-fil kaldet `netvork_security_config.xml` til folderen `Resources/values/xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<network-security-config>
  <base-config cleartextTrafficPermitted="false">
    <trust-anchors>
      <certificates src="system" />
      <certificates src="user"/>
    </trust-anchors>
  </base-config>
</network-security-config>
```

Der skal desuden linkes til filen i AndroidManifest.xml:

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.companyname.httpclientdemo">
    <uses-sdk android:minSdkVersion="21" android:targetSdkVersion="28" />
      <application android:label="HttpClientDemo.Android" 
          android:theme="@style/MainTheme" 
          android:networkSecurityConfig="@xml/network_security_config">
       </application>
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
</manifest>
```
&nbsp;