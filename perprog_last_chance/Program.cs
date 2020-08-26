using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace perprog_last_chance
{
    class Program
    {
        public static object LockBeforeCollide = new object();
        public static object LockOccupiedChange = new object();
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Railway> railways = new List<Railway>
            {
                new Railway("SiófokAndBalatonalmádi",false),
                new Railway("BalatonalmádiAndBalatonfüred",false),
                new Railway("BalatonfüredAndBalatonszárszó",false),
                new Railway("BalatonszarszóAndPiripócs",false),
                new Railway("PiripócsAndBaracs",false),
                new Railway("BaracsAndDunaújváros Külső",false),
                new Railway("Dunaújváros KülsőAndDunaújváros",false),
                new Railway("DunaújvárosAndSzékesFehérvár",false),
                new Railway("SzékesFehérvárAndBudapest",false),
                new Railway("Return",false)
            };

            List<TrainStation> trainStations = new List<TrainStation>
            {
                new TrainStation("Siófok"),
                new TrainStation("Balatonalmádi") ,
                new TrainStation("Balatonfüred"),
                new TrainStation("Balatonszárszó"),
                new TrainStation("Piripócs"),
                new TrainStation("Baracs") ,
                new TrainStation("Dunaújváros Külső") ,
                new TrainStation("Dunaújváros"),
                new TrainStation("SzékesFehérvár"),
                new TrainStation("Budapest")
             };

            List<Train> trains = new List<Train>
            {
                new Train("Thomas a gőzmozdony", false, trainStations[0]),
                new Train("Gőzös", false, trainStations[0]),
                new Train("BigIronTrain", false, trainStations[0]),
                new Train("Gerzson a zseni - (Reference: Röhberg (Csicska) Péter)", false, trainStations[0])
            };

            Task naplo = new Task(() =>
            {
                while (true)
                {
                    string direction = string.Empty;
                    Console.WriteLine(sw.ElapsedMilliseconds + " sec");
                    foreach (Train train in trains)
                    {
                        
                        if (train.CurrentPlace.NameOfTheStation == "Budapest")
                        {
                            direction ="-- Siófok felé";
                        }
                        else if (train.CurrentPlace.NameOfTheStation == "Siófok")
                        {
                            direction = "-- Budapest felé";
                        }

                        Console.WriteLine(train + direction);
                    }
                    Thread.Sleep(250);
                    Console.Clear();
                }
            });

            naplo.Start();

            List<Task> train_tasks = new List<Task>();

            foreach (Train train in trains)
            {
                train_tasks.Add(
                    new Task
                    (
                        () => 
                        {
                            train.GoToTheFollowingStopForwardFromSiofok(trainStations,railways, LockBeforeCollide , LockOccupiedChange);
                            
                        }, TaskCreationOptions.LongRunning
                    ));
            }

            foreach (Task task in train_tasks)
                task.Start();


            Console.ReadLine();
        }
    }
}
