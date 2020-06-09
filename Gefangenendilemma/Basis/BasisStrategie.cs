//////////////////////////
// In dieser Datei finden Sie Basis,
// die notwendig zum Ausführen, aber nicht zum Verstehen sind.
// Hier nichts anpassen.
//////////////////////////

namespace Gefangenendilemma.Basis
{
    public abstract class BasisStrategie
    {
        public const int Kooperieren = 0, Verrat = 1, NochNichtVerhoert = -1;
        public const int VLeicht = 0, VMittel = 1, VSchwer = 2;
        
        /// <summary>
        /// Gibt den Namen der Strategie zurück, wichtig zum Anzeigen für die Auswahl
        /// </summary>
        /// <returns></returns>
        public abstract string Name();

        /// <summary>
        /// Gibt den Namen des Autors der Strategie zurück, wichtig für die Turnierpart um den Sieger zu ermitteln.
        /// </summary>
        /// <returns></returns>
        public abstract string Autor();

        /// <summary>
        /// Teilt mit, dass ein Verhoer jetzt startet
        /// </summary>
        /// <param name="runde">Anzahl der Runden, die verhört wird</param>
        /// <param name="schwere">Schwere des Verbrechen (VLeicht = 0, VMittel = 1, VSchwer = 2)</param>
        public abstract void Start(int runde, int schwere);

        /// <summary>
        /// Verhoert einen Gefangenen
        /// </summary>
        /// <param name="letzteReaktion">Reaktion des anderen Gefangenen, die Runde davor (NochNichtVerhoert = -1, Kooperieren = 0, Verrat = 1)</param>
        /// <returns>Gibt die eigene Reaktion für diese Runde zurück (Kooperieren = 0, Verrat = 1)</returns>
        public abstract int Verhoer(int letzteReaktion);
    }
}