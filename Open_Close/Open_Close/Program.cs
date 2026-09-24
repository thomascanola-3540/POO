using Open_Close;
using System.Drawing;

AreaCalculator calculator = new AreaCalculator();

Circulo circle1 = new Circulo();
circle1.Radius = 2;

Circulo circle2 = new Circulo();
circle2.Radius = 3;

Circulo circle3 = new Circulo();
circle3.Radius = 4;


Cuadrado square1 = new Cuadrado();
square1.Lado = 5;

Cuadrado square2 = new Cuadrado();
square2.Lado = 4;

Cuadrado square3 = new Cuadrado();
square3.Lado = 7;


Triangulo triangulo1 = new Triangulo();
triangulo1.Base = 5;
triangulo1.Altura = 2;

Triangulo triangulo2 = new Triangulo();
triangulo2.Base = 4;
triangulo2.Altura = 3;

Triangulo triangulo3 = new Triangulo();
triangulo3.Base = 8;
triangulo3.Altura = 8;


Rectangulo rectangulo1 = new Rectangulo();
rectangulo1.Base = 5;
rectangulo1.Altura = 3;

Rectangulo rectangulo2 = new Rectangulo();
rectangulo2.Base = 8;
rectangulo2.Altura = 4;

Rectangulo rectangulo3 = new Rectangulo();
rectangulo3.Base = 9;
rectangulo3.Altura = 6;


Trapecio trapecio1 = new Trapecio();
trapecio1.BaseMayor = 5;
trapecio1.BaseMenor = 3;
trapecio1.Altura = 4;

Trapecio trapecio2 = new Trapecio();
trapecio2.BaseMayor = 7;
trapecio2.BaseMenor = 5;
trapecio2.Altura = 4;

Trapecio trapecio3 = new Trapecio();
trapecio3.BaseMayor = 10;
trapecio3.BaseMenor = 8;
trapecio3.Altura = 9;



List<Figura> shapes = new List<Figura>();
shapes.Add(circle1);
shapes.Add(circle2);
shapes.Add(circle3);

shapes.Add(square1);
shapes.Add(square2);
shapes.Add(square3);

shapes.Add(triangulo1);
shapes.Add(triangulo2);
shapes.Add(triangulo3);

shapes.Add(rectangulo1);
shapes.Add(rectangulo2);
shapes.Add(rectangulo3);

shapes.Add(trapecio1);
shapes.Add(trapecio2);
shapes.Add(trapecio3);



var resultTotalArea = calculator.TotalArea(shapes);

Console.WriteLine($"El área total es {resultTotalArea}");