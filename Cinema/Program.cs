using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Cinema
{
    internal class Program
    {
        class WrongObjectNameException : Exception { }
        static readonly Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        static readonly Dictionary<string, Seat> seats = new Dictionary<string, Seat>();
        static readonly Dictionary<string, Movie> movies = new Dictionary<string, Movie>();
        static readonly Dictionary<string, Show> shows = new Dictionary<string, Show>();
        static readonly Dictionary<string, Viewer> viewers = new Dictionary<string, Viewer>();
        static Room Room(string name)
        {
            if (!rooms.ContainsKey(name)) throw new WrongObjectNameException();
            return rooms[name];
        }
        static Seat Seat(string name)
        {
            if (!seats.ContainsKey(name)) throw new WrongObjectNameException();
            return seats[name];
        }
        static Movie Movie(string name)
        {
            if (!movies.ContainsKey(name)) throw new WrongObjectNameException();
            return movies[name];
        }
        static Show Show(string id)
        {
            if (!shows.ContainsKey(id)) throw new WrongObjectNameException();
            return shows[id];
        }
        static Viewer Viewer(string name)
        {
            if (!viewers.ContainsKey(name)) throw new WrongObjectNameException();
            return viewers[name];
        }
        static void Main(string[] args)
        {
            Cinema cinema = new("Cinemoon Filmszínház");

            TextFileReader reader = new("input.txt");
            char[] separators = { ' ', '\t' };

            while (reader.ReadLine(out string line))
            {
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    switch (tokens[0])
                    {
                        // Terem hozzáadása
                        case "ADDR":
                            Room? r = null;
                            switch (tokens[2])
                            {
                                case "S":
                                    r = new SmallRoom(tokens[1]);
                                    break;
                                case "L":
                                    r = new LargeRoom(tokens[1]);
                                    break;
                                case "V":
                                    r = new VIPRoom(tokens[1]);
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }
                            cinema.addRoom(r!);
                            rooms.Add(tokens[1], r!);
                            break;
                        // Ülőhelyek hozzáadása
                        case "ADSE":
                            Room rtase = Room(tokens[1]);
                            for (int i = 2; i < tokens.Length; i++)
                            {
                                char row = tokens[i][0];
                                int num = int.Parse(tokens[i].Substring(1));
                                Seat se = new Seat(row, num);
                                rtase.addSeat(se);
                                seats.Add(tokens[1] + "/" + tokens[i], se);
                            }
                            break;
                        // Film hozzáadása
                        case "ADDM":
                            Movie m = new Movie(tokens[1], int.Parse(tokens[2]));
                            cinema.addMovie(m);
                            movies.Add(tokens[1], m);
                            break;
                        // Előadás hozzáadása
                        case "ADSH":
                            Movie mtash = Movie(tokens[1]);
                            Room rtash = Room(tokens[5]);
                            Show sh = new Show(int.Parse(tokens[2]), mtash, DateTime.Parse(tokens[3] + " " + tokens[4]), rtash, int.Parse(tokens[6]));
                            mtash.addShow(sh);
                            shows.Add(tokens[2], sh);
                            break;
                        // Felnőtt nézők hozzáadása
                        case "ADAV":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                Adult a = new Adult(tokens[i]);
                                viewers.Add(tokens[i], a);
                            }
                            break;
                        // Diák nézők hozzáadása
                        case "ADSV":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                Student s = new Student(tokens[i]);
                                viewers.Add(tokens[i], s);
                            }
                            break;
                        // Gyerek nézők hozzáadása
                        case "ADCV":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                Child c = new Child(tokens[i]);
                                viewers.Add(tokens[i], c);
                            }
                            break;
                        // Nyugdíjas nézők hozzáadása
                        case "ADPV":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                Pensioner p = new Pensioner(tokens[i]);
                                viewers.Add(tokens[i], p);
                            }
                            break;
                        // Törzstag nézők hozzáadása
                        case "ADMV":
                            for (int i = 1; i < tokens.Length; i++)
                            {
                                Member me = new Member(tokens[i]);
                                viewers.Add(tokens[i], me);
                            }
                            break;
                        // Jegy foglalása előadásra
                        case "TRES":
                            Viewer vtr = Viewer(tokens[1]);
                            Show shtr = Show(tokens[2]);
                            Seat setr = Seat(tokens[3]);
                            vtr.reserve(shtr, setr);
                            break;
                        // Foglalt jegy fizetése, annak hiányában jegyvásárlás
                        case "TRPA":
                            Viewer vtrp = Viewer(tokens[1]);
                            Show shtrp = Show(tokens[2]);
                            Seat setrp = Seat(tokens[3]);
                            try
                            {
                                vtrp.purchaseReserved(shtrp, setrp);
                            }
                            catch (Viewer.NoReservationException)
                            {
                                vtrp.purchase(shtrp, setrp);
                            }
                            break;
                        // Jegy vásárlása előadásra
                        case "TPAY":
                            Viewer vtp = Viewer(tokens[1]);
                            Show shtp = Show(tokens[2]);
                            Seat setp = Seat(tokens[3]);
                            vtp.purchase(shtp, setp);
                            break;
                        default: Console.WriteLine("Unknown command in current row of input file."); break;
                    }
                }
                catch (WrongObjectNameException)
                {
                    Console.WriteLine("The object given in file doesn't exist.");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("The room type given in file doesn't exist.");
                }
                catch (Cinema.RoomAlreadyAddedException)
                {
                    Console.WriteLine("A cinema cannot have two rooms with the same name.");
                }
                catch (Cinema.MovieAlreadyAddedException)
                {
                    Console.WriteLine("The same movie cannot be added twice.");
                }
                catch (Room.SeatAlreadyAddedException)
                {
                    Console.WriteLine("A room cannot have two seats with the same name.");
                }
                catch (Movie.ShowAlreadyAddedException)
                {
                    Console.WriteLine("A movie cannot have two shows with the same details.");
                }
                catch (Show.SeatIsTakenException)
                {
                    Console.WriteLine("A seat cannot be reserved or purchased twice for the same show.");
                }
                catch (Show.ViewerAlreadyHasTicketException)
                {
                    Console.WriteLine("A viewer cannot have two tickets for the same show.");
                }
            }
            Console.WriteLine("The most watched movie: {0}", cinema.mostViewers().name.Replace('_', ' '));
            Show shd = Show("142043");
            Console.WriteLine("The show for the movie \"{0}\" held on {1} at {2} in Room {3} has {4} taken, {5} reserved and {6} free seats.",
                shd.movie.name.Replace('_', ' '), shd.start.ToString("yyyy.MM.dd."), shd.start.ToString("HH:mm"), shd.room.name, shd.boughtTickets(), shd.reservedSeat(), shd.freeSeat());
        }
    }
}