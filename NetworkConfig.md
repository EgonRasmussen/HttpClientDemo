## Tilføjelser til Android-projektet i forhold til netværksprotokollen

### HTTP

Android tillader ikke længere at man tilgår http-ressourcer, men kræver TLS. Dette kan man imidlertid omgå ved at tilføje følgende attribut `android:usesCleartextTraffic="true"` til 
`application`-noden i *AndroidManifest.xml* (som ligger i *Properties* folderen):

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.companyname.httpclientdemo">
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

## Få adgang til WebApi hosted lokalt vha. Conveyor by Keyoti

Det er normalt ikke muligt for  en device/emulator at få adgang til den lokale developer webserver IIS Express eller Kestrel. Eller rettere, det kræver en ændring i konfigurationen, som vi helst er fri for.

Heldigvis kan man nøjes med at installere en gratis extension til Visual Studio, som kaldes [Conveyor from Keyoti](https://conveyor.cloud/).

I Visual Studio vælges *EXTENSIONS | Manage Extensions* og der søges efter *Conveyor from Keyoti*. Efter genstart af VS følger man [vejledningen](https://conveyor.cloud/Home/How_To_Install) og åbner for nogle porte i firewall. 

Kig på denne guide for at benytte IIS Express: [How to setup a remote connection to IIS Express](https://conveyor.cloud/Help/Setup_remote_connection_Visual_Studio_IIS_Express)

Det er nødvendigt at installere Conveyor's Root Certificate for at browseren accepterer HTTPS kommunikationen, hvilket er beskrevet i denne guide: [TLS and SSL Certificate Install Help](https://conveyor.cloud/Help/SSL)