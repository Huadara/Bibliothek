# Aufgaben 27.05.2019
I bims da Sali und i hob grod NVS-Test.
Für die, die aktuell nichts zu tun haben, bitte Folgendes überprüfen:
  - Wenn ein Kunde ein Buch kauft, so soll das lieber ein StoredBook sein, anstelle eines Books (?)
  - Kann man mit der StoredBookId ein StoredBook finden?
  - Wozu braucht man die StoreId im Sale?

Nachdem ihr das überprüft habt, ändert bitte die JSON-Strings entsprechend, da ich glaube, dass dort noch ein paar Sachen fehlen könnten. Wenn ihr euch nicht sicher seid, ob ein Wert in das JSON hineingehört, dann macht ihn einfach rein! (Lieber zu viel, als zu wenig!)
Danach solltet ihr entsprechende Testdaten erstellen.

# Angabe
## Bibliothek
### Beschreibung des Workﬂows in der Firma
Wir sind ein aufstrebender Buchhandel mit einem neuen Konzept. Wir kaufen die Bücher bei unserem Händler ein und der Kunde hat dann die Möglichkeit sich ein Buch auszuleihen oder direkt zu kaufen. Wenn er das Buch ausleiht und er bringt es innerhalb von drei Tagen nicht zurück, kauft er es automatisch mit seiner Kreditkarte, die er bei dem Ausleihvorgang bekanntgeben muss. Jedes Buch hat einen Titel, einen Autor, einen Verlag, einen Lieferanten, eine ISBN und einen Lagerstand. Wenn ein Kunde das Buch ausleiht muss, der Kunde mit Namen, Vornamen und Adresse und Kreditkarteninformationen hinterlegt werden. Wir müssen natürlich wissen, welcher Kunde sich welches Buch ausgeliehen hat und wann er es wieder zurückbringen muss. Das Buch kann in jeder unserer Filialenzurückgebrachtwerden.Wirmüssenalsoauchwissen,inwelcherFilialesichdasBuchgerade beﬁndet.WenneinBuchsofortgekauftwird,mussesausunseremLagerbestandausgebuchtwerden. Toll wäre es auch, wenn wir vom System informiert werden, wenn der Lagerbestand eines Buches kritisch wird.

### Was benötigt der Auftraggeber
Momentan halten wir alle Informationen in unserer Firma in einer Excel-Tabelle. Dies ist aber extremumständlich.DawirmehrereFilialenhaben,müssenwirdieExcel-Tabelleimmerherumschicken und somit entstehen Fehler, da immer wieder Eingaben überschrieben werden. Wir benötigen also einSystem,umdieVerleihvorgängeunddieVerkaufsvorgängezuerfassen.Esmussabersosein,dass jeder Mitarbeiter in jeder Filiale immer darauf zugreifen kann. Damit jeder Mitarbeiter sofort weiß, wie viele Bücher gerade verfügbar sind und wo sich diese Bücher beﬁnden. Außerdem müssen wir wissen, wann die Bücher wieder zurückkommen. Wir würden also ein ﬁrmeninternes Informationssystem benötigen. Die technologischen Entscheidungen überlasse ich Ihnen! Sie sind ja die Experten.
Siemüssenunseinfachsagen,waswirallesinderFirmaanschaﬀenmüssen,damitIhreSoftwareeinwandfrei läuft. Wir haben für die Einführung Ihres Systems insgesamt ein Budget von 100.000 Euro. Der Betrieb der Software sollte natürlich so günstig wie möglich sein! Wie viel würde der Betrieb Ihrer Software dann im Monat ungefähr kosten? 
