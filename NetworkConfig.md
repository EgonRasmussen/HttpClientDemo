## Tilf�jelser til Android-projektet i forhold til netv�rksprotokollen

### HTTP

Android tillader ikke l�ngere at man tilg�r http-ressourcer, men kr�ver TLS. Dette kan man imidlertid omg� ved at tilf�je f�lgende attribut `android:usesCleartextTraffic="true"` til 
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

Android tillader ikke l�ngere at man tilg�r https-ressourcer, som ikke har et gyldigt certifikat. Og selv om man installerer sit developer-certificat, s� er det ikke gyldigt. 
Dette kan man imidlertid klare ved at tilf�je en lille XML-fil kaldet `netvork_security_config.xml` til folderen `Resources/values/xml`:

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

## F� adgang til WebApi hosted lokalt vha. Conveyor by Keyoti

Det er normalt ikke muligt for  en device/emulator at f� adgang til den lokale developer webserver IIS Express eller Kestrel. Eller rettere, det kr�ver en �ndring i konfigurationen, som vi helst er fri for.

Heldigvis kan man n�jes med at installere en gratis extension til Visual Studio, som kaldes [Conveyor from Keyoti](https://conveyor.cloud/).

I Visual Studio v�lges *EXTENSIONS | Manage Extensions* og der s�ges efter *Conveyor from Keyoti*. Efter genstart af VS f�lger man [vejledningen](https://conveyor.cloud/Home/How_To_Install) og �bner for nogle porte i firewall. 

Kig p� denne guide for at benytte IIS Express: [How to setup a remote connection to IIS Express](https://conveyor.cloud/Help/Setup_remote_connection_Visual_Studio_IIS_Express)

Det er n�dvendigt at installere Conveyor's Root Certificate for at browseren accepterer HTTPS kommunikationen, hvilket er beskrevet i denne guide: [TLS and SSL Certificate Install Help](https://conveyor.cloud/Help/SSL)