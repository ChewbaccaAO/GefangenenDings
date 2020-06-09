//////////////////////////
// In dieser Datei finden Sie eine Beispielstrategie,
// die notwendig zum Ausführen, aber nicht zum Verstehen ist.
// Hier nichts anpassen.
//////////////////////////

using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    public class GrollStrategie : BasisStrategie
    {
        private bool _groll;
        public override string Name()
        {
            return "Groll";
        }

        public override string Autor()
        {
            return "Steffen Trutz";
        }

        public override void Start(int runde, int schwere)
        {
            _groll = false;
        }

        public override int Verhoer(int letzteReaktion)
        {
            if (!_groll && letzteReaktion == Verrat)
            {
                _groll = true;
            }

            return _groll ? Verrat : Kooperieren;
        }
    }
}