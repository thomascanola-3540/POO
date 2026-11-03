namespace API_Animales
{
    public class Gato : Animal
    {
        public string Raza { get; set; }

        public override string HacerSonido() => $"El Gato {Nombre} hace Miaauu! y Ronronea.";
        public override string Moverse() => $"{Nombre} va caminando";
    }

}
