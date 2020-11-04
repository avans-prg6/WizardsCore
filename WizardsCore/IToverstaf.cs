using System;
namespace WizardsCore
{
    public interface IToverstaf
    {
        void Links();
        void Omhoog();
        void Omlaag();
        void Rechts();

        int HoeveelheidEnergie { get;  }
    }
}
