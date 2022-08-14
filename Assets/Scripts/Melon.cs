public class Melon : Fruit
{
    //Polymorphism
    protected override int ScorePoints => 50;

    //Polymorphism (overriden property)
    //Encapsulation (protected property, only base class and inherited classes can see it. Plus it cannot be modified as it's read only property.
    protected override string Name => "Juicy Pink Melon";
}