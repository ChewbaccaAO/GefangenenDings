using Gefangenendilemma.Basis;
using System;

namespace Gefangenendilemma
{
    public class Strategie2 : BasisStrategie
    {
        /// <summary>
        /// Gibt den Namen der Strategie zurück, wichtig zum Anzeigen für die Auswahl
        /// </summary>
        /// <returns></returns>
        public override string Name()
        {
            return "Random";
        }

        /// <summary>
        /// Gibt den Namen des Autors der Strategie zurück, wichtig für die Turnierpart um den Sieger zu ermitteln.
        /// </summary>
        /// <returns></returns>
        public override string Autor()
        {
            return "Sören Stabenow";
        }

        /// <summary>
        /// Teilt mit, dass ein Verhoer jetzt startet
        /// </summary>
        /// <param name="runde">Anzahl der Runden, die verhört wird</param>
        /// <param name="schwere">Schwere des Verbrechen (VLeicht = 0, VMittel = 1, VSchwer = 2)</param>
        public override void Start(int runde, int schwere)
        {
        }

        /// <summary>
        /// Verhoert einen Gefangenen
        /// </summary>
        /// <param name="letzteReaktion">Reaktion des anderen Gefangenen, die Runde davor (NochNichtVerhoert = -1, Kooperieren = 0, Verrat = 1)</param>
        /// <returns>Gibt die eigene Reaktion für diese Runde zurück (Kooperieren = 0, Verrat = 1)</returns>
        public override int Verhoer(int letzteReaktion)
        {
            // Entscheidet nach einem 50/50 Prinzip.

            // Generiert eine Zufallszahl zwischen 0 und 100.
            Random r = new Random();
            int randomInt = r.Next(0, 100);

            // Wenn größter gleich 50, wird verraten, wenn kleiner 50, wird kooperiert.
            // Trifft aus irgendeinem Grund nichts zu, wird verraten.
            if (randomInt >= 50)
                return Verrat;
            if (randomInt < 50)
                return Kooperieren;
            return Verrat;
        }
    }
}