using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace perprog_last_chance
{
    class Train
    {
        public string NameOfTheTrain { get; private set; }
        public bool IsExpress { get; private set; }
        public TrainStation CurrentPlace { get; private set; }

        public Train(string name, bool is_express, TrainStation currentTrainStation)
        {
            this.NameOfTheTrain = name;
            this.IsExpress = is_express;
            this.CurrentPlace = currentTrainStation;
        }

        public void GoToTheFollowingStopForwardFromSiofok(List<TrainStation> trainStations, List<Railway> railways,
                                                          object LockTheTrainBeforeTheyCollide, object LockOccupiedChange)
        {
            while (true)
            {
                int stationindex;
                int railwayindex;
                if (!this.IsExpress)
                {
                    if (this.CurrentPlace.NameOfTheStation != "Budapest")
                    {
                        stationindex = 0;
                        railwayindex = 0;
                        while (stationindex < trainStations.Count - 1 && railwayindex < railways.Count - 1)
                        {
                            lock (LockOccupiedChange)
                            {
                                //Railway needed
                                if (this.CurrentPlace == trainStations[stationindex]   )
                                {
                                    railways[railwayindex].IsOccupied = true;
                                    lock (LockTheTrainBeforeTheyCollide)
                                    {
                                        if (railways[railwayindex].IsOccupied)
                                        {
                                            this.CurrentPlace = trainStations[stationindex + 1];
                                            Thread.Sleep(200);
                                            railways[railwayindex].IsOccupied = false;
                                        }
                                    }

                                }                                
                            }
                            railwayindex++;
                            stationindex++;
                           
                        }
                        Thread.Sleep(50);
                    }
                    else if (this.CurrentPlace.NameOfTheStation != "Siófok")
                    {

                        stationindex = trainStations.Count - 1;
                        railwayindex = railways.Count - 1;
                        while (stationindex > 0 && railwayindex > 0)
                        {
                            lock (LockOccupiedChange)
                            {
                                //Railway needed
                                if (this.CurrentPlace == trainStations[stationindex])
                                {
                                    railways[railwayindex].IsOccupied = true;
                                    lock (LockTheTrainBeforeTheyCollide)
                                    {
                                        if (railways[railwayindex].IsOccupied)
                                        {
                                            this.CurrentPlace = trainStations[stationindex - 1];
                                            Thread.Sleep(200);
                                            railways[railwayindex].IsOccupied = false;
                                        }
                                        
                                    }

                                }
                                
                            }
                            railwayindex--;
                            stationindex--;

                        }
                        Thread.Sleep(50);
                    }
                    
                }
                else
                {
                    
                }
            }
        }

        public override string ToString()
        {
            return $"{this.NameOfTheTrain} pozíciója: {this.CurrentPlace.NameOfTheStation}";
        }
    }
}
