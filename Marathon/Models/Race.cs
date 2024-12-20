namespace Marathon.Models;
//The only time we add a class is if we have a model that needs to be called in a class 

public class RaceCollection
{
    public Race[] races { get; set; } //The array that holds our Race Objects 
}



public class Race
{
    public int id { get; set; }  //The Id for each object in the sql database
    
    public string race_name { get; set; } //Here our name matters so that the JSON can add and match the naming conventions 
}