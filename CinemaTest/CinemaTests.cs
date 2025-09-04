using Cinema;

namespace CinemaTest
{
    [TestClass]
    public class CinemaTests
    {
        [TestMethod]
        public void CreateCinemaTest()
        {
            Cinema.Cinema c = new("cinema");
        }
        [TestMethod]
        public void AddRoomTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r = new SmallRoom("1");
            c.addRoom(r);
        }
        [TestMethod]
        public void AddTwoOfTheSameRoomTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            Room r2 = new SmallRoom("1");
            c.addRoom(r1);
            Assert.ThrowsException<Cinema.Cinema.RoomAlreadyAddedException>(() => c.addRoom(r2));
        }
        [TestMethod]
        public void AddSeatTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s = new Seat('A', 1);
            r1.addSeat(s);
        }
        [TestMethod]
        public void AddTwoOfTheSameSeatTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 1);
            r1.addSeat(s1);
            Assert.ThrowsException<Room.SeatAlreadyAddedException>(() => r1.addSeat(s2));
        }
        [TestMethod]
        public void AddMovieTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
        }
        [TestMethod]
        public void AddTwoOfTheSameMovieTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m1 = new Movie("Titanic", 194);
            Movie m2 = new Movie("Titanic", 194);
            c.addMovie(m1);
            Assert.ThrowsException<Cinema.Cinema.MovieAlreadyAddedException>(() => c.addMovie(m2));
        }
        [TestMethod]
        public void AddShowTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh);
        }
        [TestMethod]
        public void AddTwoOfTheSameShowTest()
        {
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            Show sh2 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            Assert.ThrowsException<Movie.ShowAlreadyAddedException>(() => m.addShow(sh2));
        }
        [TestMethod]
        public void AddViewersTest()
        {
            Viewer v1 = new Adult("Jóska");
            Viewer v2 = new Student("Feri");
            Child c1 = new Child("Anna");
            Pensioner p1 = new("Teréz");
            Viewer v3 = new Member("Elek");
            Adult a = new Adult("Jóska");
            Viewer v4 = new Student("Feri");
        }
        [TestMethod]
        public void ViewerReservesTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.reserve(sh1, s1);
        }
        [TestMethod]
        public void ViewerReservesTakenTest()
        {
            Viewer v1 = new Adult("Jóska");
            Viewer v2 = new Student("Feri");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.reserve(sh1, s1);
            Assert.ThrowsException<Show.SeatIsTakenException>(() => v2.reserve(sh1, s1));
        }
        [TestMethod]
        public void ViewerReservesTwoTimesTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.reserve(sh1, s1);
            Assert.ThrowsException<Show.ViewerAlreadyHasTicketException>(() => v1.reserve(sh1, s2));
        }
        [TestMethod]
        public void ViewerPurchasesTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.purchase(sh1, s1);
        }
        [TestMethod]
        public void ViewerPurchasesTakenTest()
        {
            Viewer v1 = new Adult("Jóska");
            Viewer v2 = new Student("Feri");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            r1.addSeat(s1);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.purchase(sh1, s1);
            Assert.ThrowsException<Show.SeatIsTakenException>(() => v2.purchase(sh1, s1));
        }
        [TestMethod]
        public void ViewerPurchasesTwoTimesTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.purchase(sh1, s1);
            Assert.ThrowsException<Show.ViewerAlreadyHasTicketException>(() => v1.purchase(sh1, s2));
        }
        [TestMethod]
        public void ViewerPurchasesReservationTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            v1.reserve(sh1, s1);
            v1.purchaseReserved(sh1, s1);
        }
        [TestMethod]
        public void ViewerPurchasesNonexistentReservationTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m = new Movie("Titanic", 194);
            c.addMovie(m);
            Show sh1 = new Show(213210, m, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m.addShow(sh1);
            Assert.ThrowsException<Viewer.NoReservationException>(() => v1.purchaseReserved(sh1, s1));
        }
        [TestMethod]
        public void MovieWithMostViewsTest()
        {
            Viewer v1 = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            r1.addSeat(s1);
            r1.addSeat(s2);
            Movie m1 = new Movie("Titanic", 194);
            Movie m2 = new Movie("Dune", 155);
            c.addMovie(m1);
            c.addMovie(m2);
            Show sh1 = new Show(213210, m1, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m1.addShow(sh1);
            Show sh2 = new Show(213210, m2, DateTime.Parse("2023.01.01. 10:00"), r1, 2000);
            m2.addShow(sh2);
            v1.purchase(sh2, s2);
            Assert.AreEqual(m2, c.mostViewers());
        }
        [TestMethod]
        public void ShowNumbersTest()
        {
            Viewer v1 = new Adult("Jóska");
            Viewer v2 = new Student("Feri");
            Child c1 = new Child("Anna");
            Viewer v3 = new Member("Elek");
            Adult a = new Adult("Jóska");
            Cinema.Cinema c = new("cinema");
            Room r1 = new SmallRoom("1");
            c.addRoom(r1);
            Seat s1 = new Seat('A', 1);
            Seat s2 = new Seat('A', 2);
            Seat s3 = new Seat('A', 3);
            Seat s4 = new Seat('A', 4);
            Seat s5 = new Seat('A', 5);
            Seat s6 = new Seat('A', 6);
            Seat s7 = new Seat('A', 7);
            Seat s8 = new Seat('A', 8);
            Seat s9 = new Seat('A', 9);
            Seat s10 = new Seat('A', 10);
            r1.addSeat(s1);
            r1.addSeat(s2);
            r1.addSeat(s3);
            r1.addSeat(s4);
            r1.addSeat(s5);
            r1.addSeat(s6);
            r1.addSeat(s7);
            r1.addSeat(s8);
            r1.addSeat(s9);
            r1.addSeat(s10);
            Movie m1 = new Movie("Titanic", 194);
            c.addMovie(m1);
            Show sh1 = new Show(213210, m1, DateTime.Parse("2023.01.01. 0:00"), r1, 2000);
            m1.addShow(sh1);
            v1.purchase(sh1, s2);
            v2.reserve(sh1, s3);
            v3.reserve(sh1, s1);
            c1.purchase(sh1, s6);
            a.reserve(sh1, s7);
            v2.purchaseReserved(sh1, s3);
            Assert.AreEqual(3, sh1.boughtTickets());
            Assert.AreEqual(2, sh1.reservedSeat());
            Assert.AreEqual(5, sh1.freeSeat());
        }
    }
}