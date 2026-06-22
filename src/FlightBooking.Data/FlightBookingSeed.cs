using FlightBooking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Data;

public static class FlightBookingSeed
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>().HasData(
            new Airport { Id=1, Code="SOF", Name="Летище София", City="София", Country="България", Terminal="Терминал 2", IsActive=true },
            new Airport { Id=2, Code="VAR", Name="Летище Варна", City="Варна", Country="България", Terminal="Терминал 1", IsActive=true },
            new Airport { Id=3, Code="LHR", Name="Heathrow Airport", City="Лондон", Country="Великобритания", Terminal="Terminal 5", IsActive=true },
            new Airport { Id=4, Code="FCO", Name="Fiumicino Airport", City="Рим", Country="Италия", Terminal="Terminal 3", IsActive=true },
            new Airport { Id=5, Code="CDG", Name="Charles de Gaulle Airport", City="Париж", Country="Франция", Terminal="Terminal 2E", IsActive=true },
            new Airport { Id=6, Code="FRA", Name="Frankfurt Airport", City="Франкфурт", Country="Германия", Terminal="Terminal 1", IsActive=true },
            new Airport { Id=7, Code="VIE", Name="Vienna International Airport", City="Виена", Country="Австрия", Terminal="Terminal 3", IsActive=true },
            new Airport { Id=8, Code="ATH", Name="Athens International Airport", City="Атина", Country="Гърция", Terminal="Main Terminal", IsActive=true });

        modelBuilder.Entity<Airline>().HasData(
            new Airline { Id=1, Name="Bulgaria Air", IataCode="FB", Country="България", Phone="+359 2 402 04 00", Website="https://www.air.bg", IsActive=true },
            new Airline { Id=2, Name="Lufthansa", IataCode="LH", Country="Германия", Phone="+49 69 86 799 799", Website="https://www.lufthansa.com", IsActive=true },
            new Airline { Id=3, Name="Austrian Airlines", IataCode="OS", Country="Австрия", Phone="+43 5 1766 1000", Website="https://www.austrian.com", IsActive=true },
            new Airline { Id=4, Name="British Airways", IataCode="BA", Country="Великобритания", Phone="+44 344 493 0787", Website="https://www.britishairways.com", IsActive=true },
            new Airline { Id=5, Name="ITA Airways", IataCode="AZ", Country="Италия", Phone="+39 06 8596 0020", Website="https://www.ita-airways.com", IsActive=true },
            new Airline { Id=6, Name="Aegean Airlines", IataCode="A3", Country="Гърция", Phone="+30 210 626 1000", Website="https://en.aegeanair.com", IsActive=true });

        modelBuilder.Entity<Flight>().HasData(
            new Flight { Id=1, FlightNumber="FB851", AirlineId=1, DepartureAirportId=1, ArrivalAirportId=3, DepartureTime=new(2026,7,2,7,10,0), ArrivalTime=new(2026,7,2,9,20,0), Price=219, AvailableSeats=42, HasStopover=false, Status="Навреме", Gate="B12" },
            new Flight { Id=2, FlightNumber="LH1427", AirlineId=2, DepartureAirportId=1, ArrivalAirportId=6, DepartureTime=new(2026,7,2,9,15,0), ArrivalTime=new(2026,7,2,10,40,0), Price=168, AvailableSeats=31, HasStopover=false, Status="Навреме", Gate="C04" },
            new Flight { Id=3, FlightNumber="OS796", AirlineId=3, DepartureAirportId=1, ArrivalAirportId=7, DepartureTime=new(2026,7,2,12,35,0), ArrivalTime=new(2026,7,2,13,15,0), Price=145, AvailableSeats=18, HasStopover=false, Status="Закъснял", Gate="B08" },
            new Flight { Id=4, FlightNumber="AZ521", AirlineId=5, DepartureAirportId=1, ArrivalAirportId=4, DepartureTime=new(2026,7,2,18,10,0), ArrivalTime=new(2026,7,2,19,05,0), Price=189, AvailableSeats=55, HasStopover=false, Status="Навреме", Gate="A17" },
            new Flight { Id=5, FlightNumber="A3981", AirlineId=6, DepartureAirportId=1, ArrivalAirportId=8, DepartureTime=new(2026,7,2,14,20,0), ArrivalTime=new(2026,7,2,15,35,0), Price=132, AvailableSeats=24, HasStopover=false, Status="Навреме", Gate="C09" },
            new Flight { Id=6, FlightNumber="FB972", AirlineId=1, DepartureAirportId=2, ArrivalAirportId=1, DepartureTime=new(2026,7,2,8,00,0), ArrivalTime=new(2026,7,2,8,50,0), Price=79, AvailableSeats=67, HasStopover=false, Status="Кацнал", Gate="A02" },
            new Flight { Id=7, FlightNumber="BA892", AirlineId=4, DepartureAirportId=3, ArrivalAirportId=1, DepartureTime=new(2026,7,9,10,30,0), ArrivalTime=new(2026,7,9,15,35,0), Price=236, AvailableSeats=39, HasStopover=false, Status="По разписание", Gate="C21" },
            new Flight { Id=8, FlightNumber="LH1426", AirlineId=2, DepartureAirportId=6, ArrivalAirportId=1, DepartureTime=new(2026,7,9,13,25,0), ArrivalTime=new(2026,7,9,16,35,0), Price=171, AvailableSeats=46, HasStopover=false, Status="По разписание", Gate="A14" },
            new Flight { Id=9, FlightNumber="OS795", AirlineId=3, DepartureAirportId=7, ArrivalAirportId=1, DepartureTime=new(2026,7,9,15,20,0), ArrivalTime=new(2026,7,9,18,00,0), Price=149, AvailableSeats=21, HasStopover=false, Status="По разписание", Gate="F06" },
            new Flight { Id=10, FlightNumber="AZ520", AirlineId=5, DepartureAirportId=4, ArrivalAirportId=1, DepartureTime=new(2026,7,9,20,10,0), ArrivalTime=new(2026,7,9,22,55,0), Price=195, AvailableSeats=33, HasStopover=false, Status="По разписание", Gate="E11" },
            new Flight { Id=11, FlightNumber="FB431", AirlineId=1, DepartureAirportId=1, ArrivalAirportId=5, DepartureTime=new(2026,7,3,6,40,0), ArrivalTime=new(2026,7,3,10,50,0), Price=204, AvailableSeats=28, HasStopover=true, Status="По разписание", Gate="B03" },
            new Flight { Id=12, FlightNumber="A3982", AirlineId=6, DepartureAirportId=8, ArrivalAirportId=1, DepartureTime=new(2026,7,9,17,45,0), ArrivalTime=new(2026,7,9,19,05,0), Price=138, AvailableSeats=52, HasStopover=false, Status="По разписание", Gate="A09" });

        modelBuilder.Entity<Passenger>().HasData(
            new Passenger { Id=1, FullName="Иван Петров", Email="ivan.petrov@example.com", Phone="+359 888 111 222", PassportNumber="BG100001", Nationality="България", BirthDate=new(1992,5,14), CreatedAt=new(2026,1,10) },
            new Passenger { Id=2, FullName="Мария Георгиева", Email="maria.g@example.com", Phone="+359 888 222 333", PassportNumber="BG100002", Nationality="България", BirthDate=new(1988,11,3), CreatedAt=new(2026,2,7) },
            new Passenger { Id=3, FullName="Николай Димитров", Email="nikolay.d@example.com", Phone="+359 888 333 444", PassportNumber="BG100003", Nationality="България", BirthDate=new(1997,8,22), CreatedAt=new(2026,3,15) },
            new Passenger { Id=4, FullName="Елена Стоянова", Email="elena.s@example.com", Phone="+359 888 444 555", PassportNumber="BG100004", Nationality="България", BirthDate=new(1990,1,28), CreatedAt=new(2026,4,2) },
            new Passenger { Id=5, FullName="Георги Николов", Email="georgi.n@example.com", Phone="+359 888 555 666", PassportNumber="BG100005", Nationality="България", BirthDate=new(1985,6,9), CreatedAt=new(2026,5,19) });

        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id=1, BookingCode="SKY-A1001", PassengerId=1, FlightId=1, PassengerCount=1, TripType="Еднопосочен", BookingDate=new(2026,6,1,10,20,0), TotalPrice=219, Status="Потвърдена", Notes="Място до прозореца" },
            new Booking { Id=2, BookingCode="SKY-A1002", PassengerId=2, FlightId=2, PassengerCount=2, TripType="Двупосочен", BookingDate=new(2026,6,3,12,10,0), TotalPrice=336, Status="Потвърдена", Notes="Двама възрастни" },
            new Booking { Id=3, BookingCode="SKY-A1003", PassengerId=3, FlightId=3, PassengerCount=1, TripType="Еднопосочен", BookingDate=new(2026,6,5,9,45,0), TotalPrice=145, Status="Чакаща", Notes="Ръчен багаж" },
            new Booking { Id=4, BookingCode="SKY-A1004", PassengerId=4, FlightId=4, PassengerCount=3, TripType="Двупосочен", BookingDate=new(2026,6,7,17,30,0), TotalPrice=567, Status="Потвърдена", Notes="Семейно пътуване" },
            new Booking { Id=5, BookingCode="SKY-A1005", PassengerId=5, FlightId=5, PassengerCount=1, TripType="Еднопосочен", BookingDate=new(2026,6,9,8,15,0), TotalPrice=132, Status="Потвърдена", Notes="Без чекиран багаж" },
            new Booking { Id=6, BookingCode="SKY-A1006", PassengerId=1, FlightId=8, PassengerCount=1, TripType="Двупосочен", BookingDate=new(2026,6,10,14,00,0), TotalPrice=171, Status="Потвърдена", Notes="Обратен полет" });
    }
}
