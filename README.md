# Dokumentacja Aplikacji Internetowej Formularz
#### Spis Treści
$<toc{"level":[2,3]}>
> Wersja dokumentacji V1.4
> Wersja Oprogramowania V2.314
> Autor Dokumntacji i Oprogramowania: Daniel Adamczak
## 1 Specyfikacja Techniczna
#### Nazwa Projektu: Formularz
Technologia: **ASP .NET**
Framework użyty do produkcji:
- **SDK 6.0.404**
- **ASP.NET Core Runtime 6.0.12**

- Backend: **C#, Json, XML**
- Frontend: **Html, Css, JavaScript**
- Baza Danych: **T SQL**

#### Wymogi do uruchomienia aplikacji:
**Domena:**
Użytkownik w domenie który jest wstanie odpytać domenę pod kątem sprawdzenia czy użytkownik istnieje i podał dobre hasło. Dodatkowo pobierze potrzebne mu informacje o użytkowniku.

**Serwer pocztowy:** 
Poczta na której jest użytkownik z mailem z którego można wysyłać wiadomości email na wskazaną skrzynkę

**Serwer IIS:**
Sewer który uruchomi i udostępni w sieci usługę formularza

**Serwer MS SQL:**
MS SQL Standard Edition (64 bit) 2016
Użytkownik bazodanowy mogący dodawać wpisy do rekordów oraz je pobierać
## 2 Instalacja
Trzeba zainstalować role na serwerze:
![de18abf95f11123fb770237debaf8d59.png](:/66c0d66316b74e10a030b66685e58f79)
Na serwerze również trzeba doinstalować:
![e8b09d14255a5a219438dd17a0b6fee1.png](:/e779ffd7f04a428988ee0413d6649bc1)
oraz napewno [ASP.NET Core Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) zainstalować. Dla pewności można jeszcze [SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) zainstalować dla pewności.

Folder z skompilowaną stroną trzeba umieścić w katalogu **C:\inetpub\wwwroot**
Następnie tworzymy stronę na serwerze IIS z dwoma odwołaniami jak w przykładnie
![3b0a0c49f5a23356e840d42d8184611d.png](:/26fbf20eb14d4b55b2222979d0a59961)
oraz trzeba zaznaczyć by było automatyczne przekierowanie z **http na https**
![f00b0087bd4f31f67f824b9e066a2853.png](:/896c31fc54da48db807ed13efe62a178)

Jeżeli są odwoałania w DNS do tej strony powinna się bez problemu uruchomić jeżeli jest IIS Error po otworzeniu jej a kod błędu to **500.19** z sourcem **-1 0** oznacza że serwer nie może odczytać pliku **web.config** prawdopodobnie przez **brak** jakiegoś **komponentu**
## 3 Konfiguracja
Cała konfuguracja do strony znajduje się w pliku **appsettings.json**, który znajduję sie w głównym katalogu strony.

``` json
{
	/*Ustawienia do wysyłki mali*/
	"MailProperty": {
		/*Ustawienia dotyczące logowania i połączenia się na serwer pocztowy*/
    "Settings": {
      "Domain": "",
      "Server": "",
      "Port": 587,
      "EnableSSl": true,
      "Username": "",
      "Login": "",
      "Password": ""
    },
		/*Ustawienie maili odbiorców "globalnych"*/
    "MailTo": {
      "HR": "",
      "Zarzad": "",
      "Dyrektor": "",
      "HelpDesk": ""
    },
		/*Link do podglądu formularzy na których dokonywane są zmiany
		Uwaga:
		Zmieniamy tylko fragment localhost:7147
		Cała reszta musi pozostać bez zmian*/
    "LinkForm": "https://localhost:7147/ViewFormsDetails/",
		/*Zawartość maila która nie jest automatyczna generowana*/
    "BodyMail": {
		/*Zawartość maili dla nowych formularzy*/
      "NewForm": {
		  /*do HR*/
        "ToHR": "Został wysłany nowy formularz do zaakceptowania pod addresem:",
		  /*alternatywny do HR (wysyła do dyrektora/zarządu oraz do HR)*/
        "ToHRalt": "Został wysłany nowy formularz do zaakceptowania przez Dyrektora/Zarząd pod addresem:",
		  /*do użytkonika*/
        "ToUser": "Możesz zobaczyć status formularza na stronie pod adresem:"
      },
		/*Zawartość maili dla formularzy którym zmienił się status*/
      "ChangeStatus": {
		  /*do HelpDesk*/
        "ToHelpDesk": "Został zaakceptowany przez HR nowy formularz.<br />Znajdziesz go pod adresem:",
		  /*Do użytkownika*/
        "ToUser":  "Status twojego formularza został zmieniony. Znajdziesz go pod adresem:"
      }
    }
  },
	
	/*
	Te pole oznacza wersje programu
	!!! nie należy jej zmieniać bo może dość do błędnego działania strony i bazy danych !!!
	*/
  "ProjectVerson": "2.310",

	/*
	DatabaseConnection to ciąg znaków do połaczenia się z bazą ms SQL
	w polu:
		- Serwer podaje się nazwę serwera i instancji serwera SQL
		- Database do jakiej bazy ma się połączyć
		- User jakim użytkownikiem ma się indentyfikować (Ważne by użytkonik miał dostę do bazy jako dbo.owner)
		- Password to hasło tego użytkownika
		- Language to język w jakim będą wyświetlane ewentualne błędy z sql np.: "Nie poprawne hasło"
	Reszte polecam zostawić bez zmian ale można o nich doczytać na stronie
	https://learn.microsoft.com/pl-pl/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-7.0
		*/
  "DatabaseConnection": "Server=;Database=;Persist Security Info=True;User=;Password=;Language=Polish;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True",

  "LdapUser": {
    /*
      Konto funkcjonalne służące do polaczenia się z Ldapem w domenie
      Bez tego konta system logowania za pośrednictwem domeny nie będzie działać
    */
    "SamAccountName": "",
    "Password": ""
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
## 4 Instrukcja Użytkownia
### 4.1 Logowanie
Logowanie odbywa się za pomocą kont domenowych i grup.
Jeżeli użytkownikowi wygaśnie hasło lub zablokuje się konto nie będzie wstanie się zalogować. Dodatkowo dostęp do strony odbywa się za pomocą grup domenowych. Osoba nie należąca do jednej z grup nie będzie wstanie się zalogować.
### 4.2 Wysyłanie Formularza
Po wysłaniu każdego formularza w pierwszej kolejności dostaje HR. Jeżeli HR zaakceptuje formularz dopiero wtedy trafia informacja na HelpDesk i IT może podjąć się realizacji formularza.
Jeżeli formularz wymaga akceptacji dyrektora lub zarządu wtedy w polu "Wiadomość" trzeba wybrać jedną z dwóch dostępnych obcji:
Wtedy wiadomość odrazu trafi do dyrektora/zarządu i HR z informacją że formularz wymaga akceptacji dyrektora/zarządu.
### 4.3 Lista Wysłanych Formularzy
#### 4.3.1 Zgłaszający
Zgłaszający widzi tylko formularze wysłane przez siebie. Po otworzeniu wysłanego formularza ma możliwość tylko pobrania pdf i podejrzenia statusu. Opcja zmiany statusu jest widoczna ale jest zablokowana.
#### 4.3.2 HR/Dyrektor/Zarząd
Po za podglądem i pobraniem pdf, jest również możliwość zmiany statusu formularza na:
- Nie zatwierdzony 
- Zatwierdzony
- Błędnie wypełniony
Po wybraniu statusu jest wysyłana wiadomość do zgłaszającego że status się zmienił oraz w przypadku gdy status zmienił się na "Zatwierdzony" mail również trafia na HelpDesk z informacją o zatwierdzeniu formularza.
#### 4.3.3 HelpDesk
HelpDesk ma te same prawa co HR z różnicą że może zminić status na:
- Błednie wypełniony
- W trakcie realizacji
- Gotowe
Również może ponowić wysyłkę maila w przypadku gdy, wcześniej wystąpił problem z wysłaniem
#### 4.3.4 Schemat zmiany statusów
![Statusy.png](:/98fb399d1b7b4e6180c9fe1d9d9aaf75)
## 5 Działanie
### 5.1 Przycisk "Ponów Wysyłkę Maila"
W przypadku gdy jakiemuś użytkownikowi wyświetli się strona z informacją "Formularz został zapisany w bazie danych, ale wysyłka maili napotkała problem", jeszcze jest szansa na wyświetlenie strony błędy o kodzie błedu "00-e8e2cf43c7172c02afbd3e605c75e182-372e7450eba6a5e9-00". Wtedy trzeba ponowić wysłanie maila. Jest kilka przypadków ponowienia:
- Kiedy zgłaszający wysłał nowy formularz:
	- tylko do HR
		Wysyłany jast wtedy mail z klasyczną wiadomością o nowym formularzu. Treścią maila jest to co jest ustawione w ustawieniach pod nazwą "ToHR"
	- do HR i Dyrektora\Zarządu
		Wysyłany jest mail do Hr i do dyrektora\zarządu .  Treścią maila jest to co jest ustawione w ustawieniach pod nazwą "ToHRalt"
- Kiedy HR lub HelpDesk zminił status
	Wysyłany jest wtedy mail do użytkownika i w przypadku zatwierdzenia na HelpDesk.

W każdym przypadku wiadomość jest wysyłana również do użytkownika. 
## 6 Numery Błędów
Błędy baz danych:
- **001DB-[Nazwa Widoku]**
Bład ten oznacza problem pobrania danych dla widoku.
Tabele w bazie danych które muszą zawierać dane:
	- opt.Apps
	- opt.Departments
	- opt.Printers
	- opt.VoipProject
- **002DB-ViewFormsDetails-[ID]**
	Błąd pobrania szczegułowego widoku formularza dla podanego ID.
- **003DB-SelectForms-[ID]**
	Bład wyboru odpowiedniej metody dla XML formularza dla podanego ID
- **004DB-ViewFormsDetails-StatusName-[ID]**
	Bład porania wyboru statusów do widoku formularza dla podanego ID
- **005BD-SaveForms**
	Błąd zapisu formularza do bazy danych

Błędy wysyłki:
- **006BD-SendError**
	Błąd wysyłki maila
	
Wszytkie inne błedy nie zamieszczone tutaj lub skrótowo opisane są mogą znajdywać się z dodatkowymi informacjami w lokalizacji **C:\inetpub\logs\Formularz\Logs.txt**
## 7 Licencje
Nugets:
- Microsoft.AspNetCore.Authentication - Microsoft
- Newtonsoft.Json - James Newton-King
- System.Data.SqlClient - Microsoft
- System.DirectoryServices.AccountManagement - Microsoft

Licencje zewnętrzne:
- [iconify-icon.min.js](https://iconify.design) - Iconify.DESiGN
- [cloud icon](https://icon-sets.iconify.design/ant-design/cloud-upload-outlined/) - HeskeyBaozi - [Licencja](https://github.com/ant-design/ant-design-icons/blob/master/LICENSE)
- [checked icons](https://icon-sets.iconify.design/akar-icons/check/) - Arturo Wibawa - [Licencja](https://github.com/artcoholic/akar-icons/blob/master/LICENSE)
- [html2pdf.js](https://ekoopmans.github.io/html2pdf.js/#license) - Erik Koopmans - [Licencja](https://opensource.org/licenses/MIT)