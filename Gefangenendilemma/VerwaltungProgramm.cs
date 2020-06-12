using System;
using System.Collections.Generic;
using System.Linq;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    /// <summary>
    /// Diese Klasse können Sie beliebig umschreiben, jenachdem welche Tasks sie erledigen.
    /// </summary>
    class VerwaltungProgramm
    {
        /// <summary>
        /// Diese Liste(Collection) enthält alle Gefangene/Strategien
        /// </summary>
        private static List<BasisStrategie> _strategien;

        /// <summary>
        /// Diese Liste(Collection) enthält alle möglichen Spielmodi
        /// </summary>
        private static List<string> spielModi = new List<string>()
        {
            "Einfaches Verhör zwischen zwei Strategien",
            "9-Spiele Verhör zwischen zwei Strategien",
            "Einfaches Verhör zwischen dem Benutzer und einer Strategie",
            "Turnier mit allen Strategien"
        };

        /// <summary>
        /// Dieses 3-dimensionale Array enthält die Punkteverteilung bei leichten Verstößen
        /// </summary>
        private static int[,,] leichtPunkte = new int[2, 2, 2] {
            { {3,3} , {9,0} },
            { {0,9} , {6,6} }
        };

        /// <summary>
        /// Dieses 3-dimensionale Array enthält die Punkteverteilung bei mittleren Verstößen
        /// </summary>
        private static int[,,] mittelPunkte = new int[2, 2, 2] {
            { {10,10}  , {0,8} },
            { {8,0}    , {4,4} }
        };

        /// <summary>
        /// Dieses 3-dimensionale Array enthält die Punkteverteilung bei schweren Verstößen
        /// </summary>
        private static int[,,] schwerPunkte = new int[2, 2, 2] {
            { {4,4}  , {10,0} },
            { {0,10} , {8,8}  }
        };

        /// <summary>
        /// Dieses 2-dimensionale Array enthält die Schwere- und Rundenverteilung bei einem 9-Spiele Verhör
        /// </summary>
        private static int[,] verhör9SpieleVerteilung = new int[9, 2] {
            {0,5},{0,25},{0,100},
            {1,5},{1,25},{1,100},
            {2,5},{2,25},{2,100}
        };

        static void Main(string[] args)
        {
            // Hinzufügen aller Strategien
            _strategien = new List<BasisStrategie>();
            _strategien.Add(new GrollStrategie());
            _strategien.Add(new VerrateImmerStrategie());
            _strategien.Add(new Strategie1());
            _strategien.Add(new Strategie2());

            // Öffnen des Hauptmenüs und verwalten der Eingabe
            HauptMenu();
        }

        /// <summary>
        /// Diese Methode erstellt ein Hauptmenü und lässt den Benutzer ein Spielmodus auswählen
        /// </summary>
        static void HauptMenu()
        {
            string eingabe;
            do
            {
                // Begrüßung
                WriteTrennstrich();
                Console.WriteLine("WILLKOMMEN ZUM GEFANGENENDILEMMA");
                WriteTrennstrich();

                // Spielmodi Auswahl
                for (int i = 0; i < spielModi.Count; i++)
                {
                    Console.WriteLine($"{i} - {spielModi[i]}");
                }
                Console.WriteLine("X - Beenden");

                // Eingabe der Spielmodi Auswahl einlesen
                Console.Write("Treffe eine Option: ");
                eingabe = Console.ReadLine();
                WriteLeerzeile();

                // Auswerten der Eingabe und starten des richtigen Spielmodus
                switch (eingabe.ToLower())
                {
                    case "0":
                        VerhörEinfach();
                        WriteTrennstrich();
                        break;
                    case "1":
                        Verhör9Spiele();
                        WriteTrennstrich();
                        break;
                    case "2":
                        VerhörMitBenutzer();
                        WriteTrennstrich();
                        break;
                    case "3":
                        Turnier();
                        WriteTrennstrich();
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine($"Eingabe {eingabe} nicht erkannt.");
                        break;
                }
            } while (!"x".Equals(eingabe?.ToLower()));
        }

        /// <summary>
        /// Diese Methode verwaltet ein einfaches Verhör zwischen zwei Strategien
        /// </summary>
        static void VerhörEinfach()
        {
            // Überschrift
            WriteTrennstrich();
            Console.WriteLine(spielModi[0]);
            WriteTrennstrich();

            // Auswahl der beiden Strategie, der Rundenanzahl sowie der Schwere der Verstöße
            int st1 = 0, st2 = 0;
            StrategieAuswahl(ref st1, ref st2);

            int runde, schwere;
            runde = VerwaltungKram.EingabeZahlMinMax("Rundenanzahl: ", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Schwere der Verstöße (0 = Leicht, 1 = Mittel, 2 = Schwer): ", 0, 3);

            // Holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];

            // Überschrift
            WriteTrennstrich();
            Console.WriteLine("Verhör");
            WriteTrennstrich();

            // Starten des Verhörs mit gegebenen Faktoren
            int punkte1 = 0, punkte2 = 0;
            Verhoer2Strategien(strategie1, strategie2, runde, schwere, ref punkte1, ref punkte2);

            // Ausgabe des Siegers
            WriteLeerzeile();
            Siegerehrung(strategie1.Name(), strategie2.Name(), punkte1, punkte2);
        }

        /// <summary>
        /// Diese Methode verwaltet ein 9-Spiele Verhör zwischen zwei Strategien
        /// </summary>
        static void Verhör9Spiele()
        {
            // Überschrift
            WriteTrennstrich();
            Console.WriteLine(spielModi[1]);
            WriteTrennstrich();

            // Strategieauswahl
            int st1 = 0, st2 = 0;
            StrategieAuswahl(ref st1, ref st2);

            // Holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];

            // Überschrift
            WriteTrennstrich();
            Console.WriteLine("Verhör");
            WriteTrennstrich();

            // 9 Spiele Verhör starten
            int punkte1 = 0, punkte2 = 0;
            Verhör9Spiele(strategie1, strategie2, ref punkte1, ref punkte2);

            // Ausgabe des Siegers
            WriteLeerzeile();
            Siegerehrung(strategie1.Name(), strategie2.Name(), punkte1, punkte2);
        }

        /// <summary>
        /// Startet ein 9-Spiele Verhör zwischen der Strategie an der Position strategie1 und Position strategie2
        /// </summary>
        /// <param name="strategie1">Erste Strategie</param>
        /// <param name="strategie2">Zweite Strategie</param>
        /// <param name="gesamtPunkte1">Punkte der ersten Strategie</param>
        /// <param name="gesamtPunkte2">Punkte der zweiten Strategie</param>
        static void Verhör9Spiele(BasisStrategie strategie1, BasisStrategie strategie2, ref int gesamtPunkte1, ref int gesamtPunkte2)
        {
            for (int i = 0; i < 9; i++)
            {
                int punkte1 = 0, punkte2 = 0, endPunkte1 = 0, endPunkte2 = 0;
                int schwere = verhör9SpieleVerteilung[i, 0];
                int runden = verhör9SpieleVerteilung[i, 1];
                Verhoer2Strategien(strategie1, strategie2, runden, schwere, ref punkte1, ref punkte2);

                if (runden == 5)
                {
                    endPunkte1 = punkte1 * 20;
                    endPunkte2 = punkte2 * 20;
                    gesamtPunkte1 += endPunkte1;
                    gesamtPunkte2 += endPunkte2;
                    Console.WriteLine($"   '{strategie1.Name()}': {punkte1} x 20 = {endPunkte1} Punkte (Gesamtpunkte: {gesamtPunkte1})");
                    Console.WriteLine($"   '{strategie2.Name()}': {punkte2} x 20 = {endPunkte2} Punkte (Gesamtpunkte: {gesamtPunkte2})");
                }
                else if (runden == 25)
                {
                    endPunkte1 = punkte1 * 4;
                    endPunkte2 = punkte2 * 4;
                    gesamtPunkte1 += endPunkte1;
                    gesamtPunkte2 += endPunkte2;
                    Console.WriteLine($"   '{strategie1.Name()}': {punkte1} x 4 = {endPunkte1} Punkte (Gesamtpunkte: {gesamtPunkte1})");
                    Console.WriteLine($"   '{strategie2.Name()}': {punkte2} x 4 = {endPunkte2} Punkte (Gesamtpunkte: {gesamtPunkte2})");
                } else
                {
                    gesamtPunkte1 += punkte1;
                    gesamtPunkte2 += punkte2;
                    Console.WriteLine($"   '{strategie1.Name()}': {punkte1} x 1 = {punkte1} Punkte (Gesamtpunkte: {gesamtPunkte1})");
                    Console.WriteLine($"   '{strategie2.Name()}': {punkte2} x 1 = {punkte2} Punkte (Gesamtpunkte: {gesamtPunkte2})");
                }
            }
        }

        /// <summary>
        /// Startet ein Verhör zwischen der Strategie an der Position strategie1 und Position strategie2 über die Länge von runden und der Schwere schwere
        /// </summary>
        /// <param name="strategie1">Erste Strategie</param>
        /// <param name="strategie1">Zweite Strategie</param>
        /// <param name="runden">Rundenanzahl</param>
        /// <param name="schwere">Schwere der Verstöße (0=Leicht, 1=Mittel, 2=Schwer) </param>
        /// <param name="punkte1">Punkte der ersten Strategie</param>
        /// <param name="punkte1">Punkte der zweiten Strategie</param>
        static void Verhoer2Strategien(BasisStrategie strategie1, BasisStrategie strategie2, int runden, int schwere, ref int punkte1, ref int punkte2)
        {
            // Ausgabe der Verhörfaktoren (Rundenanzahl, etc.) bevor das Verhör startet.
            string schwereTxt;
            switch (schwere)
            {
                case 0:
                    schwereTxt = "Leichte Verstöße";
                    break;
                case 1:
                    schwereTxt = "Mittlere Verstöße";
                    break;
                default:
                    schwereTxt = "Schwere Verstöße";
                    break;
            }
            Console.WriteLine($"{schwereTxt}, {runden} "+ (runden == 1 ? "Runde":"Runden") + $" zwischen '{strategie1.Name()}' und '{strategie2.Name()}'");

            // setzt Startwerte
            int reaktion1 = BasisStrategie.NochNichtVerhoert;
            int reaktion2 = BasisStrategie.NochNichtVerhoert;

            //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)
            strategie1.Start(runden, schwere);
            strategie2.Start(runden, schwere);

            //Start der Verhöre
            for (int i = 0; i < runden; i++)
            {
                //Beide Strategien werden verhört
                int aktReaktion1 = strategie1.Verhoer(reaktion2);
                int aktReaktion2 = strategie2.Verhoer(reaktion1);

                //Punkte werden berechnet
                VerhoerPunkte(schwere, aktReaktion1, aktReaktion2, ref punkte1, ref punkte2);
                
                //Reaktion für den nächsten Durchlauf merken
                reaktion1 = aktReaktion1;
                reaktion2 = aktReaktion2;
            }
        }

        /// <summary>
        /// Diese Methode verwaltet ein Verhör zwischen dem Benutzer und einer gegnerischen Strategie
        /// </summary>
        static void VerhörMitBenutzer()
        {
            // Überschrift
            WriteTrennstrich();
            Console.WriteLine(spielModi[2]);
            WriteTrennstrich();

            // Auswahl der gegnerischen Strategie, Rundenanzahl sowie Schwere
            int st1 = 0;
            StrategieAuswahl(ref st1);

            int runden, schwere;
            runden = VerwaltungKram.EingabeZahlMinMax("Rundenanzahl: ", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Schwere der Verstöße (0 = Leicht, 1 = Mittel, 2 = Schwer): ", 0, 3);

            // Holt die gegnerische Strategie aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];

            // Überschrift
            WriteTrennstrich();
            Console.WriteLine("Verhör");
            WriteTrennstrich();

            // gegnerische Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)
            strategie1.Start(runden, schwere);

            // Start der Verhöre
            int alteBenutzerReaktion = BasisStrategie.NochNichtVerhoert;
            int punkte1 = 0, punkte2 = 0;
            for (int i = 0; i < runden; i++)
            {
                Console.WriteLine("Runde " + (i + 1) + ":");
                int benutzerReaktion = VerwaltungKram.EingabeZahlMinMax("   Deine Reaktion (0=Kooperieren, 1=Verraten): ", 0, 2);

                //Gegnerische Strategie wird verhört
                int aktReaktion1 = strategie1.Verhoer(alteBenutzerReaktion);
                Console.WriteLine($"   Gegnerische Strategie ('{strategie1.Name()}'): " + aktReaktion1);

                //Punkte werden berechnet
                VerhoerPunkte(schwere, aktReaktion1, benutzerReaktion, ref punkte1, ref punkte2);

                //Reaktion für den nächsten Durchlauf merken
                alteBenutzerReaktion = benutzerReaktion;
            }

            // Ausgabe des Siegers
            WriteLeerzeile();
            Siegerehrung(strategie1.Name(), "Benutzer", punkte1, punkte2);
        }

        /// <summary>
        /// Diese Methode verwaltet ein Turnier zwischen allen Strategien
        /// </summary>
        static void Turnier()
        {
            // Überschrift
            WriteTrennstrich();
            Console.WriteLine(spielModi[3]);
            WriteTrennstrich();

            // Strategien holen
            BasisStrategie strategie1 = _strategien[0];
            BasisStrategie strategie2 = _strategien[1];
            BasisStrategie strategie3 = _strategien[2];
            BasisStrategie strategie4 = _strategien[3];

            // Turnier starten (jeder gegen jeden)
            int gesamtPunkte1 = 0, gesamtPunkte2 = 0, gesamtPunkte3 = 0, gesamtPunkte4 = 0;

            int punkte1 = 0, punkte2 = 0, punkte3 = 0, punkte4 = 0;
            Console.WriteLine($"[SPIEL 1/6] {strategie1.Name()} vs. {strategie2.Name()}");
            Verhör9Spiele(strategie1, strategie2, ref punkte1, ref punkte2);
            gesamtPunkte1 += punkte1;
            gesamtPunkte2 += punkte2;
            WriteLeerzeile();

            punkte1 = 0; punkte2 = 0; punkte3 = 0; punkte4 = 0;
            Console.WriteLine($"[SPIEL 2/6] {strategie3.Name()} vs. {strategie4.Name()}");
            Verhör9Spiele(strategie3, strategie4, ref punkte3, ref punkte4);
            gesamtPunkte3 += punkte3;
            gesamtPunkte4 += punkte4;
            WriteLeerzeile();

            punkte1 = 0; punkte2 = 0; punkte3 = 0; punkte4 = 0;
            Console.WriteLine($"[SPIEL 3/6] {strategie1.Name()} vs. {strategie3.Name()}");
            Verhör9Spiele(strategie1, strategie3, ref punkte1, ref punkte3);
            gesamtPunkte1 += punkte1;
            gesamtPunkte3 += punkte3;
            WriteLeerzeile();

            punkte1 = 0; punkte2 = 0; punkte3 = 0; punkte4 = 0;
            Console.WriteLine($"[SPIEL 4/6] {strategie2.Name()} vs. {strategie4.Name()}");
            Verhör9Spiele(strategie2, strategie4, ref punkte2, ref punkte4);
            gesamtPunkte2 += punkte2;
            gesamtPunkte4 += punkte4;
            WriteLeerzeile();

            punkte1 = 0; punkte2 = 0; punkte3 = 0; punkte4 = 0;
            Console.WriteLine($"[SPIEL 5/6] {strategie4.Name()} vs. {strategie1.Name()}");
            Verhör9Spiele(strategie4, strategie1, ref punkte4, ref punkte1);
            gesamtPunkte4 += punkte4;
            gesamtPunkte1 += punkte1;
            WriteLeerzeile();

            punkte1 = 0; punkte2 = 0; punkte3 = 0; punkte4 = 0;
            Console.WriteLine($"[SPIEL 6/6] {strategie3.Name()} vs. {strategie2.Name()}");
            Verhör9Spiele(strategie3, strategie2, ref punkte3, ref punkte2);
            gesamtPunkte3 += punkte3;
            gesamtPunkte2 += punkte2;
            WriteLeerzeile();

            // Ausgabe der Rangliste
            Console.WriteLine("Siegerehrung");
            Dictionary<BasisStrategie, int> rangliste = new Dictionary<BasisStrategie, int>();
            rangliste.Add(strategie1, gesamtPunkte1);
            rangliste.Add(strategie2, gesamtPunkte2);
            rangliste.Add(strategie3, gesamtPunkte3);
            rangliste.Add(strategie4, gesamtPunkte4);

            List<KeyValuePair<BasisStrategie, int>> ranglisteList = rangliste.ToList();
            ranglisteList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            int platz = 1;
            foreach (KeyValuePair<BasisStrategie, int> rang in rangliste)
            {
                Console.WriteLine($"   Platz {platz}: Strategie '{rang.Key.Name()}' mit {rang.Value} Punkten");
                platz++;
            }
        }

        /// <summary>
        /// Berechnet bei einem Verhör die Punkte und verwendet die 2 letzten Eingabeparameter als Rückgabe
        /// </summary>
        /// <param name="schwere">Schwere der Verstöße (0=Leicht, 1=Mittel, 2=Schwer)</param>
        /// <param name="aktReaktion1">Reaktion der ersten Strategie (0=Kooperieren, 1=Verrat)</param>
        /// <param name="aktReaktion2">Reaktion der zweiten Strategie (0=Kooperieren, 1=Verrat)</param>
        /// <param name="punkte1">Punkte der ersten Strategie</param>
        /// <param name="punkte2">Punkte der zweiten Strategie</param>
        /// 
        static void VerhoerPunkte(int schwere, int aktReaktion1, int aktReaktion2, ref int punkte1, ref int punkte2)
        {
            // Das richtige Punkte Array je nach Schwere der Verstöße wird ausgewählt
            int[,,] punkteArr;
            switch (schwere)
            {
                case 0:
                    punkteArr = leichtPunkte;
                    break;
                case 1:
                    punkteArr = mittelPunkte;
                    break;
                default:
                    punkteArr = schwerPunkte;
                    break;
            }

            // Punkte werden je nach Reaktion dem Punktestand der ersten und zweiten Strategie addiert.
            punkte1 += punkteArr[aktReaktion1, aktReaktion2, 0];
            punkte2 += punkteArr[aktReaktion1, aktReaktion2, 1];
        }

        /// <summary>
        /// Diese Methode ermittelt die Siegerstrategie und gibt den Sieger aus.
        /// </summary>
        /// <param name="strategie1">Name der ersten Strategie</param>
        /// <param name="strategie2">Name der zweiten Strategie</param>
        /// <param name="punkte1">Punkte der ersten Strategie</param>
        /// <param name="punkte2">Punkte der zweiten Strategie</param>
        static void Siegerehrung(string strategie1Name, string strategie2Name, int punkte1, int punkte2)
        {
            Console.WriteLine("Siegerehrung");
            Console.WriteLine($"   '{strategie1Name}' hat insgesamt {punkte1} Punkte.");
            Console.WriteLine($"   '{strategie2Name}' hat insgesamt {punkte2} Punkte.");
            WriteLeerzeile();

            if (punkte1 == punkte2)
            {
                Console.WriteLine("UNENTSCHIEDEN zwischen beiden Strategien!");
            }
            else
            {
                Console.WriteLine("SIEGER: Strategie '{0}'", (punkte1 < punkte2 ? strategie1Name : strategie2Name));
            }
        }

        /// <summary>
        /// Diese Methode gibt dem Benutzer die Möglichkeit, zwei Strategien auszuwählen.
        /// </summary>
        /// <param name="st1">Id der ersten Strategie</param>
        /// <param name="st2">Id der zweiten Strategie</param>
        static void StrategieAuswahl(ref int st1, ref int st2)
        {
            Console.WriteLine("Bitte wähle 2 Strategien:");
            WriteStrategien();
            WriteLeerzeile();

            st1 = VerwaltungKram.EingabeZahlMinMax("1. Strategie: ", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("2. Strategie: ", 0, _strategien.Count);
        }

        /// <summary>
        /// Diese Methode gibt dem Benutzer die Möglichkeit, eine Strategie auszuwählen.
        /// </summary>
        /// <param name="st1">Id der Strategie</param>
        static void StrategieAuswahl(ref int st1)
        {
            Console.WriteLine("Bitte wähle eine gegnerische Strategie:");
            WriteStrategien();
            WriteLeerzeile();

            st1 = VerwaltungKram.EingabeZahlMinMax("Gegnerische Strategie: ", 0, _strategien.Count);
        }

        /// <summary>
        /// Diese Methode schreibt eine Liste aller möglichen Strategien aus.
        /// </summary>
        static void WriteStrategien()
        {
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }
        }

        /// <summary>
        /// Diese Methode schreibt einen immer gleich langen Trennstrich in die Ausgabe.
        /// </summary>
        static void WriteTrennstrich()
        {
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Diese Methode schreibt eine leere Zeile in die Ausgabe.
        /// </summary>
        static void WriteLeerzeile()
        {
            Console.WriteLine();
        }
    }
}