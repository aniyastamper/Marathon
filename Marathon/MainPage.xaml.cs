using Newtonsoft.Json; //We make a request - We Ceralize it to then Decerialize it using Json
using Marathon.Models; //Call in the Models Folder to use in in app backend

namespace Marathon;

public partial class MainPage : ContentPage
{

    private RaceCollection RaceObject; //gave the RaceCollection from Race.cs a Object name

    public MainPage()
    {
        InitializeComponent();
        FillPicker(); //Call for main page
    }


    public void FillPicker()
    {
        var client = new HttpClient(); //using the Json Client we call in the GET request and use it in the code below
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var response = client.GetAsync("races").Result;  //GET request retrieves the JSON Array information
        
        //wsJson= Webservice
        var wsJson = response.Content.ReadAsStringAsync().Result; //Gets the Content of the ARRAY results and holds in wsJson
        
        RaceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson); //Deserilizing the Race Collection we have and converting it to the RaceObject to use in App

        RacePicker.ItemsSource = RaceObject.races; //Moving Our Race Array to the Format of Our Rcae Pick in APP, Populating them as Items

    }

    private void RacePicker_OnSelectedIndexChanged(object sender, EventArgs e) //When a selected race is changed we want to populate new info
    {
        var SelectedRace = ((Picker)sender).SelectedIndex; //When race is picked, chnages in picker object on app

        var RaceID = RaceObject.races[SelectedRace].id; //specifies slected race by Id 

        var client = new HttpClient(); //Specified race object json conversion to app 39-45
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var response = client.GetAsync("results/" + RaceID).Result; 
      
        var wsJson = response.Content.ReadAsStringAsync().Result;

        var ResultObject = JsonConvert.DeserializeObject<ResultCollection>(wsJson);

        //ResultObject = ResultObject; //This is how we can check to see if our variables work 
        
        var CellTemplate = new DataTemplate(typeof(TextCell));
        
        CellTemplate.SetBinding(TextCell.TextProperty,"name"); //Calling this text cell and setting the binding value to the Nae we have in our Marathon/Json
        CellTemplate.SetBinding(TextCell.DetailProperty,"detail");

        LstResults.ItemTemplate = CellTemplate;
        LstResults.ItemsSource = ResultObject.results;
    }
}