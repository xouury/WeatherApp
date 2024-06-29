using System;
using Gtk;

class Program{
    static void Start(){
        Application.Init();

        Window main = new Window("My first GTK# Application!");
        main.Resize(1400, 800);

        main.DeleteEvent += delegate { Application.Quit(); };

        main.Show();

        Application.Run();
    }

    static void Main(string[] args){
        Start();
    }
}