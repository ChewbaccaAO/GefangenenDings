//////////////////////////
// In dieser Datei finden Sie verschiedene interne Programmteile,
// die notwendig zum Ausführen, aber nicht zum Verstehen sind.
//////////////////////////

using System;

namespace Gefangenendilemma.Basis
{
    public class VerwaltungKram
    {

        /// <summary>
        /// Liest eine Zahl ein
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int EingabeZahl(string text)
        {
            int zahl;
            do
            {
                Console.WriteLine(text);
            } while (!Int32.TryParse(Console.ReadLine(), out zahl));

            return zahl;
        }

        /// <summary>
        /// Liest eine Zahl ein und prüft, ob es zwischen den beiden Bereichen liegt.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="min">Included</param>
        /// <param name="max">Not included</param>
        /// <returns></returns>
        public static int EingabeZahlMinMax(string text, int min, int max)
        {
            int zahl;
            do
            {
                zahl = EingabeZahl(text);

                if (min <= zahl && zahl < max)
                {
                    return zahl;
                }
                
                Console.WriteLine($"Die eingebene Zahl {zahl} muss zwischen {min} und {max} liegen.");
            } while (true);

        }
    }
}