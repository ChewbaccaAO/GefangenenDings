using System;
using System.Collections.Generic;
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
            "Einfaches Verhör zwischen zwei Spielern"
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

        static void Main(string[] args)
        {
            // Hinzufügen aller Strategien
            _strategien = new List<BasisStrategie>();
            _strategien.Add(new GrollStrategie());
            _strategien.Add(new VerrateImmerStrategie());
            _strategien.Add(new Strategie1());
            _strategien.Add(new Strategie2());
            _strategien.Add(new Strategie3());

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
                writeTrennstrich();
                Console.WriteLine("WILLKOMMEN ZUM GEFANGENENDILEMMA");
                writeTrennstrich();

                // Spielmodi Auswahl
                for (int i = 0; i < spielModi.Count; i++)
                {
                    Console.WriteLine($"{i} - {spielModi[i]}");
                }
                Console.WriteLine("X - Beenden");

                // Eingabe der Spielmodi Auswahl einlesen
                Console.Write("Treffe eine Option: ");
                eingabe = Console.ReadLine();
                writeLeerzeile();

                // Auswerten der Eingabe und starten des richtigen Spielmodus
                switch (eingabe.ToLower())
                {
                    case "0":
                        Gefangene2();
                        writeTrennstrich();
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
        /// Fragt 2 Strategien, Länge und Schwere ab.
        /// </summary>
        static void Gefangene2()
        {
            int st1, st2;
            int runde, schwere;

            // Überschrift
            writeTrennstrich();
            Console.WriteLine(spielModi[0]);
            writeTrennstrich();

            // Auswahl der beiden Strategie, der Rundenanzahl sowie der Schwere der Verstöße
            Console.WriteLine("Bitte wähle deine 2 Strategien:");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }
            Console.WriteLine();
            st1 = VerwaltungKram.EingabeZahlMinMax("1. Strategie: ", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("2. Strategie: ", 0, _strategien.Count);
            runde = VerwaltungKram.EingabeZahlMinMax("Rundenanzahl: ", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Schwere der Verstöße (0 = Leicht, 1 = Mittel, 2 = Schwer): ", 0, 3);
            writeLeerzeile();

            // Starten des Verhörs mit gegebenen Faktoren
            Verhoer(st1, st2, runde, schwere);
        }

        /// <summary>
        /// Startet ein Verhör zwischen der Strategie an der Position st1 und Position st2 über die Länge von runde und der Schwere schwere
        /// </summary>
        /// <param name="st1">Id der ersten Strategie</param>
        /// <param name="st2">Id der zweiten Strategie</param>
        /// <param name="runde">Rundenanzahl</param>
        /// <param name="schwere">Schwere der Verstöße (0=Leicht, 1=Mittel, 2=Schwer) </param>
        static void Verhoer(int st1, int st2, int runde, int schwere)
        {
            //holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];

            //setzt Startwerte
            int reaktion1 = BasisStrategie.NochNichtVerhoert;
            int reaktion2 = BasisStrategie.NochNichtVerhoert;
            int punkte1 = 0, punkte2 = 0;

            //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)
            strategie1.Start(runde, schwere);
            strategie2.Start(runde, schwere);

            //Überschrift
            writeTrennstrich();
            Console.WriteLine("Verhör");
            writeTrennstrich();
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
            Console.WriteLine($"Verhör ({schwereTxt}) zwischen '{strategie1.Name()}' und '{strategie2.Name()}' für {runde} " + (runde == 1 ? "Runde" : "Runden") + ".");
            writeLeerzeile();

            //Start der Verhöre
            for (int i = 0; i < runde; i++)
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
            
            //Ausgabe der Endpunkte und der Siegerstrategie
            Console.WriteLine($"'{strategie1.Name()}' hat {punkte1} Punkte erhalten.");
            Console.WriteLine($"'{strategie2.Name()}' hat {punkte2} Punkte erhalten.");
            writeLeerzeile();

            if (punkte1 == punkte2)
            {
                Console.WriteLine("Unentschieden zwischen beiden Strategien!");
            } else
            {
                Console.WriteLine("Sieger: '{0}'", (punkte1 < punkte2 ? strategie1.Name() : strategie2.Name()));
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
        /// Diese Methode schreibt einen immer gleich langen Trennstrich in die Ausgabe.
        /// </summary>
        static void writeTrennstrich()
        {
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Diese Methode schreibt eine leere Zeile in die Ausgabe.
        /// </summary>
        static void writeLeerzeile()
        {
            Console.WriteLine();
        }
    }
}