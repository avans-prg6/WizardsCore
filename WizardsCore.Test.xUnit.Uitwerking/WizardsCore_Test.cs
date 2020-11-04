using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace WizardsCore.Test.xUnit.Uitwerking
{
    public class WizardsCore_Test
    {
        private Tovenaar tovenaar;
        private Toverstaf staf;

        public WizardsCore_Test()
        {
            staf = new Toverstaf();
            tovenaar = new Tovenaar(staf);
        }

     
        [Fact]
        public void Foramisforameur_fout_verkeerdeIngredienten()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Fora", "mis", "Forameur" };
            List<String> ing = new List<String>() { "spinneweb", "oorlel", };

            //2. Act, 3.Assert
            Assert.Throws<VerkeerdeIngredientenException>(() => tovenaar.Toverspreuk(ing, woorden));
        }


        [Fact]
        public void Foramisforameur_fout_verkeerdeWoorden()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Fora", "mis", "Forameut" };
            List<String> ing = new List<String>() { "spinneweb", "oorlel", "slangegif" };

            //2. Act, 3. Assert
            Assert.Throws<VerkeerdeWoordenException>(() => tovenaar.Toverspreuk(ing, woorden));
        }


        [Fact]
        public void Foramisforameur_fout()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Fora", "mis", "Forameur" };
            List<String> ing = new List<String>() { "spinneweb", "oorlel" };

            //2. Act, 3. Assert
            Assert.Throws<VerkeerdeIngredientenException>(() => tovenaar.Toverspreuk(ing, woorden));
        }

        [Fact]
        public void FlimFlamFluister_Goed()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Flim", "Flam", "Fluister" };
            List<String> ing = new List<String>() { "Kikkerbil", "oorlel", "rattenstaart", "krokodillenoog" };

            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act
            String result = t.Toverspreuk(ing, woorden);

            //3. Assert
            //Controlleer of de tovernaar de juiste bewegingen maakt
            stafMock.Verify(s => s.Links(), Times.Once);
            stafMock.Verify(s => s.Rechts(), Times.Once);
            stafMock.Verify(s => s.Omhoog(), Times.Never);

            Assert.Equal("Er was licht, en hij zag dat het goed was!", result);
        }

        [Fact]
        public void Bandaladik_goed()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Ban", "da", "ladik" };
            List<String> ing = new List<String>() { "Kikkerbil", "oorlel", "rattenstaart", "slangegif" };
            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act
            String result = t.Toverspreuk(ing, woorden);

            //3. Assert
            //Controlleer of de tovernaar de juiste bewegingen maakt
            Assert.Equal("best friends for life", result);
            stafMock.Verify(s => s.Omhoog(), Times.Once);
            stafMock.Verify(s => s.Omlaag(), Times.Once);
            stafMock.Verify(s => s.Links(), Times.Never);
        }


        /**
         * Deze test gaat niet goed! 
         * We kunnen de spreuk Arma-kro-dilt niet testen, omdat deze gebruik maakt van een zilvere pot.
         * Onze tovenaar heeft altijd een zwarte pot :*( 
         **/
        [Fact]
        public void ArmaKroDilt_Goed()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Arma", "kro", "dilt" };
            List<String> ing = new List<String>() { "Kikkerbil", "spinneweb", "oorlel", "rattenstaart", "slangegif", "mensenhaar", "krokodillenoog" };
            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act
            String result = t.Toverspreuk(ing, woorden);

            //3. Assert
            //Controlleer of de tovernaar de juiste bewegingen maakt
            Assert.Equal("upgrades", result);
        }


        //Bal-sam-sala-bond
        //[ Kikkerbil, spinneweb, mensenhaar, krokodillenoog ]
        //“Genezingstoverij die de verloren levensenergie terugbrengt.”
        //Bewegingen: Links, omhoog, rechts, omlaag (volgorde is belangrijk!)
        //Resultaat: ‘Je bent genezen met 3 energiepunten’
        //** Aantal energie is gelijk aan de energie in je toverstaf **
        [Fact]
        public void BalSamSalaBond()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Bal", "sam", "sala", "bond" };
            List<String> ing = new List<String>() { "Kikkerbil", "spinneweb", "mensenhaar", "krokodillenoog" };

            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            stafMock.Setup(staf => staf.HoeveelheidEnergie).Returns(3);



            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act
            String result = t.Toverspreuk(ing, woorden);

            //3. Assert
            //Controlleer of de tovernaar de juiste bewegingen maakt
            Assert.Equal("Je bent genezen met 3 energiepunten", result);


        }

        [Fact]
        public void GeenIngredienten()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { };
            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act, 3. Assert
            Assert.Throws<GeenIngredientenException>(() => t.Toverspreuk(null, woorden));
        }

        [Fact]
        public void GeenToverspreuk()
        {
            //1. Arrange
            List<String> woorden = new List<String>() { "Arma" };
            List<String> ing = new List<String>() { "Kikkerbil" };
            Mock<IToverstaf> stafMock = new Mock<IToverstaf>();
            Tovenaar t = new Tovenaar(stafMock.Object);

            //2. Act, 3. Assert
            Assert.Throws<GeenToverspreukException>(() => t.Toverspreuk(ing, woorden));
        }
    }
}
