using CovarianceAndContravarianceDelegateExample;
using System;
using System.IO;
using System.Linq;

/*
 *Covariance → A method that returns a more specific type (ICECar or EVCar)
can be stored inside a delegate that returns a base type (Car).


  *Contravariance → A method that accepts a base type (Car)
can be used where a method expecting a more specific type (ICECar/EVCar) is required.
*/
namespace CovarianceAndContravarianceDelegateExample
{
    public class Program
    {
        /// <summary>
        /// CarFactory.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">car</param>
        /// <returns></returns>
        delegate Car CarFactoryDel(int id, string name);
        delegate void LogICECarDetailsDel(ICECar car);
        delegate void LogEVCarDetailsDel(EVCar car);
        static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;

            Car iceCar = carFactoryDel(1, "Tata Harrier");

            carFactoryDel = CarFactory.ReturnEvCar;

            Car evCar = carFactoryDel(2, "Hundai");

            LogICECarDetailsDel logICECarDetailsDel = LogCarDetails;

            logICECarDetailsDel(iceCar as ICECar);

            LogEVCarDetailsDel logEVCarDetailsDel = LogCarDetails;

            logEVCarDetailsDel(evCar as EVCar);

            Console.ReadKey();

        }

        /// <summary>
        /// Car details.
        /// </summary>
        /// <param name="car"></param>
        /// <exception cref="ArgumentException"></exception>
       public static void LogCarDetails(Car car)
        {
            if (car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICEDetails.txt"), true))
                {
                    sw.WriteLine($"Object Type: {car.GetType()}");
                    sw.WriteLine($"Car Detials: {car.GetCarDetails()}");
                };

            }
            else if (car is EVCar)
            {
                Console.WriteLine($"Object Type: {car.GetType()}");
                Console.WriteLine($"Car Detials: {car.GetCarDetails()}");
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }

    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar { Id = id, Name = name };
        }
        public static EVCar ReturnEvCar(int id, string name)
        {
            return new EVCar { Id = id, Name = name };
        }
    }

    /// <summary>
    /// Show the car details.
    /// </summary>
    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name} ";
        }
    }

    /// <summary>
    /// ICEcar inheriting from Car class.
    /// </summary>
    public class ICECar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Petrol Engine";
        }
    }

    /// <summary>
    /// EVcar inheriting from Car class.
    /// </summary>
    public class EVCar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Electric";
        }
    }
}