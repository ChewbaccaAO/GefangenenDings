using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    /// <summary>
    /// Strategie für das 1. Gruppenmitglied. 
    /// 1. Tragen Sie in der Name() Methode den Namen ihrer Strategie ein. 
    /// 2. Tragen Sie in der Autor() Methode ihren Namen ein. 
    /// 3. Schreiben Sie in Verhoer() ihre Strategie, über die Start() Methode erhalten Sie allgemeine Informationen zum Durchlauf. 
    /// Sie können gerne weitere Methoden, Variablen ergänzen, aber passen Sie nicht das 
    /// </summary>
    public class Strategie1 : BasisStrategie
    {
        int reaktion;
        bool stage2 = false;

        /// <summary>
        /// Gibt den Namen der Strategie zurück, wichtig zum Anzeigen für die Auswahl
        /// </summary>
        /// <returns></returns>
        public override string Name()
        {
            return "Pavlov";
        }

        /// <summary>
        /// Gibt den Namen des Autors der Strategie zurück, wichtig für die Turnierpart um den Sieger zu ermitteln.
        /// </summary>
        /// <returns></returns>
        public override string Autor()
        {
            return "Nils Steffien";
        }

        /// <summary>
        /// Teilt mit, dass ein Verhoer jetzt startet
        /// </summary>
        /// <param name="runde">Anzahl der Runden, die verhört wird</param>
        /// <param name="schwere">Schwere des Verbrechen (VLeicht = 0, VMittel = 1, VSchwer = 2)</param>
        public override void Start(int runde, int schwere)
        {
            reaktion = BasisStrategie.Kooperieren;
        }

        /// <summary>
        /// Verhoert einen Gefangenen
        /// </summary>
        /// <param name="letzteReaktion">Reaktion des anderen Gefangenen, die Runde davor (NochNichtVerhoert = -1, Kooperieren = 0, Verrat = 1)</param>
        /// <returns>Gibt die eigene Reaktion für diese Runde zurück (Kooperieren = 0, Verrat = 1)</returns>
        public override int Verhoer(int letzteReaktion)
        {
            if (letzteReaktion == reaktion)
            {
                reaktion = BasisStrategie.Kooperieren;
            } else
            {
                reaktion = BasisStrategie.Verrat;
            }
            
            return reaktion;
        }
    }
}